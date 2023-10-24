namespace Diamond.Domain.Entities
{
    /// <summary>
    /// لیست معاملات
    /// </summary>
    public class Trade
    {
        public long Id { get; set; }
        public DateTime TradeDate { get; set; }
        public byte TradeTypeId { get; set; }
        public string InstrumentId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal BrokerCommission { get; set; }
        public decimal? Tax { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Instrument Instrument { get; set; }

        //public virtual ICollection<TradeCommission> TradeCommissions { get; set; }
    }
}
