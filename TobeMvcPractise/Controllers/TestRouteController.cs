using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace TobeMvcPractise.Controllers
{
    public class TestRouteController : Controller
    {
        public string List(string name)
        {
            string str = @"Hello " + name + ".<br/><strong>From TestRoute</strong>";

            return str;
        }
    }
}
