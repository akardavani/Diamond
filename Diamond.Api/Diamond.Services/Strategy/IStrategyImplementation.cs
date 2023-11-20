using Diamond.Domain.Indicator;
using System.Collections.Generic;

namespace Diamond.Services.Strategy
{
    public interface IStrategyImplementation
    {
        string Calculate(List<IndicatorCandel> candles, IndicatorParameter indicatorParameter);        
    }
}
