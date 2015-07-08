using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Api.Socioboard
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalFilters.Filters.Add(new Api.Socioboard.App_Start.CustomAuthorize());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AuthConfig.RegisterAuth();

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 120;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpContext context = app.Context;

            byte[] buffer = new byte[context.Request.InputStream.Length];
            context.Request.InputStream.Read(buffer, 0, buffer.Length);
            context.Request.InputStream.Position = 0;

            string soapMessage = System.Text.Encoding.ASCII.GetString(buffer);
            string accesstoken = string.Empty;
            try 
            {
               
                int pFrom = soapMessage.IndexOf("<access_token>") + "<access_token>".Length;
                int pTo = soapMessage.IndexOf("</access_token>") ;
                accesstoken = soapMessage.Substring(pFrom, pTo - pFrom);
            }
            catch (Exception ex) { }
            

            if (ReferenceEquals(null, HttpContext.Current.Request.Headers["Authorization"]))
            {
                var token = HttpContext.Current.Request.Params["access_token"];
                
                    HttpContext.Current.Request.Headers.Add("Authorization", "Bearer "+accesstoken);
               
            }

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}