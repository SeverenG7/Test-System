using System.Collections.Generic;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;

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
            model.Questions = new List<QuestionDTO>();
            model.Theme = new SelectList(_themeService.GetAll(), "IdTheme", "ThemeName");
            if (Session["IdQuestions"] != null)
            {
                foreach (int id in Session["IdQuestion"] as List<int>)
                {
                    model.Questions.Add(_questionService.GetQuestion(id));
                }
            }
            return View(model);
        }

        // POST: Test/Create
        [HttpPost]
        public ActionResult CreateNewTest(TestCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(model);
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

        [HttpGet]
        public ActionResult GetQuestions(int? pageQuestions)
        {

                int pageSize = 5;
                int pageNumber = (pageQuestions ?? 1);

                IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
                List<QuestionForTestViewModel> questionViews = new List<QuestionForTestViewModel>();

                foreach (QuestionDTO question in questionDTOs)
                {
                    questionViews.Add(new QuestionForTestViewModel
                    {
                        IdQuestion = question.IdQuestion,
                        QuestionText = question.QuestionText,
                        Difficult = question.Difficult,
                        Theme = "Some Theme",
                        Chosen = false
                    });

                }
                return PartialView(questionViews);
        }

        [HttpPost]
        public void GetQuestions(List<QuestionForTestViewModel> model)
        {
            List<int> listID = new List<int>();
            foreach (QuestionForTestViewModel question in model)
            {
                if (question.Chosen)
                {
                    listID.Add(question.IdQuestion);
                }
            }
            Session["IdQuestions"] = listID;
            RedirectToAction("CreateNewTest");
        }
    }
}
