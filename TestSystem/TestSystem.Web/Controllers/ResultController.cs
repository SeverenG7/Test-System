using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;
using AutoMapper;


namespace TestSystem.Web.Controllers
{
    public class ResultController : Controller
    {
        #region Init services

        private readonly IResultService _resultService;
        private readonly ITestService _testService;
        public ResultController(IResultService resultService , ITestService testService)
        {
            _resultService = resultService;
            _testService = testService;
        }

        #endregion
        [HttpGet]
        public ActionResult GetInfoResult(string IdUser , string search)
        {
            ResultViewModel model = new ResultViewModel();
            model.Users = _resultService.GetUsers(search);
            model.Results = _resultService.GetResultsById(IdUser);
            return View(model);
        }

        [HttpGet]
        public ActionResult GivePremission(string IdUser)
        {
            PremissionViewModel model = new PremissionViewModel();
            model.UserResult.IdUserInfo = IdUser;
            foreach (TestDto test in _testService.GetTests())
            {
                model.Tests.Add(new TestPremissionViewModel
                {
                    TestName = test.TestName, 
                    Difficult = test.Difficult,
                    IdTest = test.IdTest,
                    TestDescription = test.TestDescription,
                    Theme = test.Theme
                });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GivePremission(PremissionViewModel model)
        {
            _resultService.GivePremission( model.Tests.
                Where(x => x.Choosen == true).
                FirstOrDefault().IdTest , model.UserResult.IdUserInfo,
                model.UserResult.ResultDescription);
            return RedirectToAction("GetInfoResult" , model.UserResult.IdUserInfo);
        }
    }
}