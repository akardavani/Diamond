using Diamond.API.Controllers;
using Diamond.Services.BusinessServiceDto.Strategy;
using Microsoft.AspNetCore.Mvc;
using Diamond.Services.BusinessService.Strategy;
using Diamond.Domain.Models;

namespace Diamond.Api.Controllers.Strategy
{
    public class IchimokoStrategyController : ApiBaseController
    {
        private readonly IchimokoBussinessService _bussinessService;

        public IchimokoStrategyController(IchimokoBussinessService bussinessService)
        {
            _bussinessService = bussinessService;
        }

        [HttpGet]
        public async Task FindSymbols([FromQuery] IchimokoRequest request, CancellationToken cancellation)
        {
            await _bussinessService.Find(new IchimokoDto
            {
                CrossIchimoko = request.CrossIchimokoStrategy,
                FromDate = new DateOnly(request.FromDate.Year, request.FromDate.Month, request.FromDate.Day),
                ToDate = new DateOnly(request.ToDate.Year, request.ToDate.Month, request.ToDate.Day),
                Trend = request.Trend,
                CandelPriceType = request.CandelPriceType,
                ComparisonPriceType = request.ComparisonPriceType,
                PriceAboveBelow = request.PriceAboveBelow,
                IchimokoCloudColor = request.IchimokoCloudColor,
            }, cancellation);

        }
    }
}
