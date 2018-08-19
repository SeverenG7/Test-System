using System.Web.Mvc;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
using System.Web;
using System.Net;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class QuestionController : Controller
    {

        #region Intit services

        private readonly IQuestionService _questionService;
        private readonly ICommonService _commonService;

        public QuestionController(IQuestionService questionService, ICommonService commonService)
        {
            _questionService = questionService;
            _commonService = commonService;
        }

        #endregion

        #region Create/Edit Question

        [HttpGet]
        public ActionResult CreateNewQuestion()
        {
            return View(_questionService.GetCreationModel(null));
        }

        [HttpPost]
        public ActionResult CreateNewQuestion(QuestionCreateViewModel model,
            HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                _questionService.CreateQuestion(model, image);
                return RedirectToAction("GetInfoQuestion", "Question");
            }
            else
            {
                model = _questionService.GetCreationModel(null);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult EditQuestion(int IdQuestion)
        {
            QuestionCreateViewModel model = _questionService.GetCreationModel(IdQuestion);
            if (model != null)
            {
                return View(_questionService.GetCreationModel(IdQuestion));
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult EditQuestion(QuestionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _questionService.UpdateQuestion(model);
                return RedirectToAction("GetInfoQuestion", "Question", model.IdQuestion);
            }
            else
            {
                return View(model);
            }
        }


        public ActionResult DeleteFromTest(int? idQuestion, int? idTest)
        {
            if (idTest.HasValue && idQuestion.HasValue)
            {
                _questionService.DeleteQuestionFromTest(idQuestion.Value, idTest.Value);
                return RedirectToAction("GetInfoTest", "Test");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        #endregion

        #region Delete/Details Question

        [HttpGet]
        public ActionResult GetInfoQuestion
            (int? IdTheme, string difficult, int? IdQuestion, int? IdTest, string search,
            int? page, string QuestionText)
        {
            ViewBag.QuestionText = QuestionText;
            ViewBag.IdQuestion = IdQuestion;
            return View(_commonService.FilterQuestions(IdTheme, difficult, IdQuestion, IdTest, search,
            page));
        }


        [HttpGet]
        public ActionResult DeleteQuestion(int? IdQuestion)
        {
            if (!IdQuestion.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionViewModel questionDetails = _questionService.GetQuestion(IdQuestion.Value);
            if (questionDetails == null)
            {
                return HttpNotFound();
            }
            _questionService.RemoveQuestion(IdQuestion.Value);
            return RedirectToAction("GetInfoQuestion", "Question");
        }

        #endregion

        #region Utility methods
        public FileContentResult GetImage(int idQuestion)
        {
            QuestionViewModel question = _questionService.GetQuestion(idQuestion);
            if (question != null)
            {
                return File(question.QuestionImage, question.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
