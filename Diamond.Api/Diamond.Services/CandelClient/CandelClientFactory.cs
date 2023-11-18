using Diamond.Domain.Enums;
using Diamond.Domain.Models;
using System.Threading.Tasks;

namespace Diamond.Services.CandelClient
{
    public class CandelClientFactory : ICandelClient
    {
        private readonly NahayatnegarCandelClient _nahayatnegarCandelClient;

        public CandelClientFactory(NahayatnegarCandelClient nahayatnegarCandelClient)
        {
            _nahayatnegarCandelClient = nahayatnegarCandelClient;
        }

        // نوع اش رو از setting می گیریم
        private ICandelClient GetCandelClient(string client)
        {
            // TODO : فعلا نهایت نگره اگر تغییر کرد به جاهای دیگه وصل میشم
            switch (client.ToLower())
            {
                case "nahayatnegar":
                    return _nahayatnegarCandelClient;
                default:
                    return null;
                    break;
            }
        }

        public Task<CandelDataModel> GetDataByUrl(string symbol, int yearAgo, TimeframeEnum timeFram)
        {
            var client = GetCandelClient("nahayatnegar");
            return client.GetDataByUrl(symbol, yearAgo, timeFram);
        }
    }
}
