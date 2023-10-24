using Diamond.Domain.Entities.TsePublic;
using Diamond.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamond.Services.CacheDataManager
{
    internal class CustomerTypeCacheManager
    {
        private readonly CacheManager _cacheManager;
        public CustomerTypeCacheManager(CacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task UpdateTrade_TseTrade(List<TseClientType> list)
        {
            var slidingExpiration = 60;

            var allTrades = list.Select(x => x.InsCode).ToList();

            _cacheManager.Set($"AllCustomerType", allTrades, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

            foreach (var item in list)
            {
                var key = $"CustomerType_{item.InsCode}";

                _cacheManager.Set(key, item, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
                });

                await UpdateCustomerType(key, item.InsCode, slidingExpiration, static customerType =>
                {
                    customerType.InsCode = customerType.InsCode;
                    customerType.Buy_I_Count = customerType.Buy_I_Count;
                    customerType.Buy_I_Volume = customerType.Buy_I_Volume;
                    customerType.Buy_N_Count = customerType.Buy_N_Count;
                    customerType.Buy_N_Volume = customerType.Buy_N_Volume;
                    customerType.Sell_I_Count = customerType.Sell_I_Count;
                    customerType.Sell_I_Volume = customerType.Sell_I_Volume;
                    customerType.Sell_N_Count = customerType.Sell_N_Count;
                    customerType.Sell_N_Volume = customerType.Sell_N_Volume;
                });
            }
        }

        private async Task UpdateCustomerType(string key, string insCode, int slidingExpiration, Action<TseClientType> callBack)
        {
            //var key = $"Instrument_{instrumentId}";
            if (!_cacheManager.TryGetValue<TseClientType>(key, out var instrument))
            {
                instrument = new TseClientType()
                {
                    InsCode = insCode
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
