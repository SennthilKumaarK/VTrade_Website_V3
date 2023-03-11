using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _getUpdatePassword
    {
        public Boolean ResponseStatus { get; set; }

        public Boolean IsUpdated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
