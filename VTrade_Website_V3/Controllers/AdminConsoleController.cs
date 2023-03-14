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

    }
}