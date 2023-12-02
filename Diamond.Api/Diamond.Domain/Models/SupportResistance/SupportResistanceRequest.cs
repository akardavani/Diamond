using Diamond.Domain.Enums;

namespace Diamond.Domain.Models
{
    public class SupportResistanceRequest
    {
        /// <summary>
        /// از تاریخ 
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// تا تاریخ 
        /// </summary>
        public DateTime ToDate { get; set; }
        /// <summary>
        /// نوع قیمت
        /// </summary>
        public CandelPriceEnum CandelPriceType { get; set; }
        /// <summary>
        /// تایم فریم
        /// </summary>
        public TimeframeEnum Timeframe { get; set; }
        /// <summary>
        /// درصد اختلاف تا مقاومت یا حمایت
        /// </summary>
        /// 
        public decimal PercentageDifference { get; set; }
        /// <summary>
        /// حمایت یا مقاومت
        /// </summary>
        public SupportResistanceTypeEnum SupportResistanceType { get; set; }
    }
}
