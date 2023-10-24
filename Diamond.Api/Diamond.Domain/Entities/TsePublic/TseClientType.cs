using Newtonsoft.Json;

namespace Diamond.Domain.Entities.TsePublic
{
    public class TseClientType
    {
        //کد نماد
        [JsonProperty("InsCode")]
        public string InsCode { get; set; }

        //تعداد خریداران حقیقی
        [JsonProperty("Buy_CountI")]
        public long Buy_I_Count { get; set; }

        // تعداد خریداران حقوقی
        [JsonProperty("Buy_CountN")]
        public long Buy_N_Count { get; set; }

        //حجم خرید حقیقی
        [JsonProperty("Buy_I_Volume")]
        public long Buy_I_Volume { get; set; }

        //حجم خرید حقوقی
        [JsonProperty("Buy_N_Volume")]
        public long Buy_N_Volume { get; set; }

        // تعداد فروشنده حقیقی
        [JsonProperty("Sell_CountI")]
        public long Sell_I_Count { get; set; }

        // تعداد فروشنده حقوقی
        [JsonProperty("Sell_CountN")]
        public long Sell_N_Count { get; set; }

        // حجم فروش حقیقی
        [JsonProperty("Sell_I_Volume")]
        public long Sell_I_Volume { get; set; }

        // حجم فروش حقوقی
        [JsonProperty("Sell_N_Volume")]
        public long Sell_N_Volume { get; set; }
    }
}
