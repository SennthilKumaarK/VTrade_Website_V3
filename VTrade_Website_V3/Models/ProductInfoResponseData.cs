using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTrade_Website_V3.Models
{
    public class ProductInfoResponseData
    {
        public Boolean ResponseSuccess { get; set; }
        public string ProductName { get; set; }

        public string ProductDesc { get; set; }

        public string ProductInformation { get; set; }

        public string ProductImageList { get; set; }
    }
}