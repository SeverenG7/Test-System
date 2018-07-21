using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList.Mvc;
using PagedList;
using AutoMapper;

namespace TestSystem.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;

        public CommonController(ITestService testService, IQuestionService questionService)
        {
            _testService = testService;
            _questionService = questionService;
        }
    
        [HttpGet]
        public ActionResult CommonTables()
        {
           
            return View();
        }

        public ActionResult GetTableTests(int? pageTests)
        {
            int pageSize = 1;
            int pageNumber = (pageTests ?? 1);

            IEnumerable<TestDTO> testDTOs = _testService.GetTests();
            var map = new MapperConfiguration
                (mcf => mcf.CreateMap<TestDTO, TestViewModel>()).CreateMapper();
            List<TestViewModel> testsTable = map.Map<IEnumerable<TestDTO>, List<TestViewModel>>(testDTOs);
            return PartialView(testsTable.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetTableQuestions(int? pageQuestions)
        {
            int pageSize = 1;
            int pageNumber = (pageQuestions ?? 1);

            IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
            var map = new MapperConfiguration
                (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
            List<QuestionViewModel> questionsTable = map.Map<IEnumerable<QuestionDTO>, List<QuestionViewModel>>(questionDTOs);
            return PartialView(questionsTable.ToPagedList(pageNumber, pageSize));
        }
    }
}
