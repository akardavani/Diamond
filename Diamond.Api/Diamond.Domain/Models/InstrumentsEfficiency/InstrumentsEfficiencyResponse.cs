namespace Domain.Models.InstrumentsEfficiency
{
    public class InstrumentsEfficiencyResponse
    {
        public string InstrumentId { get; set; }

        public DateTime OneMounthDate { get; set; }
        public DateTime ThreeMonthsDate { get; set; }
        public DateTime SixMonthsDate { get; set; }
        public DateTime AnnualDate { get; set; }

        public decimal ClosePrice { get; set; }
        public decimal OneMonthClosePrice { get; set; }
        public decimal ThreeMonthsClosePrice { get; set; }
        public decimal SixMonthsClosePrice { get; set; }
        public decimal AnnualClosePrice { get; set; }
        public decimal OneMonthProfit { get; set; }
        public decimal ThreeMonthsProfit { get; set; }
        public decimal SixMonthsProfit { get; set; }
        public decimal AnnualProfit { get; set; }
    }
}
