using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using DAL.Models;
using DAL;
using VTrade_AdminConsole.Models;

namespace VTrade_AdminConsole.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CheckLoginStatus(string User_Name, string User_Password)
        {
            chkUserResponseData res = new chkUserResponseData();
            try
            {

                Methods Repobj = new Methods();
                _chkLoginStatus _chkLoginStatusObj = new _chkLoginStatus();
                _chkLoginStatusObj = Repobj.getLoginStatus(User_Name, User_Password);

                if (_chkLoginStatusObj.ResponseStatus == true)
                {
                    if (_chkLoginStatusObj.IsUserExist == true)
                    {
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
    }
}