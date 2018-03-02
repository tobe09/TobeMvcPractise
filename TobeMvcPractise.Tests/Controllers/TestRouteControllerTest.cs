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
        public void TestRoute_Greeting()
        {
            TestRouteController cntrl = new TestRouteController();

            string name = "name";
            string greeting = cntrl.Greeting(name);

            Assert.AreEqual(typeof(string), greeting.GetType());
        }
    }
}
