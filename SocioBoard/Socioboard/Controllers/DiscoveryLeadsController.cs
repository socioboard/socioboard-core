using Socioboard.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Collections;
using System.Net.Http;
using log4net;

namespace Socioboard.Controllers
{
    [CustomAuthorize]
    public class DiscoveryLeadsController : Controller
    {
        //
        // GET: /Discovery/
        ILog logger = LogManager.GetLogger(typeof(DiscoveryLeadsController));
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
               
                if (Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            //return View();
        }
        public ActionResult LoadDiscovery()
        {
            // Edited by Antima

            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoveryLeads.DiscoveryLeads ApiDiscoveryLeads = new Api.DiscoveryLeads.DiscoveryLeads();
            List<string> lstSearchHistory = new List<string>();
            lstSearchHistory = (List<string>)(new JavaScriptSerializer().Deserialize(ApiDiscoveryLeads.GetAllSearchLead(objUser.Id.ToString()), typeof(List<string>)));

            return PartialView("_DiscoveryPartial", lstSearchHistory);
        }


        public ActionResult AddLeadKeyword(string Keyword)
        {
            Domain.Socioboard.Domain.User _user = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoveryLeads.DiscoveryLeads _ApiDiscoveryLeads = new Api.DiscoveryLeads.DiscoveryLeads();
            List<string> lstSearchHistory = new List<string>();
            lstSearchHistory = (List<string>)(new JavaScriptSerializer().Deserialize(_ApiDiscoveryLeads.AddLeadKeyword(Keyword, _user.Id.ToString()), typeof(List<string>)));
            return PartialView("_DiscoveryPartial", lstSearchHistory);
        }

        public async Task<ActionResult> GetFaceBookLeads(string Keyword, string skip)
        {
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
            IEnumerable<Socioboard.Model.FbGroupPost> _fbpost = new List<Socioboard.Model.FbGroupPost>();
            
            if (string.IsNullOrEmpty(skip))
            {
                HttpResponseMessage response = await WebApiConfig.GetReq("api/ApiSocialLead/GetFaceBookLeads?keyword=" + Keyword + "&skip=0","","");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        _fbpost = await response.Content.ReadAsAsync<IEnumerable<Socioboard.Model.FbGroupPost>>();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                    }
                }
                return PartialView("_SearchFacebookPartial", _fbpost);
            }
            else
            {
                int skips = Convert.ToInt16(skip);
                HttpResponseMessage response = await WebApiConfig.GetReq("api/ApiSocialLead/GetFaceBookLeads?keyword=" + Keyword + "&skip=" + skips * 60,"","");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        _fbpost = await response.Content.ReadAsAsync<IEnumerable<Socioboard.Model.FbGroupPost>>();
                    }
                    catch(Exception ex) {
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                    }
                }
                return PartialView("_SearchFacebookPartial", _fbpost);
            }
        }


        public async Task<ActionResult> GetLinkedInLead(string Keyword, string skip)
        {
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
            IEnumerable<Socioboard.Model.LIGroupPostDetails> _LIGroupPostDetails = new List<Socioboard.Model.LIGroupPostDetails>();
            if (string.IsNullOrEmpty(skip))
            {
                HttpResponseMessage response = await WebApiConfig.GetReq("api/ApiSocialLead/GetLinkedInLead?keyword=" + Keyword + "&skip=0","","");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        _LIGroupPostDetails = await response.Content.ReadAsAsync<IEnumerable<Socioboard.Model.LIGroupPostDetails>>();
                    }
                    catch (Exception EX)
                    {
                        logger.Error(EX.StackTrace);
                        logger.Error(EX.Message);
                    }

                }
                return PartialView("_SearchLinkedInPartial", _LIGroupPostDetails);
            }
            else {
                int skips = Convert.ToInt16(skip);
                HttpResponseMessage response = await WebApiConfig.GetReq("api/ApiSocialLead/GetLinkedInLead?keyword=" + Keyword + "&skip=" + skips * 60,"","");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        _LIGroupPostDetails = await response.Content.ReadAsAsync<IEnumerable<Socioboard.Model.LIGroupPostDetails>>();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        logger.Error(ex.Message);
                    }
                }
                return PartialView("_SearchLinkedInPartial", _LIGroupPostDetails);
            
            }
            
        }
      
    }
}
