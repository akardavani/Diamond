using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class IndicatorParameter
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
        /// ترند صعودی یا نزولی
        /// </summary>
        public CrossEnum Trend { get; set; }
        /// <summary>
        /// مقایسه قیمت
        /// </summary>
        public ComparisonPriceTypeEnum ComparisonPriceType { get; set; }
        /// <summary>
        /// ابر ایچیموکو
        /// </summary>
        public IchimokoCloudColorEnum IchimokoCloudColor { get; set; }
        /// <summary>
        /// نوع قیمت
        /// </summary>
        public CandelPriceEnum CandelPriceType { get; set; }
    }
}
