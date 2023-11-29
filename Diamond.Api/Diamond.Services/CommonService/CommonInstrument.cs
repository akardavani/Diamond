using Diamond.Domain.Entities.TsePublic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Diamond.Domain.Entities;
using System;

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
                .Where(e => e.InstrumentId.Contains("0001") 
                           && e.SubSectorCode != "6812" 
                           && e.Valid == 1)
                .Select(e => e.InstrumentId)
                .ToListAsync(cancellation);

            return instruments;
        }

        //public async Task<List<TseInstrument>> GetAllInstruments(CancellationToken cancellation)
        //{
        //    var instruments = await _dbContext
        //        .Set<TseInstrument>()
        //        .Where(e => e.InstrumentId.Contains("0001") && e.Valid == 1)
        //        .ToListAsync(cancellation);

        //    return instruments;
        //}

        public async Task<List<Candel>> GetAllCandles(CancellationToken cancellation)
        {
            var instruments = await GetValidInstruments(cancellation);

            var candels = await _dbContext
                .Set<Candel>()
                .Where(e => e.InstrumentId.Contains("0001")
                            && instruments.Contains(e.InstrumentId))
                .ToListAsync(cancellation);

            return candels;
        }

        public async Task<List<string>> GetValidInstruments(CancellationToken cancellation)
        {
            decimal net = 5 * 1000 * 1000 * 1000M; // 5 میلیارد ریال

            var instruments = await _dbContext
                .Set<Candel>()
                .Where(e => e.Date > DateTime.Today.AddYears(-1))
                .GroupBy(e => e.InstrumentId)
                .Where(e => e.Average(e => e.Volume * e.Close) > net)
                .Select(e => e.Key)
                .ToListAsync(cancellation);

            return instruments;
        }
    }
}
