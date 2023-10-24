using Diamond.Domain.Entities.TsePublic;
using Diamond.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diamond.Services.CacheDataManager
{
    internal class TradeCacheManager
    {
        private readonly CacheManager _cacheManager;
        private readonly string tradeLastDayAll_key = $"AllTrades";
        public TradeCacheManager(CacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task UpdateTrade_TseTrade(List<TseTrade> list)
        {
            var slidingExpiration = 60;

            var allTrades = list.Select(x => x.InsCode).ToList();

            _cacheManager.Set(tradeLastDayAll_key, allTrades, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
            });

            foreach (var item in list)
            {
                var key = $"Trade_{item.InsCode}";

                _cacheManager.Set(key, item, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
                });

                await UpdateTrade(key, item.InsCode, slidingExpiration, static trade =>
                {
                    trade.InsCode = trade.InsCode;
                    trade.ChangePrice = trade.ChangePrice;
                    trade.ClosingPrice = trade.ClosingPrice;
                    trade.Date = trade.Date;
                    trade.Name = trade.Name;
                    trade.Symbol = trade.Symbol;
                    trade.FirstPrice = trade.FirstPrice;
                    trade.HEven = trade.HEven;
                    trade.LastState = trade.LastState;
                    trade.LastTrade = trade.LastTrade;
                    trade.MaxPrice = trade.MaxPrice;
                    trade.MinPrice = trade.MinPrice;
                    trade.NumberOfSharesIssued = trade.NumberOfSharesIssued;
                    trade.NumberOfTransactions = trade.NumberOfTransactions;
                    trade.TransactionValue = trade.TransactionValue;
                    trade.YesterdayPrice = trade.YesterdayPrice;
                });
            }
        }

        private async Task UpdateTrade(string key, long insCode, int slidingExpiration, Action<TseTrade> callBack)
        {
            //var key = $"Instrument_{instrumentId}";
            if (!_cacheManager.TryGetValue<TseTrade>(key, out var instrument))
            {
                instrument = new TseTrade()
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

        public async Task<List<TseTrade>> GetAllTradeLastDay()
        {
            //var tradeLastDayAll_key = $"AllTrades";
            var obj = _cacheManager.Get(tradeLastDayAll_key);

            List<long> list = obj as List<long>;
            List<TseTrade> instruments = new List<TseTrade>();

            if (list is not null)
            {
                foreach (var item in list)
                {
                    var key = $"Trade_{item}";
                    var instrument_obj = _cacheManager.Get(key);

                    TseTrade instrument = instrument_obj as TseTrade;
                    instruments.Add(instrument);
                }
            }            

            return instruments;
        }

    }
}
