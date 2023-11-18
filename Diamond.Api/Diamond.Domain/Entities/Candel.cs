namespace Diamond.Domain.Entities
{
    public class Candel
    {
        public long Id { get; set; }
        public string InstrumentId { get; set; }        
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public int Timeframe { get; set; }
        public DateTime Date { get; set; }
        public string PersianDate { get; set; }
        public long Timestamp { get; set; }
        public decimal NetValue { get; set; }
    }
}
