using Diamond.Domain.Indicator;
using Diamond.Domain.Models.SupportResistance;
using Diamond.Services.Strategy;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Diamond.Services.BusinessService
{
    //public class SupportResistanceBusinessService
    //{
    //    private readonly IStrategyImplementation _strategy;
    //    public SupportResistanceBusinessService(IStrategyImplementation strategy)
    //    {
    //        _strategy = strategy;
    //    }

    //    public List<SupportResistancePercentageDifference> Calculate(IndicatorParameter indicatorParameter, List<IndicatorCandel> candels)
    //    {
    //        var instruments = candels
    //            .GroupBy(e => e.InstrumentId)
    //            .Select(e => e.Key)
    //            .ToList();

    //        var reasonlist = new List<SupportResistancePercentageDifference>();

    //        foreach (var instrument in instruments)
    //        {
    //            var find = candels.FindAll(e => e.InstrumentId == instrument);

    //            var supportResistance = _strategy
    //            .FindSymbols(find, indicatorParameter)
    //            .ToList();

    //            //var supportResistance1 = supportResistance
    //            //    .Where(e => e.PercentageDifference < 5M)
    //            //    .ToList();

    //            //if (supportResistance1 is not null)
    //            //{
    //            //    if (supportResistance1.Count > 0)
    //            //    {
    //            //        reasonlist.AddRange(supportResistance1);
    //            //    }
    //            //}
    //        }

    //        var json = JsonSerializer.Serialize(reasonlist);            

    //        return reasonlist;
    //    }
    //}
}
