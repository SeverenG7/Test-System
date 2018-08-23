using System.Web.Mvc;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
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
            return View(_testService.GetCreateModel(null));
        }

        [HttpPost]
        public ActionResult CreateNewTest(TestCreateViewModel model,
             HttpPostedFileBase file)
        {
            if (ModelState.IsValid && !model.Questions.TrueForAll(x => x.Chosen == false))
            {
                _testService.CreateTest(model, file);
                return RedirectToAction("GetInfoTest", "Test");
            }
            else
            {
                if (!model.Questions.TrueForAll(x => x == null))
                {
                    ModelState.AddModelError("Questions[0].IdQuestion", "Test must contains questions!");
                }
                model = _testService.GetCreateModel(null);
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult EditTest(int id)
        {
            TestCreateViewModel model = _testService.GetCreateModel(id);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult EditTest(TestCreateViewModel model,
             HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                _testService.UpdateTest(model, file);
                return RedirectToAction("GetInfoTest", "Test", model.IdTest);
            }
            else
            {
                model = _testService.GetCreateModel(null);
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult GenerateTest()
        {
            return View(_testService.GetGenerateViewModel(null));
        }


        [HttpPost]
        public ActionResult GenerateTest(TestGenerateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.Create)
                {
                    return View(_testService.GenerateTest(model));
                }
                else
                {
                    _testService.GenerateTest(model);
                    return RedirectToAction("GetInfoTest", "Test");
                }
            }
            else
            {

                return View(model);
            }

        }

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
            TestViewModel test = _testService.GetTest(idTest);
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
