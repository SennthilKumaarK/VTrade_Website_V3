using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using DAL.Models;

namespace VTrade_Website_V3.Models
{
    public class ProductInfoResponseData
    {
        public Boolean ResponseSuccess { get; set; }
        public List<ProductInfo> ProductKeyInfo { get; set; }
        public ProductListInfo ProductHeaderInfo { get; set; }
        public List<ProductImageList> ProductImageList { get; set; }
    }
}