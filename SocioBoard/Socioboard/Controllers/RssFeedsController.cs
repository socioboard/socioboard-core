using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Socioboard.App_Start;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using Socioboard.Helper;
using System.Threading.Tasks;
namespace Socioboard.Controllers
{
    //[Authorize]
    [CustomAuthorize]
    public class RssFeedsController : BaseController
    {
        //
        // GET: /RssFeed/

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
            else {
                return RedirectToAction("Index", "Index");
            }
        }
        public ActionResult RssQueue()
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
        public ActionResult RssFeedInfo()
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
            Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
            string rssFeedUrls = string.Empty;
            try
            {
                List<Domain.Socioboard.MongoDomain.Rss> lstRss = (List<Domain.Socioboard.MongoDomain.Rss>)(new JavaScriptSerializer().Deserialize(objRssFeed.GetRssDataByUser(objUser.Id.ToString()), typeof(List<Domain.Socioboard.MongoDomain.Rss>)));
                foreach (var item in lstRss)
                {
                    rssFeedUrls += item.RssFeedUrl + ",";
                }
                rssFeedUrls = rssFeedUrls.TrimEnd(',');
            }
            catch (Exception)
            {
            }
            List<Domain.Socioboard.MongoDomain.RssFeed> lstRssFeed = (List<Domain.Socioboard.MongoDomain.RssFeed>)(new JavaScriptSerializer().Deserialize(objRssFeed.GetAllRssPostFeed(rssFeedUrls),typeof(List<Domain.Socioboard.MongoDomain.RssFeed>)));
            //List<RssReader> lstRssFeed = (List<RssReader>)(new JavaScriptSerializer().Deserialize(objRsFeed.GetAllRssPostFeed(Session["access_token"].ToString()), typeof(List<RssReader>)));
            return PartialView("_RssQueuePartial", lstRssFeed);
        }
        public ActionResult GetAllFeedInfo()
        {
            User objUser=(User)Session["User"];
            Api.RssFeeds.RssFeeds objRssfeed = new Api.RssFeeds.RssFeeds();
            //List<RssFeeds> lstrssfeed = (List<RssFeeds>)(new JavaScriptSerializer().Deserialize(objRssfeed.GetAllFeedInfo(Session["access_token"].ToString()), typeof(List<RssFeeds>)));
            List<Domain.Socioboard.MongoDomain.Rss> lstRss = (List<Domain.Socioboard.MongoDomain.Rss>)(new JavaScriptSerializer().Deserialize(objRssfeed.GetRssDataByUser(objUser.Id.ToString()), typeof(List<Domain.Socioboard.MongoDomain.Rss>)));
            return PartialView("_RssFeedInfoPartial", lstRss);
        }
        public ActionResult DeleteRSSQueueMessage()
        {
            //User objUser = (User)Session["User"];
            //string profileId = Request.QueryString["ProfileId"];
            //string FeedUrl = Request.QueryString["feedUrl"];
            string strId = Request.QueryString["strId"];
            Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
            string data = objRssFeed.DeletePofile(strId);
            return Content(data);
        }

        public ActionResult EditRssFeedUrl()
        {
            //User objUser=(User)Session["User"];
            string NewFeedUrl = Request.QueryString["NewFeedUrl"];
            string OldFeedUrl=Request.QueryString["OldFeedUrl"];
            //string ProfileId=Request.QueryString["ProfieleId"];
            string strId=Request.QueryString["strId"];
            Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
            string data = objRssFeed.EditFeedUrl(NewFeedUrl, OldFeedUrl, strId);
            return Content(data);

        }
        public async Task<ActionResult> BindProfiles()
        {
            User objUser = (User)Session["User"];
            //Dictionary<TeamMemberProfile, object> dict_TeamMember = new Dictionary<TeamMemberProfile, object>();
            Dictionary<GroupProfile, object> dict_TeamMember = new Dictionary<GroupProfile, object>();
            if (Session["group"] != null)
            {
                dict_TeamMember = await SBHelper.GetGroupProfiles();
            }
            return PartialView("_ComposeMessageSchedulerPartial", dict_TeamMember);
        }

        }
}
