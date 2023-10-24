using Diamond.Domain.Entities;
using Domain.Models.InstrumentsEfficiency;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class CalculateInstrumentsEfficiencyBusinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        public CalculateInstrumentsEfficiencyBusinessService(DiamondDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InstrumentsEfficiencyResponse>> CalculateInstrumentsEfficiency(CancellationToken cancellation)
        {
            var ins = await _dbContext
            .Set<Instrument>()
            .Select(x => new 
            {
                InstrumentId = x.InstrumentId
            })
            .AsNoTracking()
            .ToListAsync(cancellation);

            var instrumentsEfficiencies = await _dbContext
            .Set<InstrumentsEfficiency>()
            .Select(x => new InstrumentsEfficiencyResponse
            {
                InstrumentId = x.InstrumentId,
                OneMounthDate = x.OneMounthDate,
                ThreeMonthsClosePrice = x.ThreeMonthsClosePrice,
                SixMonthsClosePrice = x.SixMonthsClosePrice,
                AnnualClosePrice = x.AnnualClosePrice,
                ClosePrice = x.ClosePrice,
            })
            .AsNoTracking()
            .ToListAsync(cancellation);

            return instrumentsEfficiencies;

            //var oneToTwelveMonthProfit = new List<InstrumentsEfficiency>();

            //using (var repo = _serviceProvider.GetService<CalculateInstrumentsProfitRepository>())
            //{
            //    var historicalClosePrice = await repo.GetHistoricalClosePrice(cancellationToken);
            //    oneToTwelveMonthProfit = await FindOneTwelveMonth(historicalClosePrice, cancellationToken);

            //    await repo.SaveCoefficientPrice(oneToTwelveMonthProfit);
            //}

        }
    }
}
