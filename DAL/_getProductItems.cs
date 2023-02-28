using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class _getProductItems
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public List<ProductListInfo> lstProductItem { get; set; }
    }
}
