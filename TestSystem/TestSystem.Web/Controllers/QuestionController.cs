using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;
using System.Web;
using System.Net;
using System.Data;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class QuestionController : Controller
    {

        #region Intit services

        private readonly IQuestionService _questionService;
        private readonly IThemeService _themeService;
        private readonly ITestService _testService;

        public QuestionController(IQuestionService questionService, IThemeService themeService,
            ITestService testService)
        {
            _questionService = questionService;
            _themeService = themeService;
            _testService = testService;
        }

        #endregion
 

        #region Create/Edit Question

        [HttpGet]
        public ActionResult CreateNewQuestion()
        {
            QuestionCreateViewModel newQuestion = new QuestionCreateViewModel();
            newQuestion.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
            newQuestion.Answers = new List<AnswerDTO>();
            for (int i = 0; i < 5; i++)
            {
                newQuestion.Answers.Add(new AnswerDTO());
            }
            return View(newQuestion);
        }

        [HttpPost]
        public ActionResult CreateNewQuestion(QuestionCreateViewModel model,
            HttpPostedFileBase image = null)
        {
                       
                    QuestionDTO question = new QuestionDTO
                    {
                        QuestionText = model.QuestionText,
                        Difficult = model.selectedDifficult,
                        IdTheme = Int32.Parse(model.selectedTheme)
                    };

                    question.Answers = new List<AnswerDTO>();

                    foreach (AnswerDTO ans in model.Answers)
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
                        question.QuestionImage = new byte[image.ContentLength];
                        image.InputStream.Read(question.QuestionImage, 0, image.ContentLength);
                    }

                    _questionService.CreateQuestion(question);

                    return RedirectToAction("GetInfoQuestion" , "Common");
               
        }


        [HttpGet]
        public ActionResult EditQuestion(int id)
        {
            QuestionDTO updatingQuestion = _questionService.GetQuestion(id);
            return View(updatingQuestion);
        }

        [HttpPost]
        public ActionResult EditQuestion (QuestionDTO model )
        {

            var questionUpdate = _questionService.GetQuestion(model.IdQuestion);
            if (TryUpdateModel(questionUpdate, "",
       new string[] { "QuestionText" }))
            {
                questionUpdate.Answers = model.Answers;
                _questionService.UpdateQuestion(questionUpdate);
              return  RedirectToAction("GetInfoQuestion", "Common");
            }
                return View();
        }

        #endregion

        #region Delete/Details Question

        [HttpGet]
        public ActionResult DeleteQuestion(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionDTO questionDetails = _questionService.GetQuestion(id.Value);
            if (questionDetails == null)
            {
                return HttpNotFound();
            }

            _questionService.RemoveQuestion(id.Value);
            return RedirectToAction("GetInfoQuestion", "Common");
        }

        // POST: Question/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            try
            {
                
                
                return RedirectToAction("GetInfoQuestion" , "Common");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DetailsQuestion(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionDTO questionDetails = _questionService.GetQuestion(id.Value);
            if (questionDetails == null)
            {
                return HttpNotFound();
            }

            QuestionDetailsViewModel model = new QuestionDetailsViewModel(questionDetails);
            model.Theme = _themeService.Get(questionDetails.IdTheme).ThemeName;
            foreach (TestDTO test in questionDetails.Tests)
            {
                model.Tests.Add(test);
            }
            return View(model);
        }

        #endregion

        //public FileContentResult GetImage(int gameId)
        //{
        //    Game game = repository.Games
        //        .FirstOrDefault(g => g.GameId == gameId);

        //    if (game != null)
        //    {
        //        return File(game.ImageData, game.ImageMimeType);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
