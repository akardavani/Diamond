using Diamond.Domain.Enums;

namespace Diamond.Domain.Models
{
    public class IchimokoRequest
    {
        /// <summary>
        /// کراس خط های ایچی موکو
        /// </summary>
        public bool CrossIchimokoStrategy { get; set; }
        /// <summary>
        /// استراتژی قیمت و ابر
        /// </summary>
        public bool PriceIchimokoCloudStrategy { get; set; }
        /// <summary>
        /// از تاریخ 
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// تا تاریخ 
        /// </summary>
        public DateTime ToDate { get; set; }
        /// <summary>
        /// ترند صعودی یا نزولی
        /// </summary>
        public CrossEnum Trend { get; set; }
        /// <summary>
        /// نوع قیمت
        /// </summary>
        public CandelPriceEnum CandelPriceType { get; set; }
        /// <summary>
        /// مقایسه قیمت
        /// </summary>
        public ComparisonPriceTypeEnum ComparisonPriceType { get; set; }
        /// <summary>
        /// قیمت بالاتر یا پایین تر
        /// </summary>
        public PriceAboveBelowEnum? PriceAboveBelow { get; set; }
        /// <summary>
        /// ابر ایچیموکو
        /// </summary>
        public IchimokoCloudColorEnum IchimokoCloudColor { get; set; }
    }
}
