using Diamond.Domain.Enums;
using Diamond.Domain.Indicator;
using Diamond.Domain.Models.SupportResistance;
using Diamond.Services.Indicator;
using Diamond.Utils.BrokerExtention;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diamond.Services.Strategy
{
    internal class SupportResistanceStrategy : IStrategyImplementation
    {
        public List<IndicatorCandel> FindSymbols(List<IndicatorCandel> candles, IndicatorParameter indicatorParameter)
        {
            var parameter = indicatorParameter.PivotPointsParameter;

            var candels = candles
                .Select(e => new SupportResistanceIndicatorModel(parameter.CandelPriceType, e))
                .ToList();

            //pivotPoints(candels);
            int lookbackPeriod = 300;
            var supportResistance = new List<SupportResistancePercentageDifference>();
            supportResistance.AddRange(FindResistance(candels, lookbackPeriod));
            supportResistance.AddRange(FindSupport(candels, lookbackPeriod));

            FindPercentageDifference(supportResistance, candels);

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

        private List<SupportResistancePercentageDifference> FindResistance(List<SupportResistanceIndicatorModel> candels, int lookbackPeriod)
        {
            var resistance = new List<SupportResistanceIndicatorModel>();

            for (int i = 0; i < candels.Count; i++)
            {
                var newCandle = candels
                    .Skip(i)
                    .Take(lookbackPeriod);

                //resistance

                var maxClosingPrice = newCandle
                    .Max(c => c.Close);

                var canMax = newCandle
                    .Where(e => e.Close.Equals(maxClosingPrice) && e.Date > DateTime.Now.AddDays(-300))
                    .ToList();

                resistance.AddRange(canMax);
            }

            var resistanceGroups = resistance
               .GroupBy(e => e.Close)
               .Select(group => new SupportResistancePercentageDifference
               {
                   Price = group.Key,
                   Count = group.Count(),
                   Date = group.Max(e => e.Date).ToDateOnly(),
                   SupportResistance = SupportResistanceTypeEnum.Resistance
               })
               .Where(e => e.Count > 5)
               .OrderByDescending(e => e.Count)
               .ThenByDescending(e => e.Date)
               .Take(4)  // 4 تای آخر
               .ToList();

            return resistanceGroups;
        }

        private List<SupportResistancePercentageDifference> FindSupport(List<SupportResistanceIndicatorModel> candels, int lookbackPeriod)
        {
            var support = new List<SupportResistanceIndicatorModel>();

            // توی کل کندل ها، تکه های 300 تایی جدا میکنیم و مینیمم آنها را می یابیم
            // وبعد یک کندل جلو میرویم و دوباره این کار را انجام میدهیم

            for (int i = 0; i < candels.Count; i++)
            {
                var newCandle = candels
                    .Skip(i)
                    .Take(lookbackPeriod);

                // support

                var minClosingPrice = newCandle
                    .Min(c => c.Close);

                var canMin = newCandle
                    .Where(e => e.Close.Equals(minClosingPrice) && e.Date > DateTime.Now.AddDays(-300))
                    .ToList();

                support.AddRange(canMin);
            }

            // تمام مینیمم ها را گروپ میزنیم و براساس تعداد دفعه ها، سورت میکنیم
            // بیشترین دفعات یعنی احتمالا نقاط حمایت، این نقاط هستند
            // و بعد 4 نقطه حمایت نهایی را پیدا میکنیم
            var supportGroups = support
                .GroupBy(e => e.Close)
                .Select(group => new SupportResistancePercentageDifference
                {
                    Price = group.Key,
                    Count = group.Count(),
                    Date = group.Max(e => e.Date).ToDateOnly(),
                    SupportResistance = SupportResistanceTypeEnum.Support
                })
                .Where(e => e.Count > 5)
                .OrderByDescending(e => e.Count)
                .ThenByDescending(e => e.Date)
                .Take(4) // 4 تای آخر
                .ToList();

            return supportGroups;
        }

        private void FindPercentageDifference(List<SupportResistancePercentageDifference> supportResistance,            
            List<SupportResistanceIndicatorModel> candles)
        {
            var sup = new List<SupportResistanceIndicatorModel>();
            var candle = candles
                .OrderByDescending(e => e.Date)
                .FirstOrDefault();

            foreach (var support in supportResistance)
            {
                support.PercentageDifference = Math.Abs(candle.Close - support.Price) / candle.Close;
            }
        }
    }
}
