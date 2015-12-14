using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.App_Start
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private string role = string.Empty;
        public CustomAuthorize() { }
        public CustomAuthorize(string UserRole)
        {
            this.role = UserRole;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (string.IsNullOrEmpty(role))
            {
                if (((httpContext.Session["User"] != null && httpContext.Session["access_token"] != null)) || httpContext.Session["fblogin"] != null || httpContext.Session["addfbaccount"] != null || httpContext.Session["googlepluslogin"] != null)
                {
                    authorize = true;
                }
            }
            else 
            {
                if (((httpContext.Session["User"] != null && httpContext.Session["access_token"] != null)))
                {
                    Domain.Socioboard.Domain.User user = (Domain.Socioboard.Domain.User)(httpContext.Session["User"]);
                    if (!string.IsNullOrEmpty(user.UserType) && user.UserType.Equals(role))
                    {
                        authorize = true;
                    }
                }
            }
           
            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }  
    }
}