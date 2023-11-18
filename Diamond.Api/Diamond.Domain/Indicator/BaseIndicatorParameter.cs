using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class BaseIndicatorParameter
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
    }
}
