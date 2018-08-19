using System.Web.Mvc;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
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

        #region Create/Edit Tests
        [HttpGet]
        public ActionResult CreateNewTest()
        {
            return View(_testService.GetCreateModel());
        }

        [HttpPost]
        public ActionResult CreateNewTest(TestCreateViewModel model,
             HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                _testService.CreateTest(model,file);
                return RedirectToAction("GetInfoTest", "Test");
            }
            else
            {
                model = _testService.GetCreateModel();
                return View(model);
            }

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


        //[HttpPost]
        //public ActionResult GenerateTest(TestGenerateViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!model.Create)
        //        {
        //            model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
        //            model.Questions.Clear();
        //            TestViewModel test = _testService.GenerateTest(model.selectedNumber, Int32.Parse(model.selectedTheme), model.selectedDifficult);
        //            foreach (Logic.QuestionViewModel question in test.Questions)
        //            {
        //                model.Questions.Add(question);
        //            }
        //        }

        //        else
        //        {
        //            TestViewModel test = new TestViewModel
        //            {
        //                TestName = model.TestName,
        //                TestDescription = model.TestDescription,
        //                IdTheme = Int32.Parse(model.selectedTheme),
        //                Difficult = model.selectedDifficult,
        //                CreateDate = DateTime.Now,
        //                Questions = model.Questions,
        //                QuestionsNumber = model.Questions.Count
        //            };

        //            _testService.CreateTest(test);
        //            return RedirectToAction("GetInfoTest", "Test");
        //        }
        //    }

        //    return View(model);

        //}

        #endregion

        #region Delete/Details Tests

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
        public ActionResult GetInfoTest
       (int? IdTheme, string difficult, int? IdQuestion, int? IdTest, string search,
       int? page)
        {
            return View(_commonService.FilterTests(IdTheme, difficult, IdQuestion, IdTest, search,
            page));
        }

        #endregion

        #region Utility methods
        [AllowAnonymous]
        public FileContentResult GetImage(int idTest)
        {
            TestViewModel test= _testService.GetTest(idTest);
            if (test.ImageMimeType != null)
            {
                return File(test.TestImage, test.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
