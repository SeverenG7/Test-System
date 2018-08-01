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
        #region Init services

        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        private readonly IThemeService _themeService;

        public CommonController
            (ITestService testService, IQuestionService questionService, IAnswerService answerService, IThemeService themeService)
        {
            _testService = testService;
            _questionService = questionService;
            _answerService = answerService;
            _themeService = themeService;
        }

        #endregion     

        [HttpGet]
        public ActionResult GetInfoTest
            (int? IdTheme , string difficult , int? IdTest, int? IdQuestion ,string search)
        {
            FiltrationTestViewModel viewTests = new FiltrationTestViewModel();

            IEnumerable<TestDTO> tests = _testService.GetTests();

            if (IdTheme.HasValue && IdTheme != 0)
            {
               tests = tests.Where(x => x.IdTheme == IdTheme);
            }

            if (!String.IsNullOrEmpty(difficult) && !difficult.Equals("All"))
            {
               tests = tests.Where(x => x.Difficult == difficult);
            }

            if (!String.IsNullOrEmpty(search))
            {
                tests = tests.Where(x => x.TestName.Contains(search));
            }

            List<ThemeDTO> themes = _themeService.GetAll().ToList();
            themes.Insert(0, new ThemeDTO() { IdTheme = 0, ThemeName = "All" });
            viewTests.Tests = tests.ToList();

            if (IdTest.HasValue)
            {
                ViewBag.IdTest = IdTest.Value;
                viewTests.Questions = viewTests.Tests.
                    Where(x => x.IdTest == IdTest).
                    SingleOrDefault().
                    Questions.ToList();
            }

            if (IdQuestion.HasValue)
            {
                if(viewTests.Questions == null)
                ViewBag.IdQuestion = IdQuestion.Value;
                viewTests.Answers = _questionService.GetQuestions().
                    Where(x => x.IdQuestion == IdQuestion).
                    SingleOrDefault().
                    Answers;
            }

            viewTests.Themes = new SelectList(themes, "IdTheme", "ThemeName");

            return View(viewTests);
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
            return PartialView(testDTOs.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetTableQuestions(int? pageQuestions)
        {
            int pageSize = 5;
            int pageNumber = (pageQuestions ?? 1);

            IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
            return PartialView(questionDTOs.ToPagedList(pageNumber, pageSize));
        }

    }
}