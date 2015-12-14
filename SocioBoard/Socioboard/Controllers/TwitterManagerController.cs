using log4net;
using Newtonsoft.Json.Linq;
using Socioboard.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace Socioboard.Controllers
{
    //[Authorize]
    //[CustomAuthorize]
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
            var requestToken = (String)Request.QueryString["oauth_token"];
            var requestSecret = (String)Session["requestSecret"];
            var requestVerifier = (String)Request.QueryString["oauth_verifier"];
            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
            if ((string)Session["twitterlogin"] == "twitterlogin")
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Domain.Socioboard.Domain.User checkuserexist = (Domain.Socioboard.Domain.User)Session["User"];
                Api.User.User ApiobjUser = new Api.User.User();
                objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(apiobjTwitter.TwitterLogIn(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], requestToken, requestSecret, requestVerifier), typeof(Domain.Socioboard.Domain.User)));
                try
                {
                    checkuserexist = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoForSocialLogin(objUser.SocialLogin.ToString()), typeof(Domain.Socioboard.Domain.User)));
                    System.Web.Security.FormsAuthentication.SetAuthCookie(checkuserexist.UserName, false);
                }
                catch (Exception e) { }
                if (checkuserexist != null)
                {
                    Session["twitterlogin"] = null;
                    Session["User"] = checkuserexist;
                    int daysremaining = 0;

                    daysremaining = (checkuserexist.ExpiryDate.Date - DateTime.Now.Date).Days;
                    if (daysremaining > 0)
                    {
                        #region Count Used Accounts
                        try
                        {
                            Session["Paid_User"] = "Paid";
                            Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                            Session["ProfileCount"] = Convert.ToInt32(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        #endregion
                    }
                    else
                    {
                        Session["Paid_User"] = "Unpaid";
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    objUser.ActivationStatus = "1";
                    Session["User"] = objUser;
                    return RedirectToAction("Registration", "Index");
                }
            }
            else
            {
                try
                {
                    string AddTwitterAccount = string.Empty;
                    Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                    apiobjTwitter.Timeout = 120 * 1000;
                    //AddTwitterAccount = apiobjTwitter.AddTwitterAccount(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], objUser.Id.ToString(), Session["group"].ToString(), requestToken, requestSecret, requestVerifier);
                    Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = (Domain.Socioboard.Domain.TwitterAccount)new JavaScriptSerializer().Deserialize(apiobjTwitter.AddTwitterAccount(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], objUser.Id.ToString(), Session["group"].ToString(), requestToken, requestSecret, requestVerifier), typeof(Domain.Socioboard.Domain.TwitterAccount));
                    
                    //code to follow socioboard
                    if (Session["FollowTwitter"] != null && Session["FollowTwitter"].ToString().Equals("true")) 
                    {
                        Session["FollowTwitter"] = null;
                        Socioboard.Helper.TwitterHelper.FollowAccount(objTwitterAccount.OAuthToken, objTwitterAccount.OAuthSecret, "Socioboard", "");
                    }
                    //follow socioboard code end
                    
                    AddTwitterAccount = objTwitterAccount.TwitterUserId;
                    Session["SocialManagerInfo"] = AddTwitterAccount;

                    //To enable the Tweet Pop up
                    TempData["IsTwitterAccountAdded"] = 1;
                    TempData["TwitterAccount"] = objTwitterAccount;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }

                if (Session["SocialManagerInfo"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateTwitter(string op, string follow)
        {
            if (!string.IsNullOrEmpty(follow) && follow.Equals("true")) 
            {
                Session["FollowTwitter"] = "true";
            }
            logger.Error("Abhay twittermanager");
            try
            {
                try
                {
                    if (op != null)
                    {
                        if (op == "twitterlogin")
                        {
                            Session["twitterlogin"] = op;
                            Api.Twitter.Twitter apiobjTwitter = new Api.Twitter.Twitter();
                            string TwitterUrl = apiobjTwitter.GetTwitterRedirectUrl(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                            string str = TwitterUrl.Split('~')[0].ToString();
                            Session["requestSecret"] = TwitterUrl.Split('~')[1].ToString();
                            //Response.Redirect(TwitterUrl.Split('~')[0].ToString(), true);
                            return Content(TwitterUrl.Split('~')[0].ToString());
                        }
                    }
                    else
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
