using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductFilter
    {
        public string CategoryID { get; set; }

        public string BrandID { get; set; }

        public string SortID { get; set; }

        public int PageNO { get; set; }
    }
}
