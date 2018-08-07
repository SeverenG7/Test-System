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
    [Authorize(Roles ="user")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ITestService _testService;
        private readonly IResultService _resultService;

        public UserController(IUserService userService , ITestService testService,
            IResultService resultService)
        {
            _userService = userService;
            _testService = testService;
            _resultService = resultService;
        }

        // GET: User
        public ActionResult MainMenu(int? id)
        {
            UserMainViewModel model = new UserMainViewModel();
            model.Results = _resultService.GetResults().
                Where(x => x.UserLogin == HttpContext.User.Identity.Name).ToList();

            if (id.HasValue)
            {
                model.Test = _resultService.GetResult(id.Value).Test;
                ViewBag.Result = id.Value;
            }
           ViewBag.Name = HttpContext.User.Identity.Name;
            return View(model);
        }

        // GET: User/Details/5
        public ActionResult StartTest(int? id)
        {
     
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
