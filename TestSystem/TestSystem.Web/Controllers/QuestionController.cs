using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
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
        private readonly IThemeService _themeService;

        public QuestionController(IQuestionService questionService, ICommonService commonService,
            IThemeService themeService)
        {
            _questionService = questionService;
            _commonService = commonService;
            _themeService = themeService;
        }

        #endregion


        #region Create/Edit Question

        [HttpGet]
        public ActionResult CreateNewQuestion()
        {
            QuestionCreateViewModel newQuestion = new QuestionCreateViewModel();
            newQuestion.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
            newQuestion.Answers = new List<AnswerDto>();
            for (int i = 0; i < 5; i++)
            {
                newQuestion.Answers.Add(new AnswerDto());
            }
            return View(newQuestion);
        }

        [HttpPost]
        public ActionResult CreateNewQuestion(QuestionCreateViewModel model,
            HttpPostedFileBase image = null)
        {
           
                QuestionDto question = new QuestionDto
                {
                    QuestionText = model.QuestionText,
                    Difficult = model.selectedDifficult,
                    IdTheme = Int32.Parse(model.selectedTheme)
                };

                question.Answers = new List<AnswerDto>();

                foreach (AnswerDto ans in model.Answers)
                {
                    if (!String.IsNullOrEmpty(ans.AnswerText))
                    {
                        question.Answers.Add(ans);
                    }
                }

                question.AnswerNumber = question.Answers.Count;
                question.CreateDate = DateTime.Now;

                if (image != null)
                {
                    question.ImageMimeType = image.ContentType;
                    question.QuestionImage = new byte[image.ContentLength];
                    image.InputStream.Read(question.QuestionImage, 0, image.ContentLength);
                }

                _questionService.CreateQuestion(question);

                return RedirectToAction("GetInfoQuestion", "Question");
            
            //else
            //{
            //    model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
            //    return View(model);
            //}

        }


        [HttpGet]
        public ActionResult EditQuestion(int IdQuestion)
        {
            QuestionDto updatingQuestion = _questionService.GetQuestion(IdQuestion);
            return View(updatingQuestion);
        }

        [HttpPost]
        public ActionResult EditQuestion(QuestionDto model)
        {

            var questionUpdate = _questionService.GetQuestion(model.IdQuestion);
            if (TryUpdateModel(questionUpdate, "",
       new string[] { "QuestionText" }))
            {
                questionUpdate.Answers = model.Answers;
                _questionService.UpdateQuestion(questionUpdate);
                return RedirectToAction("GetInfoQuestion", "Question", model.IdQuestion);
            }
            return View();
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
            int? page , string QuestionText)
        {
            ViewBag.QuestionText = QuestionText;
            ViewBag.IdQuestion = IdQuestion;
            return View(_commonService.FilterQuestions( IdTheme, difficult, IdQuestion,  IdTest, search,
            page ));
        }


        [HttpGet]
        public ActionResult DeleteQuestion(int? IdQuestion)
        {
            if (!IdQuestion.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionDto questionDetails = _questionService.GetQuestion(IdQuestion.Value);
            if (questionDetails == null)
            {
                return HttpNotFound();
            }

            _questionService.RemoveQuestion(IdQuestion.Value);
            return RedirectToAction("GetInfoQuestion", "Question");
        }

        #endregion

        public FileContentResult GetImage(int idQuestion)
        {
             QuestionDto question =  _questionService.GetQuestion(idQuestion);
            if (question != null)
            {
                return File(question.QuestionImage , question.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
