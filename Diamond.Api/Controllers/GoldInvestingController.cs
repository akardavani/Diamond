using Diamond.Services.BusinessService;
using Diamond.Services.BusinessService.Strategy;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.API.Controllers
{
    public class TseTmcController : ApiBaseController
    {
        private readonly InstrumentInfoBusinessService _businessService;
        private readonly ITradingStrategyHandler<IchimokuStrategyCommand> _tradingStrategy;
        private readonly ITradingStrategyHandler<GoldCommand> _trading;

        public TseTmcController(InstrumentInfoBusinessService businessService,
            ITradingStrategyHandler<IchimokuStrategyCommand> tradingStrategy,
            ITradingStrategyHandler<GoldCommand> trading)
        {
            _businessService = businessService;
            _tradingStrategy = tradingStrategy;
            _trading = trading;
        }

        [HttpGet]
        public async Task Get(CancellationToken cancellation)
        {
            await _businessService.GetTseTmcData(cancellation);
        }

        [HttpGet]
        public async Task GetCandelData(CancellationToken cancellation)
        {
            await _businessService.GetCandelData(cancellation);
        }

        [HttpGet]
        public async Task FindStock(CancellationToken cancellation)
        {
            var strategy = new IchimokuStrategyCommand();
            await _tradingStrategy.TradingSignal(strategy, cancellation);

            var t = new GoldCommand();
            await _trading.TradingSignal(t, cancellation);
        }
    }
}
