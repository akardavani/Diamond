using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Enums;
using Diamond.Domain.Models.TseInstrument;
using Diamond.Domain.Setting;
using Diamond.Infrastructure;
using Diamond.Services.CacheDataManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using TsePublicService;
using static TsePublicService.TsePublicV2SoapClient;

namespace Diamond.Services.BusinessService
{
    public class TseInstrumentBussinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        private readonly TseTmcWebServiceSettings _settings;
        private readonly CacheManager _cacheManager;

        public TseInstrumentBussinessService(DiamondDbContext dbContext,
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

        public async Task<List<TseInstrument>> GetAllInstrumentsAsync(CancellationToken cancellation)
        {
            var allInstrumentsList_key = $"AllInstrumentsList";
            var obj = _cacheManager.Get(allInstrumentsList_key);

            List<string> list = obj as List<string>;
            List<TseInstrument> instruments = new List<TseInstrument>();

            var allIns = new List<string>();

            foreach (var item in list)
            {
                var key = $"Instrument_{item}";
                var instrument_obj = _cacheManager.Get(key);

                TseInstrument instrument = instrument_obj as TseInstrument;
                instruments.Add(instrument);
            }

            //if (string.IsNullOrEmpty(request.Sort))
            //{
            //    request.Sort = "instrumentId desc";
            //}
            //data = data.OrderBy(request.Sort);

            return instruments;
        }

        public async Task SaveTseInstrumentAsync(CancellationToken cancellation)
        {
            var client = new TsePublicV2SoapClient(EndpointConfiguration.TsePublicV2Soap12);
            var root = new List<XElement>();

            var tseInstruments = await client.InstrumentAsync(_settings.User, _settings.Password, (int)TseFlowMarket.Bourse);
            var otcInstruments = await client.InstrumentAsync(_settings.User, _settings.Password, (int)TseFlowMarket.FaraBourse);
            var otcInstruments_base = await client.InstrumentAsync(_settings.User, _settings.Password, (int)TseFlowMarket.OTCBase);

            if (tseInstruments != null
                && otcInstruments != null
                && otcInstruments_base != null)
            {
                var tse = tseInstruments.Nodes[1].Elements("Instruments").Elements("TseInstruments").ToList();
                var otc = otcInstruments.Nodes[1].Elements("Instruments").Elements("TseInstruments").ToList();
                var baseotc = otcInstruments_base.Nodes[1].Elements("Instruments").Elements("TseInstruments").ToList();

                root.AddRange(tse);
                root.AddRange(otc);
                root.AddRange(baseotc);

                List<TseInstrument> list = root
                    .Select(ins => new TseInstrument()
                    {
                        Date = (long)ins.Element("DEVen"),
                        InsCode = (long)ins.Element("InsCode"),
                        InstrumentId = (string)ins.Element("InstrumentID"),
                        InstrumentMnemonic = (string)ins.Element("CValMne"),
                        EnglishName = (string)ins.Element("LVal18"),
                        FourDigitCompanyCode = (string)ins.Element("CSocCSAC"),
                        CompanyName = (string)ins.Element("LSoc30"),
                        Symbol = (string)ins.Element("LVal18AFC"),
                        Name = (string)ins.Element("LVal30"),
                        CIsin = (string)ins.Element("CIsin"),
                        InstrumentGroupId = (string)ins.Element("CGrValCot"),
                        MarketSegment = (string)ins.Element("YMarNSC"),
                        BoardCode = (long)ins.Element("CComVal"),
                        SectorCode = ((string)ins.Element("CSecVal")).Trim(),
                        SubSectorCode = ((string)ins.Element("CSoSecVal")).Trim(),
                        Flow = (int)ins.Element("Flow"),
                        Valid = (int)ins.Element("Valid"),

                    }).ToList();

                var instrumentCache = new InstrumentCacheManager(_cacheManager);
                await instrumentCache.UpdateInstrument_TseInstrument(list);

                await SaveTseInstrument(list,cancellation);
            }
        }

        

        public async Task SaveTseInstrument(List<TseInstrument> list, CancellationToken cancellation)
        {
            var tseInstruments = await _dbContext.Set<TseInstrument>()
                .ToListAsync(cancellationToken: cancellation);

            foreach (var item in list)
            {
                var entity = tseInstruments.FirstOrDefault(x => x.InstrumentId == item.InstrumentId);
                if (entity == null)
                {
                    entity = new TseInstrument()
                    {
                        InsCode = item.InsCode
                    };
                    _dbContext.Add(entity);
                }                            
                
                entity.Date = item.Date;
                entity.InsCode = item.InsCode;
                entity.InstrumentId = item.InstrumentId;
                entity.InstrumentMnemonic = item.InstrumentMnemonic;
                entity.EnglishName = item.EnglishName;
                entity.FourDigitCompanyCode = item.FourDigitCompanyCode;
                entity.CompanyName = item.CompanyName;
                entity.Symbol = item.Symbol;
                entity.Name = item.Name;
                entity.CIsin = item.CIsin;
                entity.InstrumentGroupId = item.InstrumentGroupId;
                entity.MarketSegment = item.MarketSegment;
                entity.BoardCode = item.BoardCode;
                entity.SectorCode = item.SectorCode;
                entity.SubSectorCode = item.SubSectorCode;
                entity.Flow = item.Flow;
                entity.BaseVol = item.BaseVol;
                entity.Valid = item.Valid;

                entity.QtitMaxSaiOmProd = item.QtitMaxSaiOmProd;
                entity.QtitMinSaiOmProd = item.QtitMinSaiOmProd;
                entity.QQtTranMarVal = item.QQtTranMarVal;
                entity.QPasCotFxeVal = item.QPasCotFxeVal;
                entity.YVal = item.YVal;
                entity.PSaiSMinOkValMdv = item.PSaiSMinOkValMdv;
                entity.PSaiSMaxOkValMdv = item.PSaiSMaxOkValMdv;
                entity.YDeComp = item.YDeComp;
                entity.YUniExpP = item.YUniExpP;
                entity.DInMar = item.DInMar;
                entity.CGdSVal = item.CGdSVal;
                entity.Yopsj = item.Yopsj;
                entity.DeSop = item.DeSop;
                entity.ZTitad = item.ZTitad;
                entity.QNmVlo = item.QNmVlo;
            }

            var response = await _dbContext.SaveChangesAsync(cancellation);
        }


        public async Task GetShareChangeAsync(CancellationToken cancellation)
        {
            var client = new TsePublicV2SoapClient(EndpointConfiguration.TsePublicV2Soap12);

            var root = new List<XElement>();

            var tseShareChanges = await client.ShareChangeAsync(_settings.User, _settings.Password, (int)TseFlowMarket.Bourse);
            var otcShareChanges = await client.ShareChangeAsync(_settings.User, _settings.Password, (int)TseFlowMarket.FaraBourse);
            var otcShareChange_base = await client.ShareChangeAsync(_settings.User, _settings.Password, (int)TseFlowMarket.OTCBase);

            if (tseShareChanges != null
                && otcShareChanges != null
                && otcShareChange_base != null)
            {
                var tse = tseShareChanges.Nodes[1].Elements("TseShare").Elements("TseShare").ToList();
                var otc = otcShareChanges.Nodes[1].Elements("TseShare").Elements("TseShare").ToList();
                var baseotc = otcShareChange_base.Nodes[1].Elements("TseShare").Elements("TseShare").ToList();

                root.AddRange(tse);
                root.AddRange(otc);
                root.AddRange(baseotc);

                List<TseShareChange> list = root.Select(ins => new TseShareChange()
                {
                    InsCode = (string)ins.Element("InsCode"),
                    Date = (string)ins.Element("DEven"),
                    NumberOfShareOld = (long)ins.Element("NumberOfShareOld"),
                    NumberOfShareNew = (long)ins.Element("NumberOfShareNew"),
                    Name = (string)ins.Element("LVal30"),
                    Symbol = (string)ins.Element("LVal18AFC"),
                }).ToList();
            }
        }

    }
}



