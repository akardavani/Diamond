using Diamond.Domain.Enums;

namespace Diamond.Utils.BrokerExtention
{
    public static class NahayatnegarExtention
    {
        public static string GetTimeFrame(TimeframeEnum timeFrame)
        {
            var result = "";
            switch (timeFrame)
            {
                case TimeframeEnum.Weekly:
                    result = "1W";
                    break;
                case TimeframeEnum.Daily:
                    result = "1D";
                    break;
                case TimeframeEnum.FourHours:
                    result = "4h";
                    break;
                case TimeframeEnum.OneHour:
                    result = "1";
                    break;
                case TimeframeEnum.FifteenMinutes:
                    result = "15m";
                    break;
                case TimeframeEnum.FiveMinutes:
                    result = "5m";
                    break;
            }

            return result;
        }
    }
}
