using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using GlobusTwitterLib.Authentication;
using GlobusLinkedinLib.Authentication;
using GlobusInstagramLib.Authentication;
using SocioBoard.Model;
using SocioBoard;
using SocioBoard.Domain;
using Newtonsoft.Json.Linq;
using System.Data;
using Facebook;
using GlobusGooglePlusLib.Authentication;
using System.Collections;
using SocioBoard.Helper;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using log4net;
using GlobusTumblrLib.Authentication;


namespace SocialSuitePro
{
    public partial class Home : System.Web.UI.Page
    {

        public int male = 0, female = 0;
        public decimal twtmale = 0, twtfemale = 0;
        public string strArray = string.Empty;
        public string strSentArray = string.Empty;
        public string strTwtArray = "[" + 0 + "]";
        public string strFBArray = string.Empty;
        public int tot_acc = 0;
        public int profileCount = 0;
       
        ILog logger = LogManager.GetLogger(typeof(Home));
      
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                UserRefRelationRepository objUserRefRelationRepository=new UserRefRelationRepository ();
                UserRepository userrepo = new UserRepository();
                Registration regObject = new Registration();
                TeamRepository objTeamRepo = new TeamRepository();
                NewsRepository objNewsRepo = new NewsRepository();
                AdsRepository objAdsRepo = new AdsRepository();
                UserActivation objUserActivation = new UserActivation();
                UserActivationRepository objUserActivationRepository = new UserActivationRepository();
                SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();
                GroupRepository objGroupRepository = new GroupRepository();
                TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                Team team=new Team ();


                SocioBoard.Domain.User user = (User)Session["LoggedUser"];

                try
                {

                    if (Session["GroupName"] == null)
                    {
                        Groups objGroupDetails = objGroupRepository.getGroupDetail(user.Id);
                        team = objTeamRepo.getAllDetails(objGroupDetails.Id, user.EmailId);
                        Session["GroupName"] = team;
                    }

                    else
                    {
                        team = (SocioBoard.Domain.Team)Session["GroupName"];
                    }

                }
                catch (Exception ex)
                {

                    logger.Error("Error: "+ex.Message);
                }
                Session["facebooktotalprofiles"] = null;

                if (user.Password == null)
                {
                    Response.Redirect("/Pricing.aspx");
                }
            
                #region Days remaining
                if (Session["days_remaining"] == null )
                {
                    if (user.PaymentStatus == "unpaid" && user.AccountType!="Free")
                    {
                        int daysremaining = (user.ExpiryDate.Date - DateTime.Now.Date).Days;
                        if (daysremaining < 0)
                        {
                            daysremaining = -1;
                        }
                        Session["days_remaining"] = daysremaining;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are using '" + user.AccountType + "' account only '" + daysremaining + "' days is remaining !');", true);
                        if (daysremaining <= -1)
                        {
                        }
                        else if (daysremaining == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your trial " + user.AccountType + " account will expire end of the day, please upgrade to paid plan.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your trial " + user.AccountType + " account will expire in " + daysremaining + " days, please upgrade to paid plan.');", true);
                        }
                    }
                }

                #endregion

                #region for You can use only 30 days as Unpaid User

                if (user.PaymentStatus.ToLower() == "unpaid" && user.AccountType != "Free")
                {
                    if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                    {
                        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can use only 30 days as Unpaid User !');", true);

                        Session["GreaterThan30Days"] = "GreaterThan30Days";

                        Response.Redirect("/Settings/Billing.aspx");
                    }
                }

                Session["GreaterThan30Days"] = null;
                #endregion

                if (!IsPostBack)
                {
                   try
                    {
                        if (user == null)
                        {

                            Response.Redirect("Default.aspx");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.StackTrace);
                    }


                    try
                    {
                        objUserActivation = objUserActivationRepository.GetUserActivationStatus(user.Id.ToString());
                    }
                    catch (Exception ex)
                    {
                        Session["objUserActivationException"] = "objUserActivationException";

                        Console.WriteLine(ex.Message);
                        logger.Error(ex.StackTrace);


                    }
               

                    #region Count Used Accounts
                    try
                    {
                        if (user.AccountType.ToString().ToLower() == AccountType.Deluxe.ToString().ToLower())
                            tot_acc = 50;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Standard.ToString().ToLower())
                            tot_acc = 10;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Premium.ToString().ToLower())
                            tot_acc = 20;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Free.ToString().ToLower())
                            tot_acc = 5;

