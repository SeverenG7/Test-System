using System.Collections.Generic;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using System;
using System.Net;
using System.Web;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : Controller
    {
        #region Init services

        private readonly ITestService _testService;
        private readonly IThemeService _themeService;
        private readonly IQuestionService _questionService;
        private readonly ICommonService _commonService;

        public TestController
            (ITestService testService, IThemeService themeService, IQuestionService questionService,
            ICommonService commonService)
        {
            _testService = testService;
            _themeService = themeService;
            _questionService = questionService;
            _commonService = commonService;
        }

        #endregion


        [HttpGet]
        public ActionResult GetInfoTest
          (int? IdTheme, string difficult, int? IdQuestion, int? IdTest, string search,
            int? page)
        {            
            return View(_commonService.FilterTests(IdTheme, difficult, IdQuestion, IdTest, search,
            page));
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
                    Theme = question.Theme.ThemeName,
                    Chosen = false
                });

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewTest(TestCreateViewModel model,
             HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
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
                    QuestionsNumber = questions.Count,
                    Time = new TimeSpan(0, model.selectedTime, 0)
                };

                if (image != null)
                {
                    test.ImageMimeType = image.ContentType;
                    test.TestImage = new byte[image.ContentLength];
                    image.InputStream.Read(test.TestImage, 0, image.ContentLength);
                }
                _testService.CreateTest(test);

                return RedirectToAction("GetInfoTest", "Test");
            }
            else
            {

                model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
               

                IEnumerable<QuestionDto> questionDTOs = _questionService.GetQuestions();
                model.Questions = new List<QuestionForTestViewModel>();

                foreach (QuestionDto question in questionDTOs)
                {
                    model.Questions.Add(new QuestionForTestViewModel
                    {
                        IdQuestion = question.IdQuestion,
                        QuestionText = question.QuestionText,
                        Difficult = question.Difficult,
                        Theme = question.Theme.ThemeName,
                        Chosen = false
                    });

                }

                return View(model);
            }

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


        public FileContentResult GetImage(int idTest)
        {
            TestDto test= _testService.GetTest(idTest);
            if (test != null)
            {
                return File(test.TestImage, test.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
