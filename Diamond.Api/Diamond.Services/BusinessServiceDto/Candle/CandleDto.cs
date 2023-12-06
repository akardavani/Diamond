using Diamond.Domain.Enums;

namespace Diamond.Services.BusinessServiceDto.Candle
{
    public class CandleDto
    {
        /// <summary>
        /// تایم فریم گرفتن دیتا
        /// </summary>
        public TimeframeEnum TimeFrame { get; set; }
        /// <summary>
        /// گرفتن دیتای امروز
        /// </summary>
        public bool TakeTodayData { get; set; }
    }
}
