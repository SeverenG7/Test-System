﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;
using System.Net;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ITestService _testService;
        private readonly IResultService _resultService;

        public UserController(IUserService userService , ITestService testService,
            IResultService resultService)
        {
            _userService = userService;
            _testService = testService;
            _resultService = resultService;
        }

        public ActionResult MainMenu(int? id)
        {
            UserMainViewModel model = new UserMainViewModel();
            
            model.Results = _resultService.GetResults().
                Where(x => x.UserInfo.IdUserInfo == _userService.FindIdUser(HttpContext.User.Identity.Name)).
                ToList();

            if (id.HasValue)
            {
                model.Test = _resultService.GetResult(id.Value).Test;
                ViewBag.Result = id.Value;
            }
           ViewBag.Name = HttpContext.User.Identity.Name;
            return View(model);
        }

        public ActionResult StartTest(int? idTest)
        {

            return View();
        }
     
    }
}
