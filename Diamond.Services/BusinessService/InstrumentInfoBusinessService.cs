using Diamond.Domain;
using Diamond.Domain.Entities;
using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Enums;
using Diamond.Domain.Models;
using Diamond.Services.BusinessService.Strategy;
using Diamond.Services.CandelClient;
using Diamond.Services.TseTmcClient;
using Diamond.Utils.BrokerExtention;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class InstrumentInfoBusinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        private readonly TseTmcClientService _tseService;
        private readonly CandelClientService _candelService;

        public InstrumentInfoBusinessService(DiamondDbContext dbContext,
            TseTmcClientService tseService,
            CandelClientService candelService)
        {
            _dbContext = dbContext;
            _tseService = tseService;
            _candelService = candelService;
        }
        public async Task GetTseTmcData(CancellationToken cancellation)
        {

            var list = await _tseService.GetInstrumentList(cancellation);

            var instruments = await _dbContext.Set<Instrument>()
                .ToListAsync(cancellationToken: cancellation);

            var instrumentExtraInfos = await _dbContext.Set<InstrumentExtraInfo>()
                .ToListAsync(cancellationToken: cancellation);

            //_dbContext.RemoveRange(instruments);
            //_dbContext.RemoveRange(instrumentExtraInfos);

            //foreach (var entity in instruments)
            //    _dbContext.Remove(entity);

            //foreach (var entity in instrumentExtraInfos)
            //    _dbContext.Remove(entity);

            //await _dbContext.SaveChangesAsync();


            try
            {
                var count = 0;
                foreach (var item in list)
                {
                    var instrumentExtraInfo = await _tseService.GetTseTmcData(item.InsCode, cancellation);

                    var entity = instrumentExtraInfos.FirstOrDefault(x => x.InstrumentId == item.InstrumentId);
                    if (entity == null)
                    {
                        entity = new InstrumentExtraInfo()
                        { 
                            InstrumentId = item.InstrumentId 
                        };
                        _dbContext.Add(entity);
                    }

                    entity.InstrumentId = instrumentExtraInfo.InstrumentId;
                    entity.Eps = instrumentExtraInfo.Eps;
                    entity.PE = instrumentExtraInfo.PE;
                    entity.SectorPE = instrumentExtraInfo.SectorPE;
                    entity.PS = instrumentExtraInfo.PS;
                    entity.Nav = instrumentExtraInfo.Nav;
                    entity.BaseVol = instrumentExtraInfo.BaseVol;
                    entity.AverageMonthVolume = instrumentExtraInfo.AverageMonthVolume;
                    entity.FloatingShares = instrumentExtraInfo.FloatingShares;
                    entity.AllowedPriceMax = instrumentExtraInfo.AllowedPriceMax;
                    entity.AllowedPriceMin = instrumentExtraInfo.AllowedPriceMin;
                    entity.MinWeek = instrumentExtraInfo.MinWeek;
                    entity.MaxWeek = instrumentExtraInfo.MaxWeek;
                    entity.MinYear = instrumentExtraInfo.MinYear;
                    entity.MaxYear = instrumentExtraInfo.MaxYear;

                    var instrumentEntity = instruments.FirstOrDefault(x => x.InstrumentId == item.InstrumentId);
                    if (instrumentEntity == null)
                    {
                        instrumentEntity = new Instrument();                        
                        _dbContext.Add(instrumentEntity);
                    }

                    instrumentEntity.InstrumentId = item.InstrumentId;
                    instrumentEntity.CompanyName = item.InstrumentName;
                    instrumentEntity.InsCode = item.InsCode;
                    instrumentEntity.InstrumentPersianName = item.InstrumentPersianName;
                    instrumentEntity.MarketName = instrumentExtraInfo.MarketName;
                    instrumentEntity.IndustryGroupCode = item.InstrumentGroupId;
                    instrumentEntity.IndustryGroup = item.InstrumentSectorGroup;
                    instrumentEntity.Board = item.Board;
                    instrumentEntity.Title = instrumentExtraInfo.Title;
                    instrumentEntity.IsDisabled = false;
                    instrumentEntity.UpdateDateTime = DateTime.Now;

                    if (count % 50 == 0)
                    {
                        Thread.Sleep(2000);
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {

            }

            var response = await _dbContext.SaveChangesAsync(cancellation);
        }

        public async Task GetCandelData(CancellationToken cancellation)
        {
            var timeframe = TimeframeEnum.Daily;
            await GetAllCandelData(timeframe, cancellation);
        }

        public async Task GetAllCandelData(TimeframeEnum timeFram, CancellationToken cancellation)
        {
            var tseInstruments = await _dbContext
                .Set<TseInstrument>()
                //.Where(e=>e.InstrumentId == "IRO1TSBE0001")
                .Where(e => e.MarketSegment == "No" 
                && !e.InstrumentId.Contains("0101"))                     
                .ToListAsync(cancellationToken: cancellation);
            
            var candels = await _dbContext
               .Set<Candel>()
               .ToListAsync(cancellation);

            var candelsLastTimestamp = candels.GroupBy(c => new
            {
                c.InstrumentId,
                c.TimeFrame,
            }).Select(g => new
            {
                InstrumentId = g.Key.InstrumentId,
                TimeFrame = g.Key.TimeFrame,
                LastTimestamp = g.Max(e => e.Timestamp)
            });

            var from = BrokerExtention.DateTimeToUnixTimestamp(DateTime.Now.AddYears(-15));

            foreach (var instrument in tseInstruments)
            {
                var lastTimestamp = long.Parse(from);
                var can = candelsLastTimestamp
                    .Where(e => e.InstrumentId == instrument.InstrumentId && e.TimeFrame == timeFram).FirstOrDefault();

                if (can is not null)                
                   lastTimestamp = can.LastTimestamp; 

                var candelModel = await _candelService.GetDataByUrl(instrument.InstrumentId, timeFram);

                foreach (var candel in candelModel.Candels.Where(e=>e.Timestamp > lastTimestamp))
                {
                    var entity = new Candel
                    {
                        Close = candel.Close,
                        Date = candel.Date,
                        High = candel.High,
                        Low = candel.Low,
                        Open = candel.Open,
                        Volume = candel.Volume,
                        NetValue = candel.NetValue,
                        Timestamp = candel.Timestamp,
                        InstrumentId = instrument.InstrumentId,
                        TimeFrame = timeFram,
                    };
                    _dbContext.Add(entity);
                }
            }

            var response = await _dbContext.SaveChangesAsync(cancellation);
        }
    }
}
