using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diamond.Domain.Indicator;
using Diamond.Services.BusinessServiceDto.TseTmcControllerDtos;
using Diamond.Services.CommonService;
using Diamond.Services.Strategy;

namespace Diamond.Services.BusinessService
{
    public class FindSymbolBussinessService : IBusinessService
    {
        private readonly InstrumentInfoBusinessService _instrumentInfoBusinessService;
        //private readonly SupportResistanceBusinessService _supportResistanceBusiness;
        private readonly TseInstrumentBussinessService _tseInstrument;
        private readonly CommonInstrument _instrument;

        public FindSymbolBussinessService(InstrumentInfoBusinessService instrumentInfoBusinessService,
            //  SupportResistanceBusinessService supportResistanceBusiness,
            TseInstrumentBussinessService tseInstrument,
            CommonInstrument instrument)
        {
            _instrumentInfoBusinessService = instrumentInfoBusinessService;
            //_supportResistanceBusiness = supportResistanceBusiness;
            _tseInstrument = tseInstrument;
            _instrument = instrument;
        }

        public async Task<List<IndicatorCandel>> GetCandelData(FindSymbolDto request, CancellationToken cancellation)
        {
            var response = true;
            var symbols = new List<IndicatorCandel>();

            if (request.TakingSymbolsAndCandles)
            {
                response = await _tseInstrument.SaveTseInstrumentAsync(cancellation);
                if (response)
                {
                    response = await _instrumentInfoBusinessService.GetCandlesInformation(cancellation);
                }
            }

            if (response)
            {
                var instruments = await _instrument.GetAllCandles(cancellation);

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

                CrossIchimoko(request, indicatorCandel);

                PriceIchimokoCloud(request, indicatorCandel);

                //if (request.CrossIchimoko)
                //{
                //    var indicatorParameter = new IndicatorParameter
                //    {
                //        CrossIchimokoParameter = new CrossIchimokoIndicatorParameter
                //        {
                //            FromDate = request.FromDate,
                //            ToDate = request.ToDate,
                //            Trend = request.Trend
                //        }
                //    };

                //    var strategy = new StrategyImplementation(new CrossIchimokoStrategy(), indicatorParameter);
                //    symbols = strategy.FindSymbols(indicatorCandel);
                //}

                //if (request.PriceAboveBelow is not null)
                //{
                //    var indicatorParameter = new IndicatorParameter
                //    {
                //        FromDate = request.FromDate,
                //        ToDate = request.ToDate,
                //        CandelPriceType = request.CandelPriceType,
                //        ComparisonPriceType = request.ComparisonPriceType,
                //        IchimokoCloudColor = request.IchimokoCloudColor
                //    };

                //    var strategy = new StrategyImplementation(new PriceIchimokoCloudStrategy(), indicatorParameter);
                //    symbols = strategy.FindSymbols(indicatorCandel);
                //}
            }


            return symbols;
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


        private void SupportResistances(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
        {
            if (request.SupportResistancesStrategy)
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

        private void CrossIchimoko(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
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

        private void PriceIchimokoCloud(FindSymbolDto request, List<IndicatorCandel> indicatorCandel)
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
