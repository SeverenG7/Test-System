using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestSystem.Web.Controllers;
using TestSystem.Model.Models;
using TestSystem.DataProvider.ContextData;
using TestSystem.Logic.Services;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using System.Collections.Generic;
using Moq;
using System.Web.Mvc;

namespace TestSystem.UnitTests
{
    [TestClass]
    public class TestControllerMethod
    {
        [TestMethod]
        public void TestFromDbAccept()
        {
            var mock = new Mock<ITestService>();
            mock.Setup(a => a.GetTests()).Returns(new List<TestDTO>());
            HomeController homeController = new HomeController(mock.Object);

            var result = homeController.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

          
       
    }
}
