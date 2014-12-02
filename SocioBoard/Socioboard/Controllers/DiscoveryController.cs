using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    [Authorize]
    public class DiscoveryController : Controller
    {
        //
        // GET: /Discovery/

        public ActionResult Index()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
            //return View();
        }
        public ActionResult LoadDiscovery()
        {
            // Edited by Antima

            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<string> lstSearchHistory = new List<string>();
            lstSearchHistory = (List<string>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.getAllSearchKeywords(objUser.Id.ToString()), typeof(List<string>)));

            return PartialView("_DiscoveryPartial", lstSearchHistory);
        }

        public ActionResult SearchFacebook(string keyword)
        {
            keyword = Uri.EscapeDataString(keyword);
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchFacebook(objUser.Id.ToString(), keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_SearchFacebookPartial", lstDiscoverySearch);
        }
        public ActionResult SearchTwitter(string keyword)
        {
            keyword = Uri.EscapeDataString(keyword);
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchTwitter(objUser.Id.ToString(), keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_SearchTwitterPartial", lstDiscoverySearch);
        }

        

      
    }
}
