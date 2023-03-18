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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProductCategories()
        {
            Methods Repobj = new Methods();
            List<CategoryItem> lstObj = new List<CategoryItem>();

            _getCategoryItems _getCategoryItemsObj = new _getCategoryItems();
            _getCategoryItemsObj = Repobj.getCategoryItems();

            if (_getCategoryItemsObj.ResponseStatus == true)
            {
                if (_getCategoryItemsObj.lstCategoryItem != null)
                {
                    lstObj = _getCategoryItemsObj.lstCategoryItem;
                }
            }

            return PartialView("GetProductCategories", lstObj);

        }

        [HttpGet]
        public ActionResult GetProductItems()
        {
            Methods Repobj = new Methods();
            List<ProductListInfo> lstObj = new List<ProductListInfo>();

            _getProductItems _getProductItemsObj = new _getProductItems();

            var ProdID_List = new int[6];
            ProdID_List[0] = 3;
            ProdID_List[1] = 12;
            ProdID_List[2] = 15;
            ProdID_List[3] = 19;
            ProdID_List[4] = 39;
            ProdID_List[5] = 46;

            _getProductItemsObj = Repobj.getProductItemsbyID(ProdID_List);

            if (_getProductItemsObj.ResponseStatus == true)
            {
                if (_getProductItemsObj.lstProductItem != null)
                {
                    lstObj = _getProductItemsObj.lstProductItem;
                }
            }
            return PartialView("GetProductItems", lstObj);
        }

        [HttpPost]
        public JsonResult AddSubscribeEmail(string subcribeemailaddress)
        {
            ResponseData res = new ResponseData();
            try
            {
                Methods Repobj = new Methods();
                _getUpdateStatus _setSubscribeEmailObj = new _getUpdateStatus();
                _setSubscribeEmailObj = Repobj.insertSubscribeEmail(subcribeemailaddress);

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
    }
}