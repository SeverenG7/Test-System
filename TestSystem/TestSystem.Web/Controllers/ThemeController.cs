using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using System.Data;
using System.Net;

namespace TestSystem.Web.Controllers
{
    public class ThemeController : Controller
    {
        private readonly IThemeService _themeService;
        public ThemeController(IThemeService themeService)
        {
            _themeService = themeService;
        }

        // GET: Theme
        public ActionResult ThemeTable()
        {
            return View(_themeService.GetAll());
        }

        // GET: Theme/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Theme/Create
        public ActionResult CreateTheme()
        {
            return View();
        }

        // POST: Theme/Create
        [HttpPost]
        public ActionResult CreateTheme([Bind(Exclude = "IdTheme")]ThemeDTO theme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _themeService.Create(theme);
                    return RedirectToAction("ThemeTable");
                }

            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(theme);
        }

        [HttpGet]
        public ActionResult EditTheme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThemeDTO theme = _themeService.Get(id);
            return View(theme);
        }

        // POST: Theme/Edit/5
        [HttpPost , ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            ThemeDTO theme;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                theme = _themeService.Get(id);
            }
            try
            {
                _themeService.Update(theme);
                return RedirectToAction("ThemeTable");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return RedirectToAction("ThemeTable");
        }

        // GET: Theme/Delete/5
        public ActionResult DeleteTheme(int? id , bool? saveChangesError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            return View(_themeService.Get(id));
        }

        // POST: Theme/Delete/5
        [HttpPost]
        public ActionResult DeleteTheme(int id)
        {
            try
            {
                _themeService.Remove(id);
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("ThemeTable");

        }
    }
}
