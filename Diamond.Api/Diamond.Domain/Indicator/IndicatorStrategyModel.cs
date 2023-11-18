using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class IndicatorStrategyModel
    {
        public string Symbol { get; set; }
        public List<IndicatorCandel> Candels { get; set; }
        public TimeframeEnum TimeFram { get; set; }
        public TimeframeEnum TimeFrameType { get; set; }
    }
}
