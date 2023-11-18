using Diamond.Domain.Enums;
using Skender.Stock.Indicators;

namespace Diamond.Domain.Indicator
{
    public class IndicatorCandel : IQuote
    {
        public string InstrumentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public long UnixTimestamp { get; set; }
        public decimal NetValue { get; set; }
        public DateOnly DateOnly => new DateOnly(Date.Year, Date.Month, Date.Day);
        public DateTime UtcDateTime => TimeZoneInfo.ConvertTimeToUtc(Date, TimeZoneInfo.Local);

        protected decimal GetSelectedPrice(CandelPriceEnum candelPrice)
        {
            // استفاده از پراپرتی داینامیک
            return candelPrice switch
            {
                CandelPriceEnum.Open => Open,
                CandelPriceEnum.Close => Close,
                CandelPriceEnum.High => High,
                CandelPriceEnum.Low => Low,
                _ => Close,
                //_ => throw new ArgumentException("Invalid selected price."),
            };
        }
    }    
}
