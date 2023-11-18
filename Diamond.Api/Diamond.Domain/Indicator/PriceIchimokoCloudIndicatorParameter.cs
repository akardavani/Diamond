using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class PriceIchimokoCloudIndicatorParameter : BaseIndicatorParameter
    {
        /// <summary>
        /// مقایسه قیمت
        /// </summary>
        public ComparisonPriceTypeEnum ComparisonPriceType { get; set; }
        /// <summary>
        /// ابر ایچیموکو
        /// </summary>
        public IchimokoCloudColorEnum IchimokoCloudColor { get; set; }
    }
}
