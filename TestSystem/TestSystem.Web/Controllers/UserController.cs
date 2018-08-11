using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;
using System.Net;
using TestSystem.Logic.Infrastructure;
using TestSystem.Logic.Services;
using System.Threading;
using System.Runtime.Remoting.Lifetime;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ITestPassService _testPassService;
        private readonly IResultService _resultService;

        public UserController(IUserService userService , ITestPassService testPassService,
            IResultService resultService)
        {
            _userService = userService;
            _testPassService = testPassService;
            _resultService = resultService;
        }

        public ActionResult MainMenu(int? id)
        {
            OperationDetails details = _testPassService.UserPassingTest(HttpContext.User.Identity.Name);
            if (!details.Succedeed)
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
            else
            {
                TestPassService.TimerModule timer =(TestPassService.TimerModule) HttpContext.Application["Timer" + details.Value.ToString()];
                TimeSpan time =  timer.CurrentInterval();
                return View();
            }
        }

        public ActionResult StartTest(int IdResult)
        {
            QuestionDto question = _testPassService.StartTest(IdResult);
            OperationDetails details = _testPassService.UserPassingTest(HttpContext.User.Identity.Name);
            TestPassService.TimerModule timer = (TestPassService.TimerModule)HttpContext.Application["Timer" + details.Value.ToString()];
            TimeSpan time = timer.CurrentInterval();
            ViewBag.Time = time.Seconds;
            return View(question);
        }

        public ActionResult TestPassing(int IdResult)
        {
            //_testPassService.TestPassing(IdResult);
            TestPassService.TimerModule timer =(TestPassService.TimerModule)HttpContext.Application["Timer" + IdResult.ToString()];
            string so = "str";
            return View();
        }
     
    }
}
