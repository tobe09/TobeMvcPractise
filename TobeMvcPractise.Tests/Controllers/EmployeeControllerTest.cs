using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Mvc;
using TobeMvcPractise.Controllers;
using TobeMvcPractise.Models;

namespace TobeMvcPractise.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        [TestMethod]
        public void Employee_Index()
        {
            var controller = new EmployeeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Employee_Details_With_Null_Id()
        {
            var controller = new EmployeeController();

            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void Employee_Details_With_Wrong_Id()
        {
            var controller = new EmployeeController();

            HttpNotFoundResult result = controller.Details(int.MaxValue) as HttpNotFoundResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void Employee_Details_With_Correct_Id()
        {
            var controller = new EmployeeController();

            int expectedId = 1;
            ViewResult result = controller.Details(expectedId) as ViewResult;
            int id = (result.Model as Employee).Id;

            Assert.AreEqual(expectedId, id);
        }
    }
}
