using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TobeMvcPractise.ActionFilters;

namespace TobeMvcPractise.Controllers
{
    /*
     * Filter types:
     * Authorization filter >   Action filter   >   Result Filter   >   Exception Filter
     * Action Filters (Implements IActionFilterAttribute interface): OutputCache, HandleError, Authorize
     */
    [MyLogActionFilter("FilterController")]      //attaches the filter to the whole class
    public class FilterController : Controller
    {
        [OutputCache(Duration = 5)]
        public string Index()
        {
            return DateTime.Now.ToString();
        }

        //[HandleError(ExceptionType = typeof(Exception), Master = "Home/index.cshtml")]        //not fully understood
        //public int Number(string id)
        //{
        //    return Convert.ToInt32(id);
        //}
    }
}
