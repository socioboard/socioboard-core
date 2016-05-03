using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Socioboard.Helper;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Socioboard.Controllers
{
    public class WordpressManagerController : BaseController
    {
        //
        // GET: /Wordpess/

        public ActionResult Index(string code)
        {
            return View();
        }
        public ActionResult Wordpress(string code) 
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            string _WPcode = code;
            Api.Wordpress.Wordpress objApiWordpress = new Api.Wordpress.Wordpress();
            string add = objApiWordpress.AddWordpressAccount(_WPcode, objUser.Id.ToString(), Session["group"].ToString());
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AuthenticateWordpress()
        {
            string wordpressredirecturl = string.Empty;
            try
            {
                Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
                JObject group = JObject.Parse(objApiGroups.GetGroupDetailsByGroupId(Session["group"].ToString().ToString()));
                wordpressredirecturl = SBUtils.GetWordpressRedirectLink();
                Response.Redirect(wordpressredirecturl);
                //int profilecount = (Int16)(Session["ProfileCount"]);
                //int totalaccount = (Int16)Session["TotalAccount"];
                //if (Convert.ToString(group["GroupName"]) == "Socioboard")
                //{
                //    if (profilecount < totalaccount)
                //    {
                //        wordpressredirecturl = SBUtils.GetWordpressRedirectLink();
                //        Response.Redirect(wordpressredirecturl);
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index", "Home");
                //    }
                //}
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            return Content(wordpressredirecturl);
        }
    }
}
