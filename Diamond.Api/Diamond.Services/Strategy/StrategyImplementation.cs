using Diamond.Domain.Indicator;
using System.Collections.Generic;
using System.Linq;

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

            //var instruments = candels
            //    .GroupBy(e => e.InstrumentId)
            //    .Select(e => e.Key)
            //    .ToList();

            //var reasonlist = new List<IndicatorCandel>();

            //foreach (var instrument in instruments)
            //{
            //    var find = candels.FindAll(e => e.InstrumentId == instrument);

            //    if (instrument== "IRO1TAYD0001")
            //    {

            //    }
            //    var symbol = _strategy
            //    .Calculate(find, _indicatorParameter)
            //    .ToList();

            //    if (symbol is not null)
            //    {
            //        if (symbol.Count > 0)
            //        {
            //            //reasonlist.AddRange(symbol);
            //        }
            //    }
            //}

            //var instrumentList = reasonlist
            //    .GroupBy(e => e.InstrumentId)
            //    .Select(e => e.Key)
            //    .ToList();

            return symbol;
        }
    }
}
