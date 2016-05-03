using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using log4net;
using Socioboard.App_Start;
using System.Web.Security;
using System.Text.RegularExpressions;
using Socioboard.Helper;
using System.Threading.Tasks;

namespace Socioboard.Controllers
{
    public class YoutubeManagerController : BaseController
    {
        //
        // GET: /YoutubeManager/

        ILog logger = LogManager.GetLogger(typeof(YoutubeManagerController));
        Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();
        public int daysremaining = 0;
        
        public async Task<ActionResult> Youtube()
        {
            string AddYoutubeAccount = string.Empty;
            string AddGPlusAccount = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Domain.Socioboard.Domain.User checkuserexist = (Domain.Socioboard.Domain.User)Session["User"];
            string code = (String)Request.QueryString["code"];
            Api.Youtube.Youtube apiobjYoutube = new Api.Youtube.Youtube();
            Api.GooglePlus.GooglePlus ApiobjGooglePlus = new Api.GooglePlus.GooglePlus();
            Api.GoogleAnalytics.GoogleAnalytics ApiGoogleAnalytics = new Api.GoogleAnalytics.GoogleAnalytics();
            Api.User.User ApiobjUser = new Api.User.User();
            if (Session["googlepluslogin"] != "youtube")
            {
                if (!string.IsNullOrEmpty(code))
                {
                    if (Session["googlepluslogin"].ToString() == "googlepluslogin")
                    {
                        string Googleloginreturn = apiobjYoutube.GoogleLogin(code);
                        string[] arrgoogleloginreturn = Regex.Split(Googleloginreturn, "_#_");
                        objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(arrgoogleloginreturn[0], typeof(Domain.Socioboard.Domain.User)));
                        Session["AccesstokenFblogin"] = arrgoogleloginreturn[1];
                        Session["googlepluslogin"] = "googlelogin";
                        checkuserexist = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(objUser.EmailId.ToString()), typeof(Domain.Socioboard.Domain.User)));
                        if (checkuserexist != null)
                        {
                            objUser = checkuserexist;
                            Session["User"] = checkuserexist;
                            Session["group"] = await SBHelper.LoadGroups(objUser.Id);
                            Socioboard.Helper.apiClientProvider ac = new Socioboard.Helper.apiClientProvider(System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"] + "/token");
                            try
                            {
                                Dictionary<string, string> re = await ac.GetTokenDictionary(checkuserexist.EmailId, checkuserexist.Password);
                                Session["access_token"] = re["access_token"];
                            }
                            catch (Exception e)
                            {
                                return RedirectToAction("Index", "Home");
                            }

                            daysremaining = 0;
                            daysremaining = (checkuserexist.ExpiryDate.Date - DateTime.Now.Date).Days;
                            if (daysremaining > 0)
                            {
                                #region Count Used Accounts
                                try
                                {
                                    Session["Paid_User"] = "Paid";
                                    Session["ProfileCount"] = Convert.ToInt32(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                                    Session["TotalAccount"] = Convert.ToInt16(SBUtils.GetUserPackageProfileCount(objUser.AccountType));
                                }
                                catch (Exception ex)
                                {
                                    logger.Error(ex.StackTrace);
                                }
                                #endregion
                            }
                            else
                            {
                                Session["Paid_User"] = "Unpaid";
                            }
                            Response.Cookies.Add(FormsAuthentication.GetAuthCookie(objUser.UserName, true));
                            ApiobjUser.UpdateLastLoginTime(checkuserexist.Id.ToString());

                            HttpCookie myCookie = new HttpCookie("referal_url");
                            myCookie = Request.Cookies["referal_url"];
                            if (myCookie != null)
                            {
                                Response.Redirect(".." + myCookie.Value);
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
                    else if (Session["googlepluslogin"].ToString() == "gplugin")
                    {
                        string Googleloginreturn = apiobjYoutube.GoogleLogin(code);
                        string[] arrgoogleloginreturn = Regex.Split(Googleloginreturn, "_#_");
                        objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(arrgoogleloginreturn[0], typeof(Domain.Socioboard.Domain.User)));
                        Session["AccesstokenFblogin"] = arrgoogleloginreturn[1];
                      
                        checkuserexist = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(objUser.EmailId.ToString()), typeof(Domain.Socioboard.Domain.User)));
                        if (checkuserexist != null)
                        {
                            objUser = checkuserexist;
                            Session["User"] = checkuserexist;
                            FormsAuthentication.SetAuthCookie(objUser.UserName, false);
                            ApiobjUser.UpdateLastLoginTime(checkuserexist.Id.ToString());
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(objUser.EmailId))
                            {
                                string user = ApiobjUser.Register(objUser.EmailId, "", "Free", objUser.UserName, "1");
                                objUser = (Domain.Socioboard.Domain.User)new JavaScriptSerializer().Deserialize(user, typeof(Domain.Socioboard.Domain.User));
                                Session["User"] = objUser;
                            }
                            else {
                                return RedirectToAction("Index", "Index", new { hint = "plugin" });
                            }
                        }
                        Session["group"] = await SBHelper.LoadGroups(objUser.Id);
                        Socioboard.Helper.apiClientProvider ac = new Socioboard.Helper.apiClientProvider(System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"] + "/token");
                        try
                        {
                            Dictionary<string, string> re = await ac.GetTokenDictionary(checkuserexist.EmailId, checkuserexist.Password);
                            Session["access_token"] = re["access_token"];
                        }
                        catch (Exception e)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        daysremaining = 0;
                        try
                        {
                            daysremaining = (checkuserexist.ExpiryDate.Date - DateTime.Now.Date).Days;
                        }
                        catch (Exception ex)
                        {
                        }
                        #region Count Used Accounts
                        try
                        {
                            Session["ProfileCount"] = Convert.ToInt32(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                            Session["TotalAccount"] = Convert.ToInt16(SBUtils.GetUserPackageProfileCount(objUser.AccountType));
                        }
                        catch (Exception ex)
                        {
                            Session["ProfileCount"] = 0;
                            Session["TotalAccount"] = 0;
                        }
                        #endregion
                        if (daysremaining > 0)
                        {
                            Session["Paid_User"] = "Paid";
                        }
                        else
                        {
                            Session["Paid_User"] = "Unpaid";
                        }
                        Session["googlepluslogin"] = "googlelogin";
                        return RedirectToAction("Index", "Home", new { hint = "plugin" });
                    }
                    else if (Session["googlepluslogin"].ToString() == "gplus")
                    {
                        AddGPlusAccount = ApiobjGooglePlus.AddGPlusAccount(ConfigurationManager.AppSettings["YtconsumerKey"], ConfigurationManager.AppSettings["YtconsumerSecret"], ConfigurationManager.AppSettings["Ytredirect_uri"], objUser.Id.ToString(), Session["group"].ToString(), code);
                        if (AddGPlusAccount == "Refresh Token Not Found")
                        {
                            AuthenticateYoutube(Session["googlepluslogin"].ToString());
                        }
                        else
                        {
                            Session["SocialManagerInfo"] = AddGPlusAccount;
                        }
                    }
                    else if (Session["googlepluslogin"].ToString() == "ga")
                    {
                        string ret_data = ApiGoogleAnalytics.GetAnalyticsProfile(code);

                        if (ret_data == "Refresh Token Not Found")
                        {
                            AuthenticateYoutube(Session["googlepluslogin"].ToString());
                        }
                        else
                        {
                            List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles> lstGoogleAnalyticsProfiles = (List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles>)new JavaScriptSerializer().Deserialize(ret_data, typeof(List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles>));
                            Session["GAProfiles"] = lstGoogleAnalyticsProfiles;
                        }
                        
                        return RedirectToAction("Index", "Home", new { hint = "gaprofile" });
                    }
                }
                else
                {
                    if (Session["googlepluslogin"].ToString() == "gplugin")
                    {
                        Session["googlepluslogin"] = null;
                        return RedirectToAction("Index", "Index", new { hint = "plugin" });
                    }
                    
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                try
                {
                    AddYoutubeAccount = apiobjYoutube.AddYoutubeAccount(ConfigurationManager.AppSettings["YtconsumerKey"], ConfigurationManager.AppSettings["YtconsumerSecret"], ConfigurationManager.AppSettings["Ytredirect_uri"], objUser.Id.ToString(), Session["group"].ToString(), code);
                    if (AddYoutubeAccount == "Refresh Token Not Found")
                    {
                        AuthenticateYoutube("");
                    }
                    else
                    {
                        Session["SocialManagerInfo"] = AddYoutubeAccount;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateYoutube(string op)
        {
            string googleurl = string.Empty;
            if (!string.IsNullOrEmpty(op))
            {
                if (op == "googlepluslogin")
                {
                    Session["googlepluslogin"] = op;
                    googleurl = "https://accounts.google.com/o/oauth2/auth?client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline";
                }
                else if (op == "gplugin")
                {
                    Session["googlepluslogin"] = op;
                    googleurl = "https://accounts.google.com/o/oauth2/auth?client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline";
                }
                else if (op == "gplus")
                {
                    Session["googlepluslogin"] = op;
                    try
                    {
                        //Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                        //JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
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
                        {
                            if (profilecount < totalaccount)
                            {
                                Response.Redirect("https://accounts.google.com/o/oauth2/auth?approval_prompt=force&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/plus.login+https://www.googleapis.com/auth/plus.profile.emails.read+https://www.googleapis.com/auth/plus.stream.write+https://www.googleapis.com/auth/plus.stream.read+https://www.googleapis.com/auth/plus.circles.read+https://www.googleapis.com/auth/plus.circles.write+https://www.googleapis.com/auth/plus.profiles.read+https://www.googleapis.com/auth/plus.media.upload+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
                            }
                            else if (profilecount == 0 || totalaccount == 0)
                            {
                                Response.Redirect("https://accounts.google.com/o/oauth2/auth?approval_prompt=force&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/plus.login+https://www.googleapis.com/auth/plus.profile.emails.read+https://www.googleapis.com/auth/plus.stream.write+https://www.googleapis.com/auth/plus.stream.read+https://www.googleapis.com/auth/plus.circles.read+https://www.googleapis.com/auth/plus.circles.write+https://www.googleapis.com/auth/plus.profiles.read+https://www.googleapis.com/auth/plus.media.upload+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else if (op == "ga")
                {

                    try
                    {
                        //Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                        //JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
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

                        Session["googlepluslogin"] = op;
                        if (profilecount < totalaccount)
                        {

                            Response.Redirect("https://accounts.google.com/o/oauth2/auth?approval_prompt=force&access_type=offline&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/analytics+https://www.googleapis.com/auth/analytics.edit+https://www.googleapis.com/auth/analytics.readonly&response_type=code");
                        }
                        else if (profilecount == 0 || totalaccount == 0)
                        {
                            Response.Redirect("https://accounts.google.com/o/oauth2/auth?approval_prompt=force&access_type=offline&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/analytics+https://www.googleapis.com/auth/analytics.edit+https://www.googleapis.com/auth/analytics.readonly&response_type=code");
                        }
                        else {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }

            else
            {
                try
                {
                    try
                    {
                        Session["googlepluslogin"] = "youtube";
                        //Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                        //JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
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
                        {
                            if (profilecount < totalaccount)
                            {
                                //string redirect = "https://accounts.google.com/o/oauth2/auth?approval_prompt=force&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline";
                                Response.Redirect("https://accounts.google.com/o/oauth2/auth?approval_prompt=force&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
                            }
                            else if (profilecount == 0 || totalaccount == 0)
                            {
                                Response.Redirect("https://accounts.google.com/o/oauth2/auth?approval_prompt=force&client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
                            }
                            else
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return Content(googleurl);
        }




    }
}
