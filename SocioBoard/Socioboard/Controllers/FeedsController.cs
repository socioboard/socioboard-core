using Domain.Socioboard.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    public class FeedsController : Controller
    {
        public static int facebookwallcount = 0;
        //
        // GET: /Feeds/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult loadaccount()
        {

            return PartialView("_FeedLeftPartial");
        }

        public ActionResult loadfeeds()
        {

            return PartialView("_FeedRightPartial");
        }
        public ActionResult loadmenu()
        {
            return PartialView("_FeedMenu", Helper.SBUtils.GetFeedsMenuAccordingToGroup());
        }

        public ActionResult LoadFeedPartialPage(string network)
        {
            return PartialView("_FeedPartial", network);
        }
       
        public ActionResult wallposts(string op, string load, string profileid)
        {
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            if (load == "first")
            {
                Session["FacebookProfileIdForFeeds"] = profileid;
                facebookwallcount = 0;
            }
            else
            {
                profileid = (string)Session["FacebookProfileIdForFeeds"];
                facebookwallcount = facebookwallcount + 10;
            }
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = (List<Domain.Socioboard.Domain.FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.GetAllWallpostsOfProfileAccordingtoGroup(profileid, facebookwallcount, objGroups.UserId.ToString()), typeof(List<Domain.Socioboard.Domain.FacebookMessage>)));
            List<object> lstobject = new List<object>();
            foreach (var item in lstFacebookMessage)
            {
                lstobject.Add(item);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }

        public ActionResult AjaxFeeds(string profileid)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(objGroups.UserId.ToString(), profileid), typeof(List<FacebookFeed>)));
            foreach (var twittermsg in lstFacebookFeed)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }

        public ActionResult scheduler(string network, string profileid)
        {
            Dictionary<object, List<ScheduledMessage>> dictscheduler = new Dictionary<object, List<ScheduledMessage>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            if (network == "facebook")
            {
                Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(FacebookAccount)));
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                List<ScheduledMessage> objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
                dictscheduler.Add(objFacebookAccount, objScheduledMessage);
            }
            else if (network == "twitter")
            {
                Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(TwitterAccount)));
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                List<ScheduledMessage> objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
                dictscheduler.Add(objTwitterAccount, objScheduledMessage);
            }
            else if (network == "linkedin")
            {
                Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(LinkedInAccount)));
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                List<ScheduledMessage> objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
                dictscheduler.Add(objLinkedInAccount, objScheduledMessage);
            }

            return PartialView("_Panel3Partial", dictscheduler);
        }

        public ActionResult FacebookComment(string fbcommentid, string profileid, string message)
        {
            
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            string ret = ApiobjFacebook.FacebookComment(message, profileid, fbcommentid, objGroups.UserId.ToString());
            return Content(ret);
        }

        public ActionResult FacebookLike(string fbid, string profileid, string msgid)
        {
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            string ret = ApiobjFacebook.FacebookLike(msgid, profileid, fbid, objGroups.UserId.ToString());
            return Content(ret);
        }


        public ActionResult TwitterNetworkDetails(string profileid)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
            List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objGroups.UserId.ToString(), profileid), typeof(List<TwitterFeed>)));
            foreach (var twitterfeed in lstTwitterFeed)
            {
                lstobject.Add(twitterfeed);
            }
            dictwallposts.Add("twitter", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }


        public ActionResult TwitterFeeds(string profileid)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterMessage = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessages(profileid, objGroups.UserId.ToString()), typeof(List<TwitterMessage>)));
            foreach (var twittermsg in lstTwitterMessage)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("twitter", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }


        public ActionResult linkedinwallposts(string profileid)
        {
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
            List<Domain.Socioboard.Domain.LinkedInFeed> lstLinkedInFeed = (List<Domain.Socioboard.Domain.LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.LinkedInFeed>)));
            List<object> lstobject = new List<object>();
            foreach (var item in lstLinkedInFeed)
            {
                lstobject.Add(item);
            }
            dictwallposts.Add("linkedin", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }

        public ActionResult LinkedinFeeds(string profileid)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            List<Domain.Socioboard.Domain.LinkedInUser.User_Updates> lstlinkedinFeeds = (List<Domain.Socioboard.Domain.LinkedInUser.User_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedUserUpdates(profileid, objGroups.UserId.ToString()), typeof(List<Domain.Socioboard.Domain.LinkedInUser.User_Updates>)));
            foreach (var linkledinfeed in lstlinkedinFeeds)
            {
                lstobject.Add(linkledinfeed);
            }
            dictwallposts.Add("linkedin", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }


        public ActionResult InstagramImages(string profileid)
        {
            //List<object> lstobject = new List<object>();
            //Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            //Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            //Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            //Api.InstagramFeed.InstagramFeed ApiobjInstagramFeed = new Api.InstagramFeed.InstagramFeed();
            //List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.InstagramFeed>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramFeed.GetLinkedInFeeds(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.InstagramFeed>)));
            //foreach (var lstInstagramfeed in lstInstagramFeed)
            //{
            //    lstobject.Add(lstInstagramfeed);
            //}
            //dictwallposts.Add("instagram", lstobject);
            //return PartialView("_ImagePartial", dictwallposts);


            object lstobject = new object();           
            List<object> lstComment = null;

            Dictionary<string, Dictionary<object, List<object>>> dictwallposts = new Dictionary<string, Dictionary<object, List<object>>>();
            Dictionary<object, List<object>> dic_InstgramImg = new Dictionary<object, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.InstagramFeed.InstagramFeed ApiobjInstagramFeed = new Api.InstagramFeed.InstagramFeed();
            Api.InstagramComment.InstagramComment ApiobjInstagramFeedComment = new Api.InstagramComment.InstagramComment();

            List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.InstagramFeed>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramFeed.GetLinkedInFeeds(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.InstagramFeed>)));
           
            foreach (var item_lstInstagramfeed in lstInstagramFeed)
            {
                lstComment = new List<object>();
                List<Domain.Socioboard.Domain.InstagramComment> lstInstagramComment = (List<Domain.Socioboard.Domain.InstagramComment>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramFeedComment.GetInstagramFeedsComment(objGroups.UserId.ToString(), item_lstInstagramfeed.FeedId.ToString()), typeof(List<Domain.Socioboard.Domain.InstagramComment>)));
               
                foreach (var item in lstInstagramComment)
                {
                    lstComment.Add(item);
                }
                lstobject = (object)item_lstInstagramfeed;              
                dic_InstgramImg.Add(lstobject, lstComment); 
            }          
            dictwallposts.Add("instagram", dic_InstgramImg);         
            return PartialView("_ImagePartial", dictwallposts);

        }
     
        public ActionResult TumblrImages(string profileid)
        {
            //List<object> lstobject = new List<object>();
            //Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            //Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            //Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            //Api.TumblrFeed.TumblrFeed ApiobjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
            //List<Domain.Socioboard.Domain.TumblrFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsers(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));
            //foreach (var lstInstagramfeed in lstInstagramFeed)
            //{
            //    lstobject.Add(lstInstagramfeed);
            //}
            //dictwallposts.Add("tumblr", lstobject);
            //return PartialView("_ImagePartial", dictwallposts);

            object lstobject = new object();
            List<object> lstComment = null;
            Dictionary<string, Dictionary<object, List<object>>> dictwallposts = new Dictionary<string, Dictionary<object, List<object>>>();
            Dictionary<object, List<object>> dic_TumblrImg = new Dictionary<object, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TumblrFeed.TumblrFeed ApiobjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
            List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsers(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));
            foreach (var item_lstTumblrFeed in lstTumblrFeed)
            {
                lstComment = new List<object>();

                lstobject = (object)item_lstTumblrFeed;
                dic_TumblrImg.Add(lstobject, lstComment); 
            }
            dictwallposts.Add("tumblr", dic_TumblrImg);
            return PartialView("_ImagePartial", dictwallposts);


        }

        public ActionResult YoutubeChannelVideos(string profileid)
        {
            //List<object> lstobject = new List<object>();
            //Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            //Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            //Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            //Api.Youtube.Youtube ApiobjYoutube = new Api.Youtube.Youtube();
            //string channeldetails = ApiobjYoutube.GetYoutubeChannelVideos(objGroups.UserId.ToString(), profileid);
            //JObject obj = JObject.Parse(channeldetails);
            //JArray array = (JArray)obj["items"];

            //foreach (var item in array)
            //{
            //    try
            //    {
            //        lstobject.Add(item["snippet"]["thumbnails"]["maxres"]["url"].ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.StackTrace);
            //    }
            //}
            //dictwallposts.Add("youtube", lstobject);
            //return PartialView("_ImagePartial", dictwallposts);

            object lstobject = new object();
            List<object> lstComment = null;
            Dictionary<string, Dictionary<object, List<object>>> dictwallposts = new Dictionary<string, Dictionary<object, List<object>>>();
            Dictionary<object, List<object>> dic_youtube = new Dictionary<object, List<object>>();
           
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Youtube.Youtube ApiobjYoutube = new Api.Youtube.Youtube();
            string channeldetails = ApiobjYoutube.GetYoutubeChannelVideos(objGroups.UserId.ToString(), profileid);
            JObject obj = JObject.Parse(channeldetails);
            JArray array = (JArray)obj["items"];

            foreach (var item in array)
            {
                try
                {
                    lstComment = new List<object>();
                    lstobject=(object)item["snippet"]["thumbnails"]["maxres"]["url"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                dic_youtube.Add(lstobject, lstComment);
            }
            dictwallposts.Add("youtube", dic_youtube);
            return PartialView("_ImagePartial", dictwallposts);
        }

        public ActionResult LikeUnlikeInstagramPost(FormCollection _FormCollection)
        {
            string LikeCount = _FormCollection["LikeCount"];
            string IsLike = _FormCollection["IsLike"];
            string FeedId = _FormCollection["FeedId"];
            string InstagramId = _FormCollection["InstagramId"];
            try
            {
                Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
                Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
                Api.Instagram.Instagram ApiobjInstagram = new Api.Instagram.Instagram();
                string ret = ApiobjInstagram.InstagramLikeUnLike(LikeCount, IsLike, FeedId, InstagramId, objGroups.UserId.ToString());
                return Content(ret);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return Content("success");
        
        }



    }
}
