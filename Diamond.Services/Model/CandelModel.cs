using Diamond.Domain.Entities;
using Skender.Stock.Indicators;
using System;

namespace Diamond.Services.Model
{
    public class CandelModel : IQuote
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public string InstrumentId { get; set; }
        public long UnixTimestamp { get; set; }
        public DateTime UtcDateTime => TimeZoneInfo.ConvertTimeToUtc(Date, TimeZoneInfo.Local);
        public decimal NetValue { get; set; }
        public decimal StopLoss { get; set; }

        public CandelModel(Candel model)
        {
            Date = model.Date;
            Open = model.Open;
            Close = model.Close;
            High = model.High;
            Low = model.Low;
            Volume = model.Volume;
            InstrumentId = model.InstrumentId;
            UnixTimestamp = model.Timestamp;
            NetValue = model.NetValue;

        }
    }
}
