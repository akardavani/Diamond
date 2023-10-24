using Diamond.Domain.Entities;
using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Enums;
using Diamond.Services.CandelClient;
using Diamond.Services.Model;
using Diamond.Services.TseTmcClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Persistence.Context;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService.Strategy
{
    public class GoldHandler : ITradingStrategyHandler<GoldCommand>
    {
        private readonly DiamondDbContext _dbContext;
        private readonly TseTmcClientService _tseService;
        private readonly CandelClientService _candelService;
        private List<string> candelInstruments = new List<string>();
        public GoldHandler(DiamondDbContext dbContext,
            TseTmcClientService tseService,
            CandelClientService candelService)
        {
            _dbContext = dbContext;
            _tseService = tseService;
            _candelService = candelService;
        }

        public bool ValidationStrategy()
        {
            throw new NotImplementedException();
        }

        public async Task TradingSignal(GoldCommand StrategyName, CancellationToken cancellation)
        {
            var candels = _dbContext
               .Set<Candel>()
               .Select(x => new IchimokuCandelModel(x))
               .ToList();

            await FindInstrument(candels, cancellation);

            GetIchimokuCandels(candels);

            FindSignal(candels);
        }

        //public async Task GetAllCandelData(CancellationToken cancellation)
        //{
        //    var candels = _dbContext
        //       .Set<Candel>()
        //       .Select(x => new IchimokuCandelModel(x))
        //       .ToList();

        //    await FindInstrument(candels, cancellation);

        //    GetIchimokuCandels(candels);

        //    FindSignal(candels);
        //}


        public async Task FindInstrument(List<IchimokuCandelModel> candels,CancellationToken cancellation)
        {
           var tseInstruments = _dbContext
               .Set<TseInstrument>()
               .ToList();

            var removeInstrument = tseInstruments
               .Where(e => e.InstrumentId.Contains("IRO6MEL")    // تملی
                || e.InstrumentId.Contains("IRO3MSZ")            // تسه        
                || e.InstrumentGroupId.Equals("H1")              // صندوق درامد ثابت
                || e.InstrumentGroupId.Equals("P1"))             // بازار پایه
                .ToList();

            candelInstruments = candels
                .GroupBy(e => e.InstrumentId)
                .Select(e => new
                {
                    Count = e.Count(),
                    InstrumentId = e.Key
                })
                .Where(x => x.Count > 52)
                .Select(e => e.InstrumentId)
                .ToList();

            var removeList = new List<string>();
            foreach (var item in candelInstruments)
            {
                var find = removeInstrument.Any(e=>e.InstrumentId.Equals(item));
                if (find)
                {
                    removeList.Add(item);
                }
            }

            // حذف نماد های بالا از لیست
            candelInstruments.RemoveAll(r => removeList.Any(a => a == r));
        }

        public List<IchimokuCandelModel> GetIchimokuCandels(List<IchimokuCandelModel> candels)
        {
            var count = 13;
            decimal? chikou = null;

            List<IchimokuResult> Ichimokus = new List<IchimokuResult>();
            var countt = 0;
            foreach (var instrument in candelInstruments)
            {
                countt++;
                var stockCandels = candels.Where(e => e.InstrumentId.Equals(instrument)).ToList();

                if (stockCandels.Count < 52)
                    continue;

                Ichimokus = stockCandels.GetIchimoku(9, 26, 52, 1).ToList();

                foreach (var candel in stockCandels)
                {
                    var ichimoku = Ichimokus.Find(e => e.Date == candel.Date);

                    candel.TenkanSen = ichimoku.TenkanSen;
                    candel.KijunSen = ichimoku.KijunSen;
                    candel.SenkouSpanA = ichimoku.SenkouSpanA;
                    candel.SenkouSpanB = ichimoku.SenkouSpanB;
                    candel.ChikouSpan = ichimoku.ChikouSpan;
                }
            }

            return candels;
        }

        public void FindSignal(List<IchimokuCandelModel> candels)
        {
            var signal = new List<IchimokuCandelModel>();
            foreach (var item in candelInstruments)
            {
                var candel = candels.Where(e => e.InstrumentId == item).ToList();
                var newCandel = GoldenStrategy(candel);
                if (newCandel is not null)
                {
                    signal.Add(newCandel);
                }
            }

            var sss = signal
                .Where(e => e.Date > new DateTime(2023, 01, 01))
                .ToList();
            var ss = "";
        }

        public IchimokuCandelModel GoldenStrategy(List<IchimokuCandelModel> candels)
        {
            var count = 13;

            // قیمت بالای ابر 
            // کندل سبز
            // کراس تنکن و کیجن

            var candel = new List<IchimokuCandelModel>();
            for (int i = count; i < candels.Count; i++)
            {
                if (candels[i - 1].TenkanSen < candels[i - 1].KijunSen
                    && candels[i].TenkanSen > candels[i].KijunSen       // Cross TenkanSen - KijunSen
                    && candels[i].Close > candels[i].Open)              // Green Candel
                {
                    if (candels[i].Close > candels[i].SenkouSpanB && candels[i].Close > candels[i].SenkouSpanA)    // قیمت بالای ابر                             
                    {
                        candel.Add(candels[i]);
                    }
                }
            }

            return candel
                .OrderByDescending(e=>e.Date) // آخرین signal
                .FirstOrDefault();
        }
        
    }
}
