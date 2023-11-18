using Diamond.Domain.Enums;

namespace Diamond.Utils.BrokerExtention
{
    public static class BrokerExtention
    {
        public static string DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var timestamp = (TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

            return string.Format("{0}", (long)timestamp);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static bool CompareValues(decimal value, decimal targetValue, ComparisonPriceTypeEnum comparisonType)
        {
            return comparisonType switch
            {
                ComparisonPriceTypeEnum.Equal => value == targetValue,
                ComparisonPriceTypeEnum.GreaterThan => value > targetValue,
                ComparisonPriceTypeEnum.LessThan => value < targetValue,
                _ => throw new ArgumentException("Invalid comparison type."),
            };
        }
    }
}
