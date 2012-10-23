using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Basta
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{id}",
                defaults: new { controller = "Pastie", action = "Details"}
            );

            routes.MapRoute(
                name: "Standard",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Pastie", action = "Create", id = UrlParameter.Optional }
            );
        }
    }
}