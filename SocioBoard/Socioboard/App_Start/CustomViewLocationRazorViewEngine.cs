using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;






namespace Socioboard.App_Start
{

    /// <summary>
    /// This class 
    /// </summary>
    public class CustomViewLocationRazorViewEngine : RazorViewEngine
    {
        string domainname = System.Configuration.ConfigurationManager.AppSettings["domain"];
        public CustomViewLocationRazorViewEngine()
        {
            ViewLocationFormats = new[] 
        {
            "~/Themes/"+domainname+"/Views/{1}/{0}.cshtml", "~/Themes/"+domainname+"/Views/{1}/{0}.vbhtml",
            "~/Themes/"+domainname+"/Views/Shared/{0}.cshtml", "~/Themes/"+domainname+"/Views/Shared/{0}.vbhtml",
             "~/Themes/"+domainname+"/Views/Admin/{1}/{0}.cshtml", "~/Themes/"+domainname+"/Views/Admin/{1}/{0}.vbhtml"
        };

            MasterLocationFormats = new[] 
        {
            "~/Themes/"+domainname+"/Views/{1}/{0}.cshtml", "~/Themes/"+domainname+"/Views/{1}/{0}.vbhtml",
            "~/Themes/"+domainname+"/Views/Shared/{0}.cshtml", "~/Themes/"+domainname+"/Views/Shared/{0}.vbhtml",
             "~/Themes/"+domainname+"/Views/Admin/{1}/{0}.cshtml", "~/Themes/"+domainname+"/Views/Admin/{1}/{0}.vbhtml"
        };

            PartialViewLocationFormats = new[] 
        {
            "~/Themes/"+domainname+"/Views/{1}/{0}.cshtml", "~/Themes/"+domainname+"/Views/{1}/{0}.vbhtml",
            "~/Themes/"+domainname+"/Views/Shared/{0}.cshtml", "~/Themes/"+domainname+"/Views/Shared/{0}.vbhtml",
             "~/Themes/"+domainname+"/Views/Admin/{1}/{0}.cshtml", "~/Themes/"+domainname+"/Views/Admin/{1}/{0}.vbhtml"
        };
        }
    }


}