using System;
using System.Web.Mvc;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
using TestSystem.Web.Infrasrtuctre;
using TestSystem.Logic.Infrastructure;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        #region Init services

        private readonly IUserService _userService;
        private readonly ITestPassService _testPassService;
        private readonly IResultService _resultService;

        public UserController(IUserService userService, ITestPassService testPassService,
            IResultService resultService)
        {
            _userService = userService;
            _testPassService = testPassService;
            _resultService = resultService;
        }

        #endregion

        #region Actions

        [TestPassing]
        public ActionResult MainMenu(int? id)
        {
            if (id.HasValue)
            {
                ViewBag.Result = id.Value;
            }
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View(_userService.MainMenuUser(id));
        }

        [TestPassing]
        public ActionResult StartTest(int IdResult)
        {
            ViewBag.TestName = _resultService.GetResult(IdResult).Test.TestName;
            return RedirectToAction("TestPassing","User", new
            {
                _testPassService.StartTest(IdResult).IdQuestion
            });
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
        public ActionResult TestPassingPost(QuestionViewModel question)
        {
            QuestionViewModel questionDto = _testPassService.TestPassing(question).Value;
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

        #endregion

        #region Override method for filter

        public new RedirectToRouteResult RedirectToAction(string action, string controller, object routeValues)
        {
            return base.RedirectToAction(action, controller, routeValues);
        }

        #endregion
    }
}
