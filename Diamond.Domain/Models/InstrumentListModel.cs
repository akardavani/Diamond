using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Domain.Models
{
    public class InstrumentListModel
    {
        public string InstrumentId { get; set; }

        //گروه
        public string InstrumentGroupId { get; set; }

        //گروه های صنعت
        public string InstrumentSectorGroup { get; set; }

        //تابلو
        public string Board { get; set; }

        //نماد
        public string InstrumentPersianName { get; set; }

        //نام لاتین
        public string InstrumentName { get; set; }

        
        public string InsCode { get; set; }
    }
}
