using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class LinkedinManagerController : Controller
    {
        //
        // GET: /LinkedinManager/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Linkedin()
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            string oauth_token = Request.QueryString["oauth_token"];
            string oauth_verifier = Request.QueryString["oauth_verifier"];
            string reuqestTokenSecret=Session["reuqestTokenSecret"].ToString();
            Api.Linkedin.Linkedin objApiLinkedin = new Api.Linkedin.Linkedin();
            string AddLinkedinAccount = objApiLinkedin.AddLinkedinAccount(oauth_token, oauth_verifier, reuqestTokenSecret, ConfigurationManager.AppSettings["LiApiKey"], ConfigurationManager.AppSettings["LiSecretKey"], objUser.Id.ToString(), Session["group"].ToString());
            Session["SocialManagerInfo"] = AddLinkedinAccount;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateLinkedin()
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
                            Api.Linkedin.Linkedin objApiLinkedin = new Api.Linkedin.Linkedin();
                            string redircturl = objApiLinkedin.GetLinkedinRedirectUrl(ConfigurationManager.AppSettings["LiApiKey"], ConfigurationManager.AppSettings["LiSecretKey"]);
                            Session["reuqestToken"] = redircturl.Split('~')[1].ToString();
                            Session["reuqestTokenSecret"] = redircturl.Split('~')[2].ToString();
                            redircturl = redircturl.Split('~')[0].ToString();
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
