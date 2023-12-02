using Diamond.Domain.Indicator;
using Diamond.Services.BusinessServiceDto.Strategy;
using Diamond.Services.CommonService;
using Diamond.Services.Strategy;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class SupportResistanceBussinessService : IBusinessService
    {
        private readonly CommonInstrument _instrument;

        public SupportResistanceBussinessService(CommonInstrument instrument)
        {
            _instrument = instrument;
        }

        public async Task Find(SupportResistanceDto request, CancellationToken cancellation)
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

            SupportResistances(request, indicatorCandel);
        }

        private void SupportResistances(SupportResistanceDto request, List<IndicatorCandel> indicatorCandel)
        {
            var indicatorParameter = new IndicatorParameter
            {
                SupportResistancesParameter = new SupportResistanceIndicatorParameter
                {
                    FromDate = request.FromDate,
                    ToDate = request.ToDate,
                    CandelPriceType = request.CandelPriceType,
                    PercentageDifference = request.PercentageDifference,
                    SupportResistanceType = request.SupportResistanceType
                }
            };

            var strategy = new StrategyImplementation(new SupportResistanceStrategy(), indicatorParameter);
            var symbols = strategy.FindSymbols(indicatorCandel);
        }
    }
}
