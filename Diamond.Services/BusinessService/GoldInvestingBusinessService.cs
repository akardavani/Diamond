using Diamond.Domain.Enums;
using Diamond.Services.CandelClient;
using Diamond.Services.TseTmcClient;
using Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class GoldInvestingBusinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        private readonly InvestingCandelClientService _candelService;

        public GoldInvestingBusinessService(DiamondDbContext dbContext,
            TseTmcClientService tseService,
            InvestingCandelClientService candelService)
        {
            _dbContext = dbContext;
            _candelService = candelService;
        }


        public async Task GetAllCandelData(CancellationToken cancellation)
        {
            var symbol = "Gold";
            var timeframe = TimeframeEnum.FifteenMinutes;
            var candelModel = await _candelService.GetDataByUrl(symbol, timeframe);
        }
    }
}
