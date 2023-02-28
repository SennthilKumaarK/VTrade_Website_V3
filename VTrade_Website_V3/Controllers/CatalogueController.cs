using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTrade_Website_V3.Models;
using DAL;

namespace VTrade_Website_V3.Controllers
{
    public class CatalogueController : Controller
    {
        // GET: Catalogue
        public ActionResult Index()
        {
            try
            {
                Repository Repobj = new Repository();
                _getCountryCodes _getCountryCodesObj = new _getCountryCodes();
                _getCountryCodesObj = Repobj.getCountryCodeList();

                var countryCodeList = new List<SelectListItem>();

                countryCodeList.Add(new SelectListItem
                {
                    Selected = true,
                    Value = "0",
                    Text = "Select Country Code"
                });


                if (_getCountryCodesObj.ResponseStatus == true)
                {
                    List<CountryCodes> lstObj = new List<CountryCodes>();
                    lstObj = _getCountryCodesObj.lstCountryCodes;

                    if (lstObj != null)
                    {
                        foreach (CountryCodes varCountryCodes in lstObj)
                        {
                            countryCodeList.Add(new SelectListItem
                            {
                                Value = varCountryCodes.CountryCode,
                                Text = "(" + varCountryCodes.CountryCode + ") " + varCountryCodes.CountryName
                            });
                        }

                    }

                }

                ViewData["CountryCodeListItems"] = countryCodeList;
            }
            catch (Exception)
            {
            }
            return View();

        }

        [HttpPost]
        public JsonResult InsertContactDetails(ContactDetail _ContactDetail)
        {
            ResponseData res = new ResponseData();
            try
            {
                Repository Repobj = new Repository();
                _getUpdateStatus _setSubscribeEmailObj = new _getUpdateStatus();
                _setSubscribeEmailObj = Repobj.insertContactDetails(_ContactDetail);

                if (_setSubscribeEmailObj.ResponseStatus == true)
                {
                    res.ResponseSuccess = true;
                }
                else
                {
                    res.ResponseSuccess = false;
                    res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                res.ResponseSuccess = false;
                res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadDocument()
        {
            return View("DownloadDocument");
        }



    }
}
