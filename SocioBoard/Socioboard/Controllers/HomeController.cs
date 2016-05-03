using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Newtonsoft.Json.Linq;
using Socioboard.Helper;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.IO;
using Socioboard.App_Start;
using log4net;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net.Http;
using System.Threading.Tasks;

namespace Socioboard.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Read the timezone offset value from cookie and store in session.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Request.Cookies.AllKeys.Contains("timezoneoffset"))
            {
                Session["timezoneoffset"] = HttpContext.Request.Cookies["timezoneoffset"].Value;
            }
            base.OnActionExecuting(filterContext);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }

    public class HomeController : BaseController
    {


        private ILog logger = LogManager.GetLogger(typeof(HomeController));
        Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
        [MyExpirePageActionFilter]
        //[Authorize]
        [CustomAuthorize]
       // [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Index()
        {
            Session["addfbaccount"] = null;
           // Session["fblogin"] = null;            
            if (Session["User"] != null)
            {
                User objUser = (User)Session["User"];
                if (TempData["IsTwitterAccountAdded"] != null && TempData["TwitterAccount"] != null)
                {   
                    //To enable the Tweet Pop up
                    ViewBag.IsTwitterAccountAdded = TempData["IsTwitterAccountAdded"];
                    ViewBag.TwitterAccount = TempData["TwitterAccount"];
                }
                if (TempData["IsFacebookAccountAdded"] != null && TempData["FacebookAccount"] != null)
                {
                    //To enable the Tweet Pop up
                    ViewBag.IsFacebookAccountAdded = TempData["IsFacebookAccountAdded"];
                    ViewBag.FacebookAccount = TempData["FacebookAccount"];
                }

                //if (Request.QueryString["teamid"] != null)
                //{
                //    string teamid = Request.QueryString["teamid"].ToString();
                //    Api.Team.Team _apiteam = new Api.Team.Team();
                //    _apiteam.Timeout = 300000;
                //    _apiteam.UpdateTeambyteamid(teamid);

                //}

                if (Session["Paid_User"] != null && Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }

                else
                {
                    ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
                    #region Count Used Accounts
                    try
                    {
                        objUser = (User)Session["User"];
                        Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                        apiobjSocialProfile.Timeout = 300000;
                        //apiobjSocialProfile.GetAllSocialProfiles();

                        Session["ProfileCount"] = Convert.ToInt16(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                        Session["TotalAccount"] = Convert.ToInt16(SBUtils.GetUserPackageProfileCount(objUser.AccountType));
                        ViewBag.AccountType = objUser.AccountType;
                       
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    #endregion
                    if (Session["SocialManagerInfo"] != null)
                    {

                    }
                    int ProfileCount = int.Parse(Session["ProfileCount"].ToString());
                    if (objUser.ActivationStatus=="1")
                    {
                        return View(User);
                    }
                    else {
                        return RedirectToAction("UserActivationByEmail", "Index", new { email = objUser.EmailId });
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> ProfileSnapshot(string op)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            int CountProfileSnapshot = 0;

            try
            {
                if (op == null)
                {
                    Session["CountProfileSnapshot"] = 0;
                }
            }
            catch (Exception ex)
            {

            }

            if (Session["CountProfileSnapshot"] != null)
            {
                CountProfileSnapshot = (int)Session["CountProfileSnapshot"];
            }
            else
            {
                Session["CountProfileSnapshot"] = 0;
            }

            string groupId = System.Web.HttpContext.Current.Session["group"].ToString();
            string accesstoken = string.Empty;
            try
            {
                accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            }
            catch { }

            IEnumerable<GroupProfile> lstGrpProfiles = new List<GroupProfile>();
            HttpResponseMessage response1 = await WebApiReq.GetReq("api/ApiGroupProfiles/GetGroupProfiles?GroupId=" + groupId, "Bearer", accesstoken);
            if (response1.IsSuccessStatusCode)
            {
                lstGrpProfiles = await response1.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.GroupProfile>>();
            }
            Dictionary<Domain.Socioboard.Domain.GroupProfile, Dictionary<object, List<object>>> diclist = SBHelper.GetUserProfilesSnapsAccordingToGroup(lstGrpProfiles.ToList(), objUser, CountProfileSnapshot);

            //List<TeamMemberProfile> lstTeamMemberProfile = SBUtils.GetUserTeamMemberProfiles();
            //Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> diclist = SBUtils.GetUserProfilesSnapsAccordingToGroup(lstTeamMemberProfile, CountProfileSnapshot);

            if (diclist.Count > 0 && diclist != null)
            {
                Session["CountProfileSnapshot"] = (int)Session["CountProfileSnapshot"] + diclist.Count;
            }

            return PartialView("_HomeProfileSnapshotPartial", diclist);
        }


        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult UserProfile()
        {
            // Thread.Sleep(3 * 1000);
            return PartialView("_HomeUserProfilePartial");
        }


        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> LoadGroup()
        {
            User objUser = (User)Session["User"];
            string accesstoken = System.Web.HttpContext.Current.Session["access_token"].ToString();
            //Api.Groups.Groups objApiGroups = new Api.Groups.Groups();

            //objApiGroups.Timeout = 300000;
            //JArray profile = JArray.Parse(objApiGroups.GetGroupDetailsByUserId(objUser.Id.ToString()));

            //List<Groups> lstgroup = new List<Groups>();
            //foreach (var item in profile)
            //{
            //    Groups objGroups = new Groups();
            //    objGroups.Id = Guid.Parse(Convert.ToString(item["Id"]));
            //    objGroups.GroupName = Convert.ToString(item["GroupName"]);
            //    objGroups.UserId = Guid.Parse(Convert.ToString(item["UserId"]));
            //    objGroups.EntryDate = Convert.ToDateTime(Convert.ToString(item["EntryDate"]));
            //    lstgroup.Add(objGroups);
            //}

            IEnumerable<Domain.Socioboard.Domain.Groups> lstgroup = new List<Domain.Socioboard.Domain.Groups>();

            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroups/GetGroupsOfUser?UserId=" + objUser.Id.ToString(), "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                lstgroup = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.Groups>>();
            }


            return PartialView("_LoadGroupPartial", lstgroup.ToList());

        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ChangeGroup()
        {
            string groupid = Request.QueryString["groupid"].ToString();
            Session["group"] = groupid;

            return Content("success");
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> loadprofiles()
        {
            User objUser = (User)Session["User"];
            //List<TeamMemberProfile> lstTeamMemberProfile = new List<TeamMemberProfile>();
            Dictionary<GroupProfile, object> dict_TeamMember = new Dictionary<GroupProfile, object>();
            // Api.Team.Team objApiTeam = new Api.Team.Team();
            if (Session["group"] != null)
            {
                dict_TeamMember =await SBHelper.GetGroupProfiles();
            }
            return PartialView("_PofilePartial", dict_TeamMember);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAccount()
        {

            string type = Request.QueryString["profile"].ToString();
            string profileid = Request.QueryString["profileid"].ToString();
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            // Edited By Antima[15/12/2014]
            string GroupId = Session["group"].ToString();
            //Api.Team.Team objApiTeam = new Api.Team.Team();
            //objApiTeam.Timeout = 300000;
            //Domain.Socioboard.Domain.Team team = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Team));
            //Guid AdminUserId = team.UserId;
            //string output = string.Empty;
            //HttpResponseMessage response1 = await WebApiReq.DelReq("api/ApiGroupProfiles/DeleteProfileFromGroup?profileid=" + profileid + "&groupid=" + GroupId + "&userid=" + objUser.Id+ "&profiletype="+type, "Bearer", Session["access_token"].ToString());
            //if (response1.IsSuccessStatusCode)
            //{
            //    output = await response1.Content.ReadAsAsync<string>();
            //}

            try
            {
                //if (output.Equals("Deleted"))
                //{
                    if (type == "fb")
                    {
                        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                        ApiobjFacebookAccount.DeleteFacebookAccount(objUser.Id.ToString(), profileid, Session["group"].ToString(),"");
                    }
                    else if (type == "twt")
                    {
                        Api.TwitterAccount.TwitterAccount apiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                        apiobjTwitterAccount.DeleteTwitterAccount(objUser.Id.ToString(), profileid, Session["group"].ToString(), "twitter");
                    }
                    else if (type == "linkedin")
                    {
                        string accesstoken = "";
                        string returndata = "";
                        List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
                        Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
                        Parameters.Add(new KeyValuePair<string, string>("ProfileId", profileid));
                        Parameters.Add(new KeyValuePair<string, string>("GroupId", GroupId));
                        if (Session["access_token"] != null)
                        {
                            accesstoken = Session["access_token"].ToString();
                        }
                        HttpResponseMessage response = await WebApiReq.PostReq("api/ApiLinkedIn/DeleteLinkedinAccount", Parameters, "Bearer", accesstoken);
                        if (response.IsSuccessStatusCode)
                        {
                            returndata = await response.Content.ReadAsAsync<string>();
                        }
                    }
                    else if (type == "instagram")
                    {
                        Api.InstagramAccount.InstagramAccount apiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                        apiobjInstagramAccount.DeleteInstagramAccount(objUser.Id.ToString(), profileid, Session["group"].ToString(),type);
                    }
                    else if (type == "tumblr")
                    {
                        Api.TumblrAccount.TumblrAccount apiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                        apiobjTumblrAccount.DeleteTumblrAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "youtube")
                    {
                        Api.YoutubeAccount.YoutubeAccount apiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                        apiobjYoutubeAccount.DeleteYoutubeAccount(objUser.Id.ToString(), profileid, Session["group"].ToString(), type);
                    }
                    else if (type == "liComPage")
                    {
                        string accesstoken = "";
                        string returndata = "";
                        List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
                        Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
                        Parameters.Add(new KeyValuePair<string, string>("ProfileId", profileid));
                        Parameters.Add(new KeyValuePair<string, string>("GroupId", GroupId));
                        if (Session["access_token"] != null)
                        {
                            accesstoken = Session["access_token"].ToString();
                        }
                        HttpResponseMessage response = await WebApiReq.PostReq("api/ApiLinkedIn/DeleteLinkedinCompanyPage", Parameters, "Bearer", accesstoken);
                        if (response.IsSuccessStatusCode)
                        {
                            returndata = await response.Content.ReadAsAsync<string>();
                        }
                    }
                    else if (type == "gplus")
                    {
                        Api.GooglePlusAccount.GooglePlusAccount objGooglePlusAccount = new Api.GooglePlusAccount.GooglePlusAccount();
                        objGooglePlusAccount.DeleteGplusAccount(objUser.Id.ToString(), profileid, Session["group"].ToString(), "gplus");
                        
                    }
                    else if (type == "ga")
                    {
                        Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount ApiGoogleAnalyticsAccount = new Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount();
                        ApiGoogleAnalyticsAccount.DeleteGoogelAnalyticsAccount(objUser.Id.ToString(), profileid, Session["group"].ToString(), "googleanalytics");

                    }
                    return Content("Deleted");
                //}
                //else
                //{
                //    return Content("Not Deleted");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("Not Deleted");
            }
        }

        public async Task<ActionResult> ComposeMessage()
        {
            User objUser = (User)Session["User"];
           // Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
            Dictionary<Domain.Socioboard.Domain.GroupProfile, object> lstGroupProfiles = new Dictionary<Domain.Socioboard.Domain.GroupProfile, object>();
            if (Session["group"] != null)
            {
                lstGroupProfiles = await SBHelper.GetGroupProfiles();
            }
            return PartialView("_ComposeMessagePartial", lstGroupProfiles);
        }

        public async Task<ActionResult> ComposeMessageSend(string message, string allprofiles, string curdaatetimetime)
        {
            User objUser = (User)Session["User"];
            //curdaatetimetime = DateExtension.ToClientTime(DateTime.UtcNow);
            string groupid = Session["group"].ToString();
            Socioboard.Api.Groups.Groups ApiobjGroups = new Socioboard.Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            string[] profileandidarr = Regex.Split(allprofiles, "<:>");
            var fi = Request.Files["file"];
            string file = string.Empty;


            //Get Dropbox Selected Images
            //string[] DropboxImg = null;
            //try
            //{
            //    DropboxImg = Request.Form["DropboxImg"].Split(',');
            //}
            //catch { };



            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");

                    // var path = System.Configuration.ConfigurationManager.AppSettings["MailSenderDomain"]+"Contents/img/upload";
                    file = path + "\\" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    path = path + "\\" + fi.FileName;
                    logger.Error(path);
                }
            }
            foreach (var item in profileandidarr)
            {
                string[] profileandid = item.Split('~');
                string profiletype = profileandid[1];
                string profileid = profileandid[0];
                int DBXCount = 0;

            DbxNext:

                //if (!string.IsNullOrEmpty(DropboxImg[0]))
                //{
                //if (DropboxImg.Count() != 0 && DropboxImg.Count() >= DBXCount)
                //{
                //    file = DropboxImg[DBXCount];
                //    DBXCount++;
                //}
                //}
                try
                {

                    if (profiletype == "facebook")
                    {
                        Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                        ApiobjFacebook.FacebookComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file,"");

                    }
                    if (profiletype == "facebook_page")
                    {
                        Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                        ApiobjFacebook.FacebookComposeMessageForPage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file,"");
                    }

                    if (profiletype == "twitter")
                    {
                        Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                        ApiobjTwitter.TwitterComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file);

                    } if (profiletype == "linkedin")
                    {
                 string accesstoken = "";
                 string returndata = "";
                 List<KeyValuePair<string, string>> Parameters = new List<KeyValuePair<string, string>>();
                 Parameters.Add(new KeyValuePair<string, string>("GroupId", Session["group"].ToString()));
                 Parameters.Add(new KeyValuePair<string, string>("comment", message));
                 Parameters.Add(new KeyValuePair<string, string>("UserId", objUser.Id.ToString()));
                 Parameters.Add(new KeyValuePair<string, string>("ImageUrl", file));
                 Parameters.Add(new KeyValuePair<string, string>("ProfileId", profileid));
                 if (Session["access_token"] != null)
                 {
                     accesstoken = Session["access_token"].ToString();
                 }
                 HttpResponseMessage response = await WebApiReq.PostReq("api/ApiLinkedIn/LinkedInProfileUpdate", Parameters, "Bearer", accesstoken);
                 if (response.IsSuccessStatusCode)
                 {
                     returndata = await response.Content.ReadAsAsync<string>();
                 }
                    }
                    if (profiletype == "tumblr")
                    {
                        Api.Tumblr.Tumblr ApiobjTumblr = new Api.Tumblr.Tumblr();
                        ApiobjTumblr.TumblrComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file);
                    }


                    Api.ScheduledMessage.ScheduledMessage objAddComposeSentMessage = new Api.ScheduledMessage.ScheduledMessage();
                    objAddComposeSentMessage.AddComposeMessage(objGroups.UserId.ToString(), profileid, profiletype, message, file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //if (!string.IsNullOrEmpty(DropboxImg[0]))
                //{
                //    if (DBXCount < DropboxImg.Count())
                //    {
                //        goto DbxNext;
                //    }
                //}
            }
            return Content("");
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult RecentProfiles()
        {
            User objUser = (User)Session["User"];
            Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
            ApiobjTwitter.Timeout = 300000;
            List<Domain.Socioboard.Helper.TwitterRecentFollower> lstTwitterRecentFollower = (List<Domain.Socioboard.Helper.TwitterRecentFollower>)(new JavaScriptSerializer().Deserialize(ApiobjTwitter.TwitterRecentFollower(objUser.Id.ToString()), typeof(List<Domain.Socioboard.Helper.TwitterRecentFollower>)));
            return PartialView("_RecentFollowerPartial", lstTwitterRecentFollower);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> DisplayCount()
        {
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty; 
            int fbmsgcount = 0;
            int twtmsgcount = 0;
            int allsentmsgcount = 0;
            User objUser = (User)Session["User"];



            Dictionary<Domain.Socioboard.Domain.GroupProfile, object> allprofileofuser =await SBHelper.GetGroupProfiles();
            foreach (var item in allprofileofuser)
            {
                try
                {
                    //Domain.Socioboard.Domain.TeamMemberProfile item = (Domain.Socioboard.Domain.TeamMemberProfile)(profile.Value);
                    if (item.Key.ProfileType == "facebook" || item.Key.ProfileType == "facebook_page")
                    {
                        FbProfileId += item.Key.ProfileId + ',';
                    }
                    else if (item.Key.ProfileType == "twitter")
                    {
                        TwtProfileId += item.Key.ProfileId + ',';
                    }
                    AllProfileId += item.Key.ProfileId + ',';
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
            }
            try
            {
                FbProfileId = FbProfileId.Substring(0, FbProfileId.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                TwtProfileId = TwtProfileId.Substring(0, TwtProfileId.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                AllProfileId = AllProfileId.Substring(0, AllProfileId.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            //try
            //{
            //    Api.FacebookFeed.FacebookFeed objFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            //    objFacebookFeed.Timeout = 300000;
            //    //fbmsgcount = ((List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(objFacebookFeed.getAllFeedDetail1(FbProfileId, objUser.Id.ToString()), typeof(List<FacebookFeed>)))).Count;
            //    fbmsgcount = objFacebookFeed.GetFeedCountByProfileIdAndUserId(objUser.Id.ToString(), FbProfileId);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //}
            //try
            //{
            //    Api.TwitterMessage.TwitterMessage objTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            //    objTwitterMessage.Timeout = 300000;
            //    //twtmsgcount = ((List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(objTwitterMessage.getAlltwtMessages1(TwtProfileId, objUser.Id.ToString()), typeof(List<TwitterMessage>)))).Count;
            //    twtmsgcount = objTwitterMessage.GetFeedCountByProfileIdAndUserId(objUser.Id.ToString(), TwtProfileId);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //}
            try
            {
                Api.ScheduledMessage.ScheduledMessage objScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                objScheduledMessage.Timeout = 300000;
                //allsentmsgcount = ((List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(objScheduledMessage.getAllSentMessageDetails(AllProfileId, objUser.Id.ToString()), typeof(List<ScheduledMessage>)))).Count;
                allsentmsgcount = objScheduledMessage.GetSentMessageCountByProfileIdAndUserId(objUser.Id.ToString(), AllProfileId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            string _totalIncomingMessage = "0";
            string _totalSentMessage = "0";
            string _totalTwitterFollowers = "0";
            string _totalFacebookFan = "0";

            try
            {
                //_totalIncomingMessage = (fbmsgcount + twtmsgcount).ToString();
                Api.InboxMessages.InboxMessages _InboxMessages = new Api.InboxMessages.InboxMessages();
                _totalIncomingMessage = _InboxMessages.getInboxMessageCount(objUser.Id.ToString(), AllProfileId);
            }
            catch (Exception ex)
            {
                _totalIncomingMessage = "0";
                Console.WriteLine(ex.StackTrace);
            }
            //try
            //{
            //    _totalIncomingMessage = (fbmsgcount + twtmsgcount).ToString();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //}
            try
            {
                _totalSentMessage = allsentmsgcount.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                _totalTwitterFollowers = SBUtils.GetAllTwitterFollowersCountofUser(TwtProfileId, objUser.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                _totalFacebookFan = SBUtils.GetAllFacebookFancountofUser(FbProfileId, objUser.Id.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            ViewBag._totalIncomingMessage = _totalIncomingMessage;
            ViewBag._totalSentMessage = _totalSentMessage;
            ViewBag._totalTwitterFollowers = _totalTwitterFollowers;
            ViewBag._totalFacebookFan = _totalFacebookFan;
            return PartialView("_HomeUserActivityPartial");
        }


        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ContactSearchFacebook(string keyword)
        {
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            try
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();

                lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.contactSearchFacebook(keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return PartialView("_FacebookContactPartial", lstDiscoverySearch);
        }


        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        //public ActionResult ContactSearchTwitter(string keyword)
        //{
        //    Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
        //    Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
        //    List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
        //    lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.contactSearchTwitter(keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
        //    return PartialView("_TwitterContactPartial", lstDiscoverySearch);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ContactSearchInstagram(string keyword)
        {
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.ContactSearchInstagram(keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_InstagramContactPartial", lstDiscoverySearch);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ContactSearchTwitter(string keyword)
        {
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.contactSearchTwitter(keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_TwitterContactPartial", lstDiscoverySearch);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> InstagramProfile()
        {
            Session["AllInstagramAccount"] =await SBHelper.GetUserTeamMemberInstaProfiles();
            return PartialView("_InstagramProfilePartial");
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> TwitterProfile()
        {
            Session["AllTwitterAccount"] = await SBHelper.GetUserTeamMemberTwitterProfiles();
            return PartialView("_TwitterProfilePartial");
        }
        public ActionResult FollowInstagramUser(string fromId, string toId, string toName)
        {
            Api.Instagram.Instagram ApiInstagram = new Api.Instagram.Instagram();
            string[] arrId = fromId.Split(',');
            List<Domain.Socioboard.Domain.InstagramAccount> lstInstaAccount = ((List<Domain.Socioboard.Domain.InstagramAccount>)Session["AllInstagramAccount"]).Where(t => arrId.Contains(t.InstagramId)).ToList();
            foreach (var item in lstInstaAccount)
            {
                string ret = ApiInstagram.PostFollow(toId, toName, item.InstagramId, item.AccessToken);
            }
            return Content("success");
        }

        public ActionResult UnfollowInstagramUser(string fromId, string toId, string toName)
        {
            Api.Instagram.Instagram ApiInstagram = new Api.Instagram.Instagram();
            string[] arrId = fromId.Split(',');
            List<Domain.Socioboard.Domain.InstagramAccount> lstInstaAccount = ((List<Domain.Socioboard.Domain.InstagramAccount>)Session["AllInstagramAccount"]).Where(t => arrId.Contains(t.InstagramId)).ToList();
            foreach (var item in lstInstaAccount)
            {
                string ret = ApiInstagram.PostUnfollow(toId, toName, item.InstagramId, item.AccessToken);
            }
            return Content("success");
        }

        public ActionResult FollowTwitterUser(string fromId, string toId, string toName)
        {
            Api.Twitter.Twitter ApiTwitter = new Api.Twitter.Twitter();
            string[] arrId = fromId.Split(',');
            List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = ((List<Domain.Socioboard.Domain.TwitterAccount>)Session["AllTwitterAccount"]).Where(t => arrId.Contains(t.TwitterUserId)).ToList();
            foreach (var item in lstTwitterAccount)
            {
                string ret = ApiTwitter.PostFollow(item.OAuthToken, item.OAuthSecret, item.TwitterScreenName, item.TwitterUserId, toId, toName);
            }
            return Content("success");
        }

        public ActionResult UnfollowTwitterUser(string fromId, string toId, string toName)
        {
            Api.Twitter.Twitter ApiTwitter = new Api.Twitter.Twitter();
            string[] arrId = fromId.Split(',');
            List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = ((List<Domain.Socioboard.Domain.TwitterAccount>)Session["AllTwitterAccount"]).Where(t => arrId.Contains(t.TwitterUserId)).ToList();
            foreach (var item in lstTwitterAccount)
            {
                string ret = ApiTwitter.PostUnFollow(item.OAuthToken, item.OAuthSecret, item.TwitterScreenName, item.TwitterUserId, toId, toName);
            }
            return Content("success");
        }

        public ActionResult BindGAProfiles()
        {
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
            Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount ApiGoogleAnalyticsAccount = new Api.GoogleAnalyticsAccount.GoogleAnalyticsAccount();
            List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lstGoogleAnalyticsAccount = (List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>)new JavaScriptSerializer().Deserialize(ApiGoogleAnalyticsAccount.GetGoogleAnalyticsAccountByUser(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>));
            return PartialView("_GAProfilesPartial",lstGoogleAnalyticsAccount);
        }
        public ActionResult BindFbPage()
        { 
            Domain.Socioboard.Domain.User _User=(Domain.Socioboard.Domain.User)Session["User"];
            Api.FacebookAccount.FacebookAccount ApiFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount=(List<Domain.Socioboard.Domain.FacebookAccount>)new JavaScriptSerializer().Deserialize(ApiFacebookAccount.GetAllFacebookPageByUserIdAndGroupId(_User.Id.ToString(),Session["group"].ToString()),typeof(List<Domain.Socioboard.Domain.FacebookAccount>));
            return PartialView("_FBPagePartial", lstFacebookAccount);
        }

        public ActionResult AddGaProfiles(string arrId)
        {
            Domain.Socioboard.Domain.User _User=(Domain.Socioboard.Domain.User)Session["User"];
            Api.GoogleAnalytics.GoogleAnalytics ApiGoogleAnalytics = new Api.GoogleAnalytics.GoogleAnalytics();
            string [] arrProfiles=arrId.Split(',');
            List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles> lstGoogleAnalyticsProfiles = (List<Domain.Socioboard.Helper.GoogleAnalyticsProfiles>)Session["GAProfiles"];
            lstGoogleAnalyticsProfiles = lstGoogleAnalyticsProfiles.Where(t => arrId.Contains(t.ProfileId)).ToList();
            string strGoogleAnalyticsProfiles = new JavaScriptSerializer().Serialize(lstGoogleAnalyticsProfiles);
            string ret = ApiGoogleAnalytics.AddAnalyticsProfiles(strGoogleAnalyticsProfiles, _User.Id.ToString(), Session["group"].ToString());
            return Content("Success");
        }
        public ActionResult pagenotfound()
        {
            return View("pagenotfound");
        }

        public ActionResult Internal() 
        {
            return View();
        }

        public ActionResult training()
        {
            return View();
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AddFirstProfile()
        {
            int ProfileCount = int.Parse(Session["ProfileCount"].ToString());
            if (ProfileCount == 0)
            {
                return Content("AddProfile");
            }
            else
            {
                return Content("WrongWindow");
            }
        }

        // Edited by Antima[20/12/2014]

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Tickets()
        {
            try
            {
                Domain.Socioboard.Domain.User objuser = (Domain.Socioboard.Domain.User)Session["User"];
                string groupid = Session["group"].ToString();
                Api.SentimentalAnalysis.SentimentalAnalysis ApiobjSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
                List<Domain.Socioboard.Domain.FBTwitterFeeds> fbtwitterfeeds = new List<Domain.Socioboard.Domain.FBTwitterFeeds>();
                fbtwitterfeeds = (List<Domain.Socioboard.Domain.FBTwitterFeeds>)(new JavaScriptSerializer().Deserialize(ApiobjSentimentalAnalysis.GetTicketsofGroup(groupid, objuser.Id.ToString()), typeof(List<Domain.Socioboard.Domain.FBTwitterFeeds>)));
                //if (fbtwitterfeeds.Count > 0)
                //{
                //    return View("MyTickets", fbtwitterfeeds);
                //}
                return View("MyTickets", fbtwitterfeeds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("SomethingWentWrong");
            }
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult NotificationTickets()
        {
            try
            {
                Domain.Socioboard.Domain.User objuser = (Domain.Socioboard.Domain.User)Session["User"];

                DataSet ds = null;
                clsFeedsAndMessages clsfeedsandmess = new clsFeedsAndMessages();

                ds = clsfeedsandmess.bindMyTickets(objuser.Id);
                if (ds.Tables.Count > 0 && ds != null)
                {
                    return PartialView("_TicketsNotificationPartial", ds);
                }
                else
                {
                    return Content("nodata");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("SomethingWentWrong");
            }
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TicketTwitterReply()
        {
            try
            {
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string comment = Request.QueryString["comment"];
                string ProfileId = Request.QueryString["ProfileId"];
                string messageid = Request.QueryString["messageid"];
                string replypost = ApiobjTwitter.TicketTwitterReply(comment, ProfileId, messageid);
                return Content("success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return Content("");
            }
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TicketFacebokReply()
        {
            try
            {
                Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                string comment = Request.QueryString["comment"];
                string ProfileId = Request.QueryString["ProfileId"];
                string messageid = Request.QueryString["messageid"];
                string replaypost = ApiobjFacebook.TicketFacebokReply(comment, ProfileId, messageid);
                return Content("success");
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return Content("");
            }
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Index");
        }


        public async System.Threading.Tasks.Task<ActionResult> test()
        {
            Socioboard.Helper.apiClientProvider ac = new Socioboard.Helper.apiClientProvider("http://localhost:6361");
            try
            {
                Dictionary<string, string> re = await ac.GetTokenDictionary("bandisuresh@globussoft.com", "asd123");
            }
            catch (Exception e)
            { }
            return View();
        }



        public ActionResult PluginComposeMessage()
        {
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();

            string profile = Request.Form["profile"];
            string twitterText = Request.Form["twitterText"];
            string tweetId = Request.Form["tweetId"];
            string tweetUrl = Request.Form["tweetUrl"];
            string facebookText = Request.Form["facebookText"];
            string url = Request.Form["url"];
            string imgUrl = Request.Form["imgUrl"];
            string curdaatetimetime = DateTime.Now.ToString();
            User objUser = (User)Session["User"];
            string[] arrProfile = Regex.Split(profile, ",");

            foreach (string item in arrProfile)
            {
                string[] profile_id = item.Split('~');
                string profileType = profile_id[1];
                string profileId = profile_id[0];
                if (profileType == "facebook")
                {
                    ApiobjFacebook.FacebookComposeMessage(facebookText, profileId, objUser.Id.ToString(), curdaatetimetime, imgUrl, url);
                }
                else if (profileType == "facebook_page")
                {
                    ApiobjFacebook.FacebookComposeMessageForPage(facebookText, profileId, objUser.Id.ToString(), curdaatetimetime, imgUrl, url);
                }
                else if (profileType == "twitter")
                {
                    if (!string.IsNullOrEmpty(twitterText) || !string.IsNullOrEmpty(imgUrl))
                    {
                        twitterText = twitterText + " " + tweetUrl;
                        ApiobjTwitter.TwitterComposeMessage(twitterText, profileId, objUser.Id.ToString(), curdaatetimetime, imgUrl);
                    }
                    else
                    {
                        ApiobjTwitter.TwitterReteet_post(objUser.Id.ToString(), profileId, tweetId);
                    }
                }
            }
            return Content("success");
        }
        public ActionResult PluginScheduleMessage(string scheduleTime, string clientTime)
        {
            User objUser = (User)Session["User"];
            Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();

            string profiles = Request.Form["profile"];
            string twitterText = Request.Form["twitterText"];
            string tweetId = Request.Form["tweetId"];
            string tweetUrl = Request.Form["tweetUrl"];
            string facebookText = Request.Form["facebookText"];
            string url = Request.Form["url"];
            string imgUrl = Request.Form["imgUrl"];

            string sdTime = Convert.ToDateTime(scheduleTime).ToString("yyyy/MM/dd HH:mm:ss");
            string ctTime = clientTime;

            string[] arrDateTime = Regex.Split(sdTime, " ");

            string[] arrProfile = Regex.Split(profiles, ",");

            foreach (string item in arrProfile)
            {
                string[] profile_id = item.Split('~');
                string profileType = profile_id[1];
                string profileId = profile_id[0];

                if (profileType == "facebook")
                {
                    ApiobjScheduledMessage.AddAllScheduledMessage(item, facebookText, ctTime, arrDateTime[0], arrDateTime[1], objUser.Id.ToString(), imgUrl, url);
                }
                else if (profileType == "facebook_page")
                {
                    ApiobjScheduledMessage.AddAllScheduledMessage(item, facebookText, ctTime, arrDateTime[0], arrDateTime[1], objUser.Id.ToString(), imgUrl, url);
                }
                else if (profileType == "twitter")
                {
                    if (!string.IsNullOrEmpty(twitterText) || !string.IsNullOrEmpty(imgUrl))
                    {
                        twitterText = twitterText + " " + tweetUrl;
                        ApiobjScheduledMessage.AddAllScheduledMessage(item, twitterText, ctTime, arrDateTime[0], arrDateTime[1], objUser.Id.ToString(), imgUrl, "");
                    }
                    else
                    {
                        ApiobjTwitter.TwitterReteet_post(objUser.Id.ToString(), profileId, tweetId);
                    }
                }
            }
            return Content("success");
        }

        public ActionResult UploadImage()
        {
            var fi = Request.Files["file"];
            string file = string.Empty;
            var retPath = System.Configuration.ConfigurationManager.AppSettings["DomainName"] + "/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload";
            if (Request.Files.Count > 0)
            {
                if (fi != null)
                {
                    var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "/Contents/img/upload");


                    file = path + "\\" + fi.FileName;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fi.SaveAs(file);
                    retPath = retPath + "/" + fi.FileName;
                }
            }
            return Content(retPath);
        }



        public async Task<ActionResult> LoadGroups() 
        {
            string accesstoken = string.Empty;
            User objUser = (User)Session["user"];
            try
            {
                accesstoken = Session["access_token"].ToString();
            }
            catch { }
            IEnumerable<Domain.Socioboard.Domain.Groups> lstGroups = new List<Domain.Socioboard.Domain.Groups>();
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroups/GetGroupsOfUser?UserId=" + objUser.Id, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                lstGroups = await response.Content.ReadAsAsync<IEnumerable<Domain.Socioboard.Domain.Groups>>();
            }
            return View(lstGroups);

        }


        [CustomAuthorize]
        public async Task<ActionResult> GroupMember(string MemberId, string code) 
        {
            string res = string.Empty;
            string accesstoken = string.Empty;
            User user = (User)Session["user"];
            try 
            {
                accesstoken = Session["access_token"].ToString();
            }
            catch { }
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupMembers/VadifyGroupMemeber?MemberId=" + MemberId + "&Code=" + code+"&UserId="+user.Id, "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsAsync<string>();
                if (res.Equals("Wrong Code"))
                {
                    ViewBag.Data = "Wrong Code";
                    return View();
                }
                else if (res.Equals("Member Not Exist"))
                {
                    ViewBag.Data = "Invalid invitation pls check with admin";
                    return View();
                }
                else if (res.Equals("Email Doesn't match"))
                {
                    ViewBag.Data = "Invalid invitation pls check with admin";
                    return View();
                }
                else { return RedirectToAction("index", "home"); }
            }
            else 
            {
                res = await response.Content.ReadAsAsync<string>();

               
                return RedirectToAction("Registration", "index");
            }

            return View("Registration", "index");
        }


        public async Task<ActionResult> GrpProfileSev()
        {
            string res = string.Empty;
            string accesstoken = string.Empty;
            User user = (User)Session["user"];
            try
            {
                accesstoken = Session["access_token"].ToString();
            }
            catch { }
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupProfiles/TeamProfileTOGroupProfileService", "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsAsync<string>();
            }

            return View();
        }


        public async Task<ActionResult> GrpMemberSev()
        {
            string res = string.Empty;
            string accesstoken = string.Empty;
            User user = (User)Session["user"];
            try
            {
                accesstoken = Session["access_token"].ToString();
            }
            catch { }
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiGroupProfiles/GrpMemService", "Bearer", accesstoken);
            if (response.IsSuccessStatusCode)
            {
                res = await response.Content.ReadAsAsync<string>();
            }

            return View();
        }
    }
}
