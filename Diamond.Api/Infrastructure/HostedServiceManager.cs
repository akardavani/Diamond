using Diamond.Services;

namespace Diamond.Api.Infrastructure
{
    internal class HostedServiceManager : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;

        public HostedServiceManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
          
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var managerStoreBusiness = scope.ServiceProvider.GetRequiredService<ManagerStoreBusiness>();
                await managerStoreBusiness.InitializeAsync(cancellationToken);
            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {            
            return Task.CompletedTask;
        }
    }
}
