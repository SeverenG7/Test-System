using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using TestSystem.Web.Infrasrtuctre;
using TestSystem.Logic.Infrastructure;
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