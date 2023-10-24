using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Domain.Models
{
    public class CandelModel
    {
        // required base properties        
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }

        // custom properties
        public long Timestamp { get; set; }
        public decimal NetValue { get; set; }

    }
}
