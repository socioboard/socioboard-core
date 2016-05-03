using log4net;
using Newtonsoft.Json.Linq;
using Socioboard.Api.Linkedin;
using Socioboard.App_Start;
using Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class LinkedinManagerController : BaseController
    {
        //
        // GET: /LinkedinManager/
        ILog logger=LogManager.GetLogger(typeof(LinkedinManagerController));
        public ActionResult Index()
        {
            return View();
        }


        //public ActionResult Linkedin()
        //{
        //    Session["LinkedinCompanyPage"] = null;
        //    Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
        //    string oauth_token = Request.QueryString["oauth_token"];
        //    string oauth_verifier = Request.QueryString["oauth_verifier"];
        //    string reuqestTokenSecret=Session["reuqestTokenSecret"].ToString();
        //    if (Session["linSocial"] == "a")
        //    {
        //        Api.Linkedin.Linkedin objApiLinkedin = new Api.Linkedin.Linkedin();
        //        string AddLinkedinAccount = objApiLinkedin.AddLinkedinAccount(oauth_token, oauth_verifier, reuqestTokenSecret, ConfigurationManager.AppSettings["LiApiKey"], ConfigurationManager.AppSettings["LiSecretKey"], objUser.Id.ToString(), Session["group"].ToString());
        //        Session["SocialManagerInfo"] = AddLinkedinAccount;
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else

        //    {
        //        Api.LinkedinCompanyPage.LinkedinCompanyPage objLiCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();

        //        List<Helper.AddliPage> lstLinkedinCompanyPage = new List<Helper.AddliPage>();

        //        string page = objLiCompanyPage.GetLinkedinCompanyPage(oauth_token, oauth_verifier, reuqestTokenSecret, ConfigurationManager.AppSettings["LiApiKey"], ConfigurationManager.AppSettings["LiSecretKey"], objUser.Id.ToString(), Session["group"].ToString());
        //        lstLinkedinCompanyPage = (List<Helper.AddliPage>)(new JavaScriptSerializer().Deserialize(page, typeof(List<Helper.AddliPage>)));
        //        Session["LinkedinCompanyPage"] = lstLinkedinCompanyPage;
        //        return RedirectToAction("Index", "Home", new { hint = "linpage" });
        //    }
        //}
      

         public ActionResult linPage_connect()
        {
            string redirectURL = "";
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
                            Session["linSocial"] = "p";
                            Api.Linkedin.Linkedin objApiLinkedin = new Api.Linkedin.Linkedin();
                            string redircturl = objApiLinkedin.GetLinkedinRedirectUrl(ConfigurationManager.AppSettings["LiApiKey"], ConfigurationManager.AppSettings["LiSecretKey"]);
                            Session["reuqestToken"] = redircturl.Split('~')[1].ToString();
                            Session["reuqestTokenSecret"] = redircturl.Split('~')[2].ToString();
                            redircturl = redircturl.Split('~')[0].ToString();
                            redirectURL = redircturl;
                            //Response.Redirect(redircturl);

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
            return Content(redirectURL);//View();
        }

         public ActionResult GetLinkedinCompanyPage()
         {
             string ret = string.Empty;
             List<Helper.AddliPage> lstLinkedinCompanyPage = new List<Helper.AddliPage>();
             lstLinkedinCompanyPage = (List<Helper.AddliPage>)Session["LinkedinCompanyPage"];
             if (lstLinkedinCompanyPage.Count > 0)
             {
                 foreach (var item in lstLinkedinCompanyPage)
                 {
                     ret += item.PageId + "`" + item.PageName + "`" + item._Oauth + "~";
                     Session["LinkedinPagedetail"] = item._Oauth;
                 }
                 ret = ret.Substring(0, ret.Length - 1);
                 Session["LinkedinCompanyPage"] = null;

             }
             else if (lstLinkedinCompanyPage == null)
             {
                 ret = "Pages Not Found in this Account!";
             }
             else
             {
                 ret = "Pages Not Found in this Account!";
             }
             return Content(ret);
         }

         public async Task<ActionResult> AddLinkedinCompanyPage(string id, string OAuth)
         {

             Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
             objUser = (Domain.Socioboard.Domain.User)Session["User"];
             oAuthLinkedIn oauthtoken = (oAuthLinkedIn)Session["LinkedinPagedetail"];
             string Oauth = new JavaScriptSerializer().Serialize(oauthtoken);
             string accesstoken = "";
             string returndata = "";
             List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
             Parameters.Add(new KeyValuePair<string, string>("GroupId", Session["group"].ToString()));
             Parameters.Add(new KeyValuePair<string, string>("Oauth", Oauth));
             Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
             Parameters.Add(new KeyValuePair<string, string>("ProfileId", id));
             if (Session["access_token"] != null)
             {
                 accesstoken = Session["access_token"].ToString();
             }
             HttpResponseMessage response = await WebApiReq.PostReq("api/ApiLinkedIn/AddLinkedinCompanyPage", Parameters, "Bearer", accesstoken);
             if (response.IsSuccessStatusCode)
             {
                 returndata = await response.Content.ReadAsAsync<string>();
             }

             return Content("");
         }


         public ActionResult AuthenticateLinkedin(string op)
         {
            
             try
             {
                 try
                 {
                     Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                     JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
                     int profilecount = 0;
                     int totalaccount = 0;
                     Random ran = new Random();
                     int x = ran.Next(8976557);
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
                             if (op != "page")
                             {
                                 Session["linkedinSocial"] = "AddProfile";
                                 Response.Redirect("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=" + ConfigurationManager.AppSettings["LinkedinApiKey"] + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(ConfigurationManager.AppSettings["LinkedinCallBackURL"]) + "&state=" + x.ToString() + "&?scope=r_basicprofile+w_share");
                             }
                             else {
                                 Session["linkedinSocial"] = "LinkedinCompanyPage";
                                 Response.Redirect("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id=" + ConfigurationManager.AppSettings["LinkedinApiKey"] + "&redirect_uri=" + System.Web.HttpUtility.UrlEncode(ConfigurationManager.AppSettings["LinkedinCallBackURL"]) + "&state=" + x.ToString() + "&?scope=r_basicprofile+w_share+rw_company_admin");
                             
                             }
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


        [CustomAuthorize]
         public async Task<ActionResult> LinkedinRedirect()
         {
            
             Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
             string code = Request.QueryString["code"];
             string state = Request.QueryString["state"];
             string accesstoken = "";
             string returndata = "";
             if (Session["linkedinSocial"] == "AddProfile")
             {
                 List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
                 Parameters.Add(new KeyValuePair<string, string>("GroupId", Session["group"].ToString()));
                 Parameters.Add(new KeyValuePair<string, string>("Code", code));
                 Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
                 if (Session["access_token"] != null)
                 {
                     accesstoken = Session["access_token"].ToString();
                 }
                 HttpResponseMessage response = await WebApiReq.PostReq("api/ApiLinkedIn/AddLinkedInAccount", Parameters, "Bearer", accesstoken);
                 if (response.IsSuccessStatusCode)
                 {
                     returndata = await response.Content.ReadAsAsync<string>();
                 }
                 return RedirectToAction("Index", "Home");
             }
             else
             {
                 List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
                 Parameters.Add(new KeyValuePair<string, string>("GroupId", Session["group"].ToString()));
                 Parameters.Add(new KeyValuePair<string, string>("Code", code));
                 Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
                 if (Session["access_token"] != null)
                 {
                     accesstoken = Session["access_token"].ToString();
                 }
                 HttpResponseMessage response = await WebApiReq.PostReq("api/ApiLinkedIn/GetLinkedinCompanyPage", Parameters, "Bearer", accesstoken);
                 if (response.IsSuccessStatusCode)
                 {
                     returndata = await response.Content.ReadAsAsync<string>();
                 }
                 if (returndata != "No Company Page Found")
                 {
                     List<Helper.AddliPage> lstLinkedinCompanyPage = new List<Helper.AddliPage>();
                     lstLinkedinCompanyPage = (List<Helper.AddliPage>)(new JavaScriptSerializer().Deserialize(returndata, typeof(List<Helper.AddliPage>)));
                     Session["LinkedinCompanyPage"] = lstLinkedinCompanyPage;
                 }
                 else {

                     Session["LinkedinCompanyPage"] = new List<Helper.AddliPage>();
                 }
                 return RedirectToAction("Index", "Home", new { hint = "linpage" });
             }
         }
    }
}
