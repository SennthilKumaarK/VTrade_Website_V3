using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _getProductInfo
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public List<ProductInfo> lstProductInfo { get; set; }
    }
}
