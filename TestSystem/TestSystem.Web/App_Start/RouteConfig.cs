﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TestSystem.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Enter", action = "EnterToSystem", id = UrlParameter.Optional }
            );
        }
    }
}
