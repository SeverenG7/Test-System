using System.Collections.Generic;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using System;
using System.Net;
using System.Linq;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : Controller
    {
        #region Init services

        private readonly ITestService _testService;
        private readonly IThemeService _themeService;
        private readonly IQuestionService _questionService;

        public TestController
            (ITestService testService, IThemeService themeService, IQuestionService questionService)
        {
            _testService = testService;
            _themeService = themeService;
            _questionService = questionService;

        }

        #endregion


        [HttpGet]
        public ActionResult GetInfoTest
          (int? IdTheme, string difficult, int? IdTest, int? IdQuestion, string search)
        {
            FiltrationViewModel viewTests = new FiltrationViewModel();

            IEnumerable<TestDto> tests = _testService.GetTests().
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

            List<ThemeDto> themes = _themeService.GetAll().ToList();
            themes.Insert(0, new ThemeDto() { IdTheme = 0, ThemeName = "All" });
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
                if (viewTests.Questions == null)
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
        public ActionResult CreateNewTest()
        {
            TestCreateViewModel model = new TestCreateViewModel
            {
                Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName")
            };

            IEnumerable<QuestionDto> questionDTOs = _questionService.GetQuestions();
            model.Questions = new List<QuestionForTestViewModel>();

            foreach (QuestionDto question in questionDTOs)
            {
                model.Questions.Add(new QuestionForTestViewModel
                {
                    IdQuestion = question.IdQuestion,
                    QuestionText = question.QuestionText,
                    Difficult = question.Difficult,
                    Chosen = false
                });

            }

            return View(model);
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult CreateNewTest(TestCreateViewModel model)
        {
            List<QuestionDto> questions = new List<QuestionDto>();

            foreach (QuestionForTestViewModel question in model.Questions)
            {
                if (question.Chosen)
                {
                    questions.Add(_questionService.GetQuestion(question.IdQuestion));
                }
            }

            TestDto test = new TestDto
            {
                TestName = model.TestName,
                TestDescription = model.TestDescription,
                IdTheme = Int32.Parse(model.selectedTheme),
                Difficult = model.selectedDifficult,
                CreateDate = DateTime.Now,
                Questions = questions,
                QuestionsNumber = questions.Count
            };

            _testService.CreateTest(test);

            return RedirectToAction("GetInfoTest", "Test");

        }

        public ActionResult DeleteTest(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (_testService.GetTest(id.Value) == null)
            {
                return HttpNotFound();
            }
            _testService.RemoveTest(id.Value);
            return RedirectToAction("GetInfoTest", "Test");
        }

        [HttpGet]
        public ActionResult GenerateTest()
        {
            TestGenerateViewModel model = new TestGenerateViewModel
            {
                Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName")
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerateTest(TestGenerateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.Create)
                {
                    model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");

                    TestDto test = _testService.GenerateTest(model.selectedNumber, Int32.Parse(model.selectedTheme), model.selectedDifficult);
                    foreach (QuestionDto question in test.Questions)
                    {
                        model.Questions.Add(question);
                    }
                }

                else
                {
                    TestDto test = new TestDto
                    {
                        TestName = model.TestName,
                        TestDescription = model.TestDescription,
                        IdTheme = Int32.Parse(model.selectedTheme),
                        Difficult = model.selectedDifficult,
                        CreateDate = DateTime.Now,
                        Questions = model.Questions,
                        QuestionsNumber = model.Questions.Count
                    };

                    _testService.CreateTest(test);
                    return RedirectToAction("GetInfoTest", "Test");
                }
            }

            return View(model);

        }
    }
}
