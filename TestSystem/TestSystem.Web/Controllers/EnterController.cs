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
    public class EnterController : Controller
    {
        #region InitService

        private readonly IUserService _userService;

        public EnterController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion


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