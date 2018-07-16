using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using AutoMapper;



namespace TestSystem.Web.Controllers
{
    public class HomeController : Controller
    {

        ITestService testService;

        public HomeController(ITestService service)
        {
            testService = service;
        }
        public ActionResult Index()
        {
            //TestDTO testDTO = testService.GetTest(1);
           

            IEnumerable<TestDTO> testDTOs = testService.GetTests();
            var mapper = new MapperConfiguration(mapperConfig =>
            mapperConfig.CreateMap<TestDTO, TestViewModel>()).CreateMapper();
            var tests = mapper.Map<IEnumerable<TestDTO>, List<TestViewModel>>(testDTOs);
            
            return View(tests);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}