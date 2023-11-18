using Diamond.Domain.Enums;
using Diamond.Domain.Indicator;
using Diamond.Services.Indicator;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diamond.Services.Strategy
{
    public class CrossIchimokoStrategy : IStrategyImplementation
    {
        public List<IndicatorCandel> FindSymbols(List<IndicatorCandel> ichimokuCandles, IndicatorParameter parameter)
        {
            var candels = ichimokuCandles
                .Select(e => new IchimokuIndicatorModel(parameter.CandelPriceType, e))
                .ToList();

            //var candels = ichimokuCandles
            //    .Select(e => new IchimokuIndicatorModel(parameter.CandelPriceType)
            //    {
            //        Close = e.Close,
            //        Date = e.Date,
            //        High = e.High,
            //        InstrumentId = e.InstrumentId,
            //        Low = e.Low,
            //        NetValue = e.NetValue,
            //        Open = e.Open,
            //        UnixTimestamp = e.UnixTimestamp,
            //        Volume = e.Volume,
            //    })
            //    .ToList();

            var crosses = FindCross_SenkouSpanA_SenkouSpanB(candels, parameter.FromDate,parameter.ToDate);

            var list = (parameter.Trend == CrossEnum.Up) 
                ? FindCrossUp(crosses) 
                : FindCrossDown(crosses);

            return list;
        }

        private List<IchimokuIndicatorModel> FindCross_SenkouSpanA_SenkouSpanB(List<IchimokuIndicatorModel> candels, DateOnly fromDate, DateOnly toDate)
        {
            if (candels.Count < 52)
                return candels;

            var count = 13;
            decimal? chikou = null;
            List<IchimokuResult> Ichimokus = candels.GetIchimoku(9, 26, 52, 1).ToList();

            for (int i = count; i < candels.Count; i++)
            {
                var sen_B = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanB;
                var sen_A = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanA;

                if (candels[i - count] is not null)
                {
                    //chikou = Ichimokus.Find(x => x.Date == candels[i - count].Date).ChikouSpan;
                    chikou = candels[i - count].SelectedPrice;

                    if (sen_A > sen_B &&                //sen_A > sen_B   ==> ابر سبز
                         candels[i].SelectedPrice > sen_A &&    // قیمت بالای ابر سبز
                        candels[i].SelectedPrice > chikou)      // چیکو بالای قیمت
                    {
                        candels[i].Trend = CrossEnum.Up;
                    }
                    else if (sen_A < sen_B &&           //sen_A < sen_B   ==> ابر قرمز
                        candels[i].SelectedPrice < sen_A &&     // قیمت پایین ابر قرمز                                                  
                        candels[i].SelectedPrice < chikou)      // چیکو پایین قیمت  
                    {
                        candels[i].Trend = CrossEnum.Down;
                    }
                    else
                        candels[i].Trend = CrossEnum.Noting;
                }
            }

            return candels
                .Where(e => e.DateOnly >= fromDate && e.DateOnly < toDate)
                .ToList();
        }

        private List<IndicatorCandel> FindCrossUp(List<IchimokuIndicatorModel> candels)
        {
            return candels
                    .Where(e => e.Trend == CrossEnum.Up)
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

        private List<IndicatorCandel> FindCrossDown(List<IchimokuIndicatorModel> candels)
        {
            return candels
                    .Where(e => e.Trend == CrossEnum.Down)
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
    }
}
