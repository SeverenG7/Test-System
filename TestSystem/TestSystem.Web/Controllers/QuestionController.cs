using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using AutoMapper;
using System.Net;
using System.Data;
using TestSystem.Logic.MapGeneric;

namespace TestSystem.Web.Controllers
{
    public class QuestionController : Controller
    {

        private readonly IQuestionService _questionService;
        private readonly IThemeService _themeService;

        public QuestionController(IQuestionService questionService, IThemeService themeService)
        {
            _questionService = questionService;
            _themeService = themeService;
        }
        // GET: Question
        public ActionResult AllQuestions()
        {
            IEnumerable<QuestionDTO> questionsTable = _questionService.GetQuestions();
            return View(questionsTable);
        }

        // GET: Question/Details/5
        public ActionResult QuestionFullInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionDTO question = _questionService.GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        [HttpGet]
        public ActionResult CreateQuestion()
        {
            ThemesDropDownList();

            //string[] difficults = new string[3] { "Junior", "Middle", "Senoir" };
            QuestionDTO question = new QuestionDTO();

            question.Answers = new List<AnswerDTO>();
            for (int i = 0; i < 5; i++)
            {
                question.Answers.Add(new AnswerDTO());
            }
            return View(question);
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(SelectListItem ThemeName,
            [Bind(Include = "Answers , QuestionText , AnswerNumber , Score ")]QuestionDTO question,
            HttpPostedFileBase Image = null )
        {
            try
            {
                ThemesDropDownList();
                if (ModelState.IsValid )
                {
                  /*  foreach (AnswerDTO answer in question.Answers)
                    {
                        if (answer.AnswerText == null)
                        {
                            question.Answers.Remove(answer);
                        }
                    }*/

                    if (Image == null)
                    {
                        _questionService.CreateQuestion(question , ThemeName.Value  , "Junior");
                        return RedirectToAction("AllQuestions");
                    }
                    else
                    {
                        question.QuestionImage = new byte[Image.ContentLength];
                        Image.InputStream.Read(question.QuestionImage, 0, Image.ContentLength);
                        _questionService.CreateQuestion(question, ThemeName.Value, "Junior");
                         return RedirectToAction("AllQuestions");
                    }
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(question);
        }

        public ActionResult EditQuestion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionDTO question = _questionService.GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Answers = question.Answers;
            }

            return View(question);
        }

        [HttpPost, ActionName("EditQuestion")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPostQuestion(int? id)
        {

            QuestionDTO questionUpdate = _questionService.GetQuestion(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (TryUpdateModel(questionUpdate, "",
               new string[] { "QuestionText" , "AnswerNumber" , "Score" }))
            {
                try
                {
                    _questionService.UpdateQuestion(questionUpdate);

                    return RedirectToAction("AllQuestions");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(questionUpdate);
        }

        // GET: Question/Delete/5
        public ActionResult DeleteQuestion(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            QuestionDTO question = _questionService.GetQuestion(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuestion(int id)
        {
            try
            {
                _questionService.RemoveQuestion(id);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("AllQuestions");
        }

        #region Auxiliary methods

        private void ThemesDropDownList(object selectedTheme = null)
        {
            var themes = _themeService.GetAll().ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (ThemeDTO theme in themes)
            {
                items.Add(new SelectListItem { Text = theme.ThemeName , Value = theme.ThemeName});
            }
            ViewData["ThemeName"] = items; 
        }

        #endregion


    }
}
