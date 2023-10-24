namespace Diamond.Domain.Models.InstrumentExtraInfo
{
    public class InstrumentExtraInfoResponse
    {
        public int Id { get; set; }
        public string InstrumentId { get; set; }
        public decimal Eps { get; set; }
        public decimal PE { get; set; }

        // PE  گروه
        public decimal SectorPE { get; set; }
        public decimal PS { get; set; }
        public decimal Nav { get; set; }

        //حجم مبنا
        public decimal BaseVol { get; set; }

        //کد گروه صنعت 
        public int IndustryGroupCode { get; set; }

        // گروه صنعت	
        public string IndustryGroup { get; set; }

        // قیمت مجاز بالا
        public decimal AllowedPriceMax { get; set; }

        // قیمت مجاز پایین
        public decimal AllowedPriceMin { get; set; }
        public string Title { get; set; }

        // اسم بازار
        public string MarketName { get; set; }

        // بازه هفته - پایین
        public decimal MinWeek { get; set; }

        // بازه هفته - بالا
        public decimal MaxWeek { get; set; }

        // بازه سال - پایین
        public decimal MinYear { get; set; }

        // بازه سال - بالا
        public decimal MaxYear { get; set; }

        // میانگین حجم ماه
        public decimal AverageMonthVolume { get; set; }

        // سهام شناور	
        public decimal FloatingShares { get; set; }


        // From InstrumentListModel
        //گروه های صنعت
        public string InstrumentSectorGroup { get; set; }

        //تابلو
        public string Board { get; set; }

        //نماد
        public string InstrumentPersianName { get; set; }

        //نام لاتین
        public string InstrumentName { get; set; }

        public string InsCode { get; set; }

        public DateTime DateTime { get; set; }
    }
}
