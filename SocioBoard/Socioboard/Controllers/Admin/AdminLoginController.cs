using Domain.Socioboard.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Socioboard.Controllers
{
    public class AdminLoginController : Controller
    {
        //
        // GET: /AdminLogin/

        ILog logger = LogManager.GetLogger(typeof(AdminLoginController));

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogin(string username, string password)
        {
            string returnmsg = string.Empty;
            Domain.Socioboard.Domain.Admin objAdmin = new Domain.Socioboard.Domain.Admin();
            string uname = Request.QueryString["username"].ToString();
            string pass = Request.QueryString["password"].ToString();
            Api.Admin.Admin ApiobjAdmin = new Api.Admin.Admin();
            string LoginData = ApiobjAdmin.Login(uname, pass);
            string str = LoginData.Replace("\"", string.Empty).Trim();
            if (str != "Not Exist")
            {
                objAdmin = (Domain.Socioboard.Domain.Admin)(new JavaScriptSerializer().Deserialize(LoginData, typeof(Domain.Socioboard.Domain.Admin)));
                FormsAuthentication.SetAuthCookie(objAdmin.UserName, false);
                Session["AdminProfile"] = objAdmin;
                returnmsg = "Admin";
            }
            else
            {
                objAdmin = null;
                returnmsg = "Invalid Email or Password";
                return Content(returnmsg);
            }
            return Content(returnmsg);
        }

        public ActionResult AdminLogout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
