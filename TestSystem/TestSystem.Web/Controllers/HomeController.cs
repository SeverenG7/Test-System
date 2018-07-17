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
        ITestService _testService;
        IQuestionService _questionService;
        public HomeController(ITestService testService)
        {
            _testService = testService;
        }
        public ActionResult Index()
        {
            //TestDTO testDTO = testService.GetTest(1);
          

            IEnumerable<TestDTO> testDTOs = _testService.GetTests();
            IEnumerable<QuestionDTO> questionDTOs = testDTOs.FirstOrDefault().Questions;
            var mapper = new MapperConfiguration(mapperConfig =>
            mapperConfig.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
            var tests = mapper.Map<IEnumerable<QuestionDTO>, List<QuestionViewModel>>(questionDTOs);
            
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