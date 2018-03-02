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
        public void Filter_Index()
        {
            //Arrange
            FilterController filterController = new FilterController();

            //Act
            string result = filterController.Index();       //returns today's date
            string now = DateTime.Now.ToString();

            //Assert
            Assert.AreEqual(result, now);
        }
    }
}
