﻿using System.Web.Mvc;
using System.Net;

namespace TestSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFoundPage()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("user"))
                {
                    ViewBag.Reference ="User/MainMenu";
                    ViewBag.Layout = null;
                }
                if (HttpContext.User.IsInRole("admin"))
                {
                    ViewBag.Reference = "Common/CommonTables";
                    ViewBag.Layout = "~/Views/Shared/_MyLayout.cshtml";
                    ViewBag.Image = "~/Content/actionImages/notFoundError.jpg";
                }
            }
            else
            {
                ViewBag.Layuot = null;
            }


            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult ForbiddenPage()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("user"))
                {
                    ViewBag.Reference = "User/MainMenu";
                    ViewBag.Layout = null;
                }
                if (HttpContext.User.IsInRole("admin"))
                {
                    ViewBag.Reference = "Common/CommonTables";
                    ViewBag.Layout = "~/Views/Shared/_MyLayout.cshtml";
                    ViewBag.Image = "~/Content/actionImages/.jpg";
                }
            }
            else
            {
                ViewBag.Layuot = null;
            }

            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View();
        }

        public ActionResult InternalServerErrorPage()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("user"))
                {
                    ViewBag.Reference = "User/MainMenu";
                    ViewBag.Layout = null;
                }
                if (HttpContext.User.IsInRole("admin"))
                {
                    ViewBag.Reference = "Common/CommonTables";
                    ViewBag.Layout = "~/Views/Shared/_MyLayout.cshtml";
                    ViewBag.Image = "~/Content/actionImages/.jpg";
                }
            }
            else
            {
                ViewBag.Layuot = null;
            }

            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }
    }
}