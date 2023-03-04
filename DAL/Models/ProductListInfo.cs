using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class ProductListInfo
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public string ProductImgPath { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
    }
}
