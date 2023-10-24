using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Diamond.Utils
{
    public static class StringExtensions
    {
        static PersianCalendar pcal = new PersianCalendar();
        private static ConcurrentDictionary<string, string> _dictionary = new ConcurrentDictionary<string, string>();
        public static string ToPersianDate(this DateTime dt, bool includeTime = false, char separator = '/')
        {
            if (dt == default) return string.Empty;
            var date = $"{pcal.GetYear(dt):0000}{separator}{pcal.GetMonth(dt):00}{separator}{pcal.GetDayOfMonth(dt):00}";
            if (includeTime)
                date += $@" {dt.Hour:00}:{dt.Minute:00}";
            return date;
        }

        public static DateTime PersianToGregorianDate(this string date)
        {
            var plainTest = date.Replace("/", "").Replace("-", "");
            if (plainTest.Length != 8 || plainTest.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Invalid input");
            }
            var y = int.Parse(plainTest.Substring(0, 4));
            var m = int.Parse(plainTest.Substring(4, 2));
            var d = int.Parse(plainTest.Substring(6, 2));
            var persianCalendar = new PersianCalendar();

            return persianCalendar.ToDateTime(y, m, d, 0, 0, 0, 0);
        }
        public static DateTime PersianToGregorianDateTime(this string date)
        {
            var plainTest = date.Replace("/", "").Replace("-", "").Replace(":", "").Replace(".", "").Replace(" ", "");
            if (plainTest.Length < 14 || plainTest.Length > 21 || plainTest.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Invalid input");
            }
            var persianCalendar = new PersianCalendar();
            var year = int.Parse(plainTest.Substring(0, 4));
            var month = int.Parse(plainTest.Substring(4, 2));
            var day = int.Parse(plainTest.Substring(6, 2));
            var hour = int.Parse(plainTest.Substring(8, 2));
            var minute = int.Parse(plainTest.Substring(10, 2));
            var second = int.Parse(plainTest.Substring(12, 2));
            var millisecond = 0;
            if (plainTest.Length > 14)
                millisecond = int.Parse(date.Substring(14, plainTest.Length - 14));

            return persianCalendar.ToDateTime(year, month, day, hour, minute, second, millisecond);
        }

        public static string ApplyParameterTransformation(this string str)
        {
            var s = RegexEvaluator.Current.EvaluateString(str);
            return s;
        }

        public static string ToSeparatedPascalCaseString(this string str)
        {
            str = str.Trim();

            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (_dictionary.TryGetValue(str, out string value))
            {
                return value;
            }
            else
            {
                value = Regex.Replace(str, "(\\B[A-Z])", " $1");
                value = $"{value.Substring(0, 1)}{value.Substring(1, value.Length - 1).ToLower()}";

                _dictionary.TryAdd(str, value);
                return value;
            }
        }
        public static TimeSpan ToTimeSpan(this string timeFormat)
        {
            return TimeSpan.Parse(timeFormat);
        }

        public class TrimStartComparer : IEqualityComparer<string>
        {
            private static char[] trimmableMsgType = { ' ', '0' };
            private static TrimStartComparer _instance;

            public bool Equals(string x, string y)
            {
                if (string.IsNullOrWhiteSpace(x) || string.IsNullOrWhiteSpace(y))
                {
                    return string.IsNullOrWhiteSpace(x) && string.IsNullOrWhiteSpace(y);
                }

                return Safe(x).Equals(Safe(y), StringComparison.CurrentCultureIgnoreCase);
            }

            public static string Safe(string x)
            {
                return x.Trim().TrimStart(trimmableMsgType);
            }

            public int GetHashCode(string obj)
            {
                if (string.IsNullOrWhiteSpace(obj)) return string.Empty.GetHashCode();
                return Safe(obj).GetHashCode();
            }

            public static TrimStartComparer Instance => _instance ?? (_instance = new TrimStartComparer());
        }
        public static string RemoveAngleBrackets(this string value)
        {
            //return EncodeHtml(value);
            if (string.IsNullOrWhiteSpace(value)) return value;
            return value.Replace("<", "").Replace(">", "");
        }
        public static bool EqualsTrimStart(this string msgType, string referenceMsgType)
        {
            return TrimStartComparer.Instance.Equals(referenceMsgType, msgType);
        }

        public static bool IsInTrimStart(this string str, IEnumerable<string> strs)
        {
            if (string.IsNullOrWhiteSpace(str) || !strs.Any())
            {
                return string.IsNullOrWhiteSpace(str) && !strs.Any();
            }

            var strFixed = TrimStartComparer.Safe(str);
            //Above line makes this function different from EqualsMessageType (one trim for source variable makes it a little fast,
            //so please DO NOT call EqualsMessageType in this function, instead, we have repeated the process of compare


            return strs.Any(x =>
                !string.IsNullOrWhiteSpace(x) && TrimStartComparer.Instance.Equals(x, strFixed));
        }


        public static string EncodeHtmlSafe(this string value)//Please don't use custom html contract resolver
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            var nwvalue = System.Net.WebUtility.HtmlDecode(System.Net.WebUtility.HtmlDecode(value));
            if (string.IsNullOrWhiteSpace(nwvalue)) return nwvalue;
            //if (value.Contains(">") || value.Contains("<"))
            return System.Net.WebUtility.HtmlEncode(nwvalue);
        }
        public static string ReplaceInvalidCharacters(this string str)
        {
            return str.Replace("ي", "ی").Replace("ك", "ک");
        }
        static readonly char[] TrimChars = new[] { ' ', '\0', '\n', '\r', ' ', ',', '\"', '\'', '\t' };
        public static string TrimAll(this string str, params char[] otherChars)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;
            char[] trimChars = TrimChars;
            if (otherChars != null && otherChars.Length > 0)
            {
                trimChars = trimChars.Concat(otherChars).ToArray();
            }

            return str.Trim(trimChars);
        }
        public static decimal? ParseDecimal(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            str = str.Replace(",", "").TrimAll();
            return (string.IsNullOrWhiteSpace(str)) ? (decimal?)null : decimal.Parse(str.TrimAll().Replace(",", ""));
        }

        public static long? ParseLong(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            str = str.Replace(",", "").TrimAll();
            return (string.IsNullOrWhiteSpace(str)) ? (long?)null : long.Parse(str.TrimAll().Replace(",", ""));
        }

        public static string ToTradeCode(this string nationalCode, string brokerCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode)) return string.Empty;
            nationalCode = nationalCode.TrimAll();
            if (nationalCode.StartsWith(brokerCode))
            {

            }
            else
            {
                if (nationalCode.Length < 10)
                {
                    nationalCode = nationalCode.LeadStr("0", 10);
                }

                nationalCode = brokerCode + nationalCode;
            }

            return nationalCode;
            //if (nationalCode.StartsWith(brokerCode, StringComparison.InvariantCultureIgnoreCase))
            //{
            //    var foreignTypeCodes = new char[] { '4', '7', '6', '5' };
            //    if (nationalCode.Length > brokerCode.Length &&
            //        foreignTypeCodes.Contains(nationalCode[brokerCode.Length])) return nationalCode;
            //}

            //if (nationalCode.Length < 10 && !nationalCode.StartsWith(brokerCode + "8")) LeadStr(nationalCode, "0", 10);
            //var typeIdentifier = nationalCode.Length <= 10 ? "9" : (nationalCode.Length == 11 ? "80" : "");
            //return ((nationalCode.StartsWith(brokerCode + "9") && nationalCode.Length == 14 ||
            //         nationalCode.StartsWith(brokerCode + "80") && (nationalCode.Length <= 10 &&  && nationalCode.Length >= 5) ||
            //         nationalCode.StartsWith(brokerCode + "81") && (nationalCode.Length <= 10 && nationalCode.Length>=5) ||
            //         nationalCode.StartsWith(brokerCode) && nationalCode.Length > 11)
            //           ? string.Empty
            //           : brokerCode + typeIdentifier) +nationalCode;
            //7779 1 234 567 890
            //77780 12345
            //4-->DDD
            //7-->Foreigner
        }

        private static ConcurrentDictionary<Type, List<PropertyInfo>> properties =
            new ConcurrentDictionary<Type, List<PropertyInfo>>();
        private static ConcurrentDictionary<Type, List<FieldInfo>> fields =
            new ConcurrentDictionary<Type, List<FieldInfo>>();
        public static Dictionary<string, object> GetPublicPropertiesAndFieldsDictionary(this object obj)
        {
            if (obj == null) return null;
            var dic = new Dictionary<string, object>();
            var type = obj.GetType();
            if (type.IsPrimitive) return null;
            if (typeof(IDictionary<string, string>).IsAssignableFrom(type))
            {
                return (obj as IDictionary<string, string>)?.ToDictionary(x => x.Key, x => (object)x.Value);
            }
            if (typeof(IDictionary<string, int>).IsAssignableFrom(type))
            {
                return (obj as IDictionary<string, int>)?.ToDictionary(x => x.Key, x => (object)x.Value);
            }
            if (typeof(IDictionary<string, long>).IsAssignableFrom(type))
            {
                return (obj as IDictionary<string, long>)?.ToDictionary(x => x.Key, x => (object)x.Value);
            }
            if (typeof(IDictionary<string, decimal>).IsAssignableFrom(type))
            {
                return (obj as IDictionary<string, decimal>)?.ToDictionary(x => x.Key, x => (object)x.Value);
            }
            if (typeof(IDictionary<string, object>).IsAssignableFrom(type))
            {
                return (obj as IDictionary<string, object>)?.ToDictionary(x => x.Key, x => x.Value);
            }
            var lstp = properties.GetOrAdd(type, typ => typ.GetProperties()?.ToList())?.ToList();
            if (lstp != null && lstp.Count > 0)
            {
                foreach (var propertyInfo in lstp)
                {
                    dic.Add(propertyInfo.Name, propertyInfo.GetValue(obj));
                }
            }
            var lstf = fields.GetOrAdd(type, typ => typ.GetFields()?.ToList())?.ToList();
            if (lstf != null && lstf.Count > 0)
            {
                foreach (var fieldInfo in lstf)
                {
                    dic.Add(fieldInfo.Name, fieldInfo.GetValue(obj));
                }
            }

            return dic;

        }

        public static bool GentlyEqual(this string s1, string s2)
        {
            if (string.IsNullOrWhiteSpace(s1) && string.IsNullOrWhiteSpace(s2)) return true;
            if (string.IsNullOrWhiteSpace(s1)) s1 = String.Empty;
            if (string.IsNullOrWhiteSpace(s2)) s2 = String.Empty;
            s2 = s2.Trim().Replace("  ", " ");
            s1 = s1.Trim().Replace("  ", " ");
            return s1.Equals(s2, StringComparison.InvariantCultureIgnoreCase);
        }
        public static string AddQueryString(this string url, object obj)
        {
            if (obj != null)
            {
                var members = obj.GetPublicPropertiesAndFieldsDictionary();
                if (members != null && members.Count > 0)
                {
                    url = QueryHelpers.AddQueryString(url,
                        members.Where(x => x.Key != null && x.Value != null)
                            .ToDictionary(x => x.Key, x =>
                            {
                                if (x.Value != null)
                                {
                                    if (x.Value is DateTime || x.Value is DateTime?)
                                    {
                                        var val = (DateTime)x.Value;
                                        if (val == default) return string.Empty;
                                        if (val.TimeOfDay.TotalSeconds < 1) return val.ToString("yyyy-MM-dd");
                                        return val.ToString("yyyy-MM-dd") + " " + val.TimeOfDay.ToString("g");
                                    }

                                    if (x.Value is DateTimeOffset || x.Value is DateTimeOffset?)
                                    {
                                        var val = (DateTimeOffset)x.Value;
                                        if (val == default) return string.Empty;
                                        if (val.TimeOfDay.TotalSeconds < 1) return val.ToString("yyyy-MM-dd");
                                        return val.ToString("yyyy-MM-dd") + " " + val.TimeOfDay.ToString("g");
                                    }
                                }

                                return x.Value?.ToString() ?? string.Empty;
                            }));
                }
            }
            return url;
        }
        public static string LeadStr(this string s, string lead, int count)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            while (s.Length < count)
                s = lead + s;
            return s;
        }

        public static string SafeMobileValue(this string mobile)
        {
            if (!string.IsNullOrWhiteSpace(mobile))
            {
                if (mobile.StartsWith("98") && mobile.Length > 2)
                {
                    mobile = "0" + mobile.Substring(2);
                }
                else if (mobile.StartsWith("0098") && mobile.Length > 4)
                {
                    mobile = "0" + mobile.Substring(4);
                }
                else if (mobile.StartsWith("+98") && mobile.Length > 3)
                {
                    mobile = "0" + mobile.Substring(3);
                }
                else if (mobile.StartsWith("9"))
                {
                    mobile = "0" + mobile;
                }
            }
            else
            {
                mobile = string.Empty;
            }

            return mobile;
        }
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> TypeStringPropertyMapCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public static void NormalizeModel<T>(this T obj) where T : class
        {
            if (obj == null) return;
            if (obj is string) return;
            if (obj.GetType().IsPrimitive) return;
            var members = TypeStringPropertyMapCache.GetOrAdd(typeof(T), type => typeof(T).GetProperties(BindingFlags.Public).Where(x => x.PropertyType == typeof(string)).ToArray());
            foreach (var stringMember in members)
            {
                var propValue = stringMember.GetValue(obj);
                if (propValue != default)
                {
                    stringMember.SetValue(obj, ((string)propValue).EncodeHtmlSafe());
                }
            }
        }
    }
}
