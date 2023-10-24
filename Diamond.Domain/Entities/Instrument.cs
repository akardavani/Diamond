namespace Diamond.Domain.Entities
{
    public class Instrument
    {
        public int Id { get; set; }
        public string InstrumentId { get; set; }
        public string InstrumentPersianName { get; set; }
        public string CompanyName { get; set; }
        public string InsCode { get; set; }

        // اسم بازار
        public string MarketName { get; set; }
        
        //کد گروه صنعت 
        public string IndustryGroupCode { get; set; }

        // گروه صنعت	
        public string IndustryGroup { get; set; }

        //تابلو
        public string Board { get; set; }

        public string Title { get; set; }
        public bool IsDisabled { get; set; }

        public DateTime UpdateDateTime { get; set; }

        //public int UnitPriceType { get; set; }
        //public int InstrumentType { get; set; }
        //public string SectorCode { get; set; }
        //public string SubSectorCode { get; set; }  
        //public string BoardCode { get; set; }
        //public int MarketNatureId { get; set; }
        //public int MarketTypeId { get; set; }
        //public int MarketUnitTypeId { get; set; }
        //public string InstrumentGroupId { get; set; }
        //public int MarketSegmentId { get; set; }    
        //public string InstrumentFullName { get; set; }


        //public virtual InstrumentGroup InstrumentGroup { get; set; }
        //public virtual MarketSegment MarketSegment { get; set; }

        public virtual ICollection<InstrumentsEfficiency> InstrumentsEfficiencies { get; set; }
        public virtual ICollection<Trade> Trades { get; set; }
        //public virtual ICollection<Candel> Candels { get; set; }
    }
}
