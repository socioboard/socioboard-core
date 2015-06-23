using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Socioboard
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapRoute(
            //    name: "Admin",
            //    url: "Admin/{action}/{id}",
            //    defaults: new { controller = "AdminLogin", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
             name: "CompanyDashboard",
              url: "Company/{q}",
              defaults: new { controller = "CompanyDashboard", action = "Company", q = UrlParameter.Optional }
              );
            routes.MapRoute(
               name: "Boards",
        url: "boards/{action}/{q}",
        defaults: new { controller = "Boards", action = "Company", q = UrlParameter.Optional }
        );
            routes.MapRoute(
       name: "Board",
        url: "board/{q}",
        defaults: new { controller = "Boards", action = "Company", q = UrlParameter.Optional }
        );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}