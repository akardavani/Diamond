using System.Globalization;

namespace Diamond.Utils.BrokerExtention
{
    public static class DateTimeExtensions
    {
        private const string DateFormat = "yyyy-MM-dd";
        public static readonly PersianCalendar Calendar = new();
        public static readonly CultureInfo FaCulture = new("fa-IR");

        public static string ToPersian(this DateTime dateTime, string format = DateFormat)
        {
            return dateTime.ToString(format, FaCulture);
        }

        public static string ToPersian(this DateOnly date, string format = DateFormat)
        {
            return date.ToString(format, FaCulture);
        }

        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }

        public static DateOnly? ToDateOnly(this DateTime? dateTime)
        {
            return dateTime is null ?
                null :
                DateOnly.FromDateTime(dateTime.Value);
        }

        public static TimeOnly ToTimeOnly(this DateTime dateTime)
        {
            return TimeOnly.FromDateTime(dateTime);
        }

        public static TimeOnly? ToTimeOnly(this DateTime? dateTime)
        {
            return dateTime is null ?
                null :
                TimeOnly.FromDateTime(dateTime.Value);
        }

        public static TimeOnly ToTimeOnly(this TimeSpan time)
        {
            return TimeOnly.FromTimeSpan(time);
        }

        public static TimeOnly? ToTimeOnly(this TimeSpan? time)
        {
            return time is null ?
                null :
                TimeOnly.FromTimeSpan(time.Value);
        }

        public static DateOnly Min(this DateOnly original, DateOnly compare)
        {
            return (original <= compare) ?
                original :
                compare;
        }

        public static int GetDaysInYear(this DateOnly date)
        {
            return GetDaysInYear(date.ToDateTime(TimeOnly.MinValue));
        }

        public static int GetDaysInYear(this DateTime date)
        {
            var year = Calendar.GetYear(date);
            return Calendar.GetDaysInYear(year);
        }

        public static DateOnly GetFirstDayOfYear(this DateTime date)
        {
            var persianYear = Calendar.GetYear(date);
            return Calendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0).ToDateOnly();
        }

        public static IReadOnlyCollection<DateOnly> GetDaysOfYear(this DateOnly date)
        {
            return GetDaysOfYear(date.ToDateTime(new TimeOnly(0, 0, 0)));
        }

        public static IReadOnlyCollection<DateOnly> GetDaysOfYear(this DateTime date)
        {
            var days = new List<DateOnly>(366);
            var persianYear = Calendar.GetYear(date);
            var isLeapYear = Calendar.IsLeapYear(persianYear);
            var startDate = Calendar.ToDateTime(persianYear, 1, 1, 0, 0, 0, 0);
            var endDate = Calendar.ToDateTime(persianYear, 12, isLeapYear ? 30 : 29, 0, 0, 0, 0);
            while (startDate <= endDate)
            {
                days.Add(startDate.ToDateOnly());
                startDate = startDate.AddDays(1);
            }

            return days;
        }

        public static bool IsHoliday(this DateOnly date, out string reason)
        {
            return IsHoliday(date.ToDateTime(TimeOnly.MinValue), out reason);
        }

        public static bool IsHoliday(this DateTime date, out string reason)
        {
            var month = Calendar.GetMonth(date);
            var day = Calendar.GetDayOfMonth(date);
            var monthName = FaCulture.DateTimeFormat.GetMonthName(month);

            var isHoliday =
                //تعطیلات فروردین
                (month == 1 && (day == 1 || day == 2 || day == 3 || day == 4 || day == 12 || day == 13)) ||
                // تعطیلات خرداد
                (month == 3 && (day == 14 || day == 15)) ||
                // تعطیلات بهمن
                (month == 11 && (day == 22)) ||
                // تعطیلات اسفند
                (month == 12 && (day == 29 || day == 30));

            if (isHoliday)
            {
                reason = $"تعطیل رسمی {day} {monthName}";
                return true;
            }
            else if (date.DayOfWeek == DayOfWeek.Thursday || date.DayOfWeek == DayOfWeek.Friday)
            {
                reason = $"تعطیل {FaCulture.DateTimeFormat.GetDayName(date.DayOfWeek)} روز غیرکاری";
                return true;
            }
            else
            {
                reason = null;
                return false;
            }
        }
    }
}
