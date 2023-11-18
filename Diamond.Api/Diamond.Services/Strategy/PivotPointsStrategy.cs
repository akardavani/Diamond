using Diamond.Domain.Indicator;
using Diamond.Services.Indicator;
using Skender.Stock.Indicators;
using System.Collections.Generic;
using System.Linq;

namespace Diamond.Services.Strategy
{
    internal class PivotPointsStrategy : IStrategyImplementation
    {
        public List<IndicatorCandel> FindSymbols(List<IndicatorCandel> candles, IndicatorParameter indicatorParameter)
        {
            var parameter = indicatorParameter.PivotPointsParameter;

            var candels = candles
                .Select(e => new PivotPointsIndicatorModel(parameter.CandelPriceType, e))
                .ToList();

            pivotPoints(candels);

            return candels
                    .Where(e => e.DateOnly >= parameter.FromDate && e.DateOnly < parameter.ToDate)
                    .Select(e => new IndicatorCandel
                    {
                        InstrumentId = e.InstrumentId,
                        Close = e.Close,
                        Date = e.Date,
                        High = e.High,
                        Low = e.Low,
                        NetValue = e.NetValue,
                        Open = e.Open,
                        UnixTimestamp = e.UnixTimestamp,
                        Volume = e.Volume
                    })
                    .ToList();
        }

        private void pivotPoints(List<PivotPointsIndicatorModel> candels)
        {
            if (candels.Count < 52)
                return;

            List<PivotPointsResult> pivotPoints = candels.GetPivotPoints(PeriodSize.Week, PivotPointType.Standard).ToList();

            foreach (var item in pivotPoints)
            {
                if (item.S1 is not null)
                {

                }
                var ssss = item.S1;
            }

            var order = pivotPoints.OrderByDescending(e=>e.Date).ToList();
            var sssss = pivotPoints.Where(e => e.S1 is not null || e.R1 is not null).ToList();

            var groupS1 = pivotPoints.GroupBy(e=>e.S1).Select(e=>e.Key).ToList();
            var groupr1 = pivotPoints.GroupBy(e => e.R1).Select(e => e.Key).ToList();
            var ss = 0;
            //for (int i = count; i < candels.Count; i++)
            //{
            //    var sen_B = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanB ?? 0;
            //    var sen_A = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanA ?? 0;

            //    if (candels[i - count] is not null)
            //    {


            //        // قیمت بالای ابر 
            //        if (candels[i].SelectedPrice > sen_A && candels[i].SelectedPrice > sen_B)
            //        {
            //            candels[i].PriceAboveBelow = PriceAboveBelowEnum.Above;
            //        }
            //        // قیمت پایین ابر 
            //        else if (candels[i].SelectedPrice < sen_A && candels[i].SelectedPrice < sen_B)
            //        {
            //            candels[i].PriceAboveBelow = PriceAboveBelowEnum.Below;
            //        }
            //        // قیمت داخل ابر 
            //        else
            //        {
            //            candels[i].PriceAboveBelow = PriceAboveBelowEnum.Noting;
            //        }
            //    }
            //}
        }
    }
}
