using System;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.Interfaces;
using TestSystem.Logic.ViewModel;
using System.Net;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ThemeController : Controller
    {

        #region Intit services

        private readonly IThemeService _themeService;

        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        #endregion

        #region Create/Edit Themes

        [HttpGet]
        public ActionResult CreateNewTheme()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewTheme(ThemeCreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                _themeService.Create(model);
                return RedirectToAction("AboutThemes");
            }
            else
            {
                return View(model);
            }
        }

        #endregion

        #region Delete/Details Themes
        [HttpPost]
        public ActionResult DeleteTheme(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (_themeService.Get(id.Value) == null)
            {
                return HttpNotFound();
            }

            _themeService.Remove(id.Value);
            return RedirectToAction("AboutThemes");
        }

        [HttpGet]
        public ActionResult AboutThemes(int? IdTheme, string search)
        {
            if (IdTheme.HasValue)
            {
                ViewBag.IdTheme = IdTheme.Value;
            }
            return View(_themeService.AboutThemes(IdTheme, search));
        }
        #endregion
    }
}
