using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService.Strategy
{
    public interface ITradingStrategyHandler<in TTradingStrategy>
        where TTradingStrategy : ITradingStrategy
    {
        //bool ValidationStrategy();
        Task TradingSignal(TTradingStrategy tradingStrategy, CancellationToken cancellation);
    }
}
