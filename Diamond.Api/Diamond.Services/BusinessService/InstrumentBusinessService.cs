using Diamond.Domain.Entities;
using Diamond.Utils.ApiResult;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessService
{
    public class InstrumentBusinessService: IBusinessService
    {
        private readonly DiamondDbContext _dbContext;
        public InstrumentBusinessService(DiamondDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResult<List<Instrument>>> GetAllInstruments()
        {
            var instruments = await _dbContext.Set<Instrument>().ToListAsync();
            return new ApiResult<List<Instrument>>()
            {
                Data = instruments,
                IsSuccess = true,
            };
        }
    }
}
