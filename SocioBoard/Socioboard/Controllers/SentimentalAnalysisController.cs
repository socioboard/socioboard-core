using Socioboard.App_Start;
using Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Socioboard.Controllers
{
    [CustomAuthorize]
    public class SentimentalAnalysisController : Controller
    {
        public static int messageCount = 0;
        public static int facebookFeedCount = 0;
        public static int facebookMessageCount = 0;
        public static int twitterFeedCount = 0;
        public static int twitterMessageCount = 0;
        public static int inboxchatmessagecount = 0;
        //
        // GET: /SentimentalAnalysis/


        [Route("Sentiments/Inbox-Message")]
        public ActionResult Message()
        {
            return View();
        }

        public async Task<ActionResult> BindMessage(string load, string arrmsgtype, string arrid)
        {
            string MessageType = string.Empty;
            string TwitterProfiles = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            if (load == "first")
            {
                //Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = await SBUtils.GetUserProfilesccordingToGroup();
                Dictionary<Domain.Socioboard.Domain.GroupProfile, object> allprofileofuser = await SBHelper.GetGroupProfiles();
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "twitter")
                        {
                            TwitterProfiles += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    TwitterProfiles = TwitterProfiles.Substring(0, (TwitterProfiles.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                messageCount = 0;
                MessageType = "twt_mention,twt_retweet";
            }
            else if (load == "filter")
            {
                messageCount = 0;
                TwitterProfiles = arrid;
                MessageType = arrmsgtype;
            }
            else if (load == "scroll")
            {
                messageCount = messageCount + 10;
                TwitterProfiles = arrid;
                MessageType = arrmsgtype;

            }
            Api.InboxMessages.InboxMessages ApiInboxMessages = new Api.InboxMessages.InboxMessages();

            List<Domain.Socioboard.Domain.InboxMessages> _InboxMessages = (List<Domain.Socioboard.Domain.InboxMessages>)new JavaScriptSerializer().Deserialize(ApiInboxMessages.GetInboxMessageWithSentiments(objUser.Id.ToString(), TwitterProfiles, MessageType, messageCount.ToString(), "10"), typeof(List<Domain.Socioboard.Domain.InboxMessages>));
            if (_InboxMessages.Count > 0)
            {
                return PartialView("_MessagePartial", _InboxMessages);
            }
            else
            {
                return Content("no_data");
            }
           
        }
        [Route("Sentiments/Facebook-Feed")]
        public ActionResult FacebbokFeed()
        {
            return View("FacebbokFeed");
        }

        public async Task<ActionResult> BindFacebookFeed(string load, string arrid)
        {

            string FacebooProfiles = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            if (load == "first")
            {
                //Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = await SBUtils.GetUserProfilesccordingToGroup();
                Dictionary<Domain.Socioboard.Domain.GroupProfile, object> allprofileofuser = await SBHelper.GetGroupProfiles();
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "facebook")
                        {
                            FacebooProfiles += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    FacebooProfiles = FacebooProfiles.Substring(0, (FacebooProfiles.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                facebookFeedCount = 0;
            }
            else if (load == "filter")
            {
                facebookFeedCount = 0;
                FacebooProfiles = arrid;
            }
            else if (load == "scroll")
            {
                facebookFeedCount = facebookFeedCount + 10;
                FacebooProfiles = arrid;
               

            }
            //Api.InboxMessages.InboxMessages ApiInboxMessages = new Api.InboxMessages.InboxMessages();
            Api.FacebookMessage.FacebookMessage ApiFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<Domain.Socioboard.MongoDomain.FacebookMessage> _FacebookMessage = (List<Domain.Socioboard.MongoDomain.FacebookMessage>)new JavaScriptSerializer().Deserialize(ApiFacebookMessage.GetFacebookMessageWithSentiments(FacebooProfiles, facebookFeedCount.ToString(), "10"), typeof(List<Domain.Socioboard.MongoDomain.FacebookMessage>));
            if (_FacebookMessage.Count > 0)
            {
                return PartialView("_FacebookFeedPartial", _FacebookMessage);
            }
            else
            {
                return Content("no_data");
            }

        }
       [Route("Sentiments/Facebook-Wall")]
        public ActionResult FacebookMessage()
        {
            return View("FacebookWall");
        }

        public async Task<ActionResult> BindFacebookMessage(string load, string arrid)
        {
            string FacebooProfiles = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            if (load == "first")
            {
                //Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = await SBUtils.GetUserProfilesccordingToGroup();
                Dictionary<Domain.Socioboard.Domain.GroupProfile, object> allprofileofuser = await SBHelper.GetGroupProfiles();
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "facebook")
                        {
                            FacebooProfiles += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    FacebooProfiles = FacebooProfiles.Substring(0, (FacebooProfiles.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                facebookMessageCount = 0;
            }
            else if (load == "filter")
            {
                facebookMessageCount = 0;
                FacebooProfiles = arrid;
            }
            else if (load == "scroll")
            {
                facebookMessageCount = facebookMessageCount + 10;
                FacebooProfiles = arrid;


            }
            //Api.InboxMessages.InboxMessages ApiInboxMessages = new Api.InboxMessages.InboxMessages();
            Api.FacebookFeed.FacebookFeed ApiFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            List<Domain.Socioboard.Domain.MongoFacebookFeed> _FacebookFeed = (List<Domain.Socioboard.Domain.MongoFacebookFeed>)new JavaScriptSerializer().Deserialize(ApiFacebookFeed.GetFacebookFeedWithSentiments(objUser.Id.ToString(), FacebooProfiles, facebookMessageCount.ToString(), "10"), typeof(List<Domain.Socioboard.Domain.MongoFacebookFeed>));
            if (_FacebookFeed.Count > 0)
            {
                return PartialView("_FacebookWallPartial", _FacebookFeed);
            }
            else
            {
                return Content("no_data");
            }
        }

        [Route("Sentiments/Twitter-Feed")]
        public ActionResult TwitterFeed()
        {
            return View("TwitterFeed");
        }

        public async Task<ActionResult> BindTwitterFeed(string load, string arrid)
        {
            string TwitterProfiles = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            if (load == "first")
            {
                //Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = await SBUtils.GetUserProfilesccordingToGroup();
                Dictionary<Domain.Socioboard.Domain.GroupProfile, object> allprofileofuser = await SBHelper.GetGroupProfiles();
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "twitter")
                        {
                            TwitterProfiles += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    TwitterProfiles = TwitterProfiles.Substring(0, (TwitterProfiles.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                twitterFeedCount = 0;
            }
            else if (load == "filter")
            {
                twitterFeedCount = 0;
                TwitterProfiles = arrid;
            }
            else if (load == "scroll")
            {
                twitterFeedCount = twitterFeedCount + 10;
                TwitterProfiles = arrid;


            }
            Api.TwitterFeed.TwitterFeed ApiTwitterFeed=new Api.TwitterFeed.TwitterFeed();
            List<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = (List<Domain.Socioboard.MongoDomain.TwitterFeed>)new JavaScriptSerializer().Deserialize(ApiTwitterFeed.GetTwitterFeedWithSentiments(TwitterProfiles, twitterFeedCount.ToString(), "10"), typeof(List<Domain.Socioboard.MongoDomain.TwitterFeed>));
            if (_lstTwitterFeed.Count > 0)
            {
                return PartialView("_TwitterFeedPartial", _lstTwitterFeed);
            }
            else
            {
                return Content("no_data");
            }
           
        }
        [Route("Sentiments/Twitter-Tweets")]
        public ActionResult TwitterMessage()
        {
            return View("TwitterWall");
        }

        public async Task<ActionResult> BindTwitterMessage(string load, string arrid)
        {
            string TwitterProfiles = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            if (load == "first")
            {
                //Dictionary<Domain.Socioboard.Domain.TeamMemberProfile, object> allprofileofuser = await SBUtils.GetUserProfilesccordingToGroup();
                Dictionary<Domain.Socioboard.Domain.GroupProfile, object> allprofileofuser = await SBHelper.GetGroupProfiles();
                foreach (var item in allprofileofuser)
                {
                    try
                    {
                        if (item.Key.ProfileType == "twitter")
                        {
                            TwitterProfiles += item.Key.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                }
                try
                {
                    TwitterProfiles = TwitterProfiles.Substring(0, (TwitterProfiles.Length - 1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                twitterMessageCount = 0;
            }
            else if (load == "filter")
            {
                twitterMessageCount = 0;
                TwitterProfiles = arrid;
            }
            else if (load == "scroll")
            {
                twitterMessageCount = twitterMessageCount + 10;
                TwitterProfiles = arrid;


            }
            Api.TwitterMessage.TwitterMessage ApiTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<Domain.Socioboard.MongoDomain.TwitterMessage> _lstTwitterMessage = (List<Domain.Socioboard.MongoDomain.TwitterMessage>)new JavaScriptSerializer().Deserialize(ApiTwitterMessage.GetTwitterFeedWithSentiments(TwitterProfiles, twitterMessageCount.ToString(), "10"), typeof(List<Domain.Socioboard.MongoDomain.TwitterMessage>));
            if (_lstTwitterMessage.Count > 0)
            {
                return PartialView("_TwitterWallPartial", _lstTwitterMessage);
            }
            else
            {
                return Content("no_data");
            }
        }


        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> BindTwitterProfiles(string type)
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            ViewBag.type = type;
            string groupid = Session["group"].ToString();
            //Domain.Socioboard.Domain.Team team = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Team));
            //Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            //List<Domain.Socioboard.Domain.TeamMemberProfile> alstprofiles = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(team.Id.ToString()), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));
            Dictionary<Domain.Socioboard.Domain.GroupProfile,object> alstprofiles = await SBHelper.GetGroupProfilesByGroupId(groupid);
            return PartialView("_TwitterProfilePartial", alstprofiles);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public async Task<ActionResult> BindFacebookProfiles(string type)
        {
            Api.Team.Team objApiTeam = new Api.Team.Team();
            ViewBag.type = type;
            string groupid = Session["group"].ToString();
            //Domain.Socioboard.Domain.Team team = (Domain.Socioboard.Domain.Team)new JavaScriptSerializer().Deserialize(objApiTeam.GetTeamByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Team));
            //Api.TeamMemberProfile.TeamMemberProfile objApiTeamMemberProfile = new Api.TeamMemberProfile.TeamMemberProfile();
            //List<Domain.Socioboard.Domain.TeamMemberProfile> alstprofiles = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objApiTeamMemberProfile.GetTeamMemberProfilesByTeamId(team.Id.ToString()), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));
            Dictionary<Domain.Socioboard.Domain.GroupProfile, object> alstprofiles = await SBHelper.GetGroupProfilesByGroupId(groupid);
            return PartialView("_FacebookProfilePartial", alstprofiles);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult BindMessageType()
        {
            return PartialView("_MessageTypePartial");
        }
             
       

	}
}