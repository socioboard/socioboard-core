using log4net;
using Newtonsoft.Json.Linq;
using Socioboard.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class TwitterManagerController : BaseController
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
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateTwitter()
        {
            logger.Error("Abhay twittermanager");
            try
            {
                try
                {
                    Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                    JObject group = null;

                    try
                    {
                        logger.Error("GetGroupDetailsByGroupId before");
                        group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString()));
                        logger.Error("GetGroupDetailsByGroupId after");
                    }
                    catch (Exception ex)
                    {
                        logger.Error("GetGroupDetailsByGroupId Exception");
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }

                    logger.Error(Session["ProfileCount"]);
                    logger.Error(Session["TotalAccount"]);
                    int profilecount = 0;
                    int totalaccount = 0;
                    try
                    {
                        profilecount = (Int16)(Session["ProfileCount"]);
                        totalaccount = (Int16)Session["TotalAccount"];
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ex.Message : " + ex.Message);
                        logger.Error("ex.StackTrace : " + ex.StackTrace);
                    }
                    if (Convert.ToString(group["GroupName"]) == ConfigurationManager.AppSettings["DefaultGroupName"].ToString())
                    {
                        if (profilecount < totalaccount)
                        {
                            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
                            string TwitterUrl = apiobjTwitter.GetTwitterRedirectUrl(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                            string str = TwitterUrl.Split('~')[0].ToString();
                            Session["requestSecret"] = TwitterUrl.Split('~')[1].ToString();
                            Response.Redirect(TwitterUrl.Split('~')[0].ToString());
                        }
                        else if (profilecount == 0 || totalaccount == 0)
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
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
            //return View();
            return RedirectToAction("Index", "Home");
        }


    }
}
