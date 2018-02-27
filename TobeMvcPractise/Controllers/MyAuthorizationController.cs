using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TobeMvcPractise.Controllers
{
    //[Authorize]
    public class MyAuthorizationController : Controller
    {
        //[AllowAnonymous]      //not necessary because the class does not enforce authorization
        public string Index()
        {
            return "Public content";
        }

        [Authorize(Users = "chineketobenna@gmail.com, dummyser", Roles = "")]         //allows only specified user(s) and role(s)
        public string Secret()
        {
            return "Secret content";
        }

        [MyAuthorize(MyRoles.User)]         //for role based authentication
        public string SecretRole()
        {
            return "Secret Role";
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class MyAuthorize : AuthorizeAttribute
    {
        public MyAuthorize(params MyRoles[] roles)
        {
            Roles = string.Join(", ", roles.Select(r => Enum.GetName(r.GetType(), r)));     //r.ToString()
        }
    }

    public enum MyRoles
    {
        User,
        Admin,
        Others
    }    
}