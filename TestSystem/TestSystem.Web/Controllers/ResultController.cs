using System.Web.Mvc;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;

namespace TestSystem.Web.Controllers
{
    public class ResultController : Controller
    {
        #region Init services

        private readonly IResultService _resultService;
        public ResultController(IResultService resultService )
        {
            _resultService = resultService;
        }

        #endregion

        #region Actions

        [HttpGet]
        public ActionResult GetInfoResult(string IdUser , string search)
        {
            return View(_resultService.GetAllResults(search , IdUser));
        }

        [HttpGet]
        public ActionResult GivePremission(string IdUser)
        {
            return View(_resultService.CreatePremissionModel(IdUser));
        }

        [HttpPost]
        public ActionResult GivePremission(PremissionViewModel model)
        {
            _resultService.GivePremission(model);
            return RedirectToAction("GetInfoResult" ,"Result");
        }

        public ActionResult ResultInfo(int IdResult)
        {
            ResultInfoViewModel result = _resultService.GetResultInfo(IdResult);
            if (result != null)
            {
                return View(_resultService.GetResultInfo(IdResult));
            }
            else
            {
                TempData["Warning"] = "User not pass test - nothing see here";
                return RedirectToAction("GetInfoResult", "Result");            
            }
        }

        #endregion
    }
}