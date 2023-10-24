using Newtonsoft.Json;

namespace Diamond.Domain.Entities.TsePublic
{
    public class TseShareChange
    {
        //کد نماد
        [JsonProperty("InsCode")]
        public string InsCode { get; set; }

        //تاریخ
        [JsonProperty("DEven")]
        public string Date { get; set; }

        //سهام قبلی
        [JsonProperty("NumberOfShareOld")]
        public long NumberOfShareOld { get; set; }

        //سهام جدید
        [JsonProperty("NumberOfShareNew")]
        public long NumberOfShareNew { get; set; }

        //نام
        [JsonProperty("LVal30")]
        public string Name { get; set; }

        //نماد
        [JsonProperty("LVal18AFC")]
        public string Symbol { get; set; }
    }
}
