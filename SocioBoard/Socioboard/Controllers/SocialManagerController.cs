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
    public class SocialManagerController : BaseController
    {
        //
        // GET: /SocialManager/

        public ActionResult Facebook()
        {
            //Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            //string facebookcode = Request.QueryString["code"].ToString();
            //Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
            //string AddfacebookAccount = apiobjFacebook.AddFacebookAccount(facebookcode, ConfigurationManager.AppSettings["ClientId"], ConfigurationManager.AppSettings["RedirectUrl"], ConfigurationManager.AppSettings["ClientSecretKey"], objUser.Id.ToString(), Session["group"].ToString());
            //Session["SocialManagerInfo"] = AddfacebookAccount;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateFacebook()
        {
            try
            {
                try
                {
                    Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                    JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
                    int profilecount = (Int16)(Session["ProfileCount"]);
                    int totalaccount = (Int16)Session["TotalAccount"];
                    if (Convert.ToString(group["GroupName"]) == ConfigurationManager.AppSettings["DefaultGroupName"].ToString())
                    {
                        if (profilecount < totalaccount)
                        {
                            Session["fbSocial"] = "a";
                            string facebookurl = "http://www.facebook.com/dialog/oauth/?scope=user_friends,read_friendlists,publish_actions,publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                            Response.Redirect(facebookurl);
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
                Console.WriteLine(ex.Message);
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
