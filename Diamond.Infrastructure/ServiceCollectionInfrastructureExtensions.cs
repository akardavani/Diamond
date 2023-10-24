using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Diamond.Infrastructure
{
    public static class ServiceCollectionInfrastructureExtensions
    {
        public static IServiceCollection AddDiamondInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.TryAddSingleton<CacheManager>();

            return services;
        }
    }
}
