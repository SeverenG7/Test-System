using System;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using TestSystem.Logic.Infrastructure;
using TestSystem.Logic.Services;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ITestPassService _testPassService;
        private readonly IResultService _resultService;
        private readonly IQuestionService _questionService;

        public UserController(IUserService userService , ITestPassService testPassService,
            IResultService resultService , IQuestionService questionService)
        {
            _userService = userService;
            _testPassService = testPassService;
            _resultService = resultService;
            _questionService = questionService;
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
            return Redirect("TestPassing?idQuestion="+ _testPassService.StartTest(IdResult).IdQuestion.ToString());
        }

        public ActionResult TestPassing(int IdQuestion)
        {
            TestPassService.TimerModule timer = (TestPassService.TimerModule) HttpContext.Application["Timer" + HttpContext.User.Identity.Name];
            ViewBag.Time = (int)Math.Round(timer.CurrentInterval().TotalSeconds);
            return View(_questionService.GetQuestion(IdQuestion));
        }

        [HttpPost]
        public ActionResult TestPassingPost(QuestionDto question)
        {
            QuestionDto questionDto = _testPassService.TestPassing(question);
            if (questionDto != null)
            {
                return Redirect("TestPassing?idQuestion=" + questionDto.IdQuestion.ToString());
            }
            else
            {
                return RedirectToAction("EndTest" ,"User");
            }
        }

        public ActionResult EndTest()
        {
            return View();
        }

    }
}
