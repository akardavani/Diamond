using Diamond.API.Controllers;
using Diamond.Domain.Models;
using Diamond.Services.BusinessService;
using Diamond.Services.BusinessServiceDto.Candle;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers.Candle
{
    public class CandleController : ApiBaseController
    {
        private readonly FindSymbolBussinessService _findSymbol;

        public CandleController(FindSymbolBussinessService findSymbol)
        {
            _findSymbol = findSymbol;
        }

        [HttpGet]
        public async Task<bool> FindSymbols([FromQuery] CandleRequest request, CancellationToken cancellation)
        {
            var symbols = await _findSymbol.GetCandelData(new CandleDto
            {
                TimeFrame = request.TimeFrame,
                TakeTodayData = request.TakeTodayData
            }, cancellation);

            return symbols;
        }
    }
}
