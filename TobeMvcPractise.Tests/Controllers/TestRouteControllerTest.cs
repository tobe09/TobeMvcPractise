using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TobeMvcPractise.Controllers;

namespace TobeMvcPractise.Tests.Controllers
{
    [TestClass]
    public class TestRouteControllerTest
    {
        [TestMethod]
        public void Greeting()
        {
            TestRouteController cntrl = new TestRouteController();

            string name = "name";
            ContentResult greet = new ContentResult();
            greet.Content = cntrl.Greeting(name);

            Assert.AreEqual(typeof(string), greet.Content.GetType());
        }
    }
}
