using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class IndicatorParameter
    {
        /// <summary>
        /// مقادیر مربوط به استراتژی کراس ایچی موکو
        /// </summary>
        public CrossIchimokoIndicatorParameter CrossIchimokoParameter { get; set; }

        /// <summary>
        /// مقادیر مربوط به استراتژی قیمت و ابر ایچی موکو
        /// </summary>
        public PriceIchimokoCloudIndicatorParameter PriceIchimokoCloudParameter { get; set; }
        /// <summary>
        /// مقادیر مربوط به استراتژی نقاط حمایت مقاومت
        /// </summary>
        public PivotPointsIndicatorParameter PivotPointsParameter { get; set; }
    }
}
