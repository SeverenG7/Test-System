using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CommonController : Controller
    {
        #region Init services

        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IThemeService _themeService;
        private readonly IResultService _resultService;

        public CommonController
            (ITestService testService, IQuestionService questionService, IThemeService themeService , IResultService resultService)
        {
            _testService = testService;
            _questionService = questionService;         
            _themeService = themeService;
            _resultService = resultService;
        }

        public CommonController()
        {

        }

        #endregion     

        [HttpGet]
        public ActionResult CommonTables()
        {
            return View();
        }

        public ActionResult GetTableTests(int? pageTests)
        {
            int pageSize = 5;
            int pageNumber = (pageTests ?? 1);
            
            return PartialView(_testService.GetTests().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetTableQuestions(int? pageQuestions)
        {
            int pageSize = 5;
            int pageNumber = (pageQuestions ?? 1);

            IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
            List<QuestionViewModel> questionViews = new List<QuestionViewModel>();

            foreach (QuestionDTO question in questionDTOs)
            {
                questionViews.Add(new QuestionViewModel
                {
                    IdQuestion = question.IdQuestion,
                    QuestionText = question.QuestionText,
                    Difficult = question.Difficult,
                    Theme = "Some theme",
                    Score = 0
                });

            }


            return PartialView(questionViews.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetTableResults(int? pageResults)
        {
            int pageSize = 5;
            int pageNumber = (pageResults ?? 1);

            return PartialView(_resultService.GetResults().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Some()
        {
            return View();
        }

    }
}