using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class _chkLoginStatus
    {
        public Boolean ResponseStatus { get; set; }
        public string ErrorMessage { get; set; }
        public Boolean IsUserExist { get; set; }
        public string UserMessage { get; set; }
        public string UserName { get; set; }
    }
}
