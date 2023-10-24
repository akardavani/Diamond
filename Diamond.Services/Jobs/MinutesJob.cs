using Diamond.Jobs;
using Diamond.Services.BusinessService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.Jobs
{
    internal class MinutesJob : Job
    {
        public MinutesJob(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration, serviceProvider)
        {
        }

        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            using var scope = ServiceProvider.CreateScope();
            var calculate = scope.ServiceProvider.GetRequiredService<CalculateBussinessService>();

            await calculate.SaveAllNeededData(cancellationToken);
        }
    }
}
