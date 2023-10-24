using Newtonsoft.Json;

namespace Diamond.Domain.Entities.TsePublic
{
    //https://codebeautify.org/xml-to-csharp-pojo-generator

    public partial class TseInstrument
    {
        /// <summary>
        /// Date of RLC event.
        /// </summary>
        [JsonProperty("DEVen")]
        public long Date { get; set; }

        [JsonProperty("InsCode")]
        public long InsCode { get; set; }

        [JsonProperty("InstrumentID")]
        public string InstrumentId { get; set; }

        /// <summary>
        /// Instrument mnemonic code.
        /// </summary>
        [JsonProperty("CValMne")]
        public string InstrumentMnemonic { get; set; }

        /// <summary>
        /// 18-character instrument name
        /// نام نماد
        /// </summary>
        [JsonProperty("LVal18")]
        public string EnglishName { get; set; }

        /// <summary>
        /// Code for issuing company.
        /// </summary>
        [JsonProperty("CSocCSAC")]
        public string FourDigitCompanyCode { get; set; }

        /// <summary>
        /// 30-character AFC name for issuing company.
        /// </summary>
        [JsonProperty("LSoc30")]
        public string CompanyName { get; set; }

        /// <summary>
        /// 18-character instrument name.
        /// اسم نماد بصورت 18 کاراکتری
        /// </summary>
        [JsonProperty("LVal18AFC")]
        public string Symbol { get; set; }

        /// <summary>
        /// 30-character instrument name.
        /// اسم نماد بصورت 30 کاراکتری
        /// </summary>
        [JsonProperty("LVal30")]
        public string Name { get; set; }

        /// <summary>
        /// ISIN foreign cash product.
        /// شناسه نماد؛ مثلاً برای فولاد: IRO1FOLD0009
        /// </summary>
        [JsonProperty("CIsin")]
        public string CIsin { get; set; }

        /// <summary>
        /// Amount of par value of instrument for calculating amount for trade.
        /// مبلغ اسمی
        /// </summary>
        [JsonProperty("QNmVlo")]
        public long? QNmVlo { get; set; }

        /// <summary>
        /// Number of shares or bonds outstanding.
        /// تعداد سهام
        /// </summary>
        [JsonProperty("ZTitad")]
        public long? ZTitad { get; set; }

        /// <summary>
        /// Date on which the creation, modification, or deletion
        /// of an instrument takes effect.
        /// </summary>
        [JsonProperty("DESop")]
        public long? DeSop { get; set; }

        /// <summary>
        /// Type of corporate event causing instrument modification on current day.
        /// انواع مجامع
        /// </summary>
        [JsonProperty("YOPSJ")]
        public long? Yopsj { get; set; }

        /// <summary>
        /// Code of the instrument category.
        /// Possible Values: A = Share , I = Index , W = Warrant
        /// </summary>
        [JsonProperty("CGdSVal")]
        public string CGdSVal { get; set; }

        /// <summary>
        /// Instrument group ID.
        /// </summary>
        [JsonProperty("CGrValCot")]
        public string InstrumentGroupId { get; set; }

        /// <summary>
        /// Date of first day of trading for instrument.
        /// تاریخ اولین معامله نماد
        /// </summary>
        [JsonProperty("DInMar")]
        public long? DInMar { get; set; }

        /// <summary>
        /// Type of unit of expression for instrument price.
        /// واحد معرفی قیمت نماد: 
        /// 1 = In absolute value , 2 = In percentage
        /// </summary>
        [JsonProperty("YUniExpP")]
        public long? YUniExpP { get; set; }

        /// <summary>
        /// Market Segment
        /// </summary>
        [JsonProperty("YMarNSC")]
        public string MarketSegment { get; set; }

        [JsonProperty("CComVal")]
        public long BoardCode { get; set; }

        [JsonProperty("CSecVal")]
        public string SectorCode { get; set; }

        [JsonProperty("CSoSecVal")]
        public string SubSectorCode { get; set; }

        /// <summary>
        /// Settlement Delay Type.
        /// نوع تاریخ تسویه
        /// T+1 , T+2 , T+3
        /// </summary>
        [JsonProperty("YDeComp")]
        public long? YDeComp { get; set; }

        /// <summary>
        /// High Intermediate Threshold
        /// قیمت آستانه متوسط بالای نماد
        /// </summary>
        [JsonProperty("PSaiSMaxOkValMdv")]
        public long? PSaiSMaxOkValMdv { get; set; }

        /// <summary>
        /// Low Intermediate Threshold
        /// قیمت آستانه متوسط پایین نماد
        /// </summary>
        [JsonProperty("PSaiSMinOkValMdv")]
        public long? PSaiSMinOkValMdv { get; set; }

        /// <summary>
        /// حجم مبنا
        /// </summary>
        [JsonProperty("BaseVol")]
        public long BaseVol { get; set; }

        /// <summary>
        /// Type of instrument.
        /// معرف نمادهای هم گروه
        /// </summary>
        [JsonProperty("YVal")]
        public long? YVal { get; set; }

        /// <summary>
        /// Amount of the fixed price tick for an instrument.
        /// تیک سایز قیمتی نماد
        /// </summary>
        [JsonProperty("QPasCotFxeVal")]
        public long? QPasCotFxeVal { get; set; }

        /// <summary>
        /// Instrument lot size.
        /// لات سایز حجمی نماد
        /// </summary>
        [JsonProperty("QQtTranMarVal")]
        public long? QQtTranMarVal { get; set; }

        /// <summary>
        /// احتمالاً نوع بازار را برگشت می دهد.
        /// </summary>
        [JsonProperty("Flow")]
        public long Flow { get; set; }

        /// <summary>
        /// Minimum quantity which can be entered for orders on the instrument
        /// حداقل حجم سفارش
        /// </summary>
        [JsonProperty("QtitMinSaiOmProd")]
        public long? QtitMinSaiOmProd { get; set; }

        /// <summary>
        /// Maximum quantity which can be entered for orders on the instrument
        /// حداکثر حجم سفارش
        /// </summary>
        [JsonProperty("QtitMaxSaiOmProd")]
        public long? QtitMaxSaiOmProd { get; set; }


        [JsonProperty("Valid")]
        public int Valid { get; set; }
    }
}
