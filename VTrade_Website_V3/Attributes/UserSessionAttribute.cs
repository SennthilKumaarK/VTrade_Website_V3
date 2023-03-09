using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VTrade_Website_V3.Attributes
{
    public class UserSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.HttpContext.Session["USERNAME"];
            if ((userId == null) || (userId.ToString().Trim() == ""))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "AdminConsole",
                    action = "Index"
                }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}