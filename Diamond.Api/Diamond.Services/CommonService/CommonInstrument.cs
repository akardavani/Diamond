using Diamond.Domain.Entities.TsePublic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Diamond.Domain.Entities;

namespace Diamond.Services.CommonService
{
    public class CommonInstrument : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;

        public CommonInstrument(DiamondDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetAllInstrumentIds(CancellationToken cancellation)
        {
            var instruments = await _dbContext
                .Set<TseInstrument>()
                .Where(e => e.InstrumentId.Contains("0001") && e.Valid == 1)
                .Select(e => e.InstrumentId)
                .ToListAsync(cancellation);

            return instruments;
        }

        public async Task<List<TseInstrument>> GetAllInstruments(CancellationToken cancellation)
        {
            var instruments = await _dbContext
                .Set<TseInstrument>()
                .Where(e => e.InstrumentId.Contains("0001") && e.Valid == 1)
                .ToListAsync(cancellation);

            return instruments;
        }

        public async Task<List<Candel>> GetAllCandles(CancellationToken cancellation)
        {
            var candels = await _dbContext
                .Set<Candel>()
                .Where(e => e.InstrumentId.Contains("0001"))
                .ToListAsync(cancellation);

            return candels;
        }
    }
}
