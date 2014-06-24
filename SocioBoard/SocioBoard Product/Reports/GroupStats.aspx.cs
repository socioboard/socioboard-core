using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Domain;
using SocioBoard.Helper;
using Facebook;
using System.Globalization;

namespace SocialSuitePro.Reports
{
    public partial class GroupStats : System.Web.UI.Page
    {
        public int male = 0, female = 0;
        public decimal twtmale = 0, twtfemale = 0;
        public string strArray = string.Empty;
        public string strSentArray = string.Empty;
        public string strTwtArray = string.Empty;
        public string strFBArray = string.Empty;
        public string strTwtAgeArray = string.Empty;
        public string strProfileCOunt = string.Empty;
        public string strFollowerMonth = string.Empty;
        public string strFollowingMonth = string.Empty;
        public string strMonth = string.Empty;
        public int pagelikes = 0;
        public int profileCount = 0;
        public long talkingabtcount = 0;
        public string strEng = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];

            try
            {
                #region for You can use only 30 days as Unpaid User

                //SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                if (user.PaymentStatus.ToLower() == "unpaid")
                {
                    if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                        Session["GreaterThan30Days"] = "GreaterThan30Days";

                        Response.Redirect("../Settings/Billing.aspx");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }

            if (!IsPostBack)
            {





                if (user == null)
                    Response.Redirect("/Default.aspx");
                try
                {
                    getgrphData(7);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }

                try
                {
                    getNewFriends(7);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    getNewFollowers(7);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    GetFollowersAgeWise(7);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    getFollowFollowersMonth();
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                    FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
                    ArrayList arrfbProfile = fbAccRepo.getAllFacebookPagesOfUser(user.Id);
                    long talking = 0;
                    foreach (FacebookAccount item in arrfbProfile)
                    {
                        FacebookClient fb = new FacebookClient();
                        fb.AccessToken = item.AccessToken;
                        pagelikes = pagelikes + fbFeedRepo.countInteractions(item.UserId, item.FbUserId, 7);
                        dynamic talkingabout = fb.Get(item.FbUserId);
                        talking = talking + talkingabout.talking_about_count;
                        // strinteractions = pagelikes.Count(); //(long.Parse(strinteractions) + pagelikes.talking_about_count).ToString();  
                    }
                    talkingabtcount = (talking / 100) * arrfbProfile.Count;
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }
                try
                {
                    SocialProfilesRepository objsocioRepo = new SocialProfilesRepository();
                    profileCount = objsocioRepo.getAllSocialProfilesOfUser(user.Id).Count();
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                }

                var strgenderTwt = Session["twtGender"].ToString().Split(',');
                divtwtMale.InnerHtml = strgenderTwt[0] + "%";
                divtwtfeMale.InnerHtml = strgenderTwt[1] + "%";
            }

        }

        public void getgrphData(int days)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                FacebookAccountRepository objfb = new FacebookAccountRepository();
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                ArrayList alstfb = objfb.getFbMessageStats(user.Id, days);
                ArrayList alstTwt = objtwttatsRepo.gettwtMessageStats(user.Id, days);
                strArray = "[";
                int _spanIncoming = 0;
                for (int i = 0; i < 7; i++)
                {
                    string strTwtCnt = string.Empty;
                    string strFbCnt = string.Empty;
                    if (alstTwt.Count <= i)
                        strTwtCnt = "0";
                    else
                        strTwtCnt = alstTwt[i].ToString();
                    if (alstfb.Count <= i)
                        strFbCnt = "0";
                    else
                        strFbCnt = alstfb[i].ToString();
                    strArray = strArray + "[" + strFbCnt + "," + strTwtCnt + "],";
                    //spanIncoming.InnerHtml = (int.Parse(strTwtCnt) + int.Parse(strFbCnt)).ToString();
                    _spanIncoming = (int.Parse(strTwtCnt) + int.Parse(strFbCnt));
                }
                spanIncoming.InnerHtml = _spanIncoming.ToString();
                strArray = strArray.Substring(0, strArray.Length - 1) + "]";

