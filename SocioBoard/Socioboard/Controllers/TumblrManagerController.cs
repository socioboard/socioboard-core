using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class TumblrManagerController : Controller
    {
        //
        // GET: /TumblrManager/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Tumblr()
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            string code = Request.QueryString["oauth_verifier"];
            Api.Tumblr.Tumblr ApiobjTumblr = new Api.Tumblr.Tumblr();
            string AddTumblrAccount = ApiobjTumblr.AddTumblrAccount(ConfigurationManager.AppSettings["TumblrClientKey"], ConfigurationManager.AppSettings["TumblrClientSec"], ConfigurationManager.AppSettings["TumblrCallBackURL"], objUser.Id.ToString(), Session["group"].ToString(), code);
            Session["SocialManagerInfo"] = AddTumblrAccount;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AuthenticateTumblr()
        {
            try
            {
                try
                {
                    Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                    JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
                    int profilecount = (Int16)(Session["ProfileCount"]);
                    int totalaccount = (Int16)Session["TotalAccount"];
                    if (Convert.ToString(group["GroupName"]) == "Socioboard")
                    {
                        if (profilecount < totalaccount)
                        {
                            Api.Tumblr.Tumblr ApiobjTumblr = new Api.Tumblr.Tumblr();
                            string redircturl = ApiobjTumblr.GetTumblrRedirectUrl(ConfigurationManager.AppSettings["TumblrClientKey"], ConfigurationManager.AppSettings["TumblrClientSec"], ConfigurationManager.AppSettings["TumblrCallBackURL"]);
                            Response.Redirect(redircturl);
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
            return View();
        }

    }
}
