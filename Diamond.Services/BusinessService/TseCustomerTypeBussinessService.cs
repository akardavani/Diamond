using Diamond.Domain.Entities.TsePublic;
using Diamond.Domain.Setting;
using Diamond.Infrastructure;
using Diamond.Services.CacheDataManager;
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
    public class TseCustomerTypeBussinessService : IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        private readonly TseTmcWebServiceSettings _settings;
        private readonly CacheManager _cacheManager;

        public TseCustomerTypeBussinessService(DiamondDbContext dbContext,
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


        /// حقیقی و حقوقی همان روز
        public async Task SaveTseCustomerTypeCurrentDateAsync(CancellationToken cancellation)
        {
            var client = new TsePublicV2SoapClient(EndpointConfiguration.TsePublicV2Soap12);

            var clientTypes = await client.ClientTypeAsync(_settings.User, _settings.Password);

            if (clientTypes != null)
            {
                var innerXml = clientTypes.Any1.InnerXml;

                XElement xmlTree = XElement.Parse(innerXml);
                var root = xmlTree.Elements("Data").ToList();

                List<TseClientType> list = root.Select(ins => new TseClientType()
                {
                    InsCode = (string)ins.Element("InsCode"),
                    Buy_I_Count = (long)ins.Element("Buy_CountI"),
                    Buy_N_Count = (long)ins.Element("Buy_CountN"),
                    Buy_I_Volume = (long)ins.Element("Buy_I_Volume"),
                    Buy_N_Volume = (long)ins.Element("Buy_N_Volume"),
                    Sell_I_Count = (long)ins.Element("Sell_CountI"),
                    Sell_N_Count = (long)ins.Element("Sell_CountN"),
                    Sell_I_Volume = (long)ins.Element("Sell_I_Volume"),
                    Sell_N_Volume = (long)ins.Element("Sell_N_Volume")
                }).ToList();


                var instrumentCache = new CustomerTypeCacheManager(_cacheManager);
                await instrumentCache.UpdateTrade_TseTrade(list);

                await SaveTseCustomerType(list, cancellation);
            }
        }

        private async Task SaveTseCustomerType(List<TseClientType> list, CancellationToken cancellation)
        {
            var tseClientTypes = await _dbContext.Set<TseClientType>()
                .ToListAsync(cancellationToken: cancellation);

            foreach (var item in list)
            {
                var entity = tseClientTypes.FirstOrDefault(x => x.InsCode == item.InsCode);
                if (entity == null)
                {
                    entity = new TseClientType()
                    {
                        InsCode = item.InsCode
                    };
                    _dbContext.Add(entity);
                }

                entity.InsCode = item.InsCode;
                entity.Buy_I_Count = item.Buy_I_Count;
                entity.Buy_I_Volume = item.Buy_I_Volume;
                entity.Buy_N_Count = item.Buy_N_Count;
                entity.Buy_N_Volume = item.Buy_N_Volume;
                entity.Sell_I_Count = item.Sell_I_Count;
                entity.Sell_I_Volume = item.Sell_I_Volume;
                entity.Sell_N_Count = item.Sell_N_Count;
                entity.Sell_N_Volume = item.Sell_N_Volume;
            }         

            var response = await _dbContext.SaveChangesAsync(cancellation);
        }
    }
}
