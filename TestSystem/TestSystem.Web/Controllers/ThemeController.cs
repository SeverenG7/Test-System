using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;

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
        public ActionResult Index()
        {
            return View();
        }

        // GET: Theme/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Theme/Create
        public ActionResult CreateNewTheme()
        {
            return View();
        }

        // POST: Theme/Create
        [HttpPost]
        public ActionResult CreateNewTheme(ThemeCreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ThemeDTO theme = new ThemeDTO
                    {
                        ThemeName = model.ThemeName,
                        Description = model.Description
                    };

                    _themeService.Create(theme);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(model);
        }

        // GET: Theme/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Theme/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Theme/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Theme/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
