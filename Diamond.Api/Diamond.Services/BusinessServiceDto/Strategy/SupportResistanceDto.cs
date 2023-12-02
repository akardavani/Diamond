using Diamond.Domain.Enums;
using System;

namespace Diamond.Services.BusinessServiceDto.Strategy
{
    public class SupportResistanceDto
    {
        /// <summary>
        /// از تاریخ 
        /// </summary>
        public DateOnly FromDate { get; set; }
        /// <summary>
        /// تا تاریخ 
        /// </summary>
        public DateOnly ToDate { get; set; }
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
        public decimal PercentageDifference { get; set; }
        /// <summary>
        /// حمایت یا مقاومت
        /// </summary>
        public SupportResistanceTypeEnum SupportResistanceType { get; set; }
    }
}
