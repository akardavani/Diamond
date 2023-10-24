using Diamond.Domain.Entities;
using Diamond.Domain.Models;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Services.Model
{
    public class IchimokuCandelModel : CandelModel
    {
        public decimal? TenkanSen { get; set; } // conversion line
        public decimal? KijunSen { get; set; } // base line
        public decimal? SenkouSpanA { get; set; } // leading span A
        public decimal? SenkouSpanB { get; set; } // leading span B
        public decimal? ChikouSpan { get; set; } // lagging span
        public IchimokuCandelModel(Candel model) : base(model)
        {
        }

    }
}
