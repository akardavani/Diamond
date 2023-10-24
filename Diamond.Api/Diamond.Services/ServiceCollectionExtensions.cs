using Diamond.Jobs;
using Diamond.Services.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace Diamond.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDiamondServices(this IServiceCollection services)
        {
            var types = typeof(ServiceCollectionExtensions)
                .Assembly
                .ExportedTypes
                .Where(t => t != typeof(IBusinessService) && typeof(IBusinessService).IsAssignableFrom(t));

            foreach (var type in types)
            {
                services.TryAddScoped(type);
            }

            var jobs = typeof(TseTmcJob).Assembly.GetTypes().
                Where(x => x.IsClass && !x.IsAbstract && typeof(IJob).IsAssignableFrom(x));

            foreach (var job in jobs)
            {
                services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IJob), job));
            }

            return services;
        }
    }
}
