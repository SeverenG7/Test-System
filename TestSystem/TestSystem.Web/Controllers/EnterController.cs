using System.Web.Mvc;

namespace TestSystem.Web.Controllers
{
    public class EnterController : Controller
    {
        public ActionResult EnterToSystem()
        {
            if (HttpContext.Request.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("admin"))
                {
                    return RedirectToAction("CommonTables", "Common");
                }

                if (HttpContext.User.IsInRole("user"))
                {
                    return RedirectToAction("MainMenu", "User");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return RedirectToAction("Login", "Account");
        }
    }
}