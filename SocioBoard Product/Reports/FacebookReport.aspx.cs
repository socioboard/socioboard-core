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

namespace SocialSuitePro.Reports
{
    public partial class FacebookReport : System.Web.UI.Page
    {
        public string strFbAgeArray = string.Empty;
        public string strPageImpression = string.Empty;
        public string strLocationArray = string.Empty;
        public string strstoriesArray = string.Empty;
        string fbUser = string.Empty;
        string fbProfileId = string.Empty;
        string strfbAccess = string.Empty;
        FacebookInsightStatsHelper fbiHelper = new FacebookInsightStatsHelper();
        FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    User user = (User)Session["LoggedUser"];

                    if (user == null)
                        Response.Redirect("/Default.aspx");



                    ArrayList arrfbProfile = fbAccRepo.getAllFacebookPagesOfUser(user.Id);
                    spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                    foreach (FacebookAccount item in arrfbProfile)
                    {
                        string imgPath = "http://graph.facebook.com/" + item.FbUserId + "/picture";
                        fbUser = fbUser + "<div  class=\"teitter\"><ul><li><a id=\"facebook_connect\" onclick='getProfilefbGraph(\"" + item.FbUserId + "\",\"" + item.FbUserName + "\",\"" + imgPath + "\",\"" + item.AccessToken + "\")'><span style=\"float:left;margin: 3px 0 0 5px;\" >" + item.FbUserName + "</span></a></li></ul></div>";
                        fbProfileId = item.FbUserId;
                        divPageName.InnerHtml = item.FbUserName;
                        fbProfileImg.Src = "http://graph.facebook.com/" + item.FbUserId + "/picture";
                        strfbAccess = item.AccessToken;
                    }
                    if (arrfbProfile.Count > 0)
                    {
                        getAllGroupsOnHome.InnerHtml = fbUser;
                        strFbAgeArray = fbiHelper.getLikesByGenderAge(fbProfileId, user.Id, 15);
                        strPageImpression = fbiHelper.getPageImressions(fbProfileId, user.Id, 15);
                        strLocationArray = fbiHelper.getLocationInsight(fbProfileId, user.Id, 15);
                        strstoriesArray = fbiHelper.getStoriesCount(fbProfileId, user.Id, 15);
                        divpost.InnerHtml = fbiHelper.getPostDetails(fbProfileId, user.Id, 15);
                        FacebookClient fb = new FacebookClient();
                        fb.AccessToken = strfbAccess;
                        dynamic pagelikes = fb.Get(fbProfileId);
                        divPageLikes.InnerHtml = pagelikes.likes.ToString() + " Total Likes " + pagelikes.talking_about_count + " People talking about this.";
                        spanTalking.InnerHtml = pagelikes.talking_about_count.ToString();
                    }
                }
            }
            catch (Exception Err)
            {
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
                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                User user = (User)Session["LoggedUser"];     
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbProfileId, user.Id, 15);
                strPageImpression = fbiHelper.getPageImressions(fbProfileId, user.Id, 15);
                strLocationArray = fbiHelper.getLocationInsight(fbProfileId, user.Id, 15);
                divpost.InnerHtml = fbiHelper.getPostDetails(fbProfileId, user.Id, 15);
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
                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-30).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                User user = (User)Session["LoggedUser"];
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbProfileId, user.Id, 30);
                strPageImpression = fbiHelper.getPageImressions(fbProfileId, user.Id, 30);
                strLocationArray = fbiHelper.getLocationInsight(fbProfileId, user.Id, 30);
                divpost.InnerHtml = fbiHelper.getPostDetails(fbProfileId, user.Id, 30);
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
                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-60).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                User user = (User)Session["LoggedUser"];
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbProfileId, user.Id, 60);
                strPageImpression = fbiHelper.getPageImressions(fbProfileId, user.Id, 60);
                strLocationArray = fbiHelper.getLocationInsight(fbProfileId, user.Id, 60);
                divpost.InnerHtml = fbiHelper.getPostDetails(fbProfileId, user.Id, 60);
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
                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-90).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                User user = (User)Session["LoggedUser"];
                strFbAgeArray = fbiHelper.getLikesByGenderAge(fbProfileId, user.Id, 90);
                strPageImpression = fbiHelper.getPageImressions(fbProfileId, user.Id, 90);
                strLocationArray = fbiHelper.getLocationInsight(fbProfileId, user.Id, 90);
                divpost.InnerHtml = fbiHelper.getPostDetails(fbProfileId, user.Id, 90);
            }
            catch (Exception Err)
            {
                Response.Write(Err.StackTrace);
            }
        }
    }
}