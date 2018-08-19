using System.Web.Mvc;
using TestSystem.Logic.Interfaces;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommonController : Controller
    {
        #region Init services

        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IResultService _resultService;

        public CommonController
            (ITestService testService, IQuestionService questionService , IResultService resultService)
        {
            _testService = testService;
            _questionService = questionService;         
            _resultService = resultService;
        }

        public CommonController()
        {

        }

        #endregion

        #region Actions

        [HttpGet]
        public ActionResult CommonTables()
        {
            return View();
        }

        public ActionResult GetTableTests()
        {
            return PartialView(_testService.GetLastTests());
        }

        public ActionResult GetTableQuestions()
        {
            return PartialView(_questionService.GetLastQuestions());
        }

        public ActionResult GetTableResults()
        {
            return PartialView(_resultService.GetLastResults());
        }

        #endregion
    }
}