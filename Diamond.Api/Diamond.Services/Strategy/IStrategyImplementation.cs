using Diamond.Domain.Indicator;
using System.Collections.Generic;

namespace Diamond.Services.Strategy
{
    public interface IStrategyImplementation
    {
        List<IndicatorCandel> FindSymbols(List<IndicatorCandel> candles, IndicatorParameter indicatorParameter);
    }
}
