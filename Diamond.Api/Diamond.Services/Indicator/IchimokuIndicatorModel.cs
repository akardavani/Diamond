using Diamond.Domain.Enums;
using Diamond.Domain.Indicator;

namespace Diamond.Services.Indicator
{
    public class IchimokuIndicatorModel : IndicatorCandel
    {
        public IchimokuIndicatorModel(CandelPriceEnum candelPrice, IndicatorCandel indicatorCandel)
        {
            High = indicatorCandel.High;
            Low = indicatorCandel.Low;
            Open = indicatorCandel.Open;
            Close = indicatorCandel.Close;
            InstrumentId = indicatorCandel.InstrumentId;
            NetValue = indicatorCandel.NetValue;
            Open = indicatorCandel.Open;
            UnixTimestamp = indicatorCandel.UnixTimestamp;
            Volume = indicatorCandel.Volume;

            // انتخاب نوع قیمت - کمترین، بیشترین، بالاترین، پایین ترین
            SelectedPrice = GetSelectedPrice(candelPrice);
        }

        // فقط خواندی است        
        public decimal SelectedPrice { get;}
        public CrossEnum Trend { get; set; }
        public IchimokoCloudColorEnum CloudColor { get; set; }
        public PriceAboveBelowEnum PriceAboveBelow { get; set; }   
        public ComparisonPriceTypeEnum ComparisonType { get; set; }
    }
}
