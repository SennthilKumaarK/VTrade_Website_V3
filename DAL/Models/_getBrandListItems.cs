using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _getBrandListItems
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public List<BrandItem> lstBrandItem { get; set; }
        public int numSize { get; set; }
        public int PageNO { get; set; }
        public int TotalPg { get; set; }
        public int StartPg { get; set; }
    }
}