using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Enums;
using Diamond.Domain.Setting;
using Diamond.Infrastructure;
using Diamond.Services.CacheDataManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TsePublicService;
using static TsePublicService.TsePublicV2SoapClient;

namespace Diamond.Services.BusinessService
{
    public class TseTradeBussinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        private readonly TseTmcWebServiceSettings _settings;
        private readonly CacheManager _cacheManager;

        public TseTradeBussinessService(DiamondDbContext dbContext,
            IOptions<Settings> settings,
            CacheManager cacheManager)
        {
            _dbContext = dbContext;
            _settings = settings.Value.TseTmcWebServiceSettings;
            _cacheManager = cacheManager;

            if (_settings != null)
            {
                if (string.IsNullOrWhiteSpace(_settings.Url))
                {
                    //_logger.LogError("Base url is empty");
                    _settings = null;
                }
            }
            else
            {
                //_logger.LogError("Can not read ");
            }
        }

        public async Task<List<TseTrade>> GetAllTradeLastDayAsync(CancellationToken cancellation)
        {
            var tradeCache = new TradeCacheManager(_cacheManager);
            var trades = await tradeCache.GetAllTradeLastDay();
            
            return trades;
        }

        // قیمت های روز سهام
        public async Task SaveTseLastDayTradesAsync(CancellationToken cancellation)
        {
            var elementParameter = "TradeLastDayAll";
            var client = new TsePublicV2SoapClient(EndpointConfiguration.TsePublicV2Soap12);

            var root = new List<XElement>();

            var tseTrades = await client.TradeLastDayAllAsync(_settings.User, _settings.Password, (int)TseFlowMarket.Bourse);
            var otcTrades = await client.TradeLastDayAllAsync(_settings.User, _settings.Password, (int)TseFlowMarket.FaraBourse);
            var otcTrades_base = await client.TradeLastDayAllAsync(_settings.User, _settings.Password, (int)TseFlowMarket.OTCBase);

            if (tseTrades != null
                && otcTrades != null
                && otcTrades_base != null)
            {
                var tse = tseTrades.Nodes[1].Elements(elementParameter).Elements(elementParameter).ToList();
                var otc = otcTrades.Nodes[1].Elements(elementParameter).Elements(elementParameter).ToList();
                var baseotc = otcTrades_base.Nodes[1].Elements(elementParameter).Elements(elementParameter).ToList();

                root.AddRange(tse);
                root.AddRange(otc);
                root.AddRange(baseotc);

                List<TseTrade> list = root.Select(ins => new TseTrade()
                {
                    InsCode = (long)ins.Element("InsCode"),
                    Date = (string)ins.Element("DEven"),
                    Name = (string)ins.Element("LVal30"),
                    Symbol = (string)ins.Element("LVal18AFC"),
                    FirstPrice = (decimal)ins.Element("PriceFirst"),
                    ClosingPrice = (decimal)ins.Element("PClosing"),
                    LastTrade = (decimal)ins.Element("PDrCotVal"),
                    NumberOfTransactions = (string)ins.Element("ZTotTran"),
                    NumberOfSharesIssued = (string)ins.Element("QTotTran5J"),
                    TransactionValue = (decimal)ins.Element("QTotCap"),
                    ChangePrice = (decimal)ins.Element("PriceChange"),
                    MinPrice = (decimal)ins.Element("PriceMin"),
                    MaxPrice = (decimal)ins.Element("PriceMax"),
                    YesterdayPrice = (decimal)ins.Element("PriceYesterday"),
                    LastState = (string)ins.Element("Last"),
                    HEven = (string)ins.Element("HEven")
                }).ToList();

                var tradeCache = new TradeCacheManager(_cacheManager);
                await tradeCache.UpdateTrade_TseTrade(list);

                await SaveTradeLastDay(list, cancellation);
            }
        }

        private async Task SaveTradeLastDay(List<TseTrade> list, CancellationToken cancellation)
        {
            var tradeLastDay = await _dbContext.Set<TseTrade>()
                .ToListAsync(cancellationToken: cancellation);

            foreach (var item in list)
            {
                var entity = tradeLastDay.FirstOrDefault(e => e.InsCode == item.InsCode);
                if (entity == null)
                {
                    entity = new TseTrade()
                    {
                        InsCode = item.InsCode
                    };
                    _dbContext.Add(entity);
                }

                entity.InsCode = item.InsCode;
                entity.ChangePrice = item.ChangePrice;
                entity.ClosingPrice = item.ClosingPrice;
                entity.Date = item.Date;
                entity.Name = item.Name;
                entity.Symbol = item.Symbol;
                entity.FirstPrice = item.FirstPrice;
                entity.HEven = item.HEven;
                entity.LastState = item.LastState;
                entity.LastTrade = item.LastTrade;
                entity.MaxPrice = item.MaxPrice;
                entity.MinPrice = item.MinPrice;
                entity.NumberOfSharesIssued = item.NumberOfSharesIssued;
                entity.NumberOfTransactions = item.NumberOfTransactions;
                entity.TransactionValue = item.TransactionValue;
                entity.YesterdayPrice = item.YesterdayPrice;
            }

            var response = await _dbContext.SaveChangesAsync(cancellation);
        }



        //public async Task GetTradeOneDayAsync(CancellationToken cancellation)
        //{
        //    var client = new TsePublicV2SoapClient(EndpointConfiguration.TsePublicV2Soap12);

        //    var trade = await client.TradeOneDayAllAsync(_settings.User, _settings.Password, 20220912, 1);

        //    if (trade != null)
        //    {
        //        var root = trade.Nodes[1].Elements("TradeSelectedDateAll").Elements("TradeSelectedDateAll").ToList();

        //        List<TseShareChange> list = root.Select(ins => new TseShareChange()
        //        {
        //            InsCode = (string)ins.Element("InsCode"),
        //            Date = (string)ins.Element("DEven"),
        //            NumberOfShareOld = (long)ins.Element("NumberOfShareOld"),
        //            NumberOfShareNew = (long)ins.Element("NumberOfShareNew"),
        //            Name = (string)ins.Element("LVal30"),
        //            Symbol = (string)ins.Element("LVal18AFC"),
        //        }).ToList();
        //    }

        //}

    }
}
