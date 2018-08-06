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
    public class CommonController : Controller
    {
        #region Init services

        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;
        private readonly IThemeService _themeService;

        public CommonController
            (ITestService testService, IQuestionService questionService, IThemeService themeService)
        {
            _testService = testService;
            _questionService = questionService;         
            _themeService = themeService;
        }

        public CommonController()
        {

        }

        #endregion     

        [HttpGet]
        public ActionResult GetInfoTest
            (int? IdTheme , string difficult , int? IdTest, int? IdQuestion ,string search)
        {
            FiltrationViewModel viewTests = new FiltrationViewModel();

            IEnumerable<TestDTO> tests = _testService.GetTests().
                OrderBy(x => x.CreateDate);

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


        public ActionResult GetInfoQuestion
            (int? IdTheme , string difficult , int? IdQuestion , int? IdTest , string search)
        {
            FiltrationViewModel viewQuestions = new FiltrationViewModel();

            IEnumerable<QuestionDTO> questions = _questionService.GetQuestions().
                OrderBy(x => x.CreateDate);

            if (IdTheme.HasValue && IdTheme != 0)
            {
                questions = questions.Where(x => x.IdTheme == IdTheme);
            }

            if (!String.IsNullOrEmpty(difficult) && !difficult.Equals("All"))
            {
                questions = questions.Where(x => x.Difficult == difficult);
            }

            if (!String.IsNullOrEmpty(search))
            {
                questions = questions.Where(x => x.QuestionText.Contains(search));
            }

            List<ThemeDTO> themes = _themeService.GetAll().ToList();
            themes.Insert(0, new ThemeDTO() { IdTheme = 0, ThemeName = "All" });
            viewQuestions.Questions = questions.ToList();


            if (IdQuestion.HasValue)
            {
                if (viewQuestions.Questions == null)
                    ViewBag.IdQuestion = IdQuestion.Value;
                viewQuestions.Answers = _questionService.GetQuestions().
                    Where(x => x.IdQuestion == IdQuestion).
                    SingleOrDefault().
                    Answers;
            }
            if (IdTest.HasValue)
            {
                ViewBag.IdTest = IdTest.Value;
                viewQuestions.Questions = viewQuestions.Tests.
                    Where(x => x.IdTest == IdTest).
                    SingleOrDefault().
                    Questions.ToList();
            }

            viewQuestions.Themes = new SelectList(themes, "IdTheme", "ThemeName");

            return View(viewQuestions);

        }


        [HttpGet]
        public ActionResult CommonTables()
        {
            return View();
        }

        public ActionResult GetTableTests(int? pageTests)
        {
            int pageSize = 5;
            int pageNumber = (pageTests ?? 1);

            IEnumerable<TestDTO> testDTOs = _testService.GetTests();
            return PartialView(testDTOs.ToPagedList(pageNumber, pageSize));
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

        public ActionResult Some()
        {
            return View();
        }

    }
}