using Diamond.Jobs;
using Diamond.Services.BusinessService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.Jobs
{
    
    internal class TseTmcJob : Job
    {
        public TseTmcJob(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration, serviceProvider)
        {
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            //using var scope = ServiceProvider.CreateScope();
            //var s = scope.ServiceProvider.GetRequiredService<InstrumentInfoBusinessService>();

            //await s.GetTseTmcData(cancellationToken);

        }
    }

}
