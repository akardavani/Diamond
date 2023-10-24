using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Setting;
using Diamond.Infrastructure;
using Microsoft.EntityFrameworkCore;
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
    public class TseSubSectorBussinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        private readonly TseTmcWebServiceSettings _settings;
        private readonly CacheManager _cacheManager;

        public TseSubSectorBussinessService(DiamondDbContext dbContext,
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

        public async Task SaveTseSubSectorAsync(CancellationToken cancellation)
        {
            var client = new TsePublicV2SoapClient(EndpointConfiguration.TsePublicV2Soap12);

            var subSectors = await client.SubSectorAsync(_settings.User, _settings.Password);

            if (subSectors != null)
            {
                var root = subSectors.Nodes[1].Elements("SubSector").Elements("SubSector").ToList();

                List<TseSubSector> list = root.Select(ins => new TseSubSector()
                {
                    SectorCode = (string)ins.Element("CSecVal"),
                    SubSectorCode = (string)ins.Element("CSoSecVal"),
                    Date = (string)ins.Element("DEven"),
                    SubSectorName = (string)ins.Element("LSoSecVal"),
                }).ToList();

                //var instrumentCache = new InstrumentCacheManager(_cacheManager);
                //await instrumentCache.UpdateInstrument_TseInstrument(list);

                await SaveSubSector(list, cancellation);
            }
        }

        private async Task SaveSubSector(List<TseSubSector> list, CancellationToken cancellation)
        {
            var tseInstruments = await _dbContext.Set<TseSubSector>()
                .ToListAsync(cancellationToken: cancellation);

            foreach (var item in list)
            {
                if (item.SubSectorCode.Length < 4)
                    item.SubSectorCode = $"0{item.SubSectorCode}";

                var entity = tseInstruments.FirstOrDefault(x => x.SubSectorCode == item.SubSectorCode);
                if (entity == null)
                {
                    entity = new TseSubSector()
                    {
                        SubSectorCode = item.SubSectorCode
                    };
                    _dbContext.Add(entity);
                }

                entity.Date = item.Date;
                entity.SectorCode = item.SectorCode;
                entity.SubSectorName = item.SubSectorName;                
            }

            var response = await _dbContext.SaveChangesAsync(cancellation);
        }
    }
}
