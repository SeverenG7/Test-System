using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using PagedList;
using System.Net;

namespace TestSystem.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ThemeController : Controller
    {
        private readonly IThemeService _themeService;
        private readonly ITestService _testService;
        private readonly IQuestionService _questionService;

        public ThemeController(IThemeService themeService , ITestService testService 
            , IQuestionService questionService)
        {
            _themeService = themeService;
            _testService = testService;
            _questionService = questionService;
        }



        // GET: Theme
        public ActionResult AboutThemes(int? IdTheme , string search)
        {
            _themeService.GetAll();
            ThemeAboutViewModel modelView = new ThemeAboutViewModel();
            modelView.Themes = _themeService.GetAll().ToList();

            if (!String.IsNullOrEmpty(search))
            {
                modelView.Themes = modelView.Themes.Where(x => x.ThemeName.Contains(search));
            }

            if (IdTheme.HasValue)
            {
                if (modelView.Themes.Where(x => x.IdTheme == IdTheme) != null)
                {
                    ViewBag.IdTheme = IdTheme.Value;

                    modelView.Tests = _testService.GetTests().
                        Where(x => x.IdTheme == IdTheme.Value).ToList();

                    modelView.Questions = _questionService.GetQuestions().
                        Where(x => x.IdTheme == IdTheme.Value).ToList();
                }
            }


            return View(modelView);
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
                    return RedirectToAction("AboutThemes");
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
    }
}
