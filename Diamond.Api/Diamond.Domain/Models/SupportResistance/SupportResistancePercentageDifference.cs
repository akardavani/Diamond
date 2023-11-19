using Diamond.Domain.Enums;

namespace Diamond.Domain.Models.SupportResistance
{
    public class SupportResistancePercentageDifference
    {
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateOnly Date { get; set; }
        public SupportResistanceTypeEnum SupportResistance { get; set; }
        public decimal PercentageDifference { get; set; }
    }
}
