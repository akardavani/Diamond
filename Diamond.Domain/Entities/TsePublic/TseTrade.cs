using Newtonsoft.Json;

namespace Diamond.Domain.Entities.TsePublic
{
    public class TseTrade
    {
        [JsonProperty("LVal18AFC")]
        public string Symbol { get; set; }

        [JsonProperty("PriceFirst")]
        public decimal FirstPrice { get; set; }

        [JsonProperty("DEven")]
        public string Date { get; set; }

        [JsonProperty("InsCode")]
        public long InsCode { get; set; }

        [JsonProperty("LVal30")]
        public string Name { get; set; }

        [JsonProperty("PClosing")]
        public decimal ClosingPrice { get; set; }

        // آخرين قيمت معامله شده 
        [JsonProperty("PDrCotVal")]
        public decimal LastTrade { get; set; }

        //تعداد معاملات 
        [JsonProperty("ZTotTran")]
        public string NumberOfTransactions { get; set; }

        // تعداد سهام معامله شده - حجم
        [JsonProperty("QTotTran5J")]
        public string NumberOfSharesIssued { get; set; }

        //ارزش معاملات
        [JsonProperty("QTotCap")]
        public decimal TransactionValue { get; set; }

        [JsonProperty("PriceChange")]
        public decimal ChangePrice { get; set; }

        [JsonProperty("PriceMin")]
        public decimal MinPrice { get; set; }

        [JsonProperty("PriceMax")]
        public decimal MaxPrice { get; set; }

        [JsonProperty("PriceYesterday")]
        public decimal YesterdayPrice { get; set; }

        public decimal ClosingPricePercent => Math.Round(((ClosingPrice / YesterdayPrice) - 1) * 100M, 2);

        public decimal LastTradePercent => Math.Round(((LastTrade / YesterdayPrice) - 1) * 100M, 2);

        //  آخرین وضعیت
        [JsonProperty("Last")]
        public string LastState { get; set; }

        //ساعت
        [JsonProperty("HEven")]
        public string HEven { get; set; }
    }
}
