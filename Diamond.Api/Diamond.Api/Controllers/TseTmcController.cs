﻿using Diamond.Services.BusinessService;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.API.Controllers
{
    public class TseTmcController : ApiBaseController
    {
        private readonly InstrumentInfoBusinessService _businessService;

        public TseTmcController(InstrumentInfoBusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpGet]
        public async Task Get(CancellationToken cancellation)
        {
            await _businessService.GetTseTmcData(cancellation);
        }

        //[HttpGet]
        //public async Task GetCandelData(CancellationToken cancellation)
        //{
        //    await _businessService.GetCandlesInformation(cancellation);
        //}

        //[HttpGet]
        //public async Task<List<IndicatorCandel>> FindSymbols([FromQuery] FindSymbolRequest request, CancellationToken cancellation)
        //{
        //    var symbols = await _findSymbol.GetCandelData(new FindSymbolDto
        //    {
        //        TakingSymbolsAndCandles = request.TakingSymbolsAndCandles,
        //        CrossIchimoko = request.CrossIchimokoStrategy,
        //        SupportResistancesStrategy = request.SupportResistancesStrategy,
        //        FromDate = new DateOnly(request.FromDate.Year, request.FromDate.Month, request.FromDate.Day),
        //        ToDate = new DateOnly(request.ToDate.Year, request.ToDate.Month, request.ToDate.Day),
        //        Trend = request.Trend,
        //        CandelPriceType = request.CandelPriceType,
        //        ComparisonPriceType = request.ComparisonPriceType,
        //        PriceAboveBelow = request.PriceAboveBelow,
        //        IchimokoCloudColor = request.IchimokoCloudColor,
        //        SupportResistanceType = request.SupportResistanceType,
        //        PercentageDifference = request.PercentageDifference,
        //    }, cancellation);

        //    return symbols;
        //}
    }
}
