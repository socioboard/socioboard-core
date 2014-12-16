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

namespace Socioboard.Controllers
{
    public class HomeController : Controller
    {
        private ILog logger = LogManager.GetLogger(typeof(HomeController));

        [MyExpirePageActionFilter]
        // [Authorize]
        [CustomAuthorize]
        public ActionResult Index()
        {

            if (Request.QueryString["teamid"] != null)
            {
                string teamid = Request.QueryString["teamid"].ToString();
                Api.Team.Team _apiteam = new Api.Team.Team();
                _apiteam.UpdateTeambyteamid(teamid);

            }
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
                    User objUser = (User)Session["User"];
                    Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();

                    apiobjSocialProfile.GetAllSocialProfiles();

                    Session["ProfileCount"] = Convert.ToInt16(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                    Session["TotalAccount"] = Convert.ToInt16(SBUtils.GetUserPackageProfileCount(objUser.AccountType));
                    ViewBag.AccountType = objUser.AccountType;
                    //if (Session["GroupName"] == null)
                    //{
                    //    Groups objGroupDetails = objGroupRepository.getGroupDetail(user.Id);
                    //    team = objTeamRepo.getAllDetails(objGroupDetails.Id, user.EmailId);
                    //    Session["GroupName"] = team;
                    //}

                    //else
                    //{
                    //    team = (SocioBoard.Domain.Team)Session["GroupName"];
                    //}    





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
                return View(User);
                // return PartialView("_HomePartial");
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

        public ActionResult ProfileSnapshot()
        {
            List<TeamMemberProfile> lstTeamMemberProfile = SBUtils.GetUserTeamMemberProfiles();
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, Dictionary<object, List<object>>> diclist = SBUtils.GetUserProfilesSnapsAccordingToGroup(lstTeamMemberProfile);
            return PartialView("_HomeProfileSnapshotPartial", diclist);
        }

        public ActionResult UserProfile()
        {
            // Thread.Sleep(3 * 1000);
            return PartialView("_HomeUserProfilePartial");
        }

        public ActionResult LoadGroup()
        {
            User objUser = (User)Session["User"];
            Api.Groups.Groups objApiGroups = new Api.Groups.Groups();
            JArray profile = JArray.Parse(objApiGroups.GetGroupDetailsByUserId(objUser.Id.ToString()));

            List<Groups> lstgroup = new List<Groups>();
            foreach (var item in profile)
            {
                Groups objGroups = new Groups();
                objGroups.Id = Guid.Parse(Convert.ToString(item["Id"]));
                objGroups.GroupName = Convert.ToString(item["GroupName"]);
                objGroups.UserId = Guid.Parse(Convert.ToString(item["UserId"]));
                objGroups.EntryDate = Convert.ToDateTime(Convert.ToString(item["EntryDate"]));
                lstgroup.Add(objGroups);
            }

            return PartialView("_LoadGroupPartial", lstgroup);

        }

        public ActionResult ChangeGroup()
        {
            string groupid = Request.QueryString["groupid"].ToString();
            Session["group"] = groupid;

            return Content("success");
        }

        public ActionResult loadprofiles()
        {
            User objUser = (User)Session["User"];
            //List<TeamMemberProfile> lstTeamMemberProfile = new List<TeamMemberProfile>();
            Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
            // Api.Team.Team objApiTeam = new Api.Team.Team();
            if (Session["group"] != null)
            {
                dict_TeamMember = SBUtils.GetUserProfilesccordingToGroup();
            }
            return PartialView("_PofilePartial", dict_TeamMember);
        }

        [HttpPost]
        public ActionResult DeleteAccount()
        {

            string type = Request.QueryString["profile"].ToString();
            string profileid = Request.QueryString["profileid"].ToString();
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            // Edited By Antima[15/12/2014]
            string GroupId = Session["group"].ToString();
            Api.Team.Team objApiTeam = new Api.Team.Team();
            Domain.Socioboard.Domain.Team team = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Team));
            Guid AdminUserId = team.UserId;
            try
            {
                if (AdminUserId == objUser.Id)
                {
                    if (type == "fb")
                    {
                        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                        ApiobjFacebookAccount.DeleteFacebookAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "twt")
                    {
                        Api.TwitterAccount.TwitterAccount apiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                        apiobjTwitterAccount.DeleteTwitterAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "linkedin")
                    {
                        Api.LinkedinAccount.LinkedinAccount apiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                        apiobjLinkedinAccount.DeleteLinkedinAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "instagram")
                    {
                        Api.InstagramAccount.InstagramAccount apiobjInstagramAccount = new Api.InstagramAccount.InstagramAccount();
                        apiobjInstagramAccount.DeleteInstagramAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "tumblr")
                    {
                        Api.TumblrAccount.TumblrAccount apiobjTumblrAccount = new Api.TumblrAccount.TumblrAccount();
                        apiobjTumblrAccount.DeleteTumblrAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "youtube")
                    {
                        Api.YoutubeAccount.YoutubeAccount apiobjYoutubeAccount = new Api.YoutubeAccount.YoutubeAccount();
                        apiobjYoutubeAccount.DeleteYoutubeAccount(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    else if (type == "liComPage")
                    {
                        Api.LinkedinCompanyPage.LinkedinCompanyPage apiobjLinkedinCompanyPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
                        apiobjLinkedinCompanyPage.DeleteLinkedinCompanyPage(objUser.Id.ToString(), profileid, Session["group"].ToString());
                    }
                    return Content("Deleted");
                }
                else
                {
                    return Content("Not Deleted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return Content("Not Deleted");
            }
        }

        public ActionResult ComposeMessage()
        {
            User objUser = (User)Session["User"];
            Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
            if (Session["group"] != null)
            {
                dict_TeamMember = SBUtils.GetUserProfilesccordingToGroup();
            }
            return PartialView("_ComposeMessagePartial", dict_TeamMember);
        }

        public ActionResult ComposeMessageSend(string message, string allprofiles, string curdaatetimetime)
        {
            User objUser = (User)Session["User"];
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

                if (profiletype == "facebook")
                {
                    Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                    ApiobjFacebook.FacebookComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file);
                }
                if (profiletype == "twitter")
                {
                    Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                    ApiobjTwitter.TwitterComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file);

                } if (profiletype == "linkedin")
                {
                    Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
                    ApiobjLinkedin.LinkedinComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file);
                }
                if (profiletype == "tumblr")
                {
                    Api.Tumblr.Tumblr ApiobjTumblr = new Api.Tumblr.Tumblr();
                    ApiobjTumblr.TumblrComposeMessage(message, profileid, objGroups.UserId.ToString(), curdaatetimetime, file);
                }


                Api.ScheduledMessage.ScheduledMessage objAddComposeSentMessage = new Api.ScheduledMessage.ScheduledMessage();
                objAddComposeSentMessage.AddComposeMessage(objGroups.UserId.ToString(), profileid, profiletype, message);

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

        public ActionResult RecentProfiles()
        {
            User objUser = (User)Session["User"];
            Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
            List<Domain.Socioboard.Helper.TwitterRecentFollower> lstTwitterRecentFollower = (List<Domain.Socioboard.Helper.TwitterRecentFollower>)(new JavaScriptSerializer().Deserialize(ApiobjTwitter.TwitterRecentFollower(objUser.Id.ToString()), typeof(List<Domain.Socioboard.Helper.TwitterRecentFollower>)));
            return PartialView("_RecentFollowerPartial", lstTwitterRecentFollower);
        }

        public ActionResult DisplayCount()
        {
            string AllProfileId = string.Empty;
            string FbProfileId = string.Empty;
            string TwtProfileId = string.Empty;
            int fbmsgcount = 0;
            int twtmsgcount = 0;
            int allsentmsgcount = 0;
            User objUser = (User)Session["User"];
            Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = SBUtils.GetUserProfilesccordingToGroup();
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
            try
            {
                Api.FacebookFeed.FacebookFeed objFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                fbmsgcount = ((List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(objFacebookFeed.getAllFeedDetail1(FbProfileId, objUser.Id.ToString()), typeof(List<FacebookFeed>)))).Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                Api.TwitterMessage.TwitterMessage objTwitterMessage = new Api.TwitterMessage.TwitterMessage();
                twtmsgcount = ((List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(objTwitterMessage.getAlltwtMessages1(TwtProfileId, objUser.Id.ToString()), typeof(List<TwitterMessage>)))).Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                Api.ScheduledMessage.ScheduledMessage objScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                allsentmsgcount = ((List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(objScheduledMessage.getAllSentMessageDetails(AllProfileId, objUser.Id.ToString()), typeof(List<ScheduledMessage>)))).Count;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            string _totalIncomingMessage = (fbmsgcount + twtmsgcount).ToString();
            string _totalSentMessage = allsentmsgcount.ToString();
            string _totalTwitterFollowers = SBUtils.GetAllTwitterFollowersCountofUser(TwtProfileId, objUser.Id.ToString());
            string _totalFacebookFan = SBUtils.GetAllFacebookFancountofUser(FbProfileId, objUser.Id.ToString());

            ViewBag._totalIncomingMessage = _totalIncomingMessage;
            ViewBag._totalSentMessage = _totalSentMessage;
            ViewBag._totalTwitterFollowers = _totalTwitterFollowers;
            ViewBag._totalFacebookFan = _totalFacebookFan;
            return PartialView("_HomeUserActivityPartial");
        }


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


        public ActionResult ContactSearchTwitter(string keyword)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.contactSearchTwitter(keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_TwitterContactPartial", lstDiscoverySearch);
        }

        public ActionResult pagenotfound()
        {
            return View("pagenotfound");
        }

        public ActionResult training()
        {
            return View();
        }
    }
}
