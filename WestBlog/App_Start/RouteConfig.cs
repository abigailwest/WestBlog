using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WestBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "NewSlug",
                url: "Blog/{slug}", //NEVER use a name here that matches a controller (blog)
                defaults: new { controller = "Posts", action = "Details", slug = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "ByCategory",
            //    url: "Blog/{category}",
            //    defaults: new { controller = "Posts", action = "Index", category = UrlParameter.Optional }
            //);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Posts", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
