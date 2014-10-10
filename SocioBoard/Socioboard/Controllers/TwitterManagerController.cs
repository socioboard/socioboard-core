using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class TwitterManagerController : Controller
    {
        //
        // GET: /TwitterManager/

        ILog logger = LogManager.GetLogger(typeof(TwitterManagerController));

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Twitter()
        {
            string AddTwitterAccount = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            var requestToken = (String)Request.QueryString["oauth_token"];
            var requestSecret = (String)Session["requestSecret"];
            var requestVerifier = (String)Request.QueryString["oauth_verifier"];
            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
            try
            {
                AddTwitterAccount = apiobjTwitter.AddTwitterAccount(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], objUser.Id.ToString(), Session["group"].ToString(), requestToken, requestSecret, requestVerifier);
                Session["SocialManagerInfo"] = AddTwitterAccount;
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }

            if (Session["SocialManagerInfo"] == null)
            {
                return RedirectToAction("Index", "Default");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateTwitter()
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
                            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
                            string TwitterUrl = apiobjTwitter.GetTwitterRedirectUrl(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                            string str = TwitterUrl.Split('~')[0].ToString();
                            Session["requestSecret"] = TwitterUrl.Split('~')[1].ToString();
                            Response.Redirect(TwitterUrl.Split('~')[0].ToString());
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
