using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSystem.Logic.DataTransferObjects;
using TestSystem.Logic.Interfaces;
using TestSystem.Web.Models;
using AutoMapper;

namespace TestSystem.Web.Controllers
{
    public class CommonController : Controller
    {
        private readonly ITestService _testService;
        private readonly Iq

        public CommonController(ITestService testService)
        {
            _testService = testService;
        }
        // GET: Common
        public ActionResult CommonTables()
        {
            IEnumerable<TestDTO> 

            return View();
        }

        // GET: Common/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Common/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Common/Create
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

        // GET: Common/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Common/Edit/5
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

        // GET: Common/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Common/Delete/5
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
