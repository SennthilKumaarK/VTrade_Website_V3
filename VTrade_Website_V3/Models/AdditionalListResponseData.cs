using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTrade_Website_V3.Models
{
    public class AdditionalListResponseData
    {
        public Boolean ResponseSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseMessage2 { get; set; }
        public int PageNO { get; set; }
        public int NumSize { get; set; }
    }
}