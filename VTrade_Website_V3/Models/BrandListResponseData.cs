using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTrade_Website_V3.Models
{
    public class BrandListResponseData
    {
        public List<BrandItem> lstBrandItem { get; set; }
        public string PageDesc { get; set; }
        public int PageNO { get; set; }
        public int NumSize { get; set; }
    }
}