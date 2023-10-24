using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Domain.Enums
{
    public enum TseFlowMarket
    {
        Unknown = -1,

        //عمومي - مشترک بين بورس و فرابورس
        All = 0,

        //بورس TSE
        Bourse = 1,

        //فرابورس OTC
        FaraBourse = 2,

        //آتی
        Future = 3,

        //پایه فرابورس OTC
        OTCBase = 4,

        //پایه فرابورس (منتشر نمی شود) OTC
        OTCBase_NotPublished = 4,

        //بورس انرژی
        Energy = 6,

        //بورس کالا
        Mercantile = 7
    }
}