                        else if (user.AccountType.ToString().ToLower() == AccountType.SocioBasic.ToString().ToLower())
                            tot_acc = 100;
                        else if (user.AccountType.ToString().ToLower() == AccountType.SocioStandard.ToString().ToLower())
                            tot_acc = 200;
                        else if (user.AccountType.ToString().ToLower() == AccountType.SocioPremium.ToString().ToLower())
                            tot_acc = 500;
                        else if (user.AccountType.ToString().ToLower() == AccountType.SocioDeluxe.ToString().ToLower())
                            tot_acc = 1000;


                        profileCount = objSocioRepo.getAllSocialProfilesOfUser(user.Id).Count;
                        Session["ProfileCount"] = profileCount;
                        Session["TotalAccount"] = tot_acc;

                        try
                        {

                           
                            Groups lstDetail = objGroupRepository.getGroupName(team.GroupId);
                            if (lstDetail.GroupName == "Socioboard")
                            {
                                usedAccount.InnerHtml = " using " + profileCount + " of " + tot_acc;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    #endregion




                    try
                    {
                        Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                        if (lstDetails.GroupName != "Socioboard")
                        {
                            expander.Attributes.CssStyle.Add("display", "none");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }


                    //this is used to check whether facebok account Already Exist
                    if (Session["alreadyexist"] != null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Profile is Already Added please add aother Account!');", true);
                        Session["alreadyexist"] = null;
                    }
                      if ( Session["alreadypageexist"] != null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This Page is Already Added please add aother Page!');", true);
                        Session["alreadypageexist"] = null;
                    }
                    



                    if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    {
                        try
                        {
                            userrepo.UpdateAccountType(user.Id, Request.QueryString["type"]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                            logger.Error(ex.StackTrace);
                        }
                    }

                    //acrossProfile.InnerHtml = "Across " + user.UserName + "'s Twitter and Facebook accounts";
                    teamMem.InnerHtml = "managing " + user.UserName;
                    try
                    {
                        News nws = objNewsRepo.getNewsForHome();
                        //divNews.InnerHtml = nws.NewsDetail;
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }
                    try
                    {
                        ArrayList lstads = objAdsRepo.getAdsForHome();
                        if (lstads.Count < 1)
                        {
                            if (user.PaymentStatus.ToUpper() == "PAID")
                            {

                                bindads.InnerHtml = "<img src=\"../Contents/img/admin/ads.png\"  alt=\"\" >";
                            }
                            else
                            {
                                #region ADS Script
                                bindads.InnerHtml = "<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>" +
                                                    "<!-- socioboard -->" +
                                                     "<ins class=\"adsbygoogle\"" +
                                                      "style=\"display:inline-block;width:250px;height:250px\"" +
                                                        "data-ad-client=\"ca-pub-7073257741073458\"" +
                                                    "data-ad-slot=\"9533254693\"></ins>" +
                                                    "<script>" +
                                                "(adsbygoogle = window.adsbygoogle || []).push({});" +
                                                "</script>";
                                #endregion
                            }
                        }


                        foreach (var item in lstads)
                        {
                            Array temp = (Array)item;
                            //imgAds.ImageUrl = temp.GetValue(2).ToString();
                            if (user.PaymentStatus.ToUpper() == "PAID")
                            {

                                bindads.InnerHtml = "<img src=\"" + temp.GetValue(2).ToString() + "\"  alt=\"\" style=\"width:246px;height:331px\">";
                            }
                            else
                            {
                                #region ADS Script
                                bindads.InnerHtml = "<script async src=\"//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js\"></script>" +
                                                    "<!-- socioboard -->" +
                                                     "<ins class=\"adsbygoogle\"" +
                                                      "style=\"display:inline-block;width:250px;height:250px\"" +
                                                        "data-ad-client=\"ca-pub-7073257741073458\"" +
                                                    "data-ad-slot=\"9533254693\"></ins>" +
                                                    "<script>" +
                                                "(adsbygoogle = window.adsbygoogle || []).push({});" +
                                                "</script>";
                                #endregion
                            }
                            break;
                            // ads.ImageUrl;
                        }



                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }
                    #region Team Member Count
                    try
                    {

                        GroupRepository grouprepo = new GroupRepository();
                        string groupsofhome = string.Empty;
                        List<Groups> lstgroups = grouprepo.getAllGroups(user.Id);
                        if (lstgroups.Count != 0)
                        {
                            foreach (Groups item in lstgroups)
                            {
                                groupsofhome += "<li><a href=\"../Settings/InviteMember.aspx?q=" + item.Id + "\"><img src=\"../Contents/img/groups_.png\" alt=\"\" style=\" margin-right:5px;\"> " + item.GroupName + "</a></li>";
                            }
                            getAllGroupsOnHome.InnerHtml = groupsofhome;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    #endregion

                    try
                    {
                        string strTeam = string.Empty;
                        List<Team> teams = objTeamRepo.getAllTeamsOfUser(user.Id,team.GroupId,user.EmailId);
                        foreach (Team item in teams)
                        {
                            strTeam += "<div class=\"userpictiny\"><a target=\"_blank\" href=\"#\">" +
                                        "<img width=\"48\" height=\"48\" title=\"" + item.FirstName + "\" alt=\"\" src=\"../Contents/img/blank_img.png\">" +
                                        "</a></div>";
                        }
                        team_member.InnerHtml = strTeam;

                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }

                    #region Add Fan Page
                    try
                    {
                        if (Session["fbSocial"] != null)
                        {
                            if (Session["fbSocial"] == "p")
                            {
                                FacebookAccount objFacebookAccount = (FacebookAccount)Session["fbpagedetail"];

                                //    string strpageUrl = "https://graph.facebook.com/" + objFacebookAccount.FacebookId + "/accounts";
                                // objFacebookUrlBuilder = (FacebookUrlBuilder)Session["FacebookInsightUser"];
                                //    string strData = objAuthentication.RequestUrl(strpageUrl, objFacebookAccount.Token);
                                //    JObject output = objWebRequest.FacebookRequest(strData, "Get");
                                FacebookClient fb = new FacebookClient();
                                fb.AccessToken = objFacebookAccount.AccessToken;
                                dynamic output = fb.Get("/me/accounts");
                                //  JArray data = (JArray)output["data"];
                                DataTable dtFbPage = new DataTable();
                                dtFbPage.Columns.Add("Email");
                                dtFbPage.Columns.Add("PageId");
                                dtFbPage.Columns.Add("PageName");
                                dtFbPage.Columns.Add("status");
                                dtFbPage.Columns.Add("customer_id");
                                string strPageDiv = string.Empty;
                                if (output != null)
                                {
                                    foreach (var item in output["data"])
                                    {
                                        if (item.category.ToString() != "Application")
                                        {
                                            strPageDiv += "<div><a id=\"A1\"  onclick=\"getInsights('" + item["id"].ToString() + "','" + item["name"].ToString() + "')\"><span>" + item["name"].ToString() + "</span> </a></div>";
                                            fbpage.InnerHtml = strPageDiv;
                                        }
                                    }
                                }
                                else
                                {
                                    strPageDiv += "<div>No Pages Found</div>";
                                }
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", " ShowDialogHome(false);", true);
                                Session["fbSocial"] = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.StackTrace);
                    }
                    #endregion

                    #region InsightsData
                    try
                    {
                        decimal malecount = 0, femalecount = 0, cnt = 0;

                        FacebookStatsRepository objfbStatsRepo = new FacebookStatsRepository();
                        double daysSub = (DateTime.Now - user.CreateDate).TotalDays;

                        int userdays;
                        if (daysSub > 0 && daysSub <= 1)
                        {
                            userdays = 1;
                        }
                        else
                        {
                            userdays = (int)daysSub;
                        }
                        ArrayList arrFbStats = objfbStatsRepo.getAllFacebookStatsOfUser(user.Id, userdays);
                        //ArrayList arrFbStats = objfbStatsRepo.getTotalFacebookStatsOfUser(user.Id);
                        
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
                        Session["twtGender"] = twtmale + "," + twtfemale;
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.Message.ToString());
                        logger.Error(Err.StackTrace);
                    }
                    //getgrphData();
                    // getNewFriends(7);
                  //  getNewFriends();
                  //  getNewFollowers();
                    #endregion

                    #region GetFollower

        
            try
            {
                String TwtProfileId = string.Empty;
               
                TwitterStatsRepository objtwtStatsRepo = new TwitterStatsRepository();

                List<TeamMemberProfile> objTeamMemberProfile = objTeamMemberProfileRepository.getTwtTeamMemberProfileData(team.Id);
                foreach (TeamMemberProfile item in objTeamMemberProfile)
                {
                    TwtProfileId += item.ProfileId + ',';

                }
                TwtProfileId = TwtProfileId.Substring(0, TwtProfileId.Length - 1);

                List<TwitterStats> arrTwtStats = objtwtStatsRepo.getAllAccountDetail(TwtProfileId);
              
                //strTwtArray = "[";
                int NewTweet_Count = 0;
                string TotalFollower = string.Empty;
                foreach (TwitterStats item in arrTwtStats)
                {                   
                    NewTweet_Count += item.FollowerCount;
                }
                if (NewTweet_Count >= 100000)
                {
                    TotalFollower = (System.Math.Round(((float)NewTweet_Count / 1000000), 2)) + "M";
                }
                else if (NewTweet_Count > 1000 && NewTweet_Count < 100000)
                { TotalFollower = (System.Math.Round(((float)NewTweet_Count / 1000), 2)) + "K"; }
                else
                {
                    TotalFollower = NewTweet_Count.ToString();
                }

                spanNewTweets.InnerHtml = TotalFollower;         
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace);
            }
       

                    #endregion

            #region GetFacebookFanPage

            try
            {
                String FbProfileId = string.Empty;

               FacebookStatsRepository objFacebookStatsRepository = new FacebookStatsRepository();

               List<TeamMemberProfile> objTeamMemberProfile = objTeamMemberProfileRepository.getTeamMemberProfileData(team.Id);
                foreach (TeamMemberProfile item in objTeamMemberProfile)
                {
                    FbProfileId += item.ProfileId + ',';

                }
                FbProfileId = FbProfileId.Substring(0, FbProfileId.Length - 1);

                List<FacebookStats> arrFbStats = objFacebookStatsRepository.getAllAccountDetail(FbProfileId);

                //strTwtArray = "[";
                int NewFbFan_Count = 0;
                string TotalFriends = string.Empty;
                foreach (FacebookStats item in arrFbStats)
                {
                    NewFbFan_Count += item.FanCount;
                }
                if (NewFbFan_Count >= 100000)
                {
                    TotalFriends = (System.Math.Round(((float)NewFbFan_Count / 1000000), 2)) + "M";
                }
                else if (NewFbFan_Count > 1000 && NewFbFan_Count < 100000)
                { TotalFriends = (System.Math.Round(((float)NewFbFan_Count / 1000), 2)) + "K"; }
                else
                {
                    TotalFriends = NewFbFan_Count.ToString();
                }

                spanFbFans.InnerHtml = TotalFriends;

            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace);
            }
       
            #endregion



            #region IncomingMessages
            try
                    {
                        FacebookFeedRepository fbFeedRepo = new FacebookFeedRepository();
                        int fbmessagescout = fbFeedRepo.countUnreadMessages(user.Id);
                        TwitterMessageRepository twtMsgRepo = new TwitterMessageRepository();
                        int twtcount = twtMsgRepo.getCountUnreadMessages(user.Id);
                        Session["CountMessages"] = fbmessagescout + twtcount;

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    #endregion
          
                    
                    
                    
                    
                    
            #region NewIncomingMessage

            try
            {
                String FbProfileId = string.Empty;
                String TwtProfileId = string.Empty;
                List<TeamMemberProfile> objTeamMemberProfile = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(team.Id);
                foreach (TeamMemberProfile item in objTeamMemberProfile)
                {
                    try
                    {
                        if (item.ProfileType == "facebook")
                        {
                            FbProfileId += item.ProfileId + ',';
                        }

                        else if (item.ProfileType == "twitter") 
                        {
                            TwtProfileId += item.ProfileId + ',';
                        }
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                        logger.Error(Err.StackTrace);
                    }

                }

                try
                {
                    FbProfileId = FbProfileId.Substring(0, FbProfileId.Length - 1);                                    
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                    logger.Error(Err.StackTrace);
                }

                try
                {
                    TwtProfileId = TwtProfileId.Substring(0, TwtProfileId.Length - 1);
                }
                catch (Exception Err)
                {
                    Console.Write(Err.StackTrace);
                    logger.Error(Err.StackTrace);
                }

                FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
                List<FacebookFeed> alstfb = objFacebookFeedRepository.getAllFeedDetail(FbProfileId);
             // FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
             // List<FacebookMessage> alstfb = objFacebookMessageRepository.getAllMessageDetail(FbProfileId);
                List<TwitterMessage> alstTwt = objtwttatsRepo.getAlltwtMessages(TwtProfileId);

              int TotalFbMsgCount = 0;
              int TotalTwtMsgCount = 0;

              try
              {
                  TotalFbMsgCount = alstfb.Count;
              }
              catch (Exception Err)
              {
                  Console.Write(Err.StackTrace);
                  logger.Error(Err.StackTrace);
              }

              try
              {
               
                  TotalTwtMsgCount = alstTwt.Count;
              }
              catch (Exception Err)
              {
                  Console.Write(Err.StackTrace);
                  logger.Error(Err.StackTrace);
              }




             spanIncoming.InnerHtml = (TotalFbMsgCount+TotalTwtMsgCount).ToString();

             string profileid = string.Empty;               
              ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
              foreach (TeamMemberProfile item in objTeamMemberProfile)
              {
                  profileid += item.ProfileId + ',';
              }

              profileid = profileid.Substring(0, profileid.Length - 1);

              spanSent.InnerHtml = objScheduledMessageRepository.getAllSentMessageDetails(profileid).Count().ToString();

                }
            
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
                logger.Error(Err.StackTrace);
            }

        }


           #endregion

             }
            
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }
      
    

        public void AuthenticateFacebook(object sender, EventArgs e)
        {
            try
            {
                GroupRepository objGroupRepository = new GroupRepository();
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);


                try
                {
                    int profilecount = (int)Session["ProfileCount"];
                    int totalaccount = (int)Session["TotalAccount"];

                    if (lstDetails.GroupName == "Socioboard")
                    {
                        if (profilecount < totalaccount)
                        {
                            Session["fbSocial"] = "a";
                            fb_account.HRef = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access,publish_actions,manage_pages&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                            //fb_account.HRef = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                            // fb_cont.HRef = fb_account.HRef;
                            Response.Redirect(fb_account.HRef);
                        }
                        else
                        {
                            // Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);

                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateTwitter(object sender, EventArgs e)
        {
            try
            {
                GroupRepository objGroupRepository = new GroupRepository();
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);


                try
                {
                    int profilecount = (int)Session["ProfileCount"];
                    int totalaccount = (int)Session["TotalAccount"];

                    if (lstDetails.GroupName == "Socioboard")
                    {
                        if (profilecount < totalaccount)
                        {
                            TwitterHelper twthelper = new TwitterHelper();
                            string twtredirecturl = twthelper.TwitterRedirect(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                            Response.Redirect(twtredirecturl);
                        }
                        else
                        {
                            //Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateLinkedin(object sender, EventArgs e)
        {
            try
            {
                GroupRepository objGroupRepository = new GroupRepository();
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);

                try
                {
                    int profilecount = (int)Session["ProfileCount"];
                    int totalaccount = (int)Session["TotalAccount"];
                    if (lstDetails.GroupName == "Socioboard")
                    {
                        if (profilecount < totalaccount)
                        {
                            oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                            string authLink = Linkedin_oauth.AuthorizationLinkGet();
                            Session["reuqestToken"] = Linkedin_oauth.Token;
                            Session["reuqestTokenSecret"] = Linkedin_oauth.TokenSecret;

                            this.LinkedInLink.HRef = "";
                            this.LinkedInLink.HRef = authLink;
                            Response.Redirect(authLink);
                        }
                        else
                        {
                            //Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateInstagram(object sender, EventArgs e)
        {
            try
            {
                GroupRepository objGroupRepository = new GroupRepository();
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);

                try
                {
                    int profilecount = (int)Session["ProfileCount"];
                    int totalaccount = (int)Session["TotalAccount"];
                    if (lstDetails.GroupName == "Socioboard")
                    {
                        if (profilecount < totalaccount)
                        {
                            GlobusInstagramLib.Authentication.ConfigurationIns config = new GlobusInstagramLib.Authentication.ConfigurationIns("https://instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "https://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
                            oAuthInstagram _api = oAuthInstagram.GetInstance(config);
                            InstagramConnect.HRef = _api.AuthGetUrl("likes+comments+basic+relationships");
                            Response.Redirect(_api.AuthGetUrl("likes+comments+basic+relationships"));
                        }
                        else
                        {
                            // Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateGooglePlus(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
                if (profilecount < totalaccount)
                {
                    oAuthToken objToken = new oAuthToken();
                    Response.Redirect(objToken.GetAutherizationLink("https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/plus.login"));
                }
                else
                {
                    //Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateGoogleAnalytics(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
                if (profilecount < totalaccount)
                {
                   
                    oAuthTokenGa obj = new oAuthTokenGa();
                    Response.Redirect(obj.GetAutherizationLinkGa("https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/analytics.readonly"));
                }
                else
                {
                    // Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateTumblr(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
                if (profilecount < totalaccount)
                {
                    oAuthTumbler requestHelper = new oAuthTumbler();
                    oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
                    oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
                    requestHelper.CallBackUrl = ConfigurationManager.AppSettings["TumblrCallBackURL"];
                    Response.Redirect(requestHelper.GetAuthorizationLink());
                }
                else
                {
                    // Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateYoutube(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
                if (profilecount < totalaccount)
                {
                    oAuthTokenYoutube objoAuthTokenYoutube = new oAuthTokenYoutube();

                    Response.Redirect("https://accounts.google.com/o/oauth2/auth?client_id=" + ConfigurationManager.AppSettings["YtconsumerKey"] + "&redirect_uri=" + ConfigurationManager.AppSettings["Ytredirect_uri"] + "&scope=https://www.googleapis.com/auth/youtube+https://www.googleapis.com/auth/youtube.readonly+https://www.googleapis.com/auth/youtubepartner+https://www.googleapis.com/auth/youtubepartner-channel-audit+https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me&response_type=code&access_type=offline");
                }
                else
                {
                    //Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void fbPage_connect(object sender, EventArgs e)
        {
            try
            {
                GroupRepository objGroupRepository = new GroupRepository();
                SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];
                Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);

                try
                {
                    int profilecount = (int)Session["ProfileCount"];
                    int totalaccount = (int)Session["TotalAccount"];
                    if (lstDetails.GroupName == "Socioboard")
                    {
                        if (profilecount < totalaccount)
                        {
                            try
                            {
                                Session["fbSocial"] = "p";
                                string fbpageconnectClick = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                                Response.Redirect(fbpageconnectClick);
                            }
                            catch (Exception Err)
                            {
                                Console.Write(Err.Message.ToString());
                                logger.Error(Err.StackTrace);
                            }
                        }
                        else
                        {
                            // Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");

                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Change the Plan to Add More Accounts');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public bool IsUserWorkingDaysValid(DateTime registrationDate)
        {
            bool isUserWorkingDaysValid = false;
            try
            {
                TimeSpan span = DateTime.Now.Subtract(registrationDate);
                int totalDays = (int)span.TotalDays;

                if (totalDays < 30)
                {
                    isUserWorkingDaysValid = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return isUserWorkingDaysValid;
        }

        int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            return (int)span.TotalDays;
        }


      

    



    }
}