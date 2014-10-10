using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    public class DiscoveryController : Controller
    {
        //
        // GET: /Discovery/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadDiscovery()
        {
            return PartialView("_DiscoveryPartial");
        }

        public ActionResult SearchFacebook(string keyword)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchFacebook(objUser.Id.ToString(), keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_SearchFacebookPartial", lstDiscoverySearch);
        }
        public ActionResult SearchTwitter(string keyword)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.DiscoverySearchTwitter(objUser.Id.ToString(), keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_SearchTwitterPartial", lstDiscoverySearch);
        }

      
    }
}
