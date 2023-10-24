using Diamond.Services.BusinessService;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Admin.Api.Controllers
{
    public class InstrumentController : AdminApiBaseController
    {
        private readonly InstrumentBusinessService _instrumentBusinessService;

        public InstrumentController(InstrumentBusinessService instrumentBusinessService)
        {
            _instrumentBusinessService = instrumentBusinessService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var result =await _instrumentBusinessService.GetAllInstruments();
            return Ok(result);
        }
    }
}
