using Diamond.API.Controllers;
using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Models.TseInstrument;
using Diamond.Services.BusinessService;
using Microsoft.AspNetCore.Mvc;

namespace Diamond.Api.Controllers
{
    public class TsePublicController : ApiBaseController
    {
        private readonly TseInstrumentBussinessService _businessService;
        private readonly TseTradeBussinessService _tradebusinessService;
        private readonly TseSubSectorBussinessService _subSectorbusinessService;
        private readonly TseCustomerTypeBussinessService _customerbusinessService;
        private readonly CalculateBussinessService _calculateBussinessService;

        public TsePublicController(TseInstrumentBussinessService businessService,
            TseCustomerTypeBussinessService customerbusinessService,
            TseSubSectorBussinessService subSectorbusinessService,
            TseTradeBussinessService tradebusinessService,
            CalculateBussinessService calculateBussinessService)
        {
            _businessService = businessService;
            _customerbusinessService = customerbusinessService;
            _tradebusinessService = tradebusinessService;
            _subSectorbusinessService = subSectorbusinessService;
            _calculateBussinessService = calculateBussinessService;
        }


        [HttpGet]
        public async Task GetAllData([FromQuery]GetAllTseInstrumentRequest request, CancellationToken cancellation)
        {
            await _calculateBussinessService.GetAllDataAsync(request,cancellation);
        }


        [HttpGet]
        public async Task SaveTseInstrumentAsync(CancellationToken cancellation)
        {
            await _businessService.SaveTseInstrumentAsync(cancellation);
        }

        [HttpGet]
        public async Task SaveTseSubSectorAsync(CancellationToken cancellation)
        {
            await _subSectorbusinessService.SaveTseSubSectorAsync(cancellation);
        }

        [HttpGet]
        public async Task<List<TseInstrument>> GetAllInstruments(CancellationToken cancellation)
        {
            var instruments = await _businessService.GetAllInstrumentsAsync(cancellation);
            return instruments;
        }

        [HttpGet]
        public async Task SaveTseCustomerType(CancellationToken cancellation)
        {
            await _customerbusinessService.SaveTseCustomerTypeCurrentDateAsync(cancellation);
        }
       
        [HttpGet]
        public async Task SaveTseLastDayTrades(CancellationToken cancellation)
        {
            await _tradebusinessService.SaveTseLastDayTradesAsync(cancellation);
        }

        

        //[HttpGet]
        //public async Task GetShareChangeAsync(CancellationToken cancellation)
        //{
        //    await _businessService.GetShareChangeAsync(cancellation);
        //}

    }
}
