using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(TestSystem.Web.App_Start.Startup))]

namespace TestSystem.Web.App_Start
{
    public class Startup
    {
        
        public void Configuration(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

    }
}