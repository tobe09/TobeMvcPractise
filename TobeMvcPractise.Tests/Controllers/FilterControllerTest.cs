using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobeMvcPractise.Controllers;

namespace TobeMvcPractise.Tests.Controllers
{
    [TestClass]
    public class FilterControllerTest
    {
        [TestMethod]
        public void Index()
        {
            FilterController filterController = new FilterController();

            string result = filterController.Index();
            string now = DateTime.Now.ToString();

            Assert.AreEqual(now, result);
        }
    }
}
