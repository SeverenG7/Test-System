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

namespace TestSystem.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        List<QuestionViewModel> questionsTable;

        public QuestionController( IQuestionService questionService)
        {
            _questionService = questionService;
        }
        // GET: Question
        public ActionResult Index()
        {
            IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
            var map = new MapperConfiguration
                (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
             questionsTable = map.Map<IEnumerable<QuestionDTO>, List<QuestionViewModel>>(questionDTOs);
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
                var map = new MapperConfiguration
                    (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
                question = map.Map<QuestionViewModel>(questionDTO);
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
        public ActionResult Create([Bind(Exclude  = "IdQuestion, IdProperty")]QuestionViewModel question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var map = new MapperConfiguration
                        (mcf => mcf.CreateMap<QuestionViewModel,QuestionDTO>()).CreateMapper();
                    QuestionDTO questionDTO = map.Map<QuestionDTO>(question);
                    _questionService.CreateQuestion(questionDTO);
                    return RedirectToAction("Index");
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
                var map = new MapperConfiguration
                    (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
                question = map.Map<QuestionViewModel>(questionDTO);
            }

            return View(question);
           
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            QuestionViewModel questionUpdate;
            QuestionDTO questionDTO = _questionService.GetQuestion(id);
            var map = new MapperConfiguration
                    (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            else
            {
                questionUpdate = map.Map<QuestionViewModel>(questionDTO);
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
    }
}
