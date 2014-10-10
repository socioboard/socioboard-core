using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class InstagramManagerController : Controller
    {
        //
        // GET: /InstagramManager/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Instagram()
        {
            string AddTwitterAccount = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            string code = (String)Request.QueryString["code"];

            Api.Instagram.Instagram apiobjInstagram = new Api.Instagram.Instagram();
            try
            {
                AddTwitterAccount = apiobjInstagram.AddInstagramAccount(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"], objUser.Id.ToString(), Session["group"].ToString(), code);
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
        
        public ActionResult AuthenticateInstagram()
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
                            Api.Instagram.Instagram ApiobjInstagram = new Api.Instagram.Instagram();
                            string redirecturl = ApiobjInstagram.GetInstagramRedirectUrl(ConfigurationManager.AppSettings["InstagramClientKey"], ConfigurationManager.AppSettings["InstagramClientSec"], ConfigurationManager.AppSettings["InstagramCallBackURL"]);
                            Response.Redirect(redirecturl);
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
