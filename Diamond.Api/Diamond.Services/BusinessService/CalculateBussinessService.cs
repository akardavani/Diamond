using Diamond.Domain.Models.TseInstrument;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class CalculateBussinessService : IBusinessService
    {
        private readonly TseInstrumentBussinessService _instrumentBussiness;
        private readonly TseTradeBussinessService _tradeBussiness;
        public CalculateBussinessService(TseInstrumentBussinessService instrumentBussiness,
            TseTradeBussinessService tradeBussiness)
        {
            _instrumentBussiness = instrumentBussiness;
            _tradeBussiness = tradeBussiness;
        }
        public async Task GetAllDataAsync(GetAllTseInstrumentRequest request, CancellationToken cancellation)
        {            
            var instruments = await _instrumentBussiness.GetAllInstrumentsAsync(cancellation);
            var trades = await _tradeBussiness.GetAllTradeLastDayAsync(cancellation);

            if (request.Valid)
            {
                instruments = instruments
                        .Where(x => x.Valid == 1)
                        .Where(x => x.MarketSegment == "NO").ToList();

                //لیست ترید ها 
            }

            if (request.InstrumentId is not null)
            {
                var ins = instruments.Where(x=>x.InstrumentId == request.InstrumentId).FirstOrDefault();
                var trade = trades.Where(x => x.InsCode == ins.InsCode).ToList();
            }

            var ss = 0;            
        }

        public async Task SaveAllNeededData(CancellationToken cancellation)
        {
            await _tradeBussiness.SaveTseLastDayTradesAsync(cancellation);
        }
    }
}
