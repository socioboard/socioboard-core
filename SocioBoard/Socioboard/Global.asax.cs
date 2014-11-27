using Socioboard.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Socioboard
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            AreaRegistration.RegisterAllAreas();

            ViewEngines.Engines.Clear();

            ViewEngines.Engines.Add(new CustomViewLocationRazorViewEngine());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
           // GlobalFilters.Filters.Add(new MyExpirePageActionFilterAttribute());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
        protected void Session_Start()
        {
            // Code that runs when a new session is started
            Session.Timeout = 60;
        }
        
    }
}