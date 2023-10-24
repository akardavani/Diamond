using System.Text.RegularExpressions;

namespace Diamond.Utils
{
    public class DateTimeEvaluator : IEvaluator
    {
        public string RegexPattern => @"\{DtNow(\+|\-)(\d{1,3})([DdmMyYhHsS])([hH]{0,1})\}";

        //For example: {DtNow+1Yh} which means (DateTime.Now+1Year Including hour) and equals:     2021-11-04T23:12:00
        //             {DtNow+1Y}  which means (DateTime.Now+1Year NOT Including hour) and equals: 2021-11-04
        public long Priority => 1000;

        private Dictionary<string, Func<DateTime, int, DateTime>> DatePartDic =
            new Dictionary<string, Func<DateTime, int, DateTime>>()
            {
                {"Ss", (dt, num) => dt.AddSeconds(num)},
                {"m", (dt, num) => dt.AddMinutes(num)},
                {"Hh", (dt, num) => dt.AddHours(num)},
                {"Dd", (dt, num) => dt.AddDays(num)},
                {"M", (dt, num) => dt.AddMonths(num)},
                {"Yy", (dt, num) => dt.AddYears(num)}
            };

        public string Evaluate(Match match)
        {
            var str = match.Value;
            var oprstr = match.Groups[1].Value;
            var numstr = match.Groups[2].Value;
            var prtstr = match.Groups[3].Value;
            var inclhr = match.Groups.Count > 4 ? (match.Groups[4].Value ?? string.Empty) : string.Empty;
            bool hasHour = !string.IsNullOrWhiteSpace(inclhr);
            var value = DateTime.Now;
            var addParam = int.Parse(numstr);
            if (oprstr == "-") addParam = -addParam;
            foreach (var part in DatePartDic)
            {
                if (part.Key.Contains(prtstr)) value = part.Value(value, addParam);
            }

            return value.ToString(hasHour ? "s" : "yyyy-MM-dd");
        }
    }
}
