using System;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using TestSystem.Web.Infrasrtuctre;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ITestPassService _testPassService;
        private readonly IResultService _resultService;
        private readonly IQuestionService _questionService;


        public UserController(IUserService userService, ITestPassService testPassService,
            IResultService resultService, IQuestionService questionService)
        {
            _userService = userService;
            _testPassService = testPassService;
            _resultService = resultService;
            _questionService = questionService;
        }

        [TestPassing]
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

        [TestPassing]
        public ActionResult StartTest(int IdResult)
        {
            ViewBag.TestName = _resultService.GetResult(IdResult).Test.TestName;
            return Redirect("TestPassing?idQuestion=" + _testPassService.StartTest(IdResult).IdQuestion.ToString());
        }

        [TestNoPassing]
        public ActionResult TestPassing(int IdQuestion)
        {
            OperationDetails details = _testPassService.GetCurrentTestState(IdQuestion);
            if (details.Value.ToString().Equals(""))
            {
                return Redirect("EndTest");
            }
            else
            {
                ViewBag.Time = Int32.Parse(details.Id);
                return View(details.Value);
            }
        }

        [HttpPost]
        [TestNoPassing]
        public ActionResult TestPassingPost(QuestionDto question)
        {
            QuestionDto questionDto = _testPassService.TestPassing(question).Value;
            if (questionDto != null)
            {
                return RedirectToAction("TestPassing", "User", new
                { questionDto.IdQuestion });
            }
            else
            {
                return RedirectToAction("EndTest", "Ending");
            }
        }


        public new RedirectToRouteResult RedirectToAction(string action, string controller, object routeValues)
        {
            return base.RedirectToAction(action, controller, routeValues);
        }


    }
}
