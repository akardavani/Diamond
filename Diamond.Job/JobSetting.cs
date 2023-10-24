namespace Diamond.Jobs
{
    public class JobSetting
    {
        public TimeSpan? Interval { get; set; }
        public TimeSpan? StartupDelay { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }
        public bool IsActive { get; set; }
        public List<DayOfWeek> ExecutionDays { get; set; }
    }
}
