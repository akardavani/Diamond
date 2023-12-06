using Diamond.Domain.Enums;

namespace Diamond.Domain.Models
{
    public class CandleRequest
    {
        /// <summary>
        /// تایم فریم گرفتن دیتا
        /// </summary>
        public TimeframeEnum TimeFrame { get; set; } = TimeframeEnum.Daily;
        /// <summary>
        /// گرفتن دیتای امروز
        /// </summary>
        public bool TakeTodayData { get; set; } = false;
    }
}
