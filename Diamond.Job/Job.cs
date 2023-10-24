using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Diamond.Jobs
{
    public abstract class Job : IJob
    {
        protected JobSetting Setting { get; }
        private CancellationToken _cancellationToken;
        private readonly WaitHandler _waitHandler;
        protected readonly IServiceProvider ServiceProvider;
        //protected readonly ILogger Logger;
        public abstract Task RunAsync(CancellationToken cancellationToken);

        protected JobSetting JobSetting { get; }
        // Jobs:TseTmcJob
        protected Job(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            JobSetting = configuration.GetSection(GetConfigSection()).Get<JobSetting>();            
            ServiceProvider = serviceProvider;
            _waitHandler = new WaitHandler(JobSetting);
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            if ((JobSetting?.IsActive ?? false) == false)
                return;

            _cancellationToken = cancellationToken;

            await _waitHandler.WaitForStartupDelay(_cancellationToken);

            while (true)
            {
                if (_cancellationToken.IsCancellationRequested)
                    return;

                await _waitHandler.WaitForStart(_cancellationToken);

                if (_waitHandler.Execute(_cancellationToken) && _waitHandler.TodayIsValid(_cancellationToken))
                {
                    try
                    {

                        //Logger.LogInformation($"{GetType().FullName} has been started at {DateTime.Now:s}");
                        await RunAsync(_cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        //ServiceProvider.GetService<ILogger<Job>>()?.LogError($"{ex.Message} --- {ex.StackTrace ?? string.Empty}", ex);
                    }
                }

                await _waitHandler.WaitForInterval(_cancellationToken);
            }
        }

        protected virtual string GetConfigSection()
        {
            return $"Jobs:{GetType().Name}";
        }

        public virtual bool MustRun(DateTime dateTime,CancellationToken cancellation)
        {
            //_ = Start(cancellation);
            try
            {
                

                //_ = _waitHandler.WaitForStart(cancellation);

                //_ = _waitHandler.WaitForInterval(_cancellationToken);

                if (JobSetting.Interval == null && JobSetting.Start == null)
                {
                    //throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
                    return false;
                }                    

                if (JobSetting.Start == null && JobSetting.Interval.Value.TotalMilliseconds <= 0)
                {
                    return false;
                    //throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
                }

                if (JobSetting.Interval == null && JobSetting.Start.Value.TotalMilliseconds <= 0)
                {
                    return false;
                    //throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
                }

                if (JobSetting.Interval != null && JobSetting.Start != null && JobSetting.Interval.Value.TotalMilliseconds <= 0 &&
                    JobSetting.Start.Value.TotalMilliseconds <= 0)
                {
                    return false;
                    //throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
                }

                if (JobSetting.Start != null && JobSetting.End != null &&
                    JobSetting.Start.Value.TotalMilliseconds > JobSetting.End.Value.TotalMilliseconds)
                {
                    return false;
                    //throw new Exception("End must be greater than tart  ");
                }

                if (JobSetting.IsActive 
                    && (dateTime.Millisecond < JobSetting.End.Value.TotalMilliseconds)
                    && dateTime.Millisecond > JobSetting.Start.Value.TotalMilliseconds)
                {
                    return true;
                }
                else
                {
                    //if ((JobSetting?.IsActive ?? false) == false)
                    //    return false;


                    //_ = _waitHandler.WaitForStartupDelay(cancellation);
                    //return false;

                    //if (_startupDelay.HasValue && _startupDelay.Value.TotalMilliseconds > 0)
                    //{
                    //    await Task.Delay(_startupDelay.Value, cancellationToken);
                    //}

                    return false;
                }

                _waitHandler.WaitForInterval(_cancellationToken).GetAwaiter();
            }
            catch (Exception)
            {
                return false;
                throw;
            }            
        }
    }
}
