using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Web.Mvc;
using TobeMvcPractise.Controllers;
using TobeMvcPractise.Models;

namespace TobeMvcPractise.Tests.Controllers
{
    [TestClass]
    public class EmployeeEntityControllerTest
    {
        [TestMethod]
        public void EmployeeEntity_Index()
        {
            var controller = new EmployeeEntityController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmployeeEntity_Details_WithNull()
        {
            var controller = new EmployeeEntityController();

            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Details_WithUnavailableId()
        {
            var controller = new EmployeeEntityController();

            HttpNotFoundResult result = controller.Details(int.MaxValue) as HttpNotFoundResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Details_WithAvailableId()
        {
            var controller = new EmployeeEntityController();

            int expectedId = 1;
            ViewResult result = controller.Details(expectedId) as ViewResult;
            int id = (result.Model as Employee).Id;

            Assert.AreEqual(expectedId, id);
        }
    }
}
