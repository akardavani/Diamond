namespace Diamond.Jobs
{
    public class WaitHandler
    {
        private TimeSpan? _startupDelay;
        private TimeSpan? _start;
        private TimeSpan? _interval;
        private TimeSpan? _end;
        private List<DayOfWeek> _validDays;

        public WaitHandler(JobSetting setting)
        {
            Initialize(setting);
        }

        private void Initialize(JobSetting setting)
        {
            if (setting.Interval == null && setting.Start == null)
                throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");

            if (setting.Start == null && setting.Interval.Value.TotalMilliseconds <= 0)
            {
                throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
            }

            if (setting.Interval == null && setting.Start.Value.TotalMilliseconds <= 0)
            {
                throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
            }

            if (setting.Interval != null && setting.Start != null && setting.Interval.Value.TotalMilliseconds <= 0 &&
                setting.Start.Value.TotalMilliseconds <= 0)
            {
                throw new Exception("Interval and RunAt cannot be null or empty TimeSpan.");
            }

            if (setting.Start != null && setting.End != null &&
                setting.Start.Value.TotalMilliseconds > setting.End.Value.TotalMilliseconds)
            {
                throw new Exception("End must be greater than tart  ");
            }

            _startupDelay = setting.StartupDelay;
            _start = setting.Start;
            _end = setting.End;
            _interval = setting.Interval;
            _validDays = setting.ExecutionDays ?? new List<DayOfWeek>();
        }

        public async Task WaitForStartupDelay(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            if (_startupDelay.HasValue && _startupDelay.Value.TotalMilliseconds > 0)
            {
                await Task.Delay(_startupDelay.Value, cancellationToken);
            }
        }

        public async Task WaitForStart(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            if (_start.HasValue)
            {
                var now = DateTime.Now.TimeOfDay;
                TimeSpan sleepTime = TimeSpan.Zero;

                if (_start > now)
                {
                    sleepTime = _start.Value.Subtract(now);
                }

                await Task.Delay(sleepTime, cancellationToken);
            }
        }

        public async Task WaitForInterval(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            if (_interval != null && _interval.Value.TotalMilliseconds > 0)
            {
                await Task.Delay(_interval.Value, cancellationToken);
            }
        }

        public bool TodayIsValid(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return false;

            return !_validDays.Any() || _validDays.Contains(DateTime.Now.DayOfWeek);
        }

        public bool Execute(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return false;
            return !_end.HasValue || !(_end.Value.Subtract(DateTime.Now.TimeOfDay).TotalSeconds <= 0);
        }
    }
}
