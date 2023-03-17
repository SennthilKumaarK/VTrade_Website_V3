using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL.Repository;
using DAL.Models;
using VTrade_Website_V3.Models;
using DAL;
using VTrade_Website_V3.Attributes;

namespace VTrade_Website_V3.Controllers
{
    public class AdminConsoleController : Controller
    {
        // GET: AdminConsole
        public ActionResult Index()
        {
            HttpContext.Session["USERNAME"] = "";
            return View();
        }

        [UserSessionAttribute]
        public ActionResult Dashboard()
        {
            LoadDDLYear();
            LoadDashboardCount();
            return View();
        }

        [UserSessionAttribute]
        public ActionResult ChangePassword()
        {
            return View();
        }

        private void LoadDDLYear()
        {
            try
            {
                Methods Repobj = new Methods();
                _getUniqueYearVisitorAnalytics _getUniqueYearVisitorAnalyticsObj = new _getUniqueYearVisitorAnalytics();
                _getUniqueYearVisitorAnalyticsObj = Repobj.getUniqueVisitorAnalyticsYear();

                var YearList = new List<SelectListItem>();

                if (_getUniqueYearVisitorAnalyticsObj.ResponseStatus == true)
                {
                    List<string> lstObj = new List<string>();
                    lstObj = _getUniqueYearVisitorAnalyticsObj.lstYear;
                    string sCurrentYear = DateTime.Now.ToString("yyyy");

                    if (lstObj != null)
                    {
                        foreach (string varYear in lstObj)
                        {
                            if (sCurrentYear == varYear)
                            {
                                YearList.Add(new SelectListItem
                                {
                                    Selected = true,
                                    Value = varYear,
                                    Text = varYear
                                }); ;
                            }
                            else
                            {
                                YearList.Add(new SelectListItem
                                {
                                    Value = varYear,
                                    Text = varYear
                                });
                            }

                        }

                    }

                }

                ViewData["VstAnltYrListItems"] = YearList;

            }
            catch (Exception)
            {
            }
        }

        private void LoadDashboardCount()
        {
            try
            {
                Methods Repobj = new Methods();
                _getDashboardCount _getDashboardCountObj = new _getDashboardCount();
                _getDashboardCountObj = Repobj.getDashboardCount();

                if (_getDashboardCountObj.ResponseStatus == true)
                {
                    ViewData["TotalVisitorCount"] = _getDashboardCountObj.TotalVisitorCount;
                    ViewData["TotalVisitorToday"] = _getDashboardCountObj.TotalVisitorToday;
                    ViewData["TotalProducts"] = _getDashboardCountObj.TotalProducts;
                    ViewData["TotalContact"] = _getDashboardCountObj.TotalContact;
                    ViewData["TotalSubscribe"] = _getDashboardCountObj.TotalSubscribe;
                }
                else
                {
                    ViewData["TotalVisitorCount"] = "0";
                    ViewData["TotalVisitorToday"] = "0";
                    ViewData["TotalProducts"] = "0";
                    ViewData["TotalContact"] = "0";
                    ViewData["TotalSubscribe"] = "0";
                }
            }
            catch (Exception)
            {

            }
        }

