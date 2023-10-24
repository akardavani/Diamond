using Diamond.Services.BusinessService;
using Diamond.Services.BusinessService.Strategy;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.API.Controllers
{
    public class GoldInvestingController : ApiBaseController
    {
        private readonly GoldInvestingBusinessService _businessService;
        //private readonly ITradingStrategyHandler<GoldCommand> _trading;

        public GoldInvestingController(GoldInvestingBusinessService businessService
            //,ITradingStrategyHandler<GoldCommand> trading
            )
        {
            _businessService = businessService;
            //_trading = trading;
        }

        [HttpGet]
        public async Task GetCandelData(CancellationToken cancellation)
        {
            await _businessService.GetAllCandelData(cancellation);
        }

        [HttpGet]
        public async Task FindStock(CancellationToken cancellation)
        {
            var t = new GoldCommand();
            //await _trading.TradingSignal(t, cancellation);
        }
    }
}
