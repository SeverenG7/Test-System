using System.Collections.Generic;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using System;

namespace TestSystem.Web.Controllers
{
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
        public ActionResult Delete(int id)
        {
            return View();
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

        //[HttpGet]
        //public ActionResult GetQuestions(int? pageQuestions)
        //{

        //        int pageSize = 5;
        //        int pageNumber = (pageQuestions ?? 1);

        //        IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
        //        List<QuestionForTestViewModel> questionViews = new List<QuestionForTestViewModel>();

        //        foreach (QuestionDTO question in questionDTOs)
        //        {
        //            questionViews.Add(new QuestionForTestViewModel
        //            {
        //                IdQuestion = question.IdQuestion,
        //                QuestionText = question.QuestionText,
        //                Difficult = question.Difficult,
        //                Theme = "Some Theme",
        //                Chosen = false
        //            });

        //        }
        //        return PartialView(questionViews);
        //}

        //[HttpPost]
        //public void GetQuestions(List<QuestionForTestViewModel> model)
        //{
        //    List<int> listID = new List<int>();
        //    foreach (QuestionForTestViewModel question in model)
        //    {
        //        if (question.Chosen)
        //        {
        //            listID.Add(question.IdQuestion);
        //        }
        //    }
        //    Session["IdQuestions"] = listID;
        //    RedirectToAction("CreateNewTest");
        //}
    }
}
