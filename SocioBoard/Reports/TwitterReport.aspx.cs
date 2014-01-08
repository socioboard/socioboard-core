using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Helper;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Helper;

namespace SocialSuitePro.Reports
{
    public partial class TwitterReport : System.Web.UI.Page
    {
        TwitterStatsHelper objtwtStatsHelper = new TwitterStatsHelper();
        TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
        public string strTwtArray = "[" + 0 + "]";
        public string strTwtFollowing = "[" + 0 + "]";
        public string strTwtAge = string.Empty;
        public string strDmRecieve = string.Empty;
        public string strDMSent = string.Empty;
        public string strIncomingMsg = string.Empty;
        public string strSentMsg = string.Empty;
        public string strRetweet = string.Empty;
        public string strEngInf = string.Empty;
        public string strAgeDiff = string.Empty;
        public static string twtProfileId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                if (user == null)
                    Response.Redirect("/Default.aspx");       
                TwitterAccountRepository objtwtAccRepo = new TwitterAccountRepository();
                ArrayList arrTwtAcc= objtwtAccRepo.getAllTwitterAccountsOfUser(user.Id);
                string twtUser = string.Empty;
                string twtProfileId = string.Empty;
                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                foreach (TwitterAccount item in arrTwtAcc)
                {
                    twtUser = twtUser + "<div  class=\"teitter\"><ul><li><a id=\"facebook_connect\" onclick='getProfileGraph(\""+ item.TwitterUserId +"\",\""+item.TwitterScreenName+"\",\""+item.ProfileImageUrl+"\",\""+ item.FollowersCount +"\")'><span style=\"float:left;margin: 3px 0 0 5px;\" >" + item.TwitterScreenName + "</span></a></li></ul></div>";
                    twtProfileId = item.TwitterUserId;
                    divnameId.InnerHtml = item.TwitterScreenName;
                    profileImg.ImageUrl = item.ProfileImageUrl;
                    spanFollowers.InnerHtml = item.FollowersCount.ToString();
                }
                divtwtUser.InnerHtml = twtUser;
                try
                {
                    strTwtArray = objtwtStatsHelper.getNewFollowers(user, twtProfileId,15);
                    int index=strTwtArray.LastIndexOf(',');
                    divnewFollower.InnerHtml = strTwtArray.Substring(index+1);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(user, twtProfileId,15);
                int index = strTwtFollowing.LastIndexOf(',');
                divFollowed.InnerHtml = strTwtArray.Substring(index+1);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strTwtAge = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(user, twtProfileId,15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(user, twtProfileId);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(user, twtProfileId);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                strSentMsg = objtwtStatsHelper.getSentMsg(user, twtProfileId,15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                strRetweet = objtwtStatsHelper.getRetweets(user, twtProfileId,15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                strEngInf = objtwtStatsHelper.getEngagements(user, twtProfileId,15) + "@" + objtwtStatsHelper.getInfluence(user, twtProfileId,15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 15);
                }
                catch (Exception Err)
                { 
                    
                }
                var strgenderTwt = Session["twtGender"].ToString().Split(',');
                divtwtMale.InnerHtml = strgenderTwt[0] + "%";
                divtwtfeMale.InnerHtml = strgenderTwt[1]+"%";
            }
        }

        protected void btnfifteen_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(user, twtProfileId,15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(user, twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(user, twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(user, twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets(user, twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(user, twtProfileId,15) + "@" + objtwtStatsHelper.getInfluence(user, twtProfileId,15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        protected void btnthirty_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-30).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(user, twtProfileId,30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(user, twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user,30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(user, twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(user, twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets(user, twtProfileId,30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(user, twtProfileId,15) + "@" + objtwtStatsHelper.getInfluence(user, twtProfileId,30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId,30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        protected void btnsixty_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-60).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(user, twtProfileId,60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(user, twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(user, twtProfileId,60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(user, twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets(user, twtProfileId,60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(user, twtProfileId,15) + "@" + objtwtStatsHelper.getInfluence(user, twtProfileId,60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        protected void btnninty_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-90).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(user, twtProfileId,90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(user, twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(user, twtProfileId,90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(user, twtProfileId);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(user, twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets(user, twtProfileId,90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(user, twtProfileId,15) + "@" + objtwtStatsHelper.getInfluence(user, twtProfileId,90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }
    }
}