using Diamond.Domain.Indicator;
using System.Collections.Generic;

namespace Diamond.Services.Strategy
{
    public class StrategyImplementation
    {
        private readonly IStrategyImplementation _strategy;
        private readonly IndicatorParameter _indicatorParameter;

        public StrategyImplementation(IStrategyImplementation strategy, IndicatorParameter indicatorParameter)
        {
            _strategy = strategy;
            _indicatorParameter = indicatorParameter;
        }

        public string FindSymbols(List<IndicatorCandel> candels)
        {
            var symbol = _strategy.Calculate(candels, _indicatorParameter);

            return symbol;
        }
    }
}
