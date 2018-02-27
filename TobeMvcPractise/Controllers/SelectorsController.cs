using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TobeMvcPractise.Controllers
{
    public class SelectorsController : Controller
    {
        // GET: Selectors
        public ActionResult Index()
        {
            return Content("Hello from Selector Controller");
        }

        //selector: action name
        [ActionName("actionName")]
        public string GetActionName()
        {
            return "GetActionName action";
        }

        public string GetName()
        {
            return "GetName action gotten as get";
        }

        //selector: action verb
        [HttpPost]      //without this verb, the action is treated as a Get end point
        public string GetName(string action = "post")   //inherent ambiguity because of the optional parameter
        {
            return "GetName action with " + action + " verb";
        }

        //selector not actionable externally
        [NonAction]
        public string NoAction()
        {
            return "NoAction method that cannot be called as an end-point";
        }
    }
}