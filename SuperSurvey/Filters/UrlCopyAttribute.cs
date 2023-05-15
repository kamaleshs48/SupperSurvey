using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperSurvey.Filters
{
    public class UrlCopyAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //write your user right logic 
            //if user has right to do nothig otherwise redirect to error page.
            string PName = "";
            Controller controller = filterContext.Controller as Controller;
            //  var url = filterContext.HttpContext.Request.UrlReferrer;

            if (filterContext.HttpContext.Request.UrlReferrer != null)
            {
                PName = filterContext.HttpContext.Request.UrlReferrer.Segments[filterContext.HttpContext.Request.UrlReferrer.Segments.Length - 1];
            }
            // filterContext.HttpContext.Session["Action"] = NewActionName;
            if (PName == "" || filterContext.HttpContext.Session["UserID"] == null)
            {
                string message = "You have no right to view this page.";
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("area", "");
                redirectTargetDictionary.Add("action", "Login");
                redirectTargetDictionary.Add("controller", "Home");
                // redirectTargetDictionary.Add("customMessage", message);
                filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
            }

            var cookie = filterContext.HttpContext.Request.Cookies["RefreshFilter"];
            filterContext.RouteData.Values["IsRefreshed"] = cookie != null &&
            cookie.Value == filterContext.HttpContext.Request.Url.ToString();


        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.SetCookie(new HttpCookie("RefreshFilter", filterContext.HttpContext.Request.Url.ToString()));
        }
        private string Log(RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var message = String.Format("{0}", actionName);
            // Debug.WriteLine(message, "Action Filter Log");
            return message;
        }
    }
}