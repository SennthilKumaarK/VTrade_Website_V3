using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Models;

namespace VTrade_Website_V3.Models
{
    public class ProductListResponseData
    {
        public List<ProductListInfo> lstProductItem { get; set; }
        public string PageDesc { get; set; }
        public int PageNO { get; set; }
        public int NumSize { get; set; }
    }
}