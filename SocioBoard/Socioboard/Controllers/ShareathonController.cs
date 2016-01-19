using Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.App_Start;
using System.Web.Script.Serialization;
using System.Net.Http;
using Facebook;
using System.Text.RegularExpressions;


namespace Socioboard.Controllers
{
    public class ShareathonController : Controller
    {
        [CustomAuthorize]
        public async Task<ActionResult> Index() 
        {
            User objUser = (User)Session["User"];
            string accesstoken = string.Empty;
            string UserId = objUser.Id.ToString();
            if(Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

            IEnumerable<ShareathonViewModel> sharethons = new List<ShareathonViewModel>();

            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiShareathon/GetShareathons?UserId=" + UserId, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                sharethons = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.ShareathonViewModel>>();
            }



            return View(sharethons);
        }

        [CustomAuthorize]
        public async Task<ActionResult> GroupIndex()
        {
            User objUser = (User)Session["User"];
            string accesstoken = string.Empty;
            string UserId = objUser.Id.ToString();
            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

            IEnumerable<ShareathonGroupViewModel> sharethons = new List<ShareathonGroupViewModel>();

            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiShareathon/GetGroupShareathons?UserId=" + UserId, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                sharethons = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.ShareathonGroupViewModel>>();
            }



            return View(sharethons);
        }


