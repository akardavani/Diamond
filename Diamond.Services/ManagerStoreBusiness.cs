using Diamond.Domain.Entities.TsePublic;
using Diamond.Infrastructure;
using Diamond.Services.CacheDataManager;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services
{
    public class ManagerStoreBusiness : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        //private ImmutableDictionary<string, Instrument> _instruments;
        private readonly CacheManager _cacheManager;
        public DateTime InitializeDateTime { get; set; }

        public ManagerStoreBusiness(DiamondDbContext dbContext, CacheManager cacheManager)
        {
            _dbContext = dbContext;
            _cacheManager = cacheManager;
            //_instruments = ImmutableDictionary<string, Instrument>.Empty;
        }

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Instruments...");
            await FetchInstruments(cancellationToken);  
            InitializeDateTime = DateTime.Now;
        }

        private async Task FetchInstruments(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested == false)
            {
                var instruments = await _dbContext
                                .Set<TseInstrument>()
                                .AsNoTracking()
                                .ToListAsync(cancellation);


                var instrumentCache = new InstrumentCacheManager(_cacheManager);
                await instrumentCache.UpdateInstrument_TseInstrument(instruments);                
            }
        }
    }
}
