using Diamond.Domain.Entities;
using Diamond.Domain.Enums;
using Diamond.Services.CandelClient;
using Diamond.Services.TseTmcClient;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
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

            var list = await _candelService.GetDataByUrl("IRO1IKCO00013", TimeframeEnum.Daily);

        }
    }
}
