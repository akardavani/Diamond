using Diamond.Domain.Enums;
using Diamond.Domain.Indicator;
using Diamond.Domain.Models.SupportResistance;
using Diamond.Services.Indicator;
using Diamond.Utils.BrokerExtention;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diamond.Services.Strategy
{
    internal class SupportResistanceStrategy : IStrategyImplementation
    {
        public string Calculate(List<IndicatorCandel> candels, IndicatorParameter indicatorParameter)
        {
            var instruments = candels
                .GroupBy(e => e.InstrumentId)
                .Select(e => e.Key)
                .ToList();

            var reasonlist = new List<SupportResistancePercentageDifference>();

            foreach (var instrument in instruments)
            {
                var find = candels.FindAll(e => e.InstrumentId == instrument);

                var supportResistance = FindSymbols(find, indicatorParameter)
                .ToList();

                if (supportResistance is not null)
                {
                    if (supportResistance.Count > 0)
                    {
                        reasonlist.AddRange(supportResistance);
                    }
                }
            }

            string json = JsonConvert.SerializeObject(reasonlist);
            //var json = JsonSerializer.Serialize(reasonlist);

            return json;
        }

        private List<SupportResistancePercentageDifference> FindSymbols(List<IndicatorCandel> candles, IndicatorParameter indicatorParameter)
        {
            var parameter = indicatorParameter.SupportResistancesParameter;

            var candels = candles
                .Select(e => new SupportResistanceIndicatorModel(parameter.CandelPriceType, e))
                .ToList();

            int lookbackPeriod = 300;

            var supportResistance = new List<SupportResistancePercentageDifference>();
            supportResistance.AddRange(FindResistance(candels, lookbackPeriod));
            supportResistance.AddRange(FindSupport(candels, lookbackPeriod));

            FindPercentageDifference(supportResistance, candels, parameter);

            return supportResistance;
        }

        private List<SupportResistancePercentageDifference> FindResistance(List<SupportResistanceIndicatorModel> candels, int lookbackPeriod)
        {
            var resistance = new List<SupportResistanceIndicatorModel>();

            // lookbackPeriod = 300
            // توی کل کندل ها، تکه های 300 تایی جدا میکنیم و مینیمم آنها را می یابیم
            // وبعد یک کندل جلو میرویم و دوباره این کار را انجام میدهیم

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

            // تمام ماکسیمم ها را گروپ میزنیم و براساس تعداد دفعه ها، سورت میکنیم
            // بیشترین دفعات یعنی احتمالا نقاط مقاومت، این نقاط هستند
            // و بعد 4 نقطه مقاومت نهایی را پیدا میکنیم
            var resistanceGroups = resistance
               .GroupBy(e => e.Close)
               .Select(group => new SupportResistancePercentageDifference
               {
                   InstrumentId = group.Max(e => e.InstrumentId),
                   Price = group.Key,
                   Count = group.Count(), // تعداد دفعات ماکسیمم (مقاومت) بودن
                   Date = group.Max(e => e.Date),
                   SupportResistanceType = SupportResistanceTypeEnum.Resistance
               })
               .Where(e => e.Count > 15)
               .OrderByDescending(e => e.Count)
               .ThenByDescending(e => e.Date)
               .Take(4)  // 4 تای آخر
               .ToList();

            return resistanceGroups;
        }

        private List<SupportResistancePercentageDifference> FindSupport(List<SupportResistanceIndicatorModel> candels, int lookbackPeriod)
        {
            var support = new List<SupportResistanceIndicatorModel>();

            // lookbackPeriod = 300
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
                    InstrumentId = group.Max(e => e.InstrumentId),
                    Price = group.Key,
                    Count = group.Count(), // تعداد دفعات مینیم (حمایت) بودن
                    Date = group.Max(e => e.Date),
                    SupportResistanceType = SupportResistanceTypeEnum.Support
                })
                .Where(e => e.Count > 15) 
                .OrderByDescending(e => e.Count)
                .ThenByDescending(e => e.Date)
                .Take(4) // 4 تای آخر
                .ToList();

            return supportGroups;
        }

        private void FindPercentageDifference(List<SupportResistancePercentageDifference> supportResistance,
            List<SupportResistanceIndicatorModel> candles,
            SupportResistanceIndicatorParameter parameter)
        {
            var candle = candles
                .OrderByDescending(e => e.Date)
                .FirstOrDefault();

            //if (candle.InstrumentId == "IRO3IZIZ0001")
            //{

            //}

            foreach (var support in supportResistance)
            {
                support.PercentageDifference = (Math.Abs(candle.Close - support.Price) / candle.Close) * 100;
            }

            var remove = supportResistance
                .Where(e => e.PercentageDifference > parameter.PercentageDifference
                        || e.SupportResistanceType != parameter.SupportResistanceType
                        || e.Date < DateTime.Today.AddDays(-1))
            .ToList();

            supportResistance.RemoveAll(l => remove.Contains(l));
        }
    }
}
