﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Diamond.Domain.Enums;
using Diamond.Domain.Indicator;
using Diamond.Services.BusinessServiceDto.TseTmcControllerDtos;
using Diamond.Services.CommonService;
using Diamond.Services.Strategy;

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

                if (request.CrossIchimoko)
                {
                    var indicatorParameter = new IndicatorParameter
                    {
                        FromDate = request.FromDate,
                        ToDate = request.ToDate,
                        Trend = request.Trend
                    };

                    var strategy = new StrategyImplementation(new CrossIchimokoStrategy(), indicatorParameter);
                    symbols = strategy.FindSymbols(indicatorCandel);
                }

                if (request.PriceAboveBelow is not null)
                {
                    var indicatorParameter = new IndicatorParameter
                    {
                        FromDate = request.FromDate,
                        ToDate = request.ToDate,
                        CandelPriceType = request.CandelPriceType,
                        ComparisonPriceType = request.ComparisonPriceType,
                        IchimokoCloudColor = request.IchimokoCloudColor                        
                    };

                    var strategy = new StrategyImplementation(new AboveBelowPriceIchimokoCloudStrategy(), indicatorParameter);
                    symbols = strategy.FindSymbols(indicatorCandel);
                }
            }


            return symbols;
        }
    }
}