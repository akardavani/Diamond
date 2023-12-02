using Diamond.Domain.Enums;

namespace Diamond.Services.BusinessServiceDto.Candle
{
    public class CandleDto
    {
        /// <summary>
        /// گرفتن نمادها و کندل ها
        /// </summary>
        public bool TakingSymbolsAndCandles { get; set; }
        /// <summary>
        /// تایم فریم گرفتن دیتا
        /// </summary>
        public TimeframeEnum TimeFrame { get; set; }
    }
}
