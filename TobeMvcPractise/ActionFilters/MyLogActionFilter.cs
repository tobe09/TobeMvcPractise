using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TobeMvcPractise.ActionFilters
{
    public class MyLogActionFilter : ActionFilterAttribute
    {
        private string _callingClass { get; set; }

        public MyLogActionFilter() : base() { }
        public MyLogActionFilter(string caller)
        {
            _callingClass = caller;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            //filterContext.Result determines the result sent by the action method
            Log("OnActionExecuting", filterContext.RouteData);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
            bool status = (HttpStatusCode)filterContext.HttpContext.Response.StatusCode == HttpStatusCode.NotFound;
        }

        private void Log(string eventName, RouteData routeData)
        {
            var contrl = routeData.Values["controller"];
            var action = routeData.Values["action"];

            string msg = "\r\nEvent= " + eventName + ", Controller= " + contrl + ", \r\nAction= " + action + ", Calling Class= " + _callingClass;

            Debug.WriteLine(msg, "MyActionFilter Log");
        }
    }
}