using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.App_Start
{
    public class CustomAuthorize : AuthorizeAttribute
    {
      
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if ((httpContext.Session["User"] != null && httpContext.Session["access_token"] != null) || httpContext.Session["fblogin"] != null) 
            {
                authorize = true;
            }
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }  
    }
}