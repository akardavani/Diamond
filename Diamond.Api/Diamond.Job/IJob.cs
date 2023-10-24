namespace Diamond.Jobs
{
    public interface IJob
    {
        bool MustRun(DateTime dateTime, CancellationToken cancellation);
        Task RunAsync(CancellationToken cancellationToken);
    }
}
