using Diamond.Domain.Enums;

namespace Diamond.Domain.Indicator
{
    public class CrossIchimokoIndicatorParameter : BaseIndicatorParameter
    {
        /// <summary>
        /// ترند صعودی یا نزولی
        /// </summary>
        public CrossEnum Trend { get; set; }
    }
}
