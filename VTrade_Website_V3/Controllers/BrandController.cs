using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTrade_Website_V3.Models;
using DAL.Repository;
using DAL.Models;
using DAL;

namespace VTrade_Website_V3.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetBrandItems(int PageNO)
        {
            AdditionalListResponseData res = new AdditionalListResponseData();
            try
            {

                Methods Repobj = new Methods();
                _getBrandListItems _getBrandItemsObj = new _getBrandListItems();
                _getBrandItemsObj = Repobj.getBrandListItems(PageNO, 6);

                if (_getBrandItemsObj.ResponseStatus == true)
                {
                    List<BrandItem> lstObj = new List<BrandItem>();
                    lstObj = _getBrandItemsObj.lstBrandItem;
                    string str_responseData = "";

                    if (lstObj != null)
                    {
                        foreach (BrandItem varBrandListItem in lstObj)
                        {
                            str_responseData += "<div class='col-lg-4 col-md-6 Brand-item'> <a href='/product?BrandID=" + varBrandListItem.ID + "'><img src='" + varBrandListItem.BrandImgPath + "' class='img-Brand' alt=''/></a>";
                            str_responseData += "<div class='Brand-info'><a href='/product?BrandID=" + varBrandListItem.ID + "'>";
                            str_responseData += "<h4>" + varBrandListItem.BrandName + "</h4>";
                            str_responseData += "</a></div></div>";
                        }
                    }

                    res.ResponseSuccess = true;
                    res.ResponseMessage = str_responseData;
                    res.PageNO = _getBrandItemsObj.PageNO;
                    res.NumSize = _getBrandItemsObj.numSize;
                    int StartPg = _getBrandItemsObj.StartPg;
                    int TotalPg = _getBrandItemsObj.TotalPg;

                    if (lstObj.Count() > 0)
                    {
                        int StartCount = (StartPg + 1);
                        int EndCount = (StartPg + lstObj.Count());

                        if (EndCount > StartCount)
                        {
                            res.ResponseMessage2 = "Showing results of " + StartCount + " - " + EndCount + " out of " + TotalPg + " brands";
                        }
                        else
                        {
                            res.ResponseMessage2 = "Showing results of " + StartCount + " out of " + TotalPg + " brands";
                        }
                    }
                    else
                    {
                        res.ResponseMessage2 = "Showing results 0 brands";
                    }
                }
                else
                {
                    res.ResponseSuccess = false;
                    //res.ResponseMessage = _getBrandItemsObj.ErrorMessage;
                    res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                res.ResponseSuccess = false;
                res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
                //res.ResponseMessage = ex.Message.ToString();
            }

            return Json(res, JsonRequestBehavior.AllowGet);

        }
    }
}