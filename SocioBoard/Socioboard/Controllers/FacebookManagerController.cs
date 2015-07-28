using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Socioboard.Api.User;
using Facebook;
using System.Net;
using System.Web.Security;
using Socioboard.Helper;
using Socioboard.App_Start;
using System.Text.RegularExpressions;

namespace Socioboard.Controllers
{

    public class FacebookManagerController : BaseController
    {
        ILog logger = LogManager.GetLogger(typeof(FacebookManagerController));
        //
        // GET: /FacebookManager/

        //[CustomAuthorize]
        public ActionResult Index()
        {

            return View();
        }
        [CustomAuthorize]
        public ActionResult Facebook(string code)
        {
            if (Session["fblogin"] != null)
            {

                if ((string)Session["fblogin"] == "fblogin")
                {
                    Session["fblogin"] = null;
                    if (String.IsNullOrEmpty(code))
                    {
                        return RedirectToAction("Index", "Index");
                    }
                    Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                    Domain.Socioboard.Domain.User checkuserexist = (Domain.Socioboard.Domain.User)Session["User"];
                    // string facebookcode = Request.QueryString["code"].ToString();
                    string facebookcode = code;
                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    Api.User.User ApiobjUser = new Api.User.User();
                    string fbloginreturn = apiobjFacebook.FacebookLogin(code);
                    string[] arrfbloginreturn = Regex.Split(fbloginreturn, "_#_");

                    //objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(apiobjFacebook.FacebookLogin(code), typeof(Domain.Socioboard.Domain.User)));
                    objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(arrfbloginreturn[0], typeof(Domain.Socioboard.Domain.User)));
                    Session["AccesstokenFblogin"] = arrfbloginreturn[1];
                    Session["fblogin"] = "fblogin";
                    try
                    {
                        Response.Write("Facebook Returned email : " + objUser.EmailId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                    }

                    try
                    {
                        // objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(apiobjFacebook.FacebookLogin(code), typeof(Domain.Socioboard.Domain.User)));
                        checkuserexist = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(objUser.EmailId.ToString()), typeof(Domain.Socioboard.Domain.User)));
                        FormsAuthentication.SetAuthCookie(checkuserexist.UserName, false);
                    }
                    catch (Exception e)
                    {
                        checkuserexist = null;
                    }
                    if (checkuserexist != null)
                    {
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
                        ApiobjUser.UpdateLastLoginTime(checkuserexist.Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        objUser.ActivationStatus = "1";
                        Session["User"] = objUser;
                        return RedirectToAction("Registration", "Index");
                    }
                }
                else if ((string)Session["fblogin"] == "page")
                {
                    Session["fblogin"] = null;
                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    List<Domain.Socioboard.Domain.AddFacebookPage> lstAddFacebookPage = new List<Domain.Socioboard.Domain.AddFacebookPage>();
                    lstAddFacebookPage = (List<Domain.Socioboard.Domain.AddFacebookPage>)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFacebookPages(code), typeof(List<Domain.Socioboard.Domain.AddFacebookPage>)));
                    Session["fbpage"] = lstAddFacebookPage;
                    return RedirectToAction("Index", "Home", new { hint = "fbpage" });
                }
                else if ((string)Session["fblogin"] == "fbgroup")
                {
                    Session["fblogin"] = null;

                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    List<Domain.Socioboard.Domain.AddFacebookGroup> lstAddFacebookGroup = new List<Domain.Socioboard.Domain.AddFacebookGroup>();
                    lstAddFacebookGroup = (List<Domain.Socioboard.Domain.AddFacebookGroup>)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFacebookGroups(code), typeof(List<Domain.Socioboard.Domain.AddFacebookGroup>)));
                    Session["fbgrp"] = lstAddFacebookGroup;
                    return RedirectToAction("Index", "Home", new { hint = "fbgrp" });
                }
            }
            else
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string facebookcode = code;
                Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();

                apiobjFacebook.Timeout = 120 * 1000;

                //string AddfacebookAccount = apiobjFacebook.AddFacebookAccount(facebookcode, objUser.Id.ToString(), Session["group"].ToString());
                string AddfacebookAccount = "";
                Domain.Socioboard.Domain.FacebookAccount objfacebookAccount = new Domain.Socioboard.Domain.FacebookAccount();
                try
                {
                    var res_addFacebook = apiobjFacebook.AddFacebookAccount(facebookcode, objUser.Id.ToString(), Session["group"].ToString());
                    AddfacebookAccount = res_addFacebook;
                    try
                    {
                        objfacebookAccount = (Domain.Socioboard.Domain.FacebookAccount)new JavaScriptSerializer().Deserialize(res_addFacebook, typeof(Domain.Socioboard.Domain.FacebookAccount));
                        AddfacebookAccount = objfacebookAccount.FbUserId;
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
                catch (Exception)
                {
                    AddfacebookAccount = "issue_access_token";
                }

                if (AddfacebookAccount == "issue_access_token")
                {
                    Response.Redirect(Helper.SBUtils.GetFacebookRedirectLink());
                }
                else if (AddfacebookAccount == "Account already Exist !")
                {
                }
                else
                {
                    Session["SocialManagerInfo"] = AddfacebookAccount;

                    //To enable the Facebook Message Pop up
                    TempData["IsFacebookAccountAdded"] = 1;
                    TempData["FacebookAccount"] = objfacebookAccount;
                }
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult AuthenticateFacebook(string op)
        {
            string facebookurl = "../index/index";
            if (!string.IsNullOrEmpty(op))
            {

                if (op == "fbgroup")
                {
                    Session["fblogin"] = op;
                    facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                }
                else if (op == "page")
                {
                    int profilecount = (Int16)(Session["ProfileCount"]);
                    int totalaccount = (Int16)Session["TotalAccount"];
                    if (profilecount < totalaccount)
                    {
                        Session["fblogin"] = op;
                        facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                    }
                    else { }
                }
                else if (op == "fblogin")
                {
                    Session["fblogin"] = op;
                    //if (System.Web.HttpContext.Current.Request.Url.Authority.ToLower().Equals("socioboard.com")) 
                    //{
                    //    Session["fbloginredircturl"] = "http://socioboard.com/FacebookManager/Facebook";
                    //    facebookurl = "http://www.facebook.com/v2.0/dialog/oauth/?scope=user_friends,read_friendlists,publish_actions,publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=http://socioboard.com/FacebookManager/Facebook&response_type=code";
                    //}
                    //else if (System.Web.HttpContext.Current.Request.Url.Authority.ToLower().Equals("www.socioboard.com"))
                    //{
                    //    Session["fbloginredircturl"] = "http://www.socioboard.com/FacebookManager/Facebook";
                    //    facebookurl = "http://www.facebook.com/v2.0/dialog/oauth/?scope=user_friends,read_friendlists,publish_actions,publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=http://www.socioboard.com/FacebookManager/Facebook&response_type=code";
                    //}
                    //else 
                    //{
                    //    facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                    //}
                    facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                    //string aksdjf =  System.Web.HttpContext.Current.Request.Url.AbsoluteUri;

                }
            }
            else
            {
                try
                {
                    try
                    {
                        Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                        logger.Error(Session["group"]);
                        logger.Error(Session["group"].ToString());
                        JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
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
                                Session["fbSocial"] = "a";
                                facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                                Response.Redirect(facebookurl);
                            }
                            else if (profilecount == 0 || totalaccount == 0)
                            {
                                Session["fbSocial"] = "a";
                                facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                                Response.Redirect(facebookurl);
                            }
                            else
                            {
                                Response.Redirect("../Home/Index");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("ex.Message : " + ex.Message);
                        logger.Error("ex.StackTrace : " + ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            return Content(facebookurl);
        }

        public ActionResult GetFBPage()
        {
            string ret = string.Empty;
            try
            {

                List<Domain.Socioboard.Domain.AddFacebookPage> lstAddFacebookPage = new List<Domain.Socioboard.Domain.AddFacebookPage>();
                lstAddFacebookPage = (List<Domain.Socioboard.Domain.AddFacebookPage>)Session["fbpage"];
                foreach (var item in lstAddFacebookPage)
                {
                    ret += item.Name + "`" + item.ProfilePageId + "`" + item.AccessToken + "`" + item.Email + "`" + item.LikeCount + "~";
                }
                ret = ret.Substring(0, ret.Length - 1);
                Session["fbpage"] = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(ret);
        }

        public ActionResult AddFbPage(string profileid, string accesstoken, string email)
        {
            Api.Facebook.Facebook objApiFacebook = new Api.Facebook.Facebook();
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            objUser = (Domain.Socioboard.Domain.User)Session["User"];
            //objApiFacebook.AddFacebookPagesInfo(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(), email);
            //objApiFacebook.AddFacebookPagesInfoAsync(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(), email);

            //Api.Facebook.Facebook objApiFacebook1 = new Api.Facebook.Facebook();
            objApiFacebook.AddFacebookPagesInfo(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(), email);
            return Content("");
        }

        public ActionResult GetFBGroup()
        {

            string ret = string.Empty;
            try
            {

                List<Domain.Socioboard.Domain.AddFacebookGroup> lstAddFacebookGroup = new List<Domain.Socioboard.Domain.AddFacebookGroup>();
                lstAddFacebookGroup = (List<Domain.Socioboard.Domain.AddFacebookGroup>)Session["fbgrp"];
                foreach (var item in lstAddFacebookGroup)
                {
                    ret += item.Name + "`" + item.ProfileGroupId + "`" + item.AccessToken + "`" + item.Email + "~";
                }
                ret = ret.Substring(0, ret.Length - 1);
                Session["fbgrp"] = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(ret);



        }
        [Authorize]
        [CustomAuthorize]
        public ActionResult AddFbGroup(string ProfileGroupId, string accesstoken, string email, string Name)
        {
            Api.Facebook.Facebook objApiFacebook = new Api.Facebook.Facebook();
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            objUser = (Domain.Socioboard.Domain.User)Session["User"];
            objApiFacebook.AddFacebookGroupsInfo(objUser.Id.ToString(), ProfileGroupId, accesstoken, Session["group"].ToString(), email, Name);
            return Content("");
        }


        public ActionResult Addfacebookpagebyurl(string type, string url, string name)
        {
            var pageid = "";
            if (type == "fanpage")
            {
                try
                {
                    logger.Error("Enter in try Addfacebookpagebyurl");
                    try
                    {
                        Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                        logger.Error("Enter in try Addfacebookpagebyurl 1");

                        dynamic data = string.Empty;
                        string strdata = string.Empty;
                        try
                        {
                            Domain.Socioboard.Domain.AddFacebookPage objAddFacebookPage = (Domain.Socioboard.Domain.AddFacebookPage)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFbPageDetails(url), typeof(Domain.Socioboard.Domain.AddFacebookPage)));
                            pageid = objAddFacebookPage.ProfilePageId;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(strdata);
                            logger.Error(data);
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }
                        {
                            logger.Error("data = fb.Get");
                            logger.Error(pageid);
                            string Accestoken = string.Empty;
                            string mail = string.Empty;
                            if (pageid != null)
                            {
                                try
                                {
                                    logger.Error("Inside apiobjFacebook.AddFacebookPagesByUrl");
                                    Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                                    apiobjFacebook.AddFacebookPagesByUrl(objUser.Id.ToString(), pageid, Session["group"].ToString(), name);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                    logger.Error("error1");
                                    logger.Error(ex.Message);
                                    logger.Error(ex.StackTrace);
                                }
                            }
                            else
                            {
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        logger.Error("dynamic data");
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }

            else
            {

            }


            return Content("");

        }


        


    }
}
