using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class SupportResistanceIndicatorParameter : BaseIndicatorParameter
    {
        /// <summary>
        /// درصد اختلاف تا مقاومت یا حمایت
        /// </summary>
        public decimal PercentageDifference { get; set; }
        /// <summary>
        /// حمایت یا مقاومت
        /// </summary>
        public SupportResistanceTypeEnum SupportResistanceType { get; set; }
    }
}
