using Diamond.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diamond.Services.BusinessServiceDto.Strategy
{
    public class IchimokoDto
    {
        /// <summary>
        /// استراتژی کراس خط های ایچی موکو
        /// </summary>
        public bool CrossIchimoko { get; set; }
        /// <summary>
        /// استراتژی قیمت و ابر
        /// </summary>
        public bool PriceIchimokoCloud { get; set; }
        /// <summary>
        /// از تاریخ 
        /// </summary>
        public DateOnly FromDate { get; set; }
        /// <summary>
        /// تا تاریخ 
        /// </summary>
        public DateOnly ToDate { get; set; }
        /// <summary>
        /// ترند صعودی یا نزولی
        /// </summary>
        public CrossEnum Trend { get; set; }
        /// <summary>
        /// تایم فریم
        /// </summary>
        public TimeframeEnum Timeframe { get; set; }
        /// <summary>
        /// نوع قیمت
        /// </summary>
        public CandelPriceEnum CandelPriceType { get; set; }
        /// <summary>
        /// مقایسه قیمت
        /// </summary>
        public ComparisonPriceTypeEnum ComparisonPriceType { get; set; }
        /// <summary>
        /// قیمت بالاتر یا پایین تر
        /// </summary>
        public PriceAboveBelowEnum? PriceAboveBelow { get; set; }
        /// <summary>
        /// ابر ایچیموکو
        /// </summary>
        public IchimokoCloudColorEnum IchimokoCloudColor { get; set; }
    }
}
