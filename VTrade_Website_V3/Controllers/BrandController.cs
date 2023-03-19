using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Repository;
using DAL.Models;
using DAL;
using VTrade_Website_V3.Models;

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
        public PartialViewResult GetBrandItems(int PageNO)
        {
            BrandListResponseData res = new BrandListResponseData();
            Methods Repobj = new Methods();
            List<BrandItem> lstObj = new List<BrandItem>();

            _getBrandListItems _getBrandItemsObj = new _getBrandListItems();
            _getBrandItemsObj = Repobj.getBrandListItems(PageNO, 6);

            if (_getBrandItemsObj.ResponseStatus == true)
            {
                if (_getBrandItemsObj.lstBrandItem != null)
                {
                    lstObj = _getBrandItemsObj.lstBrandItem;
                }
            }

            res.lstBrandItem = lstObj;
            res.PageNO = _getBrandItemsObj.PageNO;
            res.NumSize = _getBrandItemsObj.numSize;

            if (lstObj.Count > 0)
            {
                int StartPg = _getBrandItemsObj.StartPg;
                int TotalPg = _getBrandItemsObj.TotalPg;
                int StartCount = (StartPg + 1);
                int EndCount = (StartPg + lstObj.Count);

                if (EndCount > StartCount)
                {
                    res.PageDesc = "Showing results of " + StartCount + " - " + EndCount + " out of " + TotalPg + " brands";
                }
                else
                {
                    res.PageDesc = "Showing results of " + StartCount + " out of " + TotalPg + " brands";
                }
            }
            else
            {
                res.PageDesc = "Showing results 0 brands";
            }

            return PartialView("GetBrandItems", res);
        }
    }
}