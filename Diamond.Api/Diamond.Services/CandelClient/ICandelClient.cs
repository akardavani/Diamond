using Diamond.Domain.Enums;
using Diamond.Domain.Models;
using System.Threading.Tasks;

namespace Diamond.Services.CandelClient
{
    public interface ICandelClient 
    {
        Task<CandelDataModel> GetDataByUrl(string symbol, int yearAgo,TimeframeEnum timeFram);
    }
}
