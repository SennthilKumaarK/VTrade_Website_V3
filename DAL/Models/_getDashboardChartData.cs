using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _getDashboardChartData
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public List<string> lstcountval { get; set; }
    }
}