        [CustomAuthorize]
        public async Task<ActionResult> GroupShareathonEdit(string Id)
        {
            User objuser = (User)Session["User"];
            string accesstoken = string.Empty;
            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();

            ShareathonGroup sharethons = null;

            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiShareathon/GetGroupShareaton?Id=" + Id, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                sharethons = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.ShareathonGroup>();
                    //FacebookClient fb = new FacebookClient();
                    //fb.AccessToken = sharethons.AccessToken;
                    //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    //dynamic profile = fb.Get("v2.0/" + sharethons.Facebookgroupid + "?fields=id,name");
                    //ViewBag.GroupId = (Convert.ToString(profile["id"]));
                    //ViewBag.Groupname = (Convert.ToString(profile["name"]));
                    string [] nameId=sharethons.Facebooknameid.Split(',');
                    string id = "";
                    for (int i = 0; i < nameId.Length; i++)
			        {
                    string [] name=Regex.Split(nameId[i],"###");
                    id = name[0] + "," + id;
			         //lst.Add(new KeyValuePair<string,string>(nameId[i], name[1]));
			        }
                    ViewBag.GroupId = id;
                    Api.Facebook.Facebook ApiFacebook = new Api.Facebook.Facebook();
                    List<AddFacebookGroup> lstFacebookGroup = new List<AddFacebookGroup>();
                    string fcebookgrp = ApiFacebook.GetAllFacebookGroups(sharethons.AccessToken);
                    lstFacebookGroup = (List<AddFacebookGroup>)(new JavaScriptSerializer().Deserialize(fcebookgrp, typeof(List<AddFacebookGroup>)));
                    if (lstFacebookGroup.Count > 0)
                    {
                        ViewBag.facebbokgroup = lstFacebookGroup;
                    }
                    else { ViewBag.facebbokgroup = null; }
               
            }
            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objuser.Id.ToString(), Session["group"].ToString()), typeof(List<FacebookAccount>)));
            ViewBag.FbAccounts = facebookaccounts;
            ViewBag.UserId = objuser.Id;
            ViewBag.Group = lst;
            return View(sharethons);
        }

        [CustomAuthorize]
        public ActionResult Create() 
        {
            User objUser = (User)Session["User"];
            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            List<FacebookAccount> facebookpages = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
           
            ViewBag.FbPages = facebookpages;
            Session["FbPages"] = ViewBag.FbPages;
            ViewBag.FbAccounts = facebookaccounts;
            ViewBag.UserId = objUser.Id;

            return View();
        }


        [HttpPost]
        [CustomAuthorize]
        public async Task<ActionResult> Create(ShareathonViewModel shareathon)
        {

            string id = "";
            for (int i = 0; i < shareathon.FacebookPageId.Length; i++)
            {
                string dataid = shareathon.FacebookPageId[i];
                id = dataid + "," + id;
            }
            User objUser = (User)Session["User"];
            List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
            Parameters.Add(new KeyValuePair<string, string>("Facebookaccountid", shareathon.Facebookaccountid.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Userid", objUser.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Facebookpageid", id.TrimEnd(',')));
            Parameters.Add(new KeyValuePair<string, string>("Timeintervalminutes", shareathon.Timeintervalminutes.ToString()));
            string accesstoken = string.Empty;
            string UserId = objUser.Id.ToString();
            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }

            HttpResponseMessage response = await WebApiReq.PostReq("api/ApiShareathon/AddShareathon", Parameters, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            List<FacebookAccount> facebookpages = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));

            ViewBag.FbPages = facebookpages;
            Session["FbPages"] = ViewBag.FbPages;
            ViewBag.FbAccounts = facebookaccounts;
            ViewBag.UserId = objUser.Id;
            return View();
        }



        [HttpPost]
        [CustomAuthorize]
        public async Task<ActionResult> GroupCreate(ShareathonGroupViewModel shareathon)
        {
            string groupId = "";
            string nameId = "";

            for (int i = 0; i < shareathon.FacebookGroupId.Length; i++)
            {
                string dataid = shareathon.FacebookGroupId[i];
                string[] grpid = Regex.Split(dataid, "###");
                groupId = grpid[0] + "," + groupId;
                nameId = shareathon.FacebookGroupId[i] + "," + nameId;
            }
            User objUser = (User)Session["User"];
            List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
            Parameters.Add(new KeyValuePair<string, string>("Facebookaccountid", shareathon.Facebookaccountid.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Userid", objUser.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("FacebookPageUrl",shareathon.FacebookPageUrl));
            Parameters.Add(new KeyValuePair<string, string>("FacebookGroupId", groupId));
            Parameters.Add(new KeyValuePair<string, string>("Timeintervalminutes", shareathon.Timeintervalminutes.ToString()));
            string accesstoken = string.Empty;
            string UserId = objUser.Id.ToString();
            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }

            HttpResponseMessage response = await WebApiReq.PostReq("api/ApiShareathon/AddGroupSharethon", Parameters, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            List<FacebookAccount> facebookpages = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));

            ViewBag.FbPages = facebookpages;
            Session["FbPages"] = ViewBag.FbPages;
            ViewBag.FbAccounts = facebookaccounts;
            ViewBag.UserId = objUser.Id;
            return View();
        }



        [HttpPost]
        [CustomAuthorize]
        public async Task<ActionResult> Edit(ShareathonViewModel shareathon)
        {

            string id = "";
            for (int i = 0; i < shareathon.FacebookPageId.Length; i++)
            {
                string dataid = shareathon.FacebookPageId[i];
                id = dataid + "," + id;
            }
            User objUser = (User)Session["User"];
            List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
            Parameters.Add(new KeyValuePair<string, string>("Id", shareathon.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Facebookaccountid", shareathon.Facebookaccountid.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Userid", objUser.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Facebookpageid", id.TrimEnd(',')));
            Parameters.Add(new KeyValuePair<string, string>("Timeintervalminutes", shareathon.Timeintervalminutes.ToString()));
            string accesstoken = string.Empty;
            string UserId = objUser.Id.ToString();
            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }

            HttpResponseMessage response = await WebApiReq.PostReq("api/ApiShareathon/EditShareathon", Parameters, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            List<FacebookAccount> facebookpages = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));

            ViewBag.FbPages = facebookpages;
            Session["FbPages"] = ViewBag.FbPages;
            ViewBag.FbAccounts = facebookaccounts;
            ViewBag.UserId = objUser.Id;
            return View();
        }




        [HttpPost]
        [CustomAuthorize]
        public async Task<ActionResult> EditGroup(ShareathonGroupViewModel shareathon)
        {
            string groupId = "";
            string nameId = "";
            for (int i = 0; i < shareathon.FacebookGroupId.Length; i++)
            {
                string dataid = shareathon.FacebookGroupId[i];
                string[] grpid = Regex.Split(dataid, "###");
                groupId = grpid[0] + "," + groupId;
                nameId = shareathon.FacebookGroupId[i] + "," + nameId;
            }
            User objUser = (User)Session["User"];
            List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
            Parameters.Add(new KeyValuePair<string, string>("Facebookaccountid", shareathon.Facebookaccountid.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Id", shareathon.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("Userid", objUser.Id.ToString()));
            Parameters.Add(new KeyValuePair<string, string>("FacebookPageUrl", shareathon.FacebookPageUrl));
            Parameters.Add(new KeyValuePair<string, string>("FacebookGroupId", groupId));
            Parameters.Add(new KeyValuePair<string, string>("Timeintervalminutes", shareathon.Timeintervalminutes.ToString()));
            string accesstoken = string.Empty;
            string UserId = objUser.Id.ToString();
            if (Session["access_token"] != null)
            {
                accesstoken = Session["access_token"].ToString();
            }

            HttpResponseMessage response = await WebApiReq.PostReq("api/ApiShareathon/EditShareathonGroup", Parameters, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GroupIndex");
            }

            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            List<FacebookAccount> facebookpages = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(objUser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));

            ViewBag.FbPages = facebookpages;
            Session["FbPages"] = ViewBag.FbPages;
            ViewBag.FbAccounts = facebookaccounts;
            ViewBag.UserId = objUser.Id;
            return View();
        }




        [CustomAuthorize]
        public ActionResult GroupCreate()
        { 
            User objuser=(User)Session["User"];
            Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<FacebookAccount> facebookacoount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetAllFacebookAccountsByUserIdAndGroupId(objuser.Id.ToString(), System.Web.HttpContext.Current.Session["group"].ToString()), typeof(List<FacebookAccount>)));
            ViewBag.FbAccounts = facebookacoount;
            ViewBag.UserId = objuser.Id;
            return View();
        }

         [CustomAuthorize]        
        public ActionResult GetAllFacebookGroups(string accesstoken)
        {
            Api.Facebook.Facebook ApiFacebook = new Api.Facebook.Facebook();
            List<AddFacebookGroup> lstFacebookGroup = new List<AddFacebookGroup>();
            string fcebookgrp = ApiFacebook.GetAllFacebookGroups(accesstoken);
            lstFacebookGroup = (List<AddFacebookGroup>)(new JavaScriptSerializer().Deserialize(fcebookgrp, typeof(List<AddFacebookGroup>)));
            if (lstFacebookGroup.Count > 0)
            {
                ViewBag.facebbokgroup = lstFacebookGroup;
            }
            else { ViewBag.facebbokgroup = null; }
             return PartialView("_facebookgrouppartial");
        }


         [CustomAuthorize]
         public async Task<ActionResult> DeletePageShareathon(string Id)
         {

             User objUser = (User)Session["User"];
             string accesstoken = string.Empty;
             string UserId = objUser.Id.ToString();
             if (Session["access_token"] != null)
             {
                 accesstoken = Session["access_token"].ToString();
             }
             List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

             HttpResponseMessage response = await WebApiReq.GetReq("api/ApiShareathon/DeletePageShareathon?Id=" + Id, "Bearer", accesstoken);
             if (response.IsSuccessStatusCode)
             {
                
             }
             return RedirectToAction("Index", "Shareathon");
            
            
         
         }


         [CustomAuthorize]
         public async Task<ActionResult> DeleteGroupShareathon(string Id)
         {

             User objUser = (User)Session["User"];
             string accesstoken = string.Empty;
             string UserId = objUser.Id.ToString();
             if (Session["access_token"] != null)
             {
                 accesstoken = Session["access_token"].ToString();
             }
             List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();

             HttpResponseMessage response = await WebApiReq.GetReq("api/ApiShareathon/DeleteGroupShareathon?Id=" + Id, "Bearer", accesstoken);
             if (response.IsSuccessStatusCode)
             {

             }
             return RedirectToAction("GroupIndex", "Shareathon");



         }



         [CustomAuthorize]
         public async Task<ActionResult> Edit(string Id)
         {
             User objuser = (User)Session["User"];
             string accesstoken = string.Empty;
             if (Session["access_token"] != null)
             {
                 accesstoken = Session["access_token"].ToString();
             }
             List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>();
             List<FacebookAccount> lst = new List<FacebookAccount>();

             Shareathon sharethons = null;
             string nameId ="";
             Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
             HttpResponseMessage response = await WebApiReq.GetReq("api/ApiShareathon/GetShareaton?Id=" + Id, "Bearer", accesstoken);
             if (response.IsSuccessStatusCode)
             {
                 sharethons = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.Shareathon>();

                 nameId = sharethons.Facebookpageid;
                 //foreach (var item in nameId)
                 //{
             
                 //    List<FacebookAccount> facebookaccount = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.GetFacebookAccountDetailsById(item), typeof(List<FacebookAccount>)));
                 //    lst.Add(facebookaccount[0]);
                 //}

                

             }
             List<FacebookAccount> facebookaccounts = (List<FacebookAccount>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getAllFacebookAccountsOfUser(objuser.Id.ToString()), typeof(List<FacebookAccount>)));
             List<FacebookAccount> lstpage = facebookaccounts.Where(t => t.Type == "Page").ToList();
             List<FacebookAccount> lstaccount = facebookaccounts.Where(t=>t.Type=="account").ToList();
             ViewBag.UserId = objuser.Id;
             ViewBag.FbAccounts = lstaccount;
             ViewBag.Pages = lstpage;
             ViewBag.pageid = nameId;
             return View(sharethons);
         }

      
    }
}