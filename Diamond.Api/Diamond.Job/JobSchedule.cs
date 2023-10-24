using Microsoft.Extensions.Hosting;

namespace Diamond.Jobs
{
    public class JobSchedule : BackgroundService
    {
        private readonly IEnumerable<IJob> _jobs;

        public JobSchedule(IEnumerable<IJob>jobs)
        {
            _jobs = jobs;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    var now = DateTime.Now;

            //    var tasks = _jobs
            //        .Where(job => !job.MustRun(now, stoppingToken))
            //        .Select(job => job.RunAsync(stoppingToken));

            //    await Task.WhenAll(tasks);
            //    await Task.Delay(1000 * 60, stoppingToken);
            //}
        }
    }
}
