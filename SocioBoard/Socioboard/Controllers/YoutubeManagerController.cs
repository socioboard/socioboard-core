using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    public class YoutubeManagerController : Controller
    {
        //
        // GET: /YoutubeManager/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Youtube()
        {
            string AddYoutubeAccount = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Domain.Socioboard.Domain.User checkuserexist = (Domain.Socioboard.Domain.User)Session["User"];
            string code = (String)Request.QueryString["code"];
            Api.Youtube.Youtube apiobjYoutube = new Api.Youtube.Youtube();
            Api.User.User ApiobjUser = new Api.User.User();
            if (Session["googlepluslogin"] != null)
            {
                objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(apiobjYoutube.GoogleLogin(code), typeof(Domain.Socioboard.Domain.User)));
                checkuserexist = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(objUser.EmailId.ToString()), typeof(Domain.Socioboard.Domain.User)));
                if (checkuserexist != null)
                {
                    Session["User"] = checkuserexist;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["User"] = objUser;
                    return RedirectToAction("Registration", "Index");
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
                Session["googlepluslogin"] = op;
                googleurl = "https://accounts.google.com/o/oauth2/auth?client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline";
            }
            else
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
                                //string redirect = "https://accounts.google.com/o/oauth2/auth?client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline";
                                Response.Redirect("https://accounts.google.com/o/oauth2/auth?client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
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
            }
            return Content(googleurl);
        }




    }
}
