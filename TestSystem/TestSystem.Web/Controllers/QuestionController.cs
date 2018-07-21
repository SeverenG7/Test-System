// Copyright (c) 2011 rubicon IT GmbH
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList.Mvc;
using PagedList;
using AutoMapper;
using System.Net;
using System.Data;
using TestSystem.Logic.MapGeneric;
using System.IO;

namespace TestSystem.Web.Controllers
{
    public class QuestionController : Controller , IMapGeneric<QuestionDTO,QuestionViewModel>
    {
        
        private readonly IQuestionService _questionService;
        private readonly IAnswerService _answerService;
        List<QuestionViewModel> questionsTable;

        public IMapper MapperToDb { get; set; }
        public IMapper MapperFromDb { get; set; }

        public QuestionController( IQuestionService questionService , IAnswerService answerService)
        {
            _questionService = questionService;
            _answerService = answerService;
            MapperFromDb = new MapperConfiguration
                (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
            MapperToDb = new MapperConfiguration
                        (mcf => mcf.CreateMap<QuestionViewModel, QuestionDTO>()).CreateMapper();
        }
        // GET: Question
        public ActionResult Index()
        {
            IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
             questionsTable = MapperFromDb.Map<IEnumerable<QuestionDTO>, List<QuestionViewModel>>(questionDTOs);
            return View(questionsTable);
        }

        // GET: Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionViewModel question;
            QuestionDTO questionDTO = _questionService.GetQuestion(id);
            if (questionDTO == null)
            {
                return HttpNotFound();
            }
            else
            {
                question = MapperFromDb.Map<QuestionViewModel>(questionDTO);
            }

            return View(question);
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude  = "IdQuestion, IdProperty")]QuestionViewModel question,
            HttpPostedFileBase Image = null)
        {
            try
            {
                if (ModelState.IsValid )
                {
                    if (Image == null)
                    {
                        QuestionDTO questionDTO = MapperToDb.Map<QuestionDTO>(question);
                        _questionService.CreateQuestion(questionDTO);
                        return RedirectToAction("Index");
                    }
                    else
                    {

                        question.QuestionImage = new byte[Image.ContentLength];
                        Image.InputStream.Read(question.QuestionImage, 0, Image.ContentLength);
                        QuestionDTO questionDTO = MapperToDb.Map<QuestionDTO>(question);
                        _questionService.CreateQuestion(questionDTO);
                         return RedirectToAction("Index");
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

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionViewModel question;
            QuestionDTO questionDTO = _questionService.GetQuestion(id);
            if (questionDTO == null)
            {
                return HttpNotFound();
            }
            else
            {
                question = MapperFromDb.Map<QuestionViewModel>(questionDTO);
            }

            return View(question);
           
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            QuestionViewModel questionUpdate;
            QuestionDTO questionDTO = _questionService.GetQuestion(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            else
            {
                questionUpdate = MapperFromDb.Map<QuestionViewModel>(questionDTO);
            }
            if (TryUpdateModel(questionDTO, "",
               new string[] { "QuestionText" , "AnswerNumber" , "Score" }))
            {
                try
                {
                    _questionService.UpdateQuestion(questionDTO);

                    return RedirectToAction("Index");
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
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            QuestionViewModel question;

            QuestionDTO questionDTO = _questionService.GetQuestion(id);
            if (questionDTO == null)
            {
                return HttpNotFound();
            }
            else
            {
                question = MapperFromDb.Map<QuestionViewModel>(questionDTO);
            }
            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
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
            return RedirectToAction("Index");
        }
    }
}