        [HttpGet]
        public JsonResult GetLoginUserName()
        {
            ChkUserResponseData res = new ChkUserResponseData();
            try
            {

                if (HttpContext.Session["USERNAME"] != null)
                {
                    res.ResponseSuccess = true;
                    res.ResponseMessage = HttpContext.Session["USERNAME"].ToString();
                }
                else
                {
                    res.ResponseSuccess = false;
                    res.ResponseMessage = "";
                }
            }
            catch (Exception ex)
            {
                res.ResponseSuccess = false;
                res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
            }

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CheckLoginStatus(string User_Name, string User_Password)
        {
            ChkUserResponseData res = new ChkUserResponseData();
            try
            {

                Methods Repobj = new Methods();
                _chkLoginStatus _chkLoginStatusObj = new _chkLoginStatus();
                _chkLoginStatusObj = Repobj.getLoginStatus(User_Name, User_Password);

                if (_chkLoginStatusObj.ResponseStatus == true)
                {
                    if (_chkLoginStatusObj.IsUserExist == true)
                    {
                        HttpContext.Session["USERNAME"] = _chkLoginStatusObj.UserName;
                        res.ResponseSuccess = true;
                    }
                    else
                    {
                        res.ResponseSuccess = false;
                        res.ResponseMessage = _chkLoginStatusObj.UserMessage;
                    }
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

        [HttpGet]
        public JsonResult getChartData(string YearVal)
        {
            ResponseChartData res = new ResponseChartData();
            try
            {
                Methods Repobj = new Methods();
                _getDashboardChartData _getDashboardChartDataObj = new _getDashboardChartData();
                _getDashboardChartDataObj = Repobj.getDashboardChartData(YearVal);

                if (_getDashboardChartDataObj.ResponseStatus == true)
                {
                    res.ResponseSuccess = true;
                    res.ResponseData = _getDashboardChartDataObj.lstcountval;
                }
                else
                {
                    res.ResponseSuccess = false;
                    res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
                }
            }
            catch (Exception)
            {
                res.ResponseSuccess = false;
                res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
            }

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UpdatePassword(string Current_Password, string New_Password)
        {
            ChkUserResponseData res = new ChkUserResponseData();
            try
            {

                Methods Repobj = new Methods();
                _getUpdatePassword _getUpdatePasswordObj = new _getUpdatePassword();

                string User_Name = HttpContext.Session["USERNAME"].ToString();
                _getUpdatePasswordObj = Repobj.UpdatePassword(User_Name, Current_Password, New_Password);

                if (_getUpdatePasswordObj.ResponseStatus == true)
                {
                    if (_getUpdatePasswordObj.IsUpdated == true)
                    {
                        res.ResponseSuccess = true;
                        res.ResponseMessage = "Your Password has been updated";
                    }
                    else
                    {
                        res.ResponseSuccess = false;
                        res.ResponseMessage = _getUpdatePasswordObj.ErrorMessage;
                    }
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

        [UserSessionAttribute]
        public ActionResult Visitors()
        {
            return View();
        }

        public ActionResult GetVisitorInfo(string txtDate)
        {
            Methods Repobj = new Methods();
            _getVisitorInfo _getVisitorInfoObj = new _getVisitorInfo();
            _getVisitorInfoObj = Repobj.GetVisitorInfo(txtDate);
            List<VisitorAnalytic> lstVisitorAnalytic = new List<VisitorAnalytic>();

            if (_getVisitorInfoObj.ResponseStatus == true)
            {
                if (_getVisitorInfoObj.lstVisitorAnalytic != null)
                {
                    lstVisitorAnalytic = _getVisitorInfoObj.lstVisitorAnalytic;
                }
            }

            return PartialView("GetVisitorInfo", lstVisitorAnalytic);
        }

        [UserSessionAttribute]
        public ActionResult Subscribers()
        {
            return View();
        }

        public ActionResult GetSubscriberInfo()
        {
            Methods Repobj = new Methods();
            _getSubcriberInfo _getSubcriberInfoObj = new _getSubcriberInfo();
            _getSubcriberInfoObj = Repobj.GetSubscriberInfo();
            List<SubscribeDetail> lstSubscribeDetail = new List<SubscribeDetail>();

            if (_getSubcriberInfoObj.ResponseStatus == true)
            {
                if (_getSubcriberInfoObj.lstSubscribeDetail != null)
                {
                    lstSubscribeDetail = _getSubcriberInfoObj.lstSubscribeDetail;
                }
            }

            return PartialView("GetSubscriberInfo", lstSubscribeDetail);
        }

        [UserSessionAttribute]
        public ActionResult ContactUsers()
        {
            return View();
        }

        public ActionResult GetContactUsersInfo()
        {
            Methods Repobj = new Methods();
            _getContactInfo _getContactInfoObj = new _getContactInfo();
            _getContactInfoObj = Repobj.GetContactUsersInfo();
            List<ContactDetail> lstContactDetail = new List<ContactDetail>();

            if (_getContactInfoObj.ResponseStatus == true)
            {
                if (_getContactInfoObj.lstContactDetail != null)
                {
                    lstContactDetail = _getContactInfoObj.lstContactDetail;
                }
            }

            return PartialView("GetContactUsersInfo", lstContactDetail);
        }

        [UserSessionAttribute]
        public ActionResult Products()
        {
            return View();
        }

        public ActionResult GetProductsInfo()
        {
            Methods Repobj = new Methods();
            _getProductItems _getProductItemsObj = new _getProductItems();
            _getProductItemsObj = Repobj.GetProductItemsInfo();
            List<ProductListInfo> lstProductListInfo = new List<ProductListInfo>();

            if (_getProductItemsObj.ResponseStatus == true)
            {
                if (_getProductItemsObj.lstProductItem != null)
                {
                    lstProductListInfo = _getProductItemsObj.lstProductItem;
                }
            }

            return PartialView("GetProductsInfo", lstProductListInfo);
        }

        [UserSessionAttribute]
        [EncryptedActionParameter]
        public ActionResult UpdateProductInfo(int ProductID)
        {
            Methods Repobj = new Methods();
            _getProductInfo _getProductInfoObj = new _getProductInfo();
            _getProductInfoObj = Repobj.getProductInfo(ProductID);
            
            ProductItemResponseData ProductItemResponseDataObj = new ProductItemResponseData();
            List<ProductInfo> lstProductInfo = new List<ProductInfo>();

            if (_getProductInfoObj.ResponseStatus == true)
            {
                ProductItemResponseDataObj.ProductID = _getProductInfoObj.ProductID;
                ProductItemResponseDataObj.ProductName = _getProductInfoObj.ProductName;
                ProductItemResponseDataObj.ProductDesc = _getProductInfoObj.ProductDesc;
                lstProductInfo = _getProductInfoObj.lstProductInfo;

                if (lstProductInfo != null)
                {
                    if (lstProductInfo.Count > 0)
                    {
                        ProductItemResponseDataObj.KeyName1 = lstProductInfo[0].KeyName;
                        ProductItemResponseDataObj.KeyValue1 = lstProductInfo[0].KeyValue;
                    }
                    if (lstProductInfo.Count > 1)
                    {
                        ProductItemResponseDataObj.KeyName2 = lstProductInfo[1].KeyName;
                        ProductItemResponseDataObj.KeyValue2 = lstProductInfo[1].KeyValue;
                    }
                    if (lstProductInfo.Count > 2)
                    {
                        ProductItemResponseDataObj.KeyName3 = lstProductInfo[2].KeyName;
                        ProductItemResponseDataObj.KeyValue3 = lstProductInfo[2].KeyValue;
                    }
                    if (lstProductInfo.Count > 3)
                    {
                        ProductItemResponseDataObj.KeyName4 = lstProductInfo[3].KeyName;
                        ProductItemResponseDataObj.KeyValue4 = lstProductInfo[3].KeyValue;
                    }
                    if (lstProductInfo.Count > 4)
                    {
                        ProductItemResponseDataObj.KeyName5 = lstProductInfo[4].KeyName;
                        ProductItemResponseDataObj.KeyValue5 = lstProductInfo[4].KeyValue;
                    }
                    if (lstProductInfo.Count > 5)
                    {
                        ProductItemResponseDataObj.KeyName6 = lstProductInfo[5].KeyName;
                        ProductItemResponseDataObj.KeyValue6 = lstProductInfo[5].KeyValue;
                    }
                    if (lstProductInfo.Count > 6)
                    {
                        ProductItemResponseDataObj.KeyName7 = lstProductInfo[6].KeyName;
                        ProductItemResponseDataObj.KeyValue7 = lstProductInfo[6].KeyValue;
                    }
                    if (lstProductInfo.Count > 7)
                    {
                        ProductItemResponseDataObj.KeyName8 = lstProductInfo[7].KeyName;
                        ProductItemResponseDataObj.KeyValue8 = lstProductInfo[7].KeyValue;
                    }
                    if (lstProductInfo.Count > 8)
                    {
                        ProductItemResponseDataObj.KeyName9 = lstProductInfo[8].KeyName;
                        ProductItemResponseDataObj.KeyValue9 = lstProductInfo[8].KeyValue;
                    }
                    if (lstProductInfo.Count > 9)
                    {
                        ProductItemResponseDataObj.KeyName10 = lstProductInfo[9].KeyName;
                        ProductItemResponseDataObj.KeyValue10 = lstProductInfo[9].KeyValue;
                    }
                }
            }

            return View(ProductItemResponseDataObj);
        }

        [UserSessionAttribute]
        [EncryptedActionParameter]
        public ActionResult ViewProductInfo(int ProductID)
        {
            Methods Repobj = new Methods();
            _getProductInfo _getProductInfoObj = new _getProductInfo();
            _getProductInfoObj = Repobj.getProductInfo(ProductID);

            ProductItemResponseData ProductItemResponseDataObj = new ProductItemResponseData();
            List<ProductInfo> lstProductInfo = new List<ProductInfo>();

            if (_getProductInfoObj.ResponseStatus == true)
            {
                ProductItemResponseDataObj.ProductID = _getProductInfoObj.ProductID;
                ProductItemResponseDataObj.ProductName = _getProductInfoObj.ProductName;
                ProductItemResponseDataObj.ProductDesc = _getProductInfoObj.ProductDesc;
                lstProductInfo = _getProductInfoObj.lstProductInfo;

                if (lstProductInfo != null)
                {
                    if (lstProductInfo.Count > 0)
                    {
                        ProductItemResponseDataObj.KeyName1 = lstProductInfo[0].KeyName;
                        ProductItemResponseDataObj.KeyValue1 = lstProductInfo[0].KeyValue;
                    }
                    if (lstProductInfo.Count > 1)
                    {
                        ProductItemResponseDataObj.KeyName2 = lstProductInfo[1].KeyName;
                        ProductItemResponseDataObj.KeyValue2 = lstProductInfo[1].KeyValue;
                    }
                    if (lstProductInfo.Count > 2)
                    {
                        ProductItemResponseDataObj.KeyName3 = lstProductInfo[2].KeyName;
                        ProductItemResponseDataObj.KeyValue3 = lstProductInfo[2].KeyValue;
                    }
                    if (lstProductInfo.Count > 3)
                    {
                        ProductItemResponseDataObj.KeyName4 = lstProductInfo[3].KeyName;
                        ProductItemResponseDataObj.KeyValue4 = lstProductInfo[3].KeyValue;
                    }
                    if (lstProductInfo.Count > 4)
                    {
                        ProductItemResponseDataObj.KeyName5 = lstProductInfo[4].KeyName;
                        ProductItemResponseDataObj.KeyValue5 = lstProductInfo[4].KeyValue;
                    }
                    if (lstProductInfo.Count > 5)
                    {
                        ProductItemResponseDataObj.KeyName6 = lstProductInfo[5].KeyName;
                        ProductItemResponseDataObj.KeyValue6 = lstProductInfo[5].KeyValue;
                    }
                    if (lstProductInfo.Count > 6)
                    {
                        ProductItemResponseDataObj.KeyName7 = lstProductInfo[6].KeyName;
                        ProductItemResponseDataObj.KeyValue7 = lstProductInfo[6].KeyValue;
                    }
                    if (lstProductInfo.Count > 7)
                    {
                        ProductItemResponseDataObj.KeyName8 = lstProductInfo[7].KeyName;
                        ProductItemResponseDataObj.KeyValue8 = lstProductInfo[7].KeyValue;
                    }
                    if (lstProductInfo.Count > 8)
                    {
                        ProductItemResponseDataObj.KeyName9 = lstProductInfo[8].KeyName;
                        ProductItemResponseDataObj.KeyValue9 = lstProductInfo[8].KeyValue;
                    }
                    if (lstProductInfo.Count > 9)
                    {
                        ProductItemResponseDataObj.KeyName10 = lstProductInfo[9].KeyName;
                        ProductItemResponseDataObj.KeyValue10 = lstProductInfo[9].KeyValue;
                    }
                }
            }

            return View(ProductItemResponseDataObj);
        }



        [HttpPost]
        public JsonResult UpdateProductItemInfo(ProductItemResponseData ProductItemResponseDataObj)
        {
            ChkUserResponseData res = new ChkUserResponseData();
            try
            {

                Methods Repobj = new Methods();
                _getUpdatePassword _getUpdatePasswordObj = new _getUpdatePassword();

                _getUpdatePasswordObj = Repobj.UpdateProductItemInfo(ProductItemResponseDataObj.ProductName, ProductItemResponseDataObj.ProductDesc, ProductItemResponseDataObj.ProductID);

                if (_getUpdatePasswordObj.ResponseStatus == true)
                {
                    if (_getUpdatePasswordObj.IsUpdated == true)
                    {

                        _getUpdatePasswordObj = new _getUpdatePassword();
                        _getUpdatePasswordObj = Repobj.RemoveProductItemInfo( ProductItemResponseDataObj.ProductID);


                        if (_getUpdatePasswordObj.ResponseStatus == true)
                        {
                            if (_getUpdatePasswordObj.IsUpdated == true)
                            {

                                _getUpdatePasswordObj = new _getUpdatePassword();
                                _getUpdatePasswordObj = Repobj.AddProductItemsInfo(ProductItemResponseDataObj);


                                if (_getUpdatePasswordObj.ResponseStatus == true)
                                {
                                    if (_getUpdatePasswordObj.IsUpdated == true)
                                    {

                                        res.ResponseSuccess = true;
                                        res.ResponseMessage = "";

                                    }
                                    else
                                    {
                                        res.ResponseSuccess = false;
                                        res.ResponseMessage = _getUpdatePasswordObj.ErrorMessage;
                                    }
                                }
                                else
                                {
                                    res.ResponseSuccess = false;
                                    res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
                                }



                            }
                            else
                            {
                                res.ResponseSuccess = false;
                                res.ResponseMessage = _getUpdatePasswordObj.ErrorMessage;
                            }
                        }
                        else
                        {
                            res.ResponseSuccess = false;
                            res.ResponseMessage = "The server has encountered an unexpected internal error. Please try again later.";
                        }



                    }
                    else
                    {
                        res.ResponseSuccess = false;
                        res.ResponseMessage = _getUpdatePasswordObj.ErrorMessage;
                    }
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