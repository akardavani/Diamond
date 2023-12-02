using Diamond.Domain.Enums;

namespace Diamond.Domain.Models
{
    public class SupportResistancePercentageDifference
    {
        public string Url => $"https://www.nahayatnegar.com/tv/{InstrumentId}";
        public string InstrumentId { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public SupportResistanceTypeEnum SupportResistanceType { get; set; }
        public decimal PercentageDifference { get; set; }
    }
}
