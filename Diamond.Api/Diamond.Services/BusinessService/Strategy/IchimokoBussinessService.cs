using Diamond.Domain.Indicator;
using Diamond.Services.BusinessServiceDto.Strategy;
using Diamond.Services.CommonService;
using Diamond.Services.Strategy;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace Diamond.Services.BusinessService.Strategy
{
    public class IchimokoBussinessService : IBusinessService
    {
        private readonly CommonInstrument _instrument;

        public IchimokoBussinessService(CommonInstrument instrument)
        {
            _instrument = instrument;
        }

        public async Task Find(IchimokoDto request, CancellationToken cancellation)
        {
            var instruments = await _instrument.GetAllCandles(request.Timeframe, cancellation);

            var indicatorCandel = instruments
                .Select(e => new IndicatorCandel
                {
                    InstrumentId = e.InstrumentId,
                    Close = e.Close,
                    Date = e.Date,
                    High = e.High,
                    Low = e.Low,
                    NetValue = e.NetValue,
                    Open = e.Open,
                    Volume = e.Volume,
                    //UnixTimestamp = e.Timestamp
                })
                .ToList();

            CrossIchimoko(request, indicatorCandel);

            PriceIchimokoCloud(request, indicatorCandel);
        }

        private void CrossIchimoko(IchimokoDto request, List<IndicatorCandel> indicatorCandel)
        {
            if (request.CrossIchimoko)
            {
                var indicatorParameter = new IndicatorParameter
                {
                    CrossIchimokoParameter = new CrossIchimokoIndicatorParameter
                    {
                        FromDate = request.FromDate,
                        ToDate = request.ToDate,
                        Trend = request.Trend
                    }
                };

                var strategy = new StrategyImplementation(new CrossIchimokoStrategy(), indicatorParameter);
                var symbols = strategy.FindSymbols(indicatorCandel);
            }
        }

        private void PriceIchimokoCloud(IchimokoDto request, List<IndicatorCandel> indicatorCandel)
        {
            if (request.PriceAboveBelow is not null)
            {
                var indicatorParameter = new IndicatorParameter
                {
                    PriceIchimokoCloudParameter = new PriceIchimokoCloudIndicatorParameter
                    {
                        FromDate = request.FromDate,
                        ToDate = request.ToDate,
                        CandelPriceType = request.CandelPriceType,
                        ComparisonPriceType = request.ComparisonPriceType,
                        IchimokoCloudColor = request.IchimokoCloudColor
                    }
                };

                var strategy = new StrategyImplementation(new PriceIchimokoCloudStrategy(), indicatorParameter);
                var symbols = strategy.FindSymbols(indicatorCandel);
            }
        }
    }
}