                ArrayList alstTwtFeed = objtwttatsRepo.gettwtFeedsStats(user.Id, days);
                ArrayList alstFBFeed = objfb.getFbFeedsStats(user.Id, days);
                strSentArray = "[";
                int _spanSent = 0;
                if (alstFBFeed.Count > 0 && alstTwtFeed.Count > 0)
                {
                    int alstSentCount = 0;
                    for (int i = 0; i < 7; i++)
                    {
                        string strTwtFeedCnt = string.Empty;
                        string strFbFeedCnt = string.Empty;
                        if (alstTwtFeed.Count <= i)
                            strTwtFeedCnt = "0";
                        else
                            strTwtFeedCnt = alstTwtFeed[i].ToString();
                        if (alstFBFeed.Count <= i)
                            strFbFeedCnt = "0";
                        else
                            strFbFeedCnt = alstFBFeed[i].ToString();
                        strSentArray = strSentArray + "[" + strFbFeedCnt + "," + strTwtFeedCnt + "],";
                        //spanSent.InnerHtml = (int.Parse(strFbFeedCnt) + int.Parse(strTwtFeedCnt)).ToString();
                        _spanSent = (int.Parse(strFbFeedCnt) + int.Parse(strTwtFeedCnt));
                    }
                    spanSent.InnerHtml = (_spanSent).ToString();
                }
                if (alstFBFeed.Count == 0 || alstTwtFeed.Count == 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        strSentArray += strSentArray + "[0,0],";
                    }
                }

                strSentArray = strSentArray.Substring(0, strSentArray.Length - 1) + "]";

                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                ArrayList alstEng = objtwtStatsRepo.getAllTwitterStatsOfUser(user.Id, days);
                int ii = 1;
                strEng = "[";
                foreach (var item in alstEng)
                {
                    Array temp = (Array)item;
                    strEng = strEng + "{ x: new Date(2012, " + ii + ", " + ii + "), y:" + temp.GetValue(7) + "},";
                    ii++;
                }
                if (alstEng.Count == 0)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        strEng = strEng + "{ x: new Date(2012, " + ii + ", " + ii + "), y:0},";
                    }
                }
                strEng = strEng.Substring(0, strEng.Length - 1) + "]";

                hmsgsent.InnerHtml = alstTwtFeed.Count.ToString();
                hretweet.InnerHtml = objtwttatsRepo.getUserRetweetCount(user.Id).ToString();

            }
            catch (Exception Err)
            {
                Response.Write(Err.StackTrace);
            }
        }

        public void getNewFriends(int days)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                FacebookStatsRepository objfbStatsRepo = new FacebookStatsRepository();
                ArrayList arrFbStats = objfbStatsRepo.getAllFacebookStatsOfUser(user.Id, days);
                strFBArray = "[";
                int intdays = 1;

                // Get facebook page like ...
                FacebookAccountRepository ObjAcFbAccount = new FacebookAccountRepository();
                int TotalLikes = ObjAcFbAccount.getPagelikebyUserId(user.Id);

                foreach (var item in arrFbStats)
                {
                    Array temp = (Array)item;
                    strFBArray += int.Parse(temp.GetValue(3).ToString()) + int.Parse(temp.GetValue(4).ToString()) + ",";
                    //spanFbFriends.InnerHtml = (int.Parse(temp.GetValue(3).ToString()) + int.Parse(temp.GetValue(4).ToString())).ToString();
                    spanFbFriends.InnerHtml = (TotalLikes).ToString();
                    intdays++;
                }
                if (intdays < 7)
                {
                    for (int j = 0; j < 7 - intdays; j++)
                    {
                        strFBArray = strFBArray + "0,";
                    }
                }
                strFBArray = strFBArray.Substring(0, strFBArray.Length - 1) + "]";
                // strFBArray += "]";
            }
            catch (Exception Err)
            {
                Response.Write(Err.Message.ToString());
            }
        }

        public void getNewFollowers(int days)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                ArrayList arrTwtStats = objtwtStatsRepo.getAllTwitterStatsOfUser(user.Id, days);
                strTwtArray = "[";
                int intdays = 1;
                int NewTweet_Count = 0;
                foreach (var item in arrTwtStats)
                {
                    Array temp = (Array)item;
                    strTwtArray += (temp.GetValue(4)) + ",";
                    //spanTwtFollowers.InnerHtml = temp.GetValue(4).ToString();
                    //hTwtFollowers.InnerHtml = temp.GetValue(4).ToString();
                    NewTweet_Count += Convert.ToInt16(temp.GetValue(4));
                }
                spanTwtFollowers.InnerHtml = NewTweet_Count.ToString();
                hTwtFollowers.InnerHtml = NewTweet_Count.ToString();

                if (intdays < 7)
                {
                    for (int j = 0; j < 7 - intdays; j++)
                    {
                        strTwtArray = strTwtArray + "0,";
                    }
                }
                strTwtArray = strTwtArray.Substring(0, strTwtArray.Length - 1) + "]";
                //  strTwtArray += "]";
            }
            catch (Exception Err)
            {
                Response.Write(Err.Message.ToString());
            }
        }

        public void GetFollowersAgeWise(int days)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
                object arrTwtStats = objtwtStatsRepo.getFollowersAgeCount(user.Id, days);
                if (arrTwtStats != null)
                {
                    string[] arr = ((IEnumerable)arrTwtStats).Cast<object>().Select(x => x.ToString()).ToArray();

                    for (int i = 0; i < arr.Count(); i++)
                    {
                        strTwtAgeArray += arr[i] + ",";
                    }
                    strTwtAgeArray = "[" + strTwtAgeArray.Substring(0, strTwtAgeArray.Length - 1) + "]";
                }
                else
                {
                    strTwtAgeArray = "[0,0,0,0,0,0,0]";
                }
                //strTwtArray += "]";
            }
            catch (Exception Err)
            {
                strTwtAgeArray = "[0,0,0,0,0,0,0]";
                //Response.Write(Err.Message.ToString());
            }
        }

        public void getProfileCount()
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            SocialProfilesRepository objProfile = new SocialProfilesRepository();
            List<SocialProfile> lstProfile = objProfile.getAllSocialProfilesOfUser(user.Id);
            strProfileCOunt = lstProfile.Count().ToString();
        }

        public void getFollowFollowersMonth()
        {
            SocioBoard.Domain.User user = (User)Session["LoggedUser"];
            TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();
            ArrayList objtwtffMonth = objtwtStatsRepo.getFollowerFollowingCountMonth(user.Id);
            string[,] arrMon = new string[12, 3];

            for (int i = 0; i < objtwtffMonth.Count; i++)
            {
                string month = string.Empty;
                string monfollower = string.Empty;
                string monfollowing = string.Empty;
                string[] arr = new string[1];
                try
                {
                    arr = ((IEnumerable)objtwtffMonth[i]).Cast<object>().Select(x => x.ToString()).ToArray();
                    int m = int.Parse(arr[2].ToString()) - 1;
                    arrMon[m, 2] = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(arr[2].ToString()));
                    arrMon[m, 0] = arr[0];
                    arrMon[m, 1] = arr[1];
                }
                catch (Exception Err)
                {

                }

            }
            for (int r = 0; r < 12; r++)
            {
                if (arrMon[r, 2] == null)
                {
                    arrMon[r, 2] = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(r + 1);
                    arrMon[r, 0] = "0";
                    arrMon[r, 1] = "0";
                }
            }
            for (int j = 0; j < 12; j++)
            {
                strMonth = strMonth + "'" + arrMon[j, 2] + "',";
                strFollowerMonth += arrMon[j, 0] + ",";
                strFollowingMonth += arrMon[j, 1] + ",";
            }
            strFollowerMonth = "[" + strFollowerMonth.Substring(0, strFollowerMonth.Length - 1) + "]";
            strFollowingMonth = "[" + strFollowingMonth.Substring(0, strFollowingMonth.Length - 1) + "]";
            strMonth = "[" + strMonth.Substring(0, strMonth.Length - 1) + "]";
        }



        protected void btnfifteen_Click(object sender, EventArgs e)
        {
            try
            {
                getgrphData(15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getNewFriends(15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getNewFollowers(15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                GetFollowersAgeWise(15);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getFollowFollowersMonth();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        protected void btnthirty_Click(object sender, EventArgs e)
        {
            try
            {
                getgrphData(30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getNewFriends(30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getNewFollowers(30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                GetFollowersAgeWise(30);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getFollowFollowersMonth();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        protected void btnsixty_Click(object sender, EventArgs e)
        {
            try
            {
                getgrphData(60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getNewFriends(60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getNewFollowers(60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                GetFollowersAgeWise(60);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getFollowFollowersMonth();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }

        protected void btnninty_Click(object sender, EventArgs e)
        {
            try
            {
                getgrphData(90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }

            try
            {
                getNewFriends(90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getNewFollowers(90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                GetFollowersAgeWise(90);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            try
            {
                getFollowFollowersMonth();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }
    }
}