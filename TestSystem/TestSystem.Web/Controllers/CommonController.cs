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

namespace TestSystem.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;

        public CommonController(ITestService testService, IQuestionService questionService)
        {
            _testService = testService;
            _questionService = questionService;
        }
        // GET: Common
        public async Task<ActionResult> CommonTables(int? page )
        {
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            List<TestViewModel> testsTable = await GetTestTable();
            
            List<QuestionViewModel> questionsTable = await GerQuestionTable();
            ViewBag.Tests = testsTable.ToPagedList(pageNumber,pageSize);
            ViewBag.Questions = questionsTable;
            return View();
        }

        public Task<List<TestViewModel>> GetTestTable()
        {
            return Task.Run(() =>
            {
                IEnumerable<TestDTO> testDTOs = _testService.GetTests();
                var mapper = new MapperConfiguration
                    (mcf => mcf.CreateMap<TestDTO, TestViewModel>()).CreateMapper();
                var tests = mapper.Map<IEnumerable<TestDTO>, List<TestViewModel>>(testDTOs);
                return tests;
            });
        }

        public Task<List<QuestionViewModel>> GerQuestionTable()
        {
            return Task.Run(() =>
            {
                IEnumerable<QuestionDTO> questionDTOs = _questionService.GetQuestions();
                var mapper = new MapperConfiguration
                (mcf => mcf.CreateMap<QuestionDTO, QuestionViewModel>()).CreateMapper();
                var questions = mapper.Map<IEnumerable<QuestionDTO>, List<QuestionViewModel>>(questionDTOs);
                return questions;
            });

        }
        

        // GET: Common/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Common/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Common/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Common/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Common/Edit/5
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

        // GET: Common/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Common/Delete/5
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
