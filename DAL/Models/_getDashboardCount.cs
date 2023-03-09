using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _getDashboardCount
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public string TotalVisitorCount { get; set; }
        public string TotalVisitorToday { get; set; }
        public string TotalProducts { get; set; }
        public string TotalSubscribe { get; set; }
        public string TotalContact { get; set; }
    }
}
