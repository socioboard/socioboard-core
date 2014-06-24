using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
using Facebook;
using SocioBoard.Model;

namespace SocialSuitePro.Reports
{
    public partial class AjaxReport : System.Web.UI.Page
    {
        TwitterStatsHelper objtwtStatsHelper = new TwitterStatsHelper();
        TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
        FacebookInsightStatsHelper objfbstatsHelper = new FacebookInsightStatsHelper();
        public string strTwtArray = "[" + 0 + "]";
        public string strTwtFollowing = "[" + 0 + "]";
        public string strTwtAge = string.Empty;
        public string strDmRecieve = string.Empty;
        public string strDMSent = string.Empty;
        public string strIncomingMsg = string.Empty;
        public string strSentMsg = string.Empty;
        public string strRetweet = string.Empty;
        public string strAgeDiff = string.Empty;
        public string strEngagement = string.Empty;
        public string strInfluence = string.Empty;
        //----------------------------------------------------
        public string strFbAgeArray = string.Empty;
        public string strPageImpression = string.Empty;
        public string strLocationArray = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            procesRequest();
        }
        protected void procesRequest()
        {
            if (Request.QueryString["op"] == "twitter")
            {
                string twtProfileId = Request.QueryString["id"].ToString();
                User user = (User)Session["LoggedUser"];
                strTwtArray = objtwtStatsHelper.getNewFollowers(user, twtProfileId,15);
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(user, twtProfileId,15);
                // strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user);
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(user, twtProfileId,15);
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(user, twtProfileId);
                strDMSent = objtwtStatsHelper.getDirectMessageSent(user, twtProfileId);
                strSentMsg = objtwtStatsHelper.getSentMsg(user, twtProfileId,15);
                strRetweet = objtwtStatsHelper.getRetweets(user, twtProfileId,15);
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 15);
                strEngagement = objtwtStatsHelper.getEngagements(user, twtProfileId, 15);
                strInfluence = objtwtStatsHelper.getInfluence(user, twtProfileId, 15);
                Response.Write(strTwtArray + "@" + strTwtFollowing + "@" + strIncomingMsg + "@" + strDmRecieve + "@" + strDMSent + "@" + strSentMsg + "@" + strRetweet + "@" + strAgeDiff + "@" + strEngagement + "@" + strInfluence);
            }
            if (Request.QueryString["op"] == "facebook")
            {
                string fbProfileId = Request.QueryString["id"].ToString();
                User user = (User)Session["LoggedUser"];
               // objfbstatsHelper.getAllGroupsOnHome.InnerHtml = fbUser;
                strFbAgeArray = objfbstatsHelper.getLikesByGenderAge(fbProfileId, user.Id, 10);
                strPageImpression = objfbstatsHelper.getPageImressions(fbProfileId, user.Id, 10);
                strLocationArray = objfbstatsHelper.getLocationInsight(fbProfileId, user.Id, 10);
                objfbstatsHelper.getPostDetails(fbProfileId, user.Id, 10);
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = Request.QueryString["access"].ToString();
                dynamic pagelikes = fb.Get(fbProfileId);
                string strval = pagelikes.likes.ToString() + " Total Likes " + pagelikes.talking_about_count + " People talking about this.";
                Response.Write(strFbAgeArray + "_" + strPageImpression + "_" + strLocationArray + "_" + strval);
            }
        }
    }
}