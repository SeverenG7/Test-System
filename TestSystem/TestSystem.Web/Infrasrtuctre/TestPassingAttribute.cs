using System.Web.Mvc;
using System.Web;
using System;
using TestSystem.Web.Controllers;

namespace TestSystem.Web.Infrasrtuctre
{
    public class TestNoPassingAttribute : AuthorizeAttribute
    {
        public TestNoPassingAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return (httpContext.Application["Timer" + httpContext.User.Identity.Name] != null);
        }
    }


    public class TestPassingAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Application["Timer" + filterContext.HttpContext.User.Identity.Name] != null)
            {
                var controller = (UserController)filterContext.Controller;
                filterContext.Result = controller.RedirectToAction("TestPassing", "User", new
                {
                    IdQuestion =
                    filterContext.HttpContext.Application["Test" + filterContext.HttpContext.User.Identity.Name]
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}