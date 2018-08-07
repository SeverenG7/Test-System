using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("user"))
                {
                    ViewBag.Layout = "~/Views/Shared/_UserLayout.cshtml";
                }
                if (HttpContext.User.IsInRole("admin"))
                {
                    ViewBag.Layout = "~/Views/Shared/_MyLayout.cshtml";
                }
            }
            else
            {
                ViewBag.Layuot = null;
            }


            Response.StatusCode = 404;
            return View();
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }

        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}