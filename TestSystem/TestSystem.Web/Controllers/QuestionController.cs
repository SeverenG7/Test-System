using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;
using System.Web;

namespace TestSystem.Web.Controllers
{
    public class QuestionController : Controller
    {

        #region Intit services

        private readonly IQuestionService _questionService;
        private readonly IThemeService _themeService;

        public QuestionController(IQuestionService questionService, IThemeService themeService)
        {
            _questionService = questionService;
            _themeService = themeService;
        }

        #endregion


        // GET: Question
        public ActionResult Index()
        {
            Response.Write("Well");
            return View();
        }

        // GET: Question/Details/5
        public ActionResult DetailsQuestion(int id)
        {
            return View();
        }

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

        // POST: Question/Create
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
                        if (ans.AnswerText != null)
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
      
                    //TempData["message"] = string.Format("Question success created",
                    //    question.QuestionText.Substring(0,19) + "...");

                    _questionService.CreateQuestion(question);

                    return RedirectToAction("Details");
                
        }
            
        

        

        // GET: Question/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Question/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Question/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Question/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

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
