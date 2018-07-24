using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
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
            int pageSize = 2;
            int pageNumber = (pageTests ?? 1);

            IEnumerable<TestDTO> testsTable = _testService.GetTests();
            return PartialView(testsTable.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetTableQuestions(int? pageQuestions)
        {
            int pageSize = 5;
            int pageNumber = (pageQuestions ?? 1);

            IEnumerable<QuestionDTO> questionsTable = _questionService.GetQuestions();
            return PartialView(questionsTable.ToPagedList(pageNumber, pageSize));
        }
    }
}
