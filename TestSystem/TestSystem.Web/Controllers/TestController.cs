﻿using System.Collections.Generic;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using System;
using System.Net;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class TestController : Controller
    {
        #region Init services

        private readonly ITestService _testService;
        private readonly IThemeService _themeService;
        private readonly IQuestionService _questionService;

        public TestController
            (ITestService testService , IThemeService themeService , IQuestionService questionService)
        {
            _testService = testService;
            _themeService = themeService;
            _questionService = questionService;

        }

        #endregion


        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateNewTest()
        {
            TestCreateViewModel model = new TestCreateViewModel();
            model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");

            IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
            model.Questions = new List<QuestionForTestViewModel>();

            foreach (QuestionDTO question in questionDTOs)
            {
                model.Questions.Add(new QuestionForTestViewModel
                {
                    IdQuestion = question.IdQuestion,
                    QuestionText = question.QuestionText,
                    Difficult = question.Difficult,
                    Theme = "Some Theme",
                    Chosen = false
                });

            }

            return View(model);
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult CreateNewTest(TestCreateViewModel model)
        {       
                    List<QuestionDTO> questions = new List<QuestionDTO>();

                    foreach (QuestionForTestViewModel question in model.Questions)
                    {
                        if (question.Chosen)
                        {
                            questions.Add(_questionService.GetQuestion(question.IdQuestion));
                        }
                    }

                    TestDTO test = new TestDTO
                    {
                        TestName = model.TestName,
                        TestDescription = model.TestDescription,
                        IdTheme = Int32.Parse(model.selectedTheme),
                        Difficult = model.selectedDifficult,
                        CreateDate = DateTime.Now,
                        Questions = questions,
                        QuestionsNumber = questions.Count
                    };

                     _testService.CreateTest(test);

                    return RedirectToAction("GetInfoTest" , "Common");    
          
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
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

        // GET: Test/Delete/5
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
            return RedirectToAction("GetInfoTest" , "Common");
        }

        // POST: Test/Delete/5
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

        [HttpGet]
        public ActionResult GenerateTest()
        {
            TestGenerateViewModel model = new TestGenerateViewModel();
            model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
            return View(model);
        }

        [HttpPost]
        public ActionResult GenerateTest(TestGenerateViewModel model)
        {
          
                model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
    
            TestDTO test =  _testService.GenerateTest(model.selectedNumber, Int32.Parse(model.selectedTheme), model.selectedDifficult);
            foreach (QuestionDTO question in test.Questions)
            {
                model.Questions.Add(question);
            }

            return View(model);
        }
    }
}
