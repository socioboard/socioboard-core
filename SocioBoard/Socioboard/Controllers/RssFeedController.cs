using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Socioboard.App_Start;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
namespace Socioboard.Controllers
{
    [Authorize]
    [CustomAuthorize]
    public class RssFeedController : BaseController
    {
        //
        // GET: /RssFeed/

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
        }
        public ActionResult RssQueue()
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
        public ActionResult RssFeedInfo()
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
        public ActionResult AddRss(string RssUrl, string profiles)
        {
            User objUser = (User)Session["User"];
            Api.RssFeeds.RssFeeds _objRssFeeds = new Api.RssFeeds.RssFeeds();
            string data = _objRssFeeds.AddRssFeed(RssUrl, objUser.Id.ToString(), profiles);
            return Content (data);
        }

        public ActionResult LoadAllRss()
        {
            User objUser = (User)Session["User"];
            Api.RssFeeds.RssFeeds objRsFeed = new Api.RssFeeds.RssFeeds();
           // List<RssReader> lstRssreader = objRsFeed.GetAllRssPostFeed(objUser.Id.ToString());
            List<RssReader> lstRssFeed = (List<RssReader>)(new JavaScriptSerializer().Deserialize(objRsFeed.GetAllRssPostFeed(objUser.Id.ToString()), typeof(List<RssReader>)));
            return PartialView("_RssQueuePartial", lstRssFeed);
        }
        public ActionResult GetAllFeedInfo()
        {
            User objUser=(User)Session["User"];
            Api.RssFeeds.RssFeeds objRssfeed = new Api.RssFeeds.RssFeeds();
            List<RssFeeds> lstrssfeed = (List<RssFeeds>)(new JavaScriptSerializer().Deserialize(objRssfeed.GetAllFeedInfo(objUser.Id.ToString()), typeof(List<RssFeeds>)));
            return PartialView("_RssFeedInfoPartial", lstrssfeed);
        }
        public ActionResult DeleteRSSQueueMessage()
        {
            User objUser = (User)Session["User"];
            string profileId = Request.QueryString["ProfileId"];
            string FeedUrl = Request.QueryString["feedUrl"];
            Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
            string data = objRssFeed.DeletePofile(FeedUrl, objUser.Id.ToString(), profileId);
            return Content(data);
        }

        public ActionResult EditRssFeedUrl()
        {
            User objUser=(User)Session["User"];
            string NewFeedUrl = Request.QueryString["NewFeedUrl"];
            string OldFeedUrl=Request.QueryString["OldFeedUrl"];
            string ProfileId=Request.QueryString["ProfieleId"];
            Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
            string data = objRssFeed.EditFeedUrl(NewFeedUrl, objUser.Id.ToString(), OldFeedUrl, ProfileId);
            return Content(data);

        }

        }
}
