using Diamond.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Domain.Models
{
    public class CandelDataModel
    {
        public string Symbol { get; set; }
        public List<CandelModel> Candels { get; set; }
        public TimeframeEnum Timeframe { get; set; }
    }
}
