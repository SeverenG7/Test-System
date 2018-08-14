using System.Web.Mvc;
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
                    ViewBag.Layout = "~/Views/Shared/_UserLayout.cshtml";
                }
                if (HttpContext.User.IsInRole("admin"))
                {
                    ViewBag.Reference = "Common/CommonTables";
                    ViewBag.Layout = "~/Views/Shared/_MyLayout.cshtml";
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
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View();
        }

        public ActionResult InternalServerErrorPage()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();
        }
    }
}