using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _getContactInfo
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public List<ContactDetail> lstContactDetail { get; set; }
    }
}
