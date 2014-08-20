using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json.Linq;
using SocioBoard.Helper;
using Facebook;
using Newtonsoft.Json;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Helper;
using log4net;

namespace SocialSuitePro.Reports
{
    public partial class FacebookReport : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(FacebookReport));
        public string strFbAgeArray = string.Empty;
        public string strPageImpression = string.Empty;
        public string strLocationArray = string.Empty;
        public string strstoriesArray = string.Empty;
        string fbUser = string.Empty;
        string fbProfileId = string.Empty;
        public string likeunlikedate = string.Empty;
        string strfbAccess = string.Empty;
        public static string fbpageProfileId = string.Empty;
        string fbpageaccesstkn = string.Empty;
        FacebookInsightStatsHelper fbiHelper = new FacebookInsightStatsHelper();
        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        FacebookInsightStatsHelper objfbstatsHelper = new FacebookInsightStatsHelper();

        string fbpId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    User user = (User)Session["LoggedUser"];

                    if (user == null)
                        Response.Redirect("/Default.aspx");

                    FacebookAccountRepository objFbRepo = new FacebookAccountRepository();
                    TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();

                    SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                    List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getTeamMemberProfileData(team.Id);

                    //List<FacebookAccount>arrfbPrfile=new List<FacebookAccount>();
                    ArrayList arrfbPrfile = new ArrayList();
                    try
                    {
                        foreach (TeamMemberProfile item in allprofiles)
                        {
                            //fbpId += item.ProfileId + ',';
                            FacebookAccount arrfbProfile = objFbRepo.getAllFbAccountDetail(item.ProfileId);
                            if (arrfbProfile.FbUserId != null)
                            {
                                arrfbPrfile.Add(arrfbProfile);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                    //fbpId = fbpId.Substring(0, fbpId.Length - 1);

                    //List<FacebookAccount> arrfbProfile = objFbRepo.getAllFbAccountDetail(fbpId);

                    //ArrayList arrfbProfile = fbAccRepo.getAllFacebookPagesOfUser(user.Id);
                    spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                    try
                    {
                        foreach (FacebookAccount item in arrfbPrfile)
                        {
                            string imgPath = "http://graph.facebook.com/" + item.FbUserId + "/picture";
                            fbUser = fbUser + "<div  class=\"teitter\"><ul><li><a id=\"facebook_connect\" onclick='getProfilefbGraph(\"" + item.FbUserId + "\",\"" + item.FbUserName + "\",\"" + imgPath + "\",\"" + item.AccessToken + "\")'><span style=\"float:left;margin: 3px 0 0 5px;\" >" + item.FbUserName + "</span></a></li></ul></div>";
                            fbProfileId = item.FbUserId;
                            Session["fbprofileId"] = fbProfileId;
                            divPageName.InnerHtml = item.FbUserName;
                            fbProfileImg.Src = "http://graph.facebook.com/" + item.FbUserId + "/picture";
                            strfbAccess = item.AccessToken;
                            Session["acstknfnpg"] = strfbAccess;
                        }
                    }
                    catch (Exception ex)
                    {
                        
                       logger.Error(ex.Message);
                    }
                    if (arrfbPrfile.Count > 0)
                    {
                        try
                        {
                            getAllGroupsOnHome.InnerHtml = fbUser;
                            strFbAgeArray = fbiHelper.getLikesByGenderAge(fbProfileId, 15);
                            strPageImpression = fbiHelper.getPageImressions(fbProfileId, 15);
                            strLocationArray = fbiHelper.getLocationInsight(fbProfileId, 15);
                            strstoriesArray = fbiHelper.getStoriesCount(fbProfileId, 15);
                            //divpost.InnerHtml = fbiHelper.getPostDetails(fbProfileId, user.Id, 15);
                            likeunlikedate = objfbstatsHelper.getlikeUnlike(fbProfileId, 15);

                            FacebookClient fb = new FacebookClient();
                            fb.AccessToken = strfbAccess;

                            dynamic pagelikes = fb.Get(fbProfileId);
                            divPageLikes.InnerHtml = pagelikes.likes.ToString() + " Total Likes " + pagelikes.talking_about_count + " People talking about this.";
                            spanTalking.InnerHtml = pagelikes.talking_about_count.ToString();

                            string fanpost = PageFeed(strfbAccess, fbProfileId);
                            divpost.InnerHtml = fanpost;
                        }
                        catch (Exception ex)
                        {
                            
                           logger.Error(ex.Message);
                        }

                    }
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message);
            }
        }
       

        //public void getFanPageLikesByGenderAge(string fbUserId, Guid UserId)
        //{
        //    #region LIkes By Gender & age
        //    FacebookInsightStatsHelper fbiHelper=new FacebookInsightStatsHelper();
        //    fbiHelper.getFanPageLikesByGenderAge(fbUserId,UserId, 15);          
        //    #endregion
            
        //}
        protected void btnfifteen_Click(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                fbpageProfileId = Session["fbprofileId"].ToString();
                fbProfileDetails(fbpageProfileId);

                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbpageProfileId, 15);
                strPageImpression = fbiHelper.getPageImressions(fbpageProfileId, 15);
                strLocationArray = fbiHelper.getLocationInsight(fbpageProfileId,  15);
                //divpost.InnerHtml = fbiHelper.getPostDetails(fbpageProfileId, user.Id, 15);
                strstoriesArray = fbiHelper.getStoriesCount(fbpageProfileId,  15);
                likeunlikedate = objfbstatsHelper.getlikeUnlike(fbpageProfileId, 15);

                string stracsfbpg = Session["acstknfnpg"].ToString();
                string fanpost = PageFeed(stracsfbpg, fbpageProfileId);
                divpost.InnerHtml = fanpost;
            }
            catch (Exception Err)
            {
                Response.Write(Err.StackTrace);
            }
        }

        protected void btnthirty_Click(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                fbpageProfileId = Session["fbprofileId"].ToString();
                fbProfileDetails(fbpageProfileId);

                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-30).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbpageProfileId, 30);
                strPageImpression = fbiHelper.getPageImressions(fbpageProfileId,  30);
                strLocationArray = fbiHelper.getLocationInsight(fbpageProfileId, 30);
                //divpost.InnerHtml = fbiHelper.getPostDetails(fbpageProfileId, user.Id, 30);
                strstoriesArray = fbiHelper.getStoriesCount(fbpageProfileId, 30);
                likeunlikedate = objfbstatsHelper.getlikeUnlike(fbpageProfileId, 30);

                string stracsfbpg = Session["acstknfnpg"].ToString();
                string fanpost = PageFeed(stracsfbpg, fbpageProfileId);
                divpost.InnerHtml = fanpost;

            }
            catch (Exception Err)
            {
                Response.Write(Err.StackTrace);
            }
        }

        protected void btnsixty_Click(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                fbpageProfileId = Session["fbprofileId"].ToString();
                fbProfileDetails(fbpageProfileId);

                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-60).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbpageProfileId,  60);
                strPageImpression = fbiHelper.getPageImressions(fbpageProfileId,  60);
                strLocationArray = fbiHelper.getLocationInsight(fbpageProfileId,  60);
                //divpost.InnerHtml = fbiHelper.getPostDetails(fbpageProfileId, user.Id, 60);
                strstoriesArray = fbiHelper.getStoriesCount(fbpageProfileId,  60);
                likeunlikedate = objfbstatsHelper.getlikeUnlike(fbpageProfileId, 60);

                string stracsfbpg = Session["acstknfnpg"].ToString();
                string fanpost = PageFeed(stracsfbpg, fbpageProfileId);
                divpost.InnerHtml = fanpost;

            }
            catch (Exception Err)
            {
                Response.Write(Err.StackTrace);
            }
        }

        protected void btnninty_Click(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                fbpageProfileId = Session["fbprofileId"].ToString();
                fbProfileDetails(fbpageProfileId);

                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-90).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbpageProfileId, 90);
                strPageImpression = fbiHelper.getPageImressions(fbpageProfileId,  90);
                strLocationArray = fbiHelper.getLocationInsight(fbpageProfileId,90);
                //divpost.InnerHtml = fbiHelper.getPostDetails(fbpageProfileId, user.Id, 90);
                strstoriesArray = fbiHelper.getStoriesCount(fbpageProfileId, 90);
                likeunlikedate = objfbstatsHelper.getlikeUnlike(fbpageProfileId, 90);

                string stracsfbpg = Session["acstknfnpg"].ToString();
                string fanpost = PageFeed(stracsfbpg, fbpageProfileId);
                divpost.InnerHtml = fanpost;

            }
            catch (Exception Err)
            {
                Response.Write(Err.StackTrace);
            }
        }


        protected void fbProfileDetails(string fbid)
        {
            FacebookAccountRepository objtwtAccRepo = new FacebookAccountRepository();
            FacebookAccount arrFbAcc = objtwtAccRepo.getUserDetails(fbid );
            divPageName.InnerHtml = arrFbAcc.FbUserName;
            //string src = "http://graph.facebook.com/" + arrFbAcc.FbUserId + "/picture";
            string fbpgid = arrFbAcc.FbUserId;
            fbProfileImg.Src = "http://graph.facebook.com/" + fbpgid + "/picture";
            //fbProfileImg.Src = src;
            fbpageaccesstkn = arrFbAcc.AccessToken;
            FacebookClient fb = new FacebookClient();
            fb.AccessToken = fbpageaccesstkn;
            dynamic pagelikes = fb.Get(fbid);
            divPageLikes.InnerHtml = pagelikes.likes.ToString() + " Total Likes " + pagelikes.talking_about_count + " People talking about this.";
            spanTalking.InnerHtml = pagelikes.talking_about_count.ToString();

        }

        public string PageFeed(string acs,string id)
        {
            FacebookClient fbClient = new FacebookClient();
            fbClient.AccessToken = acs;
            //SocioBoard.Helper.FacebookHelper fbhelper = new SocioBoard.Helper.FacebookHelper();
            var feeds = fbClient.Get("/" + id + "/feed");
            dynamic profile1 = fbClient.Get(id);
           string Fbpost= getFacebookpageFeeds(feeds, profile1);
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