using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum MarketNatureEnum : int
    {
        Tse = 1,
        Otc = 2
    }
    public enum MarketUnitTypeEnum : int
    {
        InstrumentType = 1,
        Stock = 2,
        Right = 3,
        StockFuture = 4,
        Bond = 5,
        Commodity = 6,
        OptionPut = 8,
        CommodityFuture = 9,
        CommodityOption = 10,
        CertificateofDeposite = 11,
        PhysicalCommodity1 = 12,
        PhysicalCommodity2 = 13,
        FutureEnergy1 = 14,
        FutureEnergy2 = 15,
        FixedETF = 16,
        MixedETF = 17,
        StockETF = 18,
        MercantileStock = 19,
        OptionCall = 20
    }
}
