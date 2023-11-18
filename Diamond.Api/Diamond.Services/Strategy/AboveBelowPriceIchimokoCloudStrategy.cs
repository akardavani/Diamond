using Diamond.Domain.Enums;
using Diamond.Domain.Indicator;
using Diamond.Services.Indicator;
using Diamond.Utils.BrokerExtention;
using Skender.Stock.Indicators;
using System.Collections.Generic;
using System.Linq;

namespace Diamond.Services.Strategy
{
    public class AboveBelowPriceIchimokoCloudStrategy : IStrategyImplementation
    {
        public List<IndicatorCandel> FindSymbols(List<IndicatorCandel> ichimokuCandles, IndicatorParameter parameter)
        {
            var candels = ichimokuCandles
                .Select(e => new IchimokuIndicatorModel(parameter.CandelPriceType, e))
                .ToList();

            PriceAboveBelowCloud(candels, parameter.ComparisonPriceType);
            FindCloudColor(candels);

            // اگر هر دو را انتخاب نکرده بود یکی را باید انتخاب کنیم
            if (parameter.IchimokoCloudColor != IchimokoCloudColorEnum.Both)
            {
                candels = candels
                    .Where(e => e.CloudColor == parameter.IchimokoCloudColor)
                    .ToList();
            }

            //var pp = parameter.ComparisonPriceType
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

        private void FindCloudColor(List<IchimokuIndicatorModel> candels)
        {
            if (candels.Count < 52)
                return;

            var count = 13;
            List<IchimokuResult> Ichimokus = candels.GetIchimoku(9, 26, 52, 1).ToList();

            for (int i = count; i < candels.Count; i++)
            {
                var sen_B = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanB;
                var sen_A = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanA;

                if (candels[i - count] is not null)
                {
                    if (sen_A > sen_B)                 //sen_A > sen_B   ==> ابر سبز
                    {
                        candels[i].CloudColor = IchimokoCloudColorEnum.Green;
                    }
                    else if (sen_A < sen_B)            //sen_A < sen_B   ==> ابر قرمز
                    {
                        candels[i].CloudColor = IchimokoCloudColorEnum.Red;
                    }
                }
            }
        }

        private void PriceAboveBelowCloud(List<IchimokuIndicatorModel> candels, ComparisonPriceTypeEnum comparisonPrice)
        {
            if (candels.Count < 52)
                return;

            var count = 13;
            List<IchimokuResult> Ichimokus = candels.GetIchimoku(9, 26, 52, 1).ToList();

            for (int i = count; i < candels.Count; i++)
            {
                var sen_B = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanB ?? 0;
                var sen_A = Ichimokus.Find(x => x.Date == candels[i].Date).SenkouSpanA ?? 0;

                if (candels[i - count] is not null)
                {
                    //var sen_A_Price = BrokerExtention.CompareValues(candels[i].SelectedPrice, sen_A, comparisonPrice);
                    //var sen_B_Price = BrokerExtention.CompareValues(candels[i].SelectedPrice, sen_B, comparisonPrice);

                    //if (BrokerExtention.CompareValues(candels[i].SelectedPrice, sen_A, comparisonPrice))
                    //{

                    //}

                    // قیمت بالای ابر 
                    if (candels[i].SelectedPrice > sen_A && candels[i].SelectedPrice > sen_B)
                    {
                        candels[i].PriceAboveBelow = PriceAboveBelowEnum.Above;
                    }
                    // قیمت پایین ابر 
                    else if (candels[i].SelectedPrice < sen_A && candels[i].SelectedPrice < sen_B)
                    {
                        candels[i].PriceAboveBelow = PriceAboveBelowEnum.Below;
                    }
                    // قیمت داخل ابر 
                    else
                    {
                        candels[i].PriceAboveBelow = PriceAboveBelowEnum.Noting;
                    }
                }
            }
        }
    }
}
