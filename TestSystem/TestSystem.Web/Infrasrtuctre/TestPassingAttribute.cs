using System.Web.Mvc;
using System.Web;

namespace TestSystem.Web.Infrasrtuctre
{
    public class TestPassingAttribute : AuthorizeAttribute
    {
        public TestPassingAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return (httpContext.Application["Timer" + httpContext.User.Identity.Name] == null);
        }
    }
}