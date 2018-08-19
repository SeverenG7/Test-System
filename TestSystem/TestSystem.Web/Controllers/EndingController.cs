using TestSystem.Logic.Interfaces;
using System.Web.Mvc;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class EndingController : Controller
    {
        private readonly ITestPassService _testPassService;

        public EndingController(ITestPassService testPassService)
        {
            _testPassService = testPassService;
        }
 
        public ActionResult EndTest()
        {
            return View(_testPassService.Results());
        }
    }
}