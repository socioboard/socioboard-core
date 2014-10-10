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

namespace Socioboard.Controllers
{
    public class FacebookManagerController : Controller
    {
        ILog logger = LogManager.GetLogger(typeof(FacebookManagerController));
        //
        // GET: /FacebookManager/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facebook(string code)
        {
            if (Session["fblogin"] != null)
            {
                if ((string)Session["fblogin"] == "fblogin")
                {
                    Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                    Domain.Socioboard.Domain.User checkuserexist = (Domain.Socioboard.Domain.User)Session["User"];
                    // string facebookcode = Request.QueryString["code"].ToString();
                    string facebookcode = code;
                    Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                    Api.User.User ApiobjUser = new Api.User.User();
                    objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(apiobjFacebook.FacebookLogin(code), typeof(Domain.Socioboard.Domain.User)));
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
                else if ((string)Session["fblogin"] == "page")
                {
                   Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                   List<Domain.Socioboard.Domain.AddFacebookPage> lstAddFacebookPage = new List<Domain.Socioboard.Domain.AddFacebookPage>();
                   lstAddFacebookPage = (List<Domain.Socioboard.Domain.AddFacebookPage>)(new JavaScriptSerializer().Deserialize(apiobjFacebook.GetFacebookPages(code), typeof(List<Domain.Socioboard.Domain.AddFacebookPage>)));
                   Session["fbpage"] = lstAddFacebookPage;
                   return RedirectToAction("Index", "Home",new{hint="fbpage" });
                }

                else if ((string)Session["fblogin"] == "fbgroup")
                {
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
                // string facebookcode = Request.QueryString["code"].ToString();
                string facebookcode = code;
                Api.Facebook.Facebook apiobjFacebook = new Api.Facebook.Facebook();
                string AddfacebookAccount = apiobjFacebook.AddFacebookAccount(facebookcode, objUser.Id.ToString(), Session["group"].ToString());
                Session["SocialManagerInfo"] = AddfacebookAccount;
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateFacebook(string op)
        {
            string facebookurl = string.Empty;
            if (!string.IsNullOrEmpty(op))
            {
                Session["fblogin"] = op;
                facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                // Response.Redirect(facebookurl, true);
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
                                Session["fbSocial"] = "a";
                                facebookurl = Helper.SBUtils.GetFacebookRedirectLink();
                                Response.Redirect(facebookurl);
                            }
                            else
                            {
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
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
                    ret += item.Name + "`" + item.ProfilePageId + "`" + item.AccessToken + "`" + item.Email + "~";
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
        public ActionResult AddFbPage(string profileid,string accesstoken,string email)
        {
            Api.Facebook.Facebook objApiFacebook = new Api.Facebook.Facebook();
           Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
             objUser= ( Domain.Socioboard.Domain.User)Session["User"];
             objApiFacebook.AddFacebookPagesInfo(objUser.Id.ToString(), profileid, accesstoken, Session["group"].ToString(),email);
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
        public ActionResult AddFbGroup(string ProfileGroupId, string accesstoken, string email, string Name)
        {
            Api.Facebook.Facebook objApiFacebook = new Api.Facebook.Facebook();
           Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
             objUser= ( Domain.Socioboard.Domain.User)Session["User"];
             objApiFacebook.AddFacebookGroupsInfo(objUser.Id.ToString(), ProfileGroupId, accesstoken, Session["group"].ToString(), email, Name);
               return Content("");
        }
    
    }
}
