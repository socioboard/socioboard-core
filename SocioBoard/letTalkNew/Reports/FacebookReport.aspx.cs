using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json.Linq;
using letTalkNew.Helper;
using Facebook;
using Newtonsoft.Json;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Helper;

namespace letTalkNew.Reports
{
    public partial class FacebookReport : System.Web.UI.Page
    {
        public string shareGraphArray = string.Empty;
        public string strgraph = string.Empty;
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
                if (Session["LoggedUser"] == null)
                {
                    Response.Redirect("/Default.aspx");
                    return;
                }

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
                        //fbUser = fbUser + "<div  class=\"teitter\"><ul><li><a id=\"facebook_connect\" onclick='getProfilefbGraph(\"" + item.FbUserId + "\",\"" + item.FbUserName + "\",\"" + imgPath + "\",\"" + item.AccessToken + "\")'><span style=\"float:left;margin: 3px 0 0 5px;\" >" + item.FbUserName + "</span></a></li></ul></div>";
                        fbUser = fbUser + "<li><a id=\"facebook_connect\" onclick='getProfilefbGraph(\"" + item.FbUserId + "\",\"" + item.FbUserName + "\",\"" + imgPath + "\",\"" + item.AccessToken + "\")'><span style=\"float:left;margin: 3px 0 0 5px;\" >" + item.FbUserName + "</span></a></li>";
                        fbProfileId = item.FbUserId;
                        divPageName.InnerHtml = item.FbUserName;
                        fbProfileImg.Src = "http://graph.facebook.com/" + item.FbUserId + "/picture";
                        strfbAccess = item.AccessToken;
                    }
                    if (arrfbProfile.Count > 0)
                    {
                        strgraph = agepageimpressiongraph();

                        shareGraphArray = GetSharingGraph();

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



        public string agepageimpressiongraph()
        {
            int[] arr1 = { 0, 150000, 100000, 50000, 200000, 250000, 350000, 300000 };
            int[] arr2 = { 50000, 200000, 150000, 100000, 250000, 300000, 400000, 350000 };
            string[] labelarr = { "label: \"10-20\"", "label: \"20-30\"", "label: \"30-40\"", "label: \"40-50\"", "label: \"50-60\"", "label: \"60-70\"", "label: \"80-90\"", "label: \"90 +\"" };
            string strarr = string.Empty;
            for (int i = 0; i < 8; i++)
            {
                strarr += "{y: " + randomno(arr1[i], arr2[i]) + ", " + labelarr[i] + "},";
            }
            strarr = "[" + strarr.Substring(0, strarr.Length - 1) + "]";
            return strarr;
        
        }
        public int randomno(int start, int end)
        {
            Random getrandom = new Random();
            return getrandom.Next(start, end);
        }


        public string GetSharingGraph()
        {
            string sharingGraphData = string.Empty;
            try
            {
                //    [
                //{ label: "jan", y: 168 } ,
                //{ label: "feb", y: 118 } ,
                //{ label: "mar", y: 38 } ,
                //{ label: "apr", y: 28 } ,
                //{ label: "may", y: 148 } ,
                //{ label: "jun", y: 38 } ,
                //{ label: "jul", y: 178 } ,
                //{ label: "aug", y: 0 } ,
                //{ label: "sep", y: 98 } ,
                //{ label: "oct", y: 68 } ,
                //{ label: "nov", y: 18 } ,
                //{ label: "dec", y: 50 } 

                //]

                User user = (User)Session["LoggedUser"];

                FacebookStatsRepository objFacebookStatsRepository = new FacebookStatsRepository();

                List<int> lstShareCount=objFacebookStatsRepository.GetShareByUserId(user.Id);

                string shareGarphArray = string.Empty;

                for (int i = 0; i < 12; i++)
                {
                    try
                    {
                        if (i == 0)
                        {
                            shareGarphArray += "{ label: \"jan\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 1)
                        {
                            shareGarphArray += "{ label: \"feb\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 2)
                        {
                            shareGarphArray += "{ label: \"mar\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 3)
                        {
                            shareGarphArray += "{ label: \"apr\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 4)
                        {
                            shareGarphArray += "{ label: \"may\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 5)
                        {
                            shareGarphArray += "{ label: \"jun\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 6)
                        {
                            shareGarphArray += "{ label: \"jul\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 7)
                        {
                            shareGarphArray += "{ label: \"aug\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 8)
                        {
                            shareGarphArray += "{ label: \"sep\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 9)
                        {
                            shareGarphArray += "{ label: \"oct\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 10)
                        {
                            shareGarphArray += "{ label: \"nov\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        if (i == 11)
                        {
                            shareGarphArray += "{ label: \"dec\", y:" + lstShareCount[i] + "},";
                            continue;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex.StackTrace);
                    }
                }

                shareGarphArray = "[" + shareGarphArray.Substring(0, shareGarphArray.Length - 1) + "]";

                sharingGraphData = shareGarphArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
            return sharingGraphData;
        }


    }
}