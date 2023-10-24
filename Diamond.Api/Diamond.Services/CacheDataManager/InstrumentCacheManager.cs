using Diamond.Domain.Entities.TsePublic;
using Diamond.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Services.CacheDataManager
{
    internal class InstrumentCacheManager
    {
        private readonly CacheManager _cacheManager;
        public InstrumentCacheManager(CacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task UpdateInstrument_TseInstrument(List<TseInstrument> list)
        {
            var slidingExpiration = 60;

            var allInstruments = list.Select(x => x.InstrumentId).ToList();

            _cacheManager.Set($"AllInstrumentsList", allInstruments, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

            foreach (var item in list)
            {
                var key = $"Instrument_{item.InstrumentId}";

                _cacheManager.Set(key, item, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
                });

                await UpdateInstrument(key,item.InstrumentId, slidingExpiration, static instrument =>
                {
                    instrument.Date = instrument.Date;
                    instrument.InsCode = instrument.InsCode;
                    instrument.InstrumentMnemonic = instrument.InstrumentMnemonic;
                    instrument.EnglishName = instrument.EnglishName;
                    instrument.FourDigitCompanyCode = instrument.FourDigitCompanyCode;
                    instrument.CompanyName = instrument.CompanyName;
                    instrument.Symbol = instrument.Symbol;
                    instrument.Name = instrument.Name;
                    instrument.CIsin = instrument.CIsin;
                    instrument.InstrumentGroupId = instrument.InstrumentGroupId;
                    instrument.MarketSegment = instrument.MarketSegment;
                    instrument.BoardCode = instrument.BoardCode;
                    instrument.SectorCode = instrument.SectorCode;
                    instrument.SubSectorCode = instrument.SubSectorCode;
                    instrument.Flow = instrument.Flow;
                    instrument.Valid = instrument.Valid;
                });
            }
        }

        public async Task UpdateInstrument(string key, string instrumentId,int slidingExpiration, Action<TseInstrument> callBack)
        {
            //var key = $"Instrument_{instrumentId}";
            if (!_cacheManager.TryGetValue<TseInstrument>(key, out var instrument))
            {
                instrument = new TseInstrument()
                {
                    InstrumentId = instrumentId
                };
            }

            callBack.Invoke(instrument);
            _cacheManager.Set(key, instrument, new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });
        }
        
    }
}
