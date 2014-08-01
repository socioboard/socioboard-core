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
        public string strTwtMention = string.Empty;
        public string strstoriesArray = string.Empty;
        public string strDatetwt = string.Empty;
        //----------------------------------------------------
        public string strFbAgeArray = string.Empty;
        public string strPageImpression = string.Empty;
        public string strLocationArray = string.Empty;
        public string likeunlikedate = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            procesRequest();
        }
        protected void procesRequest()
        {
            if (Request.QueryString["op"] == "twitter")
            {
                string twtProfileId = Request.QueryString["id"].ToString();
                Session["twtProfileId"] = twtProfileId;
                User user = (User)Session["LoggedUser"];
                strTwtArray = objtwtStatsHelper.getNewFollowers(twtProfileId, 15);
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(twtProfileId, 15);
                // strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user);
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(twtProfileId, 15);
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(twtProfileId, 15);
                strDMSent = objtwtStatsHelper.getDirectMessageSent(twtProfileId, 15);
                strSentMsg = objtwtStatsHelper.getSentMsg(twtProfileId, 15);
                strRetweet = objtwtStatsHelper.getRetweets(twtProfileId, 15);
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 15);
                strEngagement = objtwtStatsHelper.getEngagements( twtProfileId, 15);
                strInfluence = objtwtStatsHelper.getInfluence( twtProfileId, 15);
                strDatetwt = objtwtStatsHelper.getdate(twtProfileId, 15);
                strTwtMention = objtwtStatsHelper.getTwtMention(twtProfileId, 15);


                Response.Write(strTwtArray + "@" + strTwtFollowing + "@" + strIncomingMsg + "@" + strDmRecieve + "@" + strDMSent + "@" + strSentMsg + "@" + strRetweet + "@" + strAgeDiff + "@" + strEngagement + "@" + strInfluence + "@" + strTwtMention + "@" + strDatetwt);
            }
            if (Request.QueryString["op"] == "facebook")
            {
                string fbProfileId = Request.QueryString["id"].ToString();
                Session["fbprofileId"] = fbProfileId;
                User user = (User)Session["LoggedUser"];
                string nmbrdays = Request.QueryString["NumberOfDays"];
                int NumberOfDays = Convert.ToInt16(nmbrdays);
                // objfbstatsHelper.getAllGroupsOnHome.InnerHtml = fbUser;
                strFbAgeArray = objfbstatsHelper.getLikesByGenderAge(fbProfileId, 15);
                strPageImpression = objfbstatsHelper.getPageImressions(fbProfileId,  15);
                strLocationArray = objfbstatsHelper.getLocationInsight(fbProfileId, 15);
                strstoriesArray = objfbstatsHelper.getStoriesCount(fbProfileId,  15);
                objfbstatsHelper.getPostDetails(fbProfileId, user.Id, 15);
                likeunlikedate = objfbstatsHelper.getlikeUnlike(fbProfileId, 15);
                string imgPath = "http://graph.facebook.com/" + fbProfileId + "/picture";
                FacebookClient fb = new FacebookClient();
                fb.AccessToken = Request.QueryString["access"].ToString();
                dynamic pagelikes = fb.Get(fbProfileId);
                string strval = pagelikes.likes.ToString() + " Total Likes " + pagelikes.talking_about_count + " People talking about this.";
                string PeopleTalkingAboutThis = pagelikes.talking_about_count.ToString();
                string acstkn = Request.QueryString["access"].ToString();

                Session["acstknfnpg"] = acstkn;



                string fanpost = PageFeed(acstkn, fbProfileId);
                //divpost.InnerHtml = fanpost;

                Response.Write(strFbAgeArray + "_" + strPageImpression + "_" + strLocationArray + "_" + strval + "_" + PeopleTalkingAboutThis + "_" + likeunlikedate + "_" + imgPath + "_" + strstoriesArray + "_" + fanpost);
            }
        }
        public string PageFeed(string acs, string id)
        {
            FacebookClient fbClient = new FacebookClient();
            fbClient.AccessToken = acs;
            //SocioBoard.Helper.FacebookHelper fbhelper = new SocioBoard.Helper.FacebookHelper();
            var feeds = fbClient.Get("/" + id + "/feed");
            dynamic profile1 = fbClient.Get(id);
            string Fbpost = getFacebookpageFeeds(feeds, profile1);
            return Fbpost;

        }

        public string getFacebookpageFeeds(dynamic data, dynamic profile)
        {
            FacebookFeed fbfeed = new FacebookFeed();
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();
            string fanpost = string.Empty;
            if (data != null)
            {
                foreach (var result in data["data"])
                {


                    string message = string.Empty;
                    int likecount = 0;
                    int lstfbcount = 0;
                    int commentcount = 0;
                    int talking = 0;
                    string sharecount = string.Empty;

                    try
                    {
                        if (result["message"] != null)
                        {
                            message = result["message"];

                            try
                            {
                                if (result["likes"]["data"] != null)
                                {
                                    foreach (var item in result["likes"]["data"])
                                    {
                                        if (item != null)
                                        {
                                            likecount++;
                                        }
                                        else
                                        {
                                            likecount = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    likecount = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                likecount = 0;
                            }

                            try
                            {
                                if (result["comments"]["data"] != null)
                                {
                                    foreach (var item in result["comments"]["data"])
                                    {
                                        if (item != null)
                                        {
                                            commentcount++;
                                        }
                                        else
                                        {
                                            commentcount = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    commentcount = 0;
                                }
                            }
                            catch (Exception ex)
                            {
                                commentcount = 0;
                            }



                            try
                            {
                                if (result["shares"]["count"] != null)
                                {
                                    sharecount = result["shares"]["count"].ToString();
                                }
                                else
                                {
                                    sharecount = "0";
                                }
                            }
                            catch (Exception ex)
                            {
                                sharecount = "0";
                            }





                            // lstfbcount++;

                        }
                        fanpost = fanpost + "<div class=\"message-sent-table\" ><div class=\"labe-1\">" + message + "</div>" +
                                                           "<div class=\"labe-4\">" + talking + "</div><div class=\"labe-5\">" + likecount + "</div><div class=\"labe-6\">" + commentcount + "</div><div class=\"labe-5\">" + sharecount + "</div></div>";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);


                    }

                    //fanpost = fanpost + "<div class=\"message-sent-table\" ><div class=\"labe-1\">" + message + "</div>" +
                    //           "<div class=\"labe-4\">" + talking + "</div><div class=\"labe-5\">" + likecount + "</div><div class=\"labe-6\">" + commentcount + "</div><div class=\"labe-5\">" + sharecount + "</div></div>";

                }
            }
            return fanpost;
        }
    }
}