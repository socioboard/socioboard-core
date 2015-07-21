using Domain.Socioboard.Domain;
using Newtonsoft.Json.Linq;
using Socioboard.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Socioboard.Controllers
{
    [CustomAuthorize]
    public class FeedsController : BaseController
    {
        public static int facebookwallcount = 0;
        public static int twtwallcount = 0;
        public static int linkedinwallcount = 0;
        public static int facebookfeedcount = 0;
        public static int twtfeedcount = 0;
        public static int linkedinfeedcount = 0;
        public static int tumblerimagecount = 0;
        public static int instagramfeedcount = 0;
        //
        // GET: /Feeds/

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
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
            // return View();
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult loadaccount()
        {

            return PartialView("_FeedLeftPartial");
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult loadfeeds()
        {

            return PartialView("_FeedRightPartial");
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult loadmenu()
        {
            return PartialView("_FeedMenu", Helper.SBUtils.GetFeedsMenuAccordingToGroup());
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult LoadFeedPartialPage(string network, string id)
        {
            ViewBag.id = id;
            return PartialView("_FeedPartial", network);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult wallposts(string op, string load, string profileid)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
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

        // Commented By Antima

        //public ActionResult AjaxFeeds(string profileid)
        //{
        //    List<object> lstobject = new List<object>();
        //    Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
        //    List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId(objGroups.UserId.ToString(), profileid), typeof(List<FacebookFeed>)));
        //    foreach (var twittermsg in lstFacebookFeed)
        //    {
        //        lstobject.Add(twittermsg);
        //    }
        //    dictwallposts.Add("facebook", lstobject);
        //    return PartialView("_Panel2Partial", dictwallposts);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AjaxFeeds(string profileid, string load)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            if (load == "first")
            {
                Session["FacebookProfileIdForFeeds"] = profileid;
                facebookfeedcount = 0;
            }
            else
            {
                profileid = (string)Session["FacebookProfileIdForFeeds"];
                facebookfeedcount = facebookfeedcount + 10;
            }
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            List<FacebookFeed> lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId1(objGroups.UserId.ToString(), profileid, facebookfeedcount), typeof(List<FacebookFeed>)));
            foreach (var twittermsg in lstFacebookFeed)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }

        //Edited by Sumit Gupta
        public ActionResult scheduler(string network, string profileid)
        {
            List<ScheduledMessage> objScheduledMessage = new List<ScheduledMessage>();
            //Dictionary<object, List<ScheduledMessage>> dictscheduler = new Dictionary<object, List<ScheduledMessage>>();
            Dictionary<string, List<object>> dictscheduler = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            if (network == "facebook")
            {
                Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
                FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(FacebookAccount)));
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
                //dictscheduler.Add(objFacebookAccount, objScheduledMessage);
            }
            else if (network == "twitter")
            {
                Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(TwitterAccount)));
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
                //dictscheduler.Add(objTwitterAccount, objScheduledMessage);
            }
            else if (network == "linkedin")
            {
                Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
                LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(LinkedInAccount)));
                Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
                objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
                //dictscheduler.Add(objLinkedInAccount, objScheduledMessage);
            }

            List<object> lstObject = new List<object>();
            foreach (var item in objScheduledMessage)
            {
                lstObject.Add(item);
            }

            dictscheduler.Add(network, lstObject);

            return PartialView("_Panel3Partial", dictscheduler);
        }

        //Commented by Sumit Gupta
        //public ActionResult scheduler(string network, string profileid)
        //{
        //    Dictionary<object, List<ScheduledMessage>> dictscheduler = new Dictionary<object, List<ScheduledMessage>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    if (network == "facebook")
        //    {
        //        Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
        //        FacebookAccount objFacebookAccount = (FacebookAccount)(new JavaScriptSerializer().Deserialize(ApiobjFacebookAccount.getFacebookAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(FacebookAccount)));
        //        Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
        //        List<ScheduledMessage> objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
        //        dictscheduler.Add(objFacebookAccount, objScheduledMessage);
        //    }
        //    else if (network == "twitter")
        //    {
        //        Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
        //        TwitterAccount objTwitterAccount = (TwitterAccount)(new JavaScriptSerializer().Deserialize(ApiobjTwitterAccount.GetTwitterAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(TwitterAccount)));
        //        Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
        //        List<ScheduledMessage> objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
        //        dictscheduler.Add(objTwitterAccount, objScheduledMessage);
        //    }
        //    else if (network == "linkedin")
        //    {
        //        Api.LinkedinAccount.LinkedinAccount ApiobjLinkedinAccount = new Api.LinkedinAccount.LinkedinAccount();
        //        LinkedInAccount objLinkedInAccount = (LinkedInAccount)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinAccount.GetLinkedinAccountDetailsById(objGroups.UserId.ToString(), profileid.ToString()), typeof(LinkedInAccount)));
        //        Api.ScheduledMessage.ScheduledMessage ApiobjScheduledMessage = new Api.ScheduledMessage.ScheduledMessage();
        //        List<ScheduledMessage> objScheduledMessage = (List<ScheduledMessage>)(new JavaScriptSerializer().Deserialize(ApiobjScheduledMessage.GetAllUnSentMessagesAccordingToGroup(objGroups.UserId.ToString(), profileid.ToString(), network), typeof(List<ScheduledMessage>)));
        //        dictscheduler.Add(objLinkedInAccount, objScheduledMessage);
        //    }

        //    return PartialView("_Panel3Partial", dictscheduler);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult FacebookComment(string fbcommentid, string profileid, string message)
        {

            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            string ret = ApiobjFacebook.FacebookComment(message, profileid, fbcommentid, objGroups.UserId.ToString());
            return Content(ret);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult FacebookLike(string fbid, string profileid, string msgid)
        {
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            string ret = ApiobjFacebook.FacebookLike(msgid, profileid, fbid, objGroups.UserId.ToString());
            return Content(ret);
        }

        // Commented By Antima

        //public ActionResult TwitterNetworkDetails(string profileid)
        //{
        //    List<object> lstobject = new List<object>();
        //    Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
        //    List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objGroups.UserId.ToString(), profileid), typeof(List<TwitterFeed>)));
        //    foreach (var twitterfeed in lstTwitterFeed)
        //    {
        //        lstobject.Add(twitterfeed);
        //    }
        //    dictwallposts.Add("twitter", lstobject);
        //    return PartialView("_Panel1Partial", dictwallposts);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterNetworkDetails(string profileid, string load)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();

            if (load == "first")
            {
                Session["TwitterProfileIdForFeeds"] = profileid;
                twtwallcount = 0;
            }
            else
            {
                profileid = (string)Session["TwitterProfileIdForFeeds"];
                twtwallcount = twtwallcount + 10;
            }
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
            List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId1((objGroups.UserId.ToString()), profileid, twtwallcount), typeof(List<TwitterFeed>)));

            //List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objGroups.UserId.ToString(), profileid), typeof(List<TwitterFeed>)));
            foreach (var twitterfeed in lstTwitterFeed)
            {
                lstobject.Add(twitterfeed);
            }
            dictwallposts.Add("twitter", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }

        // Commented By Antima

        //public ActionResult TwitterFeeds(string profileid)
        //{
        //    List<object> lstobject = new List<object>();
        //    Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
        //    List<TwitterMessage> lstTwitterMessage = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessages(profileid, objGroups.UserId.ToString()), typeof(List<TwitterMessage>)));
        //    foreach (var twittermsg in lstTwitterMessage)
        //    {
        //        lstobject.Add(twittermsg);
        //    }
        //    dictwallposts.Add("twitter", lstobject);
        //    return PartialView("_Panel2Partial", dictwallposts);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterFeeds(string profileid, string load)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            if (load == "first")
            {
                Session["TwitterProfileIdForFeeds"] = profileid;
                twtfeedcount = 0;
            }
            else
            {
                profileid = (string)Session["TwitterProfileIdForFeeds"];
                twtfeedcount = twtfeedcount + 10;
            }

            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterMessage = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessages1(profileid, objGroups.UserId.ToString(), twtfeedcount), typeof(List<TwitterMessage>)));
            foreach (var twittermsg in lstTwitterMessage)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("twitter", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }

        // Commented By Antima

        //public ActionResult linkedinwallposts(string profileid)
        //{
        //    Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
        //    List<Domain.Socioboard.Domain.LinkedInFeed> lstLinkedInFeed = (List<Domain.Socioboard.Domain.LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.LinkedInFeed>)));
        //    List<object> lstobject = new List<object>();
        //    foreach (var item in lstLinkedInFeed)
        //    {
        //        lstobject.Add(item);
        //    }
        //    dictwallposts.Add("linkedin", lstobject);
        //    return PartialView("_Panel1Partial", dictwallposts);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult linkedinwallposts(string profileid, string load)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            if (load == "first")
            {
                Session["LinkedInProfileIdForFeeds"] = profileid;
                linkedinwallcount = 0;
            }
            else
            {
                profileid = (string)Session["LinkedInProfileIdForFeeds"];
                linkedinwallcount = linkedinwallcount + 10;
            }
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
            List<Domain.Socioboard.Domain.LinkedInFeed> lstLinkedInFeed = (List<Domain.Socioboard.Domain.LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds1(objGroups.UserId.ToString(), profileid, linkedinwallcount), typeof(List<Domain.Socioboard.Domain.LinkedInFeed>)));

            //List<Domain.Socioboard.Domain.LinkedInFeed> lstLinkedInFeed = (List<Domain.Socioboard.Domain.LinkedInFeed>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedInFeed.GetLinkedInFeeds(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.LinkedInFeed>)));
            List<object> lstobject = new List<object>();
            foreach (var item in lstLinkedInFeed)
            {
                lstobject.Add(item);
            }
            dictwallposts.Add("linkedin", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }
        //Vikash[03-04-2015]
        //public ActionResult LinkedinFeeds(string profileid)
        //{
        //    List<object> lstobject = new List<object>();
        //    Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
        //    List<Domain.Socioboard.Domain.LinkedInUser.User_Updates> lstlinkedinFeeds = (List<Domain.Socioboard.Domain.LinkedInUser.User_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedUserUpdates(profileid, objGroups.UserId.ToString()), typeof(List<Domain.Socioboard.Domain.LinkedInUser.User_Updates>)));
        //    foreach (var linkledinfeed in lstlinkedinFeeds)
        //    {
        //        lstobject.Add(linkledinfeed);
        //    }
        //    dictwallposts.Add("linkedin", lstobject);
        //    return PartialView("_Panel2Partial", dictwallposts);
        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult LinkedinFeeds(string profileid, string load)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            if (load == "first")
            {
                Session["LinkedInProfileIdForFeeds"] = profileid;
                linkedinfeedcount = 0;
            }
            else
            {
                profileid = (string)Session["LinkedInProfileIdForFeeds"];
                linkedinfeedcount = linkedinfeedcount + 10;
            }
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            //Api.Linkedin.Linkedin ApiobjLinkedin = new Api.Linkedin.Linkedin();
            //List<Domain.Socioboard.Domain.LinkedInUser.User_Updates> lstlinkedinFeeds = (List<Domain.Socioboard.Domain.LinkedInUser.User_Updates>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedin.GetLinkedUserUpdates(profileid, objGroups.UserId.ToString()), typeof(List<Domain.Socioboard.Domain.LinkedInUser.User_Updates>)));

            Api.LinkedinMessage.LinkedinMessage ApiobjLinkedinMessage = new Api.LinkedinMessage.LinkedinMessage();
            List<Domain.Socioboard.Domain.LinkedInMessage> lstLinkedInMessage = (List<Domain.Socioboard.Domain.LinkedInMessage>)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinMessage.GetLinkedInMessages(objGroups.UserId.ToString(), profileid, linkedinfeedcount), typeof(List<Domain.Socioboard.Domain.LinkedInMessage>)));

            foreach (var linkledinfeed in lstLinkedInMessage)
            {
                lstobject.Add(linkledinfeed);
            }
            dictwallposts.Add("linkedin", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }


        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
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

            //List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.InstagramFeed>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramFeed.GetLinkedInFeeds(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.InstagramFeed>)));
            List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.InstagramFeed>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramFeed.GetFeedsOfProfileWithRange(objGroups.UserId.ToString(), profileid, "0","8"), typeof(List<Domain.Socioboard.Domain.InstagramFeed>)));

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
        //vikash[03-04-2015]
        //public ActionResult TumblrImages(string profileid)
        //{
        //    //List<object> lstobject = new List<object>();
        //    //Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
        //    //Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    //Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    //Api.TumblrFeed.TumblrFeed ApiobjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
        //    //List<Domain.Socioboard.Domain.TumblrFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsers(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));
        //    //foreach (var lstInstagramfeed in lstInstagramFeed)
        //    //{
        //    //    lstobject.Add(lstInstagramfeed);
        //    //}
        //    //dictwallposts.Add("tumblr", lstobject);
        //    //return PartialView("_ImagePartial", dictwallposts);

        //    object lstobject = new object();
        //    List<object> lstComment = null;
        //    Dictionary<string, Dictionary<object, List<object>>> dictwallposts = new Dictionary<string, Dictionary<object, List<object>>>();
        //    Dictionary<object, List<object>> dic_TumblrImg = new Dictionary<object, List<object>>();
        //    Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
        //    Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
        //    Api.TumblrFeed.TumblrFeed ApiobjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
        //    //List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsers(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));
        //    //GetFeedsOfProfileWithRange
        //    List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsersWithRange(objGroups.UserId.ToString(), profileid, "0"), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));

        //    foreach (var item_lstTumblrFeed in lstTumblrFeed)
        //    {
        //        lstComment = new List<object>();

        //        lstobject = (object)item_lstTumblrFeed;
        //        dic_TumblrImg.Add(lstobject, lstComment);
        //    }
        //    dictwallposts.Add("tumblr", dic_TumblrImg);
        //    return PartialView("_ImagePartial", dictwallposts);


        //}

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TumblrImages(string profileid, string load)
        {
            if (load == "first")
            {
                Session["TumblerProfileIdForImages"] = profileid;
                tumblerimagecount = 0;
            }
            else
            {
                profileid = (string)Session["TumblerProfileIdForImages"];
                tumblerimagecount = tumblerimagecount + 6;
            }

            object lstobject = new object();
            List<object> lstComment = null;
            Dictionary<string, Dictionary<object, List<object>>> dictwallposts = new Dictionary<string, Dictionary<object, List<object>>>();
            Dictionary<object, List<object>> dic_TumblrImg = new Dictionary<object, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TumblrFeed.TumblrFeed ApiobjTumblrFeed = new Api.TumblrFeed.TumblrFeed();
            //List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsers(objGroups.UserId.ToString(), profileid), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));
            //GetFeedsOfProfileWithRange
            List<Domain.Socioboard.Domain.TumblrFeed> lstTumblrFeed = (List<Domain.Socioboard.Domain.TumblrFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTumblrFeed.GetAllTumblrFeedOfUsersWithRange(objGroups.UserId.ToString(), profileid, tumblerimagecount.ToString()), typeof(List<Domain.Socioboard.Domain.TumblrFeed>)));
            foreach (var item_lstTumblrFeed in lstTumblrFeed)
            {
                lstComment = new List<object>();

                lstobject = (object)item_lstTumblrFeed;
                dic_TumblrImg.Add(lstobject, lstComment);
            }
            dictwallposts.Add("tumblr", dic_TumblrImg);
            return PartialView("_ImagePartial", dictwallposts);


        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
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
                    lstobject = (object)item["snippet"]["thumbnails"]["maxres"]["url"].ToString();
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

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
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

        //------vikash-----------//

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult retweetmessage(string MessageId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                string retweet = ApiobjTwitter.TwitterReteet_post(objUser.Id.ToString(), ProfileId, MessageId);
                return Content(retweet);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult favoritemessage(string MessageId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                string favorite = ApiobjTwitter.TwitterFavorite_post(objUser.Id.ToString(), ProfileId, MessageId);
                return Content(favorite);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult spamuser(string SpammerScreanName, string UserProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.Twitter.Twitter ApiobjTwitter = new Api.Twitter.Twitter();
                string favorite = ApiobjTwitter.SpamUser_post(objUser.Id.ToString(), SpammerScreanName, UserProfileId);
                return Content(favorite);
            }
            catch (Exception ex)
            {
                return Content("");
            }
        }

        // Edited by Antima

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ShowPopUp(string profileId, string Id)
        {

            Api.LinkedInFeed.LinkedInFeed ApiobjLinkedInFeed = new Api.LinkedInFeed.LinkedInFeed();
            //   Guid LdFeedId = Guid.Parse(Id);
            List<object> lstfeed = new List<object>();
            Dictionary<string, object> linkinfo = new Dictionary<string, object>();
            List<Domain.Socioboard.Domain.LinkedInFeed> lstlinkedInfeed = new List<LinkedInFeed>();
            lstlinkedInfeed = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Domain.Socioboard.Domain.LinkedInFeed>>(ApiobjLinkedInFeed.GetAllLinkedInFeedsOfProfileWithId(profileId, Id));
            linkinfo.Add("linkedin", lstlinkedInfeed[0]);
            return PartialView("_MailSendingPartial", linkinfo);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ShowTwtMailPopUp(string Id)
        {

            Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
            Domain.Socioboard.Domain.TwitterFeed twtfeed = (Domain.Socioboard.Domain.TwitterFeed)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetTwitterFeedById(Id), typeof(Domain.Socioboard.Domain.TwitterFeed)));
            Dictionary<string, object> twtinfo = new Dictionary<string, object>();
            twtinfo.Add("twt", twtfeed);
            return PartialView("_MailSendingPartial", twtinfo);
            //return PartialView("_TwitterMailSendingPartial", twtfeed);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult SendFeedMail(string emailId, string feed, string fromname)
        {
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();

            string mailsender = "";

            var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_MailBodyPartial", feed);

            mailsender = ApiobjMailSender.SendFeedMail(emailId, feed, fromname, mailBody);
            return Content(mailsender);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult linkedinPageWallPost(string profileid)
        {
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLinkedinCompanyPageInfo = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
            LinkedinCompanyPage onjLiCompanyPage = (LinkedinCompanyPage)(new JavaScriptSerializer().Deserialize(ApiobjLinkedinCompanyPageInfo.GetLinkedinCompanyPageDetailsByUserIdAndPageId(objUser.Id.ToString(), profileid), typeof(LinkedinCompanyPage)));
            Api.LinkedinCompanyPage.LinkedinCompanyPage ApiLinkedinPagePost = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
            List<LinkedinCompanyPagePosts> lstlipagepost = (List<LinkedinCompanyPagePosts>)(new JavaScriptSerializer().Deserialize(ApiLinkedinPagePost.GetAllLinkedinCompanyPagePostsByUserIdAndProfileId(objUser.Id.ToString(), profileid), typeof(List<LinkedinCompanyPagePosts>)));
            ViewBag.LinkedinPageInformation = onjLiCompanyPage;
            ViewBag.LinkedinPagePost = lstlipagepost;

            return PartialView("_LinkedinCompanyPagePartial");
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult linkedinpagecomentonpost(string pageid, string updatekey, string comment)
        {
            string returnContent = string.Empty;
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLiPostComentOnPage = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
            string Return = (string)(new JavaScriptSerializer().Deserialize(ApiobjLiPostComentOnPage.PsotCommentOnLinkedinCompanyPageUpdate(objUser.Id.ToString(), pageid, updatekey, comment), typeof(string)));
            if (Return != "Something Went Wrong")
            {
                returnContent = "";
            }
            return Content(returnContent);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult LikeCompanyPagePost(string pageid, string updatekey, string isLike)
        {
            string returnContent = string.Empty;
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjLiPutLikeOnPageUpdate = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
            string Return = (string)(new JavaScriptSerializer().Deserialize(ApiobjLiPutLikeOnPageUpdate.PutLikeOnLinkedinCompanyPageUpdate(objUser.Id.ToString(), pageid, updatekey, isLike), typeof(string)));
            if (Return != "Something Went Wrong")
            {
                returnContent = "";
            }
            return Content(returnContent);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult CreatePostOnPage(string Pageid, string Post)
        {
            string returnContent = string.Empty;
            User objUser = (User)System.Web.HttpContext.Current.Session["User"];
            Api.LinkedinCompanyPage.LinkedinCompanyPage ApiobjCreateUpdate = new Api.LinkedinCompanyPage.LinkedinCompanyPage();
            string Return = (string)(new JavaScriptSerializer().Deserialize(ApiobjCreateUpdate.CreateUpdateOnLinkedinCompanyPage(objUser.Id.ToString(), Pageid, Post), typeof(string)));
            if (Return != "Something Went Wrong")
            {
                returnContent = "";
            }
            return Content(returnContent);
        }

        // Edited by Antima

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterUserTweet(string ProfileId)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictUserTweet = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterUsertweet = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.getAllTwitterUsertweetOfUsers(objGroups.UserId.ToString(), ProfileId), typeof(List<TwitterMessage>)));
            foreach (var twitterUsertweet in lstTwitterUsertweet)
            {
                lstobject.Add(twitterUsertweet);
            }
            dictUserTweet.Add("twitter", lstobject);
            return PartialView("_Panel3Partial", dictUserTweet);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterRetweets(string ProfileId)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictRetweets = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterRetweets = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.getAllTwitterRetweetOfUsers(objGroups.UserId.ToString(), ProfileId), typeof(List<TwitterMessage>)));
            foreach (var twitterRetweets in lstTwitterRetweets)
            {
                lstobject.Add(twitterRetweets);
            }
            dictRetweets.Add("twitter", lstobject);
            return PartialView("_Panel3Partial", dictRetweets);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterMentions(string ProfileId)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictMentions = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterMentions = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.getAllTwitterMentionsOfUsers(objGroups.UserId.ToString(), ProfileId), typeof(List<TwitterMessage>)));
            foreach (var twitterMentions in lstTwitterMentions)
            {
                lstobject.Add(twitterMentions);
            }
            dictMentions.Add("twitter", lstobject);
            return PartialView("_Panel3Partial", dictMentions);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult FacebookUserFeeds(string ProfileId)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictUserFeeds = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<FacebookMessage> lstFacebookUserFeeds = (List<FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.getAllFacebookUserFeedOfUsers(objGroups.UserId.ToString(), ProfileId), typeof(List<FacebookMessage>)));
            foreach (var FacebookUserFeeds in lstFacebookUserFeeds)
            {
                lstobject.Add(FacebookUserFeeds);
            }
            dictUserFeeds.Add("facebook", lstobject);
            return PartialView("_Panel3Partial", dictUserFeeds);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult FacebookStatus(string ProfileId)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictStatus = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<FacebookMessage> lstFacebookStatus = (List<FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.getAllFacebookstatusOfUsers(objGroups.UserId.ToString(), ProfileId), typeof(List<FacebookMessage>)));
            foreach (var FacebookStatus in lstFacebookStatus)
            {
                lstobject.Add(FacebookStatus);
            }
            dictStatus.Add("facebook", lstobject);
            return PartialView("_Panel3Partial", dictStatus);
        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult FacebookTag(string ProfileId)
        {
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictTag = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<FacebookMessage> lstFacebookTag = (List<FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.getAllFacebookTagOfUsers(objGroups.UserId.ToString(), ProfileId), typeof(List<FacebookMessage>)));
            foreach (var FacebookTag in lstFacebookTag)
            {
                lstobject.Add(FacebookTag);
            }
            dictTag.Add("facebook", lstobject);
            return PartialView("_Panel3Partial", dictTag);
        }


        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult SearchFacebookFeeds(string keyword)
        {
            List<Domain.Socioboard.Domain.FacebookFeed> lstFacebookFeedsSearch = new List<Domain.Socioboard.Domain.FacebookFeed>();
            try
            {
                Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
                Api.FacebookFeed.FacebookFeed ApiobjDiscoverySearch = new Api.FacebookFeed.FacebookFeed();

                int noOfDataToSkip = 0;

                lstFacebookFeedsSearch = (List<Domain.Socioboard.Domain.FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.getAllFacebookFeedsByUserIdWithRange(objUser.Id.ToString(), keyword, noOfDataToSkip.ToString()), typeof(List<Domain.Socioboard.Domain.FacebookFeed>)));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return PartialView("_FacebookContactPartial", lstFacebookFeedsSearch);
        }


        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult SearchTwitterFeeds(string keyword)
        {
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            Api.DiscoverySearch.DiscoverySearch ApiobjDiscoverySearch = new Api.DiscoverySearch.DiscoverySearch();
            List<Domain.Socioboard.Domain.DiscoverySearch> lstDiscoverySearch = new List<Domain.Socioboard.Domain.DiscoverySearch>();
            lstDiscoverySearch = (List<Domain.Socioboard.Domain.DiscoverySearch>)(new JavaScriptSerializer().Deserialize(ApiobjDiscoverySearch.contactSearchTwitter(keyword), typeof(List<Domain.Socioboard.Domain.DiscoverySearch>)));
            return PartialView("_TwitterContactPartial", lstDiscoverySearch);
        }


        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult wallposts_FeedsSearch(string op, string load, string profileid, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
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
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = new List<FacebookMessage>();
            lstFacebookMessage = (List<Domain.Socioboard.Domain.FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.GetAllWallpostsOfProfileAccordingtoGroupByUserIdAndProfileId1WithRange(objGroups.UserId.ToString(), keyword, profileid, facebookwallcount.ToString()), typeof(List<Domain.Socioboard.Domain.FacebookMessage>)));
            if (lstFacebookMessage == null)
            {
                lstFacebookMessage = new List<FacebookMessage>();
            }
            List<object> lstobject = new List<object>();
            foreach (var item in lstFacebookMessage)
            {
                lstobject.Add(item);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AjaxFeeds_FeedsSearch(string profileid, string load, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            if (load == "first")
            {
                Session["FacebookProfileIdForFeeds"] = profileid;
                facebookfeedcount = 0;
            }
            else
            {
                profileid = (string)Session["FacebookProfileIdForFeeds"];
                facebookfeedcount = facebookfeedcount + 10;
            }
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            List<FacebookFeed> lstFacebookFeed = new List<FacebookFeed>();
            lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId1WithRange(objGroups.UserId.ToString(), keyword, profileid, facebookfeedcount.ToString()), typeof(List<FacebookFeed>)));
            if (lstFacebookFeed == null)
            {
                lstFacebookFeed = new List<FacebookFeed>();
            }
            foreach (var twittermsg in lstFacebookFeed)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult FacebookUserFeeds_FeedsSearch(string ProfileId, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictUserFeeds = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            //Data to Skip, hardcoded for now
            int noOfDataToSkip = 0;

            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<FacebookMessage> lstFacebookUserFeeds = new List<FacebookMessage>();
            lstFacebookUserFeeds = (List<FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.getAllFacebookUserFeedOfUsersByUserIdAndProfileId1WithRange(objGroups.UserId.ToString(), keyword, ProfileId, noOfDataToSkip.ToString()), typeof(List<FacebookMessage>)));
            if (lstFacebookUserFeeds == null)
            {
                lstFacebookUserFeeds = new List<FacebookMessage>();
            }
            foreach (var FacebookUserFeeds in lstFacebookUserFeeds)
            {
                lstobject.Add(FacebookUserFeeds);
            }
            dictUserFeeds.Add("facebook", lstobject);
            return PartialView("_Panel3Partial", dictUserFeeds);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult wallposts_FeedsSearch_Twitter(string op, string load, string profileid, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
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
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = new List<FacebookMessage>();
            lstFacebookMessage = (List<Domain.Socioboard.Domain.FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.GetAllWallpostsOfProfileAccordingtoGroupByUserIdAndProfileId1WithRange(objGroups.UserId.ToString(), keyword, profileid, facebookwallcount.ToString()), typeof(List<Domain.Socioboard.Domain.FacebookMessage>)));
            if (lstFacebookMessage == null)
            {
                lstFacebookMessage = new List<FacebookMessage>();
            }
            List<object> lstobject = new List<object>();
            foreach (var item in lstFacebookMessage)
            {
                lstobject.Add(item);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AjaxFeeds_FeedsSearch_Twitter(string profileid, string load, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            if (load == "first")
            {
                Session["FacebookProfileIdForFeeds"] = profileid;
                facebookfeedcount = 0;
            }
            else
            {
                profileid = (string)Session["FacebookProfileIdForFeeds"];
                facebookfeedcount = facebookfeedcount + 10;
            }
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            List<FacebookFeed> lstFacebookFeed = new List<FacebookFeed>();
            lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getAllFacebookFeedsByUserIdAndProfileId1WithRange(objGroups.UserId.ToString(), keyword, profileid, facebookfeedcount.ToString()), typeof(List<FacebookFeed>)));
            if (lstFacebookFeed == null)
            {
                lstFacebookFeed = new List<FacebookFeed>();
            }
            foreach (var twittermsg in lstFacebookFeed)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("facebook", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult UserFeeds_FeedsSearch_Twitter(string ProfileId, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictUserFeeds = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            //Data to Skip, hardcoded for now
            int noOfDataToSkip = 0;

            Api.FacebookMessage.FacebookMessage ApiobjFacebookMessage = new Api.FacebookMessage.FacebookMessage();
            List<FacebookMessage> lstFacebookUserFeeds = new List<FacebookMessage>();
            lstFacebookUserFeeds = (List<FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebookMessage.getAllFacebookUserFeedOfUsersByUserIdAndProfileId1WithRange(objGroups.UserId.ToString(), keyword, ProfileId, noOfDataToSkip.ToString()), typeof(List<FacebookMessage>)));
            if (lstFacebookUserFeeds == null)
            {
                lstFacebookUserFeeds = new List<FacebookMessage>();
            }
            foreach (var FacebookUserFeeds in lstFacebookUserFeeds)
            {
                lstobject.Add(FacebookUserFeeds);
            }
            dictUserFeeds.Add("facebook", lstobject);
            return PartialView("_Panel3Partial", dictUserFeeds);
        }



        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterUserTweet_FeedsSearch(string ProfileId, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictUserTweet = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterUsertweet = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.getAllTwitterUsertweetOfUsersByKeyword(objGroups.UserId.ToString(), ProfileId, keyword), typeof(List<TwitterMessage>)));
            foreach (var twitterUsertweet in lstTwitterUsertweet)
            {
                lstobject.Add(twitterUsertweet);
            }
            dictUserTweet.Add("twitter", lstobject);
            return PartialView("_Panel3Partial", dictUserTweet);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterRetweets_FeedsSearch(string ProfileId, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictRetweets = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterRetweets = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.getAllTwitterRetweetOfUsersByKeyword(objGroups.UserId.ToString(), ProfileId, keyword), typeof(List<TwitterMessage>)));
            foreach (var twitterRetweets in lstTwitterRetweets)
            {
                lstobject.Add(twitterRetweets);
            }
            dictRetweets.Add("twitter", lstobject);
            return PartialView("_Panel3Partial", dictRetweets);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterMentions_FeedsSearch(string ProfileId, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictMentions = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));

            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterMentions = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.getAllTwitterMentionsOfUsersByKeyword(objGroups.UserId.ToString(), ProfileId, keyword), typeof(List<TwitterMessage>)));
            foreach (var twitterMentions in lstTwitterMentions)
            {
                lstobject.Add(twitterMentions);
            }
            dictMentions.Add("twitter", lstobject);
            return PartialView("_Panel3Partial", dictMentions);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterFeeds_FeedsSearch(string profileid, string load, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            if (load == "first")
            {
                Session["TwitterProfileIdForFeeds"] = profileid;
                twtfeedcount = 0;
            }
            else
            {
                profileid = (string)Session["TwitterProfileIdForFeeds"];
                twtfeedcount = twtfeedcount + 10;
            }

            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterMessage = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessages1ByKeyword(profileid, objGroups.UserId.ToString(), keyword, twtfeedcount), typeof(List<TwitterMessage>)));
            foreach (var twittermsg in lstTwitterMessage)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("twitter", lstobject);
            return PartialView("_Panel2Partial", dictwallposts);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult TwitterNetworkDetails_FeedsSearch(string profileid, string load, string keyword)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();

            if (load == "first")
            {
                Session["TwitterProfileIdForFeeds"] = profileid;
                twtwallcount = 0;
            }
            else
            {
                profileid = (string)Session["TwitterProfileIdForFeeds"];
                twtwallcount = twtwallcount + 10;
            }
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
            List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId1ByKeyword((objGroups.UserId.ToString()), profileid, keyword, twtwallcount), typeof(List<TwitterFeed>)));

            //List<TwitterFeed> lstTwitterFeed = (List<TwitterFeed>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.GetAllTwitterFeedsByUserIdAndProfileId(objGroups.UserId.ToString(), profileid), typeof(List<TwitterFeed>)));
            foreach (var twitterfeed in lstTwitterFeed)
            {
                lstobject.Add(twitterfeed);
            }
            dictwallposts.Add("twitter", lstobject);
            return PartialView("_Panel1Partial", dictwallposts);
        }


        //Added by Sumit Gupta[15-02-2015]
        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AddLoadNewFacebookNewsFeeds(string profileid)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();
            Dictionary<string, List<object>> dictFeeds = new Dictionary<string, List<object>>();

            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();

            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();

            List<FacebookFeed> lstFacebookFeed = new List<FacebookFeed>();

            try
            {
                lstFacebookFeed = (List<FacebookFeed>)(new JavaScriptSerializer().Deserialize(ApiobjFacebook.AddNewFacebookFeeds(profileid, objGroups.UserId.ToString()), typeof(List<FacebookFeed>)));
            }
            catch (Exception ex)
            {
                lstFacebookFeed = null;
            }
            if (lstFacebookFeed == null)
            {
                lstFacebookFeed = new List<FacebookFeed>();
            }
            foreach (var FacebookFeed in lstFacebookFeed)
            {
                lstobject.Add(FacebookFeed);
            }
            dictwallposts.Add("facebook", lstobject);

            return PartialView("_Panel2Partial", dictwallposts);
        }

        //Added by Sumit Gupta[15-02-2015]
        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AddLoadNewTwitterFeeds(string profileid)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            List<object> lstobject = new List<object>();

            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.TwitterMessage.TwitterMessage ApiobjTwitterMessage = new Api.TwitterMessage.TwitterMessage();
            List<TwitterMessage> lstTwitterMessage = (List<TwitterMessage>)(new JavaScriptSerializer().Deserialize(ApiobjTwitterMessage.GetTwitterMessages1(profileid, objGroups.UserId.ToString(), twtfeedcount), typeof(List<TwitterMessage>)));
            foreach (var twittermsg in lstTwitterMessage)
            {
                lstobject.Add(twittermsg);
            }
            dictwallposts.Add("twitter", lstobject);

            return PartialView("_Panel2Partial", dictwallposts);
        }

        //[OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult AddLoadNewFacebookWallPosts(string profileid, string type)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            bool isUserFeedsCalled = false;
            if (type != null)
            {
                if (type.Equals("userfeeds") && !string.IsNullOrEmpty(type))
                {
                    isUserFeedsCalled = true;
                }
            }

            Dictionary<string, List<object>> dictwallposts = new Dictionary<string, List<object>>();

            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage;
            try
            {
                lstFacebookMessage = (List<Domain.Socioboard.Domain.FacebookMessage>)(new JavaScriptSerializer().Deserialize(ApiobjFacebook.AddNewFacebookWallPosts(profileid, objGroups.UserId.ToString()), typeof(List<Domain.Socioboard.Domain.FacebookMessage>)));
            }
            catch (Exception ex)
            {
                lstFacebookMessage = new List<Domain.Socioboard.Domain.FacebookMessage>();
            }
            List<object> lstobject = new List<object>();
            foreach (var item in lstFacebookMessage)
            {
                //if (isUserFeedsCalled)
                //{
                //    if (item.FromId != item.ProfileId)
                //    {
                //        lstobject.Add(item);
                //    }
                //}
                //else
                //{
                lstobject.Add(item);
                //}
            }

            dictwallposts.Add("facebook", lstobject);

            //if (isUserFeedsCalled)
            //{
            //     return PartialView("_Panel3Partial", dictwallposts);
            //}
            //else
            //{
            return PartialView("_Panel1Partial", dictwallposts);
            //}

        }

        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult GooglePlusFeeds(string Id)
        {
            Api.GooglePlusActivities.GooglePlusActivities ApiGooglePlusActivities = new Api.GooglePlusActivities.GooglePlusActivities();
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
            List<Domain.Socioboard.Domain.GooglePlusActivities> lstGooglePlusActivities = (List<Domain.Socioboard.Domain.GooglePlusActivities>)new JavaScriptSerializer().Deserialize(ApiGooglePlusActivities.getgoogleplusActivity(_User.Id.ToString(), Id), typeof(List<Domain.Socioboard.Domain.GooglePlusActivities>));
            if (lstGooglePlusActivities.Count > 0)
            {
                return PartialView("_GplusActivityPartial", lstGooglePlusActivities);
            }
            else
            {
                return Content("no_data");
            }
        }
        [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Instagram(string id)
        {
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
            ViewBag.Id = id;
            if (_User != null)
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
        }
         [OutputCache(Duration = 45, Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult ShowInstagramFeeds(string load, string id)
        {

            if (load == "first")
            {
                Session["InsragramIdForFeeds"] = id;
                instagramfeedcount = 0;
            }
            else
            {
                id = (string)Session["InsragramIdForFeeds"];
                instagramfeedcount = instagramfeedcount + 4;
            }

            object lstobject = new object();
            List<object> lstComment = null;
            Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
            Dictionary<string, Dictionary<object, List<object>>> dictwallposts = new Dictionary<string, Dictionary<object, List<object>>>();
            Dictionary<object, List<object>> dic_InstgramImg = new Dictionary<object, List<object>>();
            Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();
            Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
            Api.InstagramFeed.InstagramFeed ApiobjInstagramFeed = new Api.InstagramFeed.InstagramFeed();
            Api.InstagramComment.InstagramComment ApiobjInstagramFeedComment = new Api.InstagramComment.InstagramComment();


            List<Domain.Socioboard.Domain.InstagramFeed> lstInstagramFeed = (List<Domain.Socioboard.Domain.InstagramFeed>)(new JavaScriptSerializer().Deserialize(ApiobjInstagramFeed.GetFeedsOfProfileWithRange(objGroups.UserId.ToString(), id, instagramfeedcount.ToString(), "4"), typeof(List<Domain.Socioboard.Domain.InstagramFeed>)));
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
            return PartialView("_InstagramPartial", dictwallposts);
        }

         public ActionResult AddInstagramComment(string FeedId, string Text, string InstagramId)
         {
             Api.Groups.Groups ApiobjGroups = new Api.Groups.Groups();

             Domain.Socioboard.Domain.Groups objGroups = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(ApiobjGroups.GetGroupDetailsByGroupId(Session["group"].ToString()), typeof(Domain.Socioboard.Domain.Groups)));
             Api.Instagram.Instagram ApiObjInstagram = new Api.Instagram.Instagram();
             string ret = ApiObjInstagram.AddComment(objGroups.UserId.ToString(),FeedId, Text, InstagramId);
            return Content("");
         }


    }
}
