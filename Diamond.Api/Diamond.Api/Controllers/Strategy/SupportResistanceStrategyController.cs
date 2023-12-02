using Diamond.API.Controllers;
using Diamond.Domain.Models;
using Diamond.Services.BusinessService;
using Diamond.Services.BusinessServiceDto.Strategy;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers
{
    public class SupportResistanceStrategyController : ApiBaseController
    {
        private readonly SupportResistanceBussinessService _bussinessService;

        public SupportResistanceStrategyController(SupportResistanceBussinessService bussinessService)
        {
            _bussinessService = bussinessService;
        }

        [HttpGet]
        public async Task FindSymbols([FromQuery] SupportResistanceRequest request, CancellationToken cancellation)
        {
            await _bussinessService.Find(new SupportResistanceDto
            {
                FromDate = new DateOnly(request.FromDate.Year, request.FromDate.Month, request.FromDate.Day),
                ToDate = new DateOnly(request.ToDate.Year, request.ToDate.Month, request.ToDate.Day),
                CandelPriceType = request.CandelPriceType,
                Timeframe = request.Timeframe,
                SupportResistanceType = request.SupportResistanceType,
                PercentageDifference = request.PercentageDifference,
            }, cancellation);

        }
    }
}
