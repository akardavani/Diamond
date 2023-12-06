using System;
using System.Threading;
using System.Threading.Tasks;
using Diamond.Domain.Enums;
using Diamond.Services.BusinessServiceDto.Candle;
using Diamond.Services.CommonService;
using Diamond.Utils;

namespace Diamond.Services.BusinessService
{
    public class FindSymbolBussinessService : IBusinessService
    {
        private readonly InstrumentInfoBusinessService _instrumentInfoBusinessService;
        private readonly TseInstrumentBussinessService _tseInstrument;
        private readonly CommonInstrument _instrument;

        public FindSymbolBussinessService(InstrumentInfoBusinessService instrumentInfoBusinessService,
            TseInstrumentBussinessService tseInstrument,
            CommonInstrument instrument)
        {
            _instrumentInfoBusinessService = instrumentInfoBusinessService;
            _tseInstrument = tseInstrument;
            _instrument = instrument;
        }

        public async Task<bool> GetCandelData(CandleDto request, CancellationToken cancellation)
        {
            var response = await TakingSymbolsAndCandles(request.TimeFrame, request.TakeTodayData, cancellation);           

            return response;
        }



        //private void SupportResistances(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
        //{
        //    if (request.PivotPointsStrategy)
        //    {
        //        var indicatorParameter = new IndicatorParameter
        //        {
        //            PivotPointsParameter = new SupportResistanceIndicatorParameter
        //            {
        //                FromDate = request.FromDate,
        //                ToDate = request.ToDate,
        //                CandelPriceType = request.CandelPriceType
        //            }
        //        };

        //        var ss = _supportResistanceBusiness.Calculate(indicatorParameter, indicatorCandel);
        //    }
        //}

        private async Task<bool> TakingSymbolsAndCandles(TimeframeEnum timeframe, bool getTodayData, CancellationToken cancellation)
        {
            var response = false;
            // تعداد تکرارها و زمان تأخیر بین تکرارها
            int retryCount = 3;
            TimeSpan delayBetweenRetries = TimeSpan.FromSeconds(1);

            await Helper.RetryAsync(async () => await _tseInstrument.SaveTseInstrumentAsync(cancellation), retryCount, delayBetweenRetries);
            response = await _tseInstrument.SaveTseInstrumentAsync(cancellation);
            if (response)
            {
                response = await _instrumentInfoBusinessService.GetCandlesInformation(timeframe, getTodayData, cancellation);
            }

            return response;
        }

        //private void SupportResistances(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
        //{
        //    if (request.SupportResistancesStrategy)
        //    {
        //        var indicatorParameter = new IndicatorParameter
        //        {
        //            SupportResistancesParameter = new SupportResistanceIndicatorParameter
        //            {
        //                FromDate = request.FromDate,
        //                ToDate = request.ToDate,
        //                CandelPriceType = request.CandelPriceType,
        //                PercentageDifference = request.PercentageDifference,
        //                SupportResistanceType = request.SupportResistanceType
        //            }
        //        };

        //        var strategy = new StrategyImplementation(new SupportResistanceStrategy(), indicatorParameter);
        //        var symbols = strategy.FindSymbols(indicatorCandel);
        //    }
        //}

        //private void CrossIchimoko(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
        //{
        //    if (request.CrossIchimoko)
        //    {
        //        var indicatorParameter = new IndicatorParameter
        //        {
        //            CrossIchimokoParameter = new CrossIchimokoIndicatorParameter
        //            {
        //                FromDate = request.FromDate,
        //                ToDate = request.ToDate,
        //                Trend = request.Trend
        //            }
        //        };

        //        var strategy = new StrategyImplementation(new CrossIchimokoStrategy(), indicatorParameter);
        //        var symbols = strategy.FindSymbols(indicatorCandel);
        //    }
        //}

        //private void PriceIchimokoCloud(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
        //{
        //    if (request.PriceAboveBelow is not null)
        //    {
        //        var indicatorParameter = new IndicatorParameter
        //        {
        //            PriceIchimokoCloudParameter = new PriceIchimokoCloudIndicatorParameter
        //            {
        //                FromDate = request.FromDate,
        //                ToDate = request.ToDate,
        //                CandelPriceType = request.CandelPriceType,
        //                ComparisonPriceType = request.ComparisonPriceType,
        //                IchimokoCloudColor = request.IchimokoCloudColor
        //            }
        //        };

        //        var strategy = new StrategyImplementation(new PriceIchimokoCloudStrategy(), indicatorParameter);
        //        var symbols = strategy.FindSymbols(indicatorCandel);
        //    }
        //}
    }
}
