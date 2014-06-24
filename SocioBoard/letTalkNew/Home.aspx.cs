using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;
using GlobusLinkedinLib.Authentication;
using GlobusGooglePlusLib.Authentication;
using GlobusInstagramLib.Authentication;
using Facebook;
using System.Data;
using System.Configuration;
using log4net;
using System.Collections;

namespace letTalkNew
{
    public partial class Home : System.Web.UI.Page
    {
        public int tot_acc = 0;
        public int profileCount = 0;
        public int male = 0, female = 0;
        public decimal twtmale = 0, twtfemale = 0;
        public int totfbfriends = 0;
        public int tottwtfriends = 0;
        ILog logger = LogManager.GetLogger(typeof(Home));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUser"] == null)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            if (!IsPostBack)
            {

                UserRepository userrepo = new UserRepository();
                Registration regObject = new Registration();
                TeamRepository objTeamRepo = new TeamRepository();
                NewsRepository objNewsRepo = new NewsRepository();
                AdsRepository objAdsRepo = new AdsRepository();
                SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();
                
                
                
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];

                #region InsightsData
                try
                {
                    decimal malecount = 0, femalecount = 0, cnt = 0;

                    FacebookStatsRepository objfbStatsRepo = new FacebookStatsRepository();
                    double daysSub = (DateTime.Now - user.CreateDate).TotalDays;
                    int userdays = (int)daysSub;
                    ArrayList arrFbStats = objfbStatsRepo.getAllFacebookStatsOfUser(user.Id, userdays);
                    Random rNum = new Random();
                    foreach (var item in arrFbStats)
                    {
                        Array temp = (Array)item;
                        cnt += int.Parse(temp.GetValue(3).ToString()) + int.Parse(temp.GetValue(4).ToString());
                        malecount += int.Parse(temp.GetValue(3).ToString());
                        femalecount += int.Parse(temp.GetValue(4).ToString());
                       
                    }
                    try
                    {
                        decimal fbfriendcnt = malecount + femalecount;
                        decimal fbfriend = (fbfriendcnt / 1000) * 100;
                        totfbfriends = (int)fbfriend;
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }
                    try
                    {
                        decimal mc = (malecount / cnt) * 100;
                        male = Convert.ToInt16(mc);
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }
                    try
                    {
                        decimal fc = (femalecount / cnt) * 100;
                        female = Convert.ToInt16(fc);
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }
                    int twtAccCount = objSocioRepo.getAllSocialProfilesTypeOfUser(user.Id, "twitter").Count;
                    if (twtAccCount > 1)
                    {
                        twtmale = rNum.Next(100);
                        twtfemale = 100 - twtmale;
                    }
                    else if (twtAccCount == 1)
                    {
                        twtmale = 100;
                        twtfemale = 0;
                    }
                    try
                    {
                        decimal twtfriendcnt = twtmale + twtfemale;
                        decimal twtfriend = (twtfriendcnt / 1000) * 100;
                        tottwtfriends = (int)twtfriend;
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }
                    Session["twtGender"] = twtmale + "," + twtfemale;
                }
                catch (Exception Err)
                {
                    Console.Write(Err.Message.ToString());
                    logger.Error(Err.StackTrace);
                }
               
                #endregion




            }
        }


    }
}