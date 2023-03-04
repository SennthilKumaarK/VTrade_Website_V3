using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Models;

namespace DAL
{
    public class XMLReader
    {
        public List<CountryCodes> ReturnListOfCountryCodes()
        {
            //var dirPath = Assembly.GetExecutingAssembly().Location;
            //dirPath = Path.GetDirectoryName(dirPath);
            //string xmlData = Path.GetFullPath(Path.Combine(dirPath, @"\XML_Path\CountryCodes.xml"));
            string xmlData = HttpContext.Current.Server.MapPath("~/App_Data/CountryCodes.xml");

            DataSet ds = new DataSet();//Using dataset to read xml file  
            ds.ReadXml(xmlData);
            var _CountryCodes = new List<CountryCodes>();
            _CountryCodes = (from rows in ds.Tables[0].AsEnumerable()
                             select new CountryCodes
                             {
                                 CountryCode = rows[0].ToString(),
                                 CountryName = rows[1].ToString(),
                             }).ToList();
            return _CountryCodes;
        }
    }
}
