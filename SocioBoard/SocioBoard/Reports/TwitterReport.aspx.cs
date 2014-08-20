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
        public string strTwtMention = string.Empty;

        public static string twtProfileId = string.Empty;
        string TwtProfileId = string.Empty;



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                if (user == null)
                    Response.Redirect("/Default.aspx");


                TwitterAccountRepository objtwtAccRepo = new TwitterAccountRepository();
                TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();


                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];

                List<TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getTwtTeamMemberProfileData(team.Id);

                foreach (TeamMemberProfile item in allprofiles)
                {
                    TwtProfileId += item.ProfileId + ',';
                }
                TwtProfileId = TwtProfileId.Substring(0, TwtProfileId.Length - 1);


                List<TwitterAccount> arrTwtAcc = objtwtAccRepo.getAllAccountDetail(TwtProfileId);                          
                string twtUser = string.Empty;
                string twtProfileId = string.Empty;

                spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
                foreach (TwitterAccount item in arrTwtAcc)
                {
                    twtUser = twtUser + "<div  class=\"teitter\"><ul><li><a id=\"facebook_connect\" onclick='getProfileGraph(\"" + item.TwitterUserId + "\",\"" + item.TwitterScreenName + "\",\"" + item.ProfileImageUrl + "\",\"" + item.FollowersCount + "\")'><span style=\"float:left;margin: 3px 0 0 5px;\" >" + item.TwitterScreenName + "</span></a></li></ul></div>";
                    twtProfileId = item.TwitterUserId;
                    divnameId.InnerHtml = item.TwitterScreenName;
                    profileImg.ImageUrl = item.ProfileImageUrl;
                    spanFollowers.InnerHtml = item.FollowersCount.ToString();
                    Session["twtProfileId"] = twtProfileId;
                }
                divtwtUser.InnerHtml = twtUser;
                try
                {
                    strTwtArray = objtwtStatsHelper.getNewFollowers(twtProfileId, 15);
                    int index = strTwtArray.LastIndexOf(',');
                    divnewFollower.InnerHtml = strTwtArray.Substring(index + 1);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strTwtFollowing = objtwtStatsHelper.getNewFollowing(twtProfileId, 15);
                    int index = strTwtFollowing.LastIndexOf(',');
                    divFollowed.InnerHtml = strTwtArray.Substring(index + 1);
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
                    strIncomingMsg = objtwtStatsHelper.getIncomingMsg( twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve( twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strDMSent = objtwtStatsHelper.getDirectMessageSent(twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strSentMsg = objtwtStatsHelper.getSentMsg(twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strRetweet = objtwtStatsHelper.getRetweets(twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    strEngInf = objtwtStatsHelper.getEngagements(twtProfileId, 15) + "@" + objtwtStatsHelper.getInfluence( twtProfileId, 15) + "@" + objtwtStatsHelper.getdate(twtProfileId, 15);
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

                try
                {
                    strTwtMention = objtwtStatsHelper.getTwtMention(twtProfileId, 15);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }


                var strgenderTwt = Session["twtGender"].ToString().Split(',');
                //divtwtMale.InnerHtml = strgenderTwt[0] + "%";
               // divtwtfeMale.InnerHtml = strgenderTwt[1] + "%";
            }
        }

        protected void btnfifteen_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            twtProfileId = Session["twtProfileId"].ToString();

            TwtProfileDetails(twtProfileId);

            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-15).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(twtProfileId, 15);
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
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
               strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(twtProfileId, 15) + "@" + objtwtStatsHelper.getInfluence(twtProfileId, 15) + "@" + objtwtStatsHelper.getdate( twtProfileId, 15);
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

            try
            {
                strTwtMention = objtwtStatsHelper.getTwtMention(twtProfileId, 15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }




        
        }

        protected void btnthirty_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            twtProfileId = Session["twtProfileId"].ToString();
            TwtProfileDetails(twtProfileId);
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-30).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing( twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtAge = objtwtStatsHelper.GetFollowersAgeWise(user, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets( twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(twtProfileId, 30) + "@" + objtwtStatsHelper.getInfluence( twtProfileId, 30) + "@" + objtwtStatsHelper.getdate( twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strAgeDiff = objtwtStatsRepo.getAgeDiffCount(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                strTwtMention = objtwtStatsHelper.getTwtMention(twtProfileId, 30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }




        }

        protected void btnsixty_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            twtProfileId = Session["twtProfileId"].ToString();
            TwtProfileDetails(twtProfileId);
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-60).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers( twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing( twtProfileId, 60);
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
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg( twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg(twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets( twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(twtProfileId, 60) + "@" + objtwtStatsHelper.getInfluence(twtProfileId, 60) + "@" + objtwtStatsHelper.getdate( twtProfileId, 60);
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

            try
            {
                strTwtMention = objtwtStatsHelper.getTwtMention(twtProfileId, 60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }






        }

        protected void btnninty_Click(object sender, EventArgs e)
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            twtProfileId = Session["twtProfileId"].ToString();
            TwtProfileDetails(twtProfileId);
            spandiv.InnerHtml = "from " + DateTime.Now.AddDays(-90).ToShortDateString() + "-" + DateTime.Now.ToShortDateString();
            try
            {
                strTwtArray = objtwtStatsHelper.getNewFollowers(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strTwtFollowing = objtwtStatsHelper.getNewFollowing(twtProfileId, 90);
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
                strIncomingMsg = objtwtStatsHelper.getIncomingMsg(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDmRecieve = objtwtStatsHelper.getDirectMessageRecieve(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strDMSent = objtwtStatsHelper.getDirectMessageSent(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strSentMsg = objtwtStatsHelper.getSentMsg( twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strRetweet = objtwtStatsHelper.getRetweets(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                strEngInf = objtwtStatsHelper.getEngagements(twtProfileId, 90) + "@" + objtwtStatsHelper.getInfluence( twtProfileId, 90) + "@" + objtwtStatsHelper.getdate(twtProfileId, 90);
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

            try
            {
                strTwtMention = objtwtStatsHelper.getTwtMention(twtProfileId, 90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }




        }



        protected void TwtProfileDetails(string twtid)
        {
            TwitterAccountRepository objtwtAccRepo = new TwitterAccountRepository();
            TwitterAccount arrTwtAcc = objtwtAccRepo.getUserInfo(twtid);

            twtProfileId = arrTwtAcc.TwitterUserId;
            divnameId.InnerHtml = arrTwtAcc.TwitterScreenName;
            profileImg.ImageUrl = arrTwtAcc.ProfileImageUrl;
            spanFollowers.InnerHtml = arrTwtAcc.FollowersCount.ToString();

        }




    }
}