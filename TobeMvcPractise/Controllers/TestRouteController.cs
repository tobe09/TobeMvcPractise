using System.Web.Mvc;

namespace TobeMvcPractise.Controllers
{
    public class TestRouteController : Controller
    {
        //returns a simple string
        public string Greeting(string name)
        {
            string str = @"Hello " + name + ".<br/><strong>From TestRoute</strong>";

            //string pureHtmlString = Server.HtmlEncode(str);

            return str;
        }

        //returns an unordered list 
        public string GetAllTests()
        {
            return @"<ul>
                <li>Test One</li>
                <li>Test Two</li>
                <li>Test Three</li>
                <li>Test Four</li>
                <li>Test Five</li>
                </ul>";
        } 

        //redirects to the greeting method
        public  ActionResult RedirectToGreet(string name)
        {
            return RedirectToAction("Greeting", new { name = name });
        }
    }
}
