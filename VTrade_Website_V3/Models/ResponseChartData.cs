using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTrade_Website_V3.Models
{
    public class ResponseChartData
    {
        public Boolean ResponseSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public List<string> ResponseData { get; set; }
    }
}