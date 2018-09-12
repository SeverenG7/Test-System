using System.Net;
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
        public ActionResult GivePremission(string IdUser,string sortOrder)
        {
            ViewBag.NameSortParm =  sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DateSortParm = sortOrder == "Difficult" ? "difficult_desc" : "Difficult";
            return View(_resultService.CreatePremissionModel(IdUser , sortOrder));
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

        [HttpGet]
        public ActionResult DeleteResult(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (_resultService.GetResult(id.Value) == null)
            {
                return HttpNotFound();
            }

            _resultService.Delete(id.Value);
            return RedirectToAction("GetInfoResult");
        }

        #endregion
    }
}