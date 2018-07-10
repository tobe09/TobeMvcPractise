using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using TobeMvcPractise.Controllers;
using TobeMvcPractise.Models;
using TobeMvcPractise.Services;
using System.Linq;

namespace TobeMvcPractise.Tests.Controllers
{
    /// <summary>
    /// Not efficient to test controllers that use DbContext objects which cannot be mocked.
    /// Services should be tested or injecting the mocked DbContext object through the controllers constructor
    /// </summary>
    [TestClass]
    public class EmployeeEntityControllerTest
    {
        [TestMethod]        //async tests not supported
        public void EmployeeEntity_Index()
        {
            var controller = new EmployeeEntityController();

            ViewResult result = (controller.IndexAsync()).Result as ViewResult;
            //ViewResult result = controller.Index() as ViewResult;     //synchronous version

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmployeeEntity_Index_Mock()
        {
            var employees = GetMockEmployees();

            var mockSet = GetMockSetForSelect(employees);

            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);                 //inject service into the controller

            ViewResult result = (controller.IndexAsync()).Result as ViewResult;
            //ViewResult result = controller.Index() as ViewResult;     //synchronous version
            int expectedCount = employees.Count();
            int count = (result.Model as IEnumerable<Employee>).Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void EmployeeEntity_Details_With_Null_Id()
        {
            var controller = new EmployeeEntityController();

            HttpStatusCodeResult result = controller.Details(null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Details_With_Wrong_Id()
        {
            var controller = new EmployeeEntityController();

            HttpNotFoundResult result = controller.Details(int.MaxValue) as HttpNotFoundResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Details_With_Wrong_Id_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);

            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            int expectedId = int.MaxValue;
            mockContext.Setup(m => m.Employees.Find(expectedId)).Returns(expectedId > employees.Count() ? null : employees.ElementAt(expectedId - 1));

            HttpNotFoundResult result = controller.Details(expectedId) as HttpNotFoundResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Details_With_Correct_Id_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);

            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            int expectedId = 1;
            mockContext.Setup(m => m.Employees.Find(expectedId)).Returns(employees.ElementAt(expectedId));

            ViewResult result = controller.Details(expectedId) as ViewResult;
            int id = (result.Model as Employee).Id;

            Assert.AreEqual(expectedId, id);
        }

        [TestMethod]
        public void EmployeeEntity_Create_With_Null_Mock()
        {
            var mockSet = GetMockSet();

            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);             //inject service into the controller

            ViewResult result = controller.Create(null) as ViewResult;
            var model = result.Model;
            
            Assert.IsNull(model);
        }

        [TestMethod]
        public void EmployeeEntity_Create_With_Employee_Mock()
        {
            var mockSet = GetMockSet();
            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);       
            var controller = new EmployeeEntityController(service);             //inject service into the controller

            RedirectToRouteResult result = controller.Create(new Employee()) as RedirectToRouteResult;
            var actionName = result.RouteValues["action"].ToString().ToUpper();

            mockSet.Verify(m => m.Add(It.IsAny<Employee>()), Times.Once());     //verify that adding was successful
            mockContext.Verify(m => m.SaveChanges(), Times.Once());             //verify that saving changes was successful

            Assert.AreEqual("INDEX", actionName);           //after successful creation of employee, redirect to Index action
        }

        [TestMethod]
        public void EmployeeEntity_Edit_With_Null_Id_Mock()
        {
            var controller = new EmployeeEntityController();

            HttpStatusCodeResult result = controller.Edit((int?)null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Edit_With_Correct_Employee_Id_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);
            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            int expectedId = 2;
            mockContext.Setup(m => m.Employees.Find(expectedId)).Returns(employees.ElementAt(expectedId));

            ViewResult result = controller.Edit(expectedId) as ViewResult;
            int actualId = (result.Model as Employee).Id;

            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void EmployeeEntity_Edit_With_Wrong_Employee_Id_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);
            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            int wrongId = int.MaxValue;
            mockContext.Setup(m => m.Employees.Find(wrongId)).Returns((Employee)null);

            ViewResult result = controller.Edit(wrongId) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void EmployeeEntity_Edit_With_Null_Employee_Mock()
        {
            var controller = new EmployeeEntityController();

            ViewResult result = controller.Edit((Employee)null) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EmployeeEntity_Edit_With_Employee_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);
            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            RedirectToRouteResult result = controller.Edit(employees.FirstOrDefault()) as RedirectToRouteResult;
            var actionName = result.RouteValues["action"].ToString().ToUpper();

            Assert.AreEqual("INDEX", actionName);
        }

        [TestMethod]
        public void EmployeeEntity_Delete_With_Null_Id_Mock()
        {
            var controller = new EmployeeEntityController();

            HttpStatusCodeResult result = controller.Edit((int?)null) as HttpStatusCodeResult;

            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void EmployeeEntity_Delete_With_Correct_Employee_Id_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);
            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            int expectedId = 2;
            mockContext.Setup(m => m.Employees.Find(expectedId)).Returns(employees.ElementAt(expectedId));

            ViewResult result = controller.Delete(expectedId) as ViewResult;
            int actualId = (result.Model as Employee).Id;

            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void EmployeeEntity_Delete_With_Wrong_Employee_Id_Mock()
        {
            var employees = GetMockEmployees();
            var mockSet = GetMockSetForSelect(employees);
            var mockContext = GetMockContext(mockSet);

            var service = new EmployeeEntityServices(mockContext.Object);
            var controller = new EmployeeEntityController(service);

            int wrongId = int.MaxValue;
            mockContext.Setup(m => m.Employees.Find(wrongId)).Returns((Employee)null);

            HttpNotFoundResult result = controller.Edit(wrongId) as HttpNotFoundResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode);
        }


        #region Private Test Setup

        private IQueryable<Employee> GetMockEmployees()
        {
            return new List<Employee>() {
                new Employee {Id=0, Name="John" , Age=19, JoiningDate=DateTime.Today},
                new Employee {Id=1, Name="Ada" , Age=47, JoiningDate=DateTime.Today},
                new Employee {Id=2, Name="Loveth" , Age=23, JoiningDate=DateTime.Today }
            }.AsQueryable();
        }

        private Mock<DbSet<Employee>> GetMockSet()
        {
            return new Mock<DbSet<Employee>>(); ;
        }

        private Mock<DbSet<Employee>> GetMockSetForSelect(IQueryable<Employee> employees)
        {
            var mockEmployees = employees;

            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(mockEmployees.Provider);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(mockEmployees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(mockEmployees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(mockEmployees.GetEnumerator());
            
            return mockSet;
        }

        private Mock<MyDbContext> GetMockContext(Mock<DbSet<Employee>> mockSet)
        {
            var mockContext = new Mock<MyDbContext>();
            mockContext.Setup(m => m.Employees).Returns(mockSet.Object);

            return mockContext;
        }
        #endregion
    }
}
