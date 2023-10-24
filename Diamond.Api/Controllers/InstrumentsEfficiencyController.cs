using Domain.Models.InstrumentsEfficiency;
using Microsoft.AspNetCore.Mvc;
using Diamond.Services.BusinessService;

namespace Diamond.API.Controllers
{  
    public class InstrumentsEfficiencyController : ApiBaseController
    {
        private readonly CalculateInstrumentsEfficiencyBusinessService _businessService;
        
        public InstrumentsEfficiencyController(CalculateInstrumentsEfficiencyBusinessService businessService)
        {
            _businessService = businessService;
        }


        [HttpGet]
        public async Task<List<InstrumentsEfficiencyResponse>> Get(CancellationToken cancellation)
        {
            return await _businessService.CalculateInstrumentsEfficiency(cancellation);
        }
    }
}
