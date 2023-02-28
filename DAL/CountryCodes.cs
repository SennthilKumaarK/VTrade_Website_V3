using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DAL
{
    [Serializable]
    [XmlRoot("CountryCodes"), XmlType("CountryCodes")]
    public class CountryCodes
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}
