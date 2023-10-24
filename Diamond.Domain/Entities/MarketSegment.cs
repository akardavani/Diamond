using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Domain.Entities
{
    /// <summary>
    /// بخش بازار
    /// </summary>
    public class MarketSegment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Code { get; set; }

        //public virtual ICollection<Instrument> Instruments { get; set; }
    }
}
