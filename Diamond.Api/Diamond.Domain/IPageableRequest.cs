using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Domain
{
    public interface IPageableRequest
    {
        int PageSize { get; set; }
        int PageNumber { get; set; }
        string Sort { get; set; }
    }
}
