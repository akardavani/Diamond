using Diamond.Domain.Enums;

namespace Diamond.Domain.Models
{
    public class CandleRequest
    {
        /// <summary>
        /// گرفتن نمادها و کندل ها
        /// </summary>
        public bool TakingSymbolsAndCandles { get; set; }
        /// <summary>
        /// تایم فریم گرفتن دیتا
        /// </summary>
        public TimeframeEnum TimeFrame { get; set; } = TimeframeEnum.Daily;
    }
}
