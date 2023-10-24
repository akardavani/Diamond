using Diamond.Services.BusinessService;
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

        [HttpGet]
        public async Task GetCandelData(CancellationToken cancellation)
        {
            await _businessService.GetCandelData(cancellation);
        }

    }
}
