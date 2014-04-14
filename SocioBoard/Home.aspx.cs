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


                UserRepository userrepo = new UserRepository();
                Registration regObject = new Registration();
                TeamRepository objTeamRepo = new TeamRepository();
                NewsRepository objNewsRepo = new NewsRepository();
                AdsRepository objAdsRepo = new AdsRepository();
                UserActivation objUserActivation = new UserActivation();
                UserActivationRepository objUserActivationRepository = new UserActivationRepository();
                SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                Session["facebooktotalprofiles"] = null;



                if (user.Password == null)
                {

                    Response.Redirect("/Pricing.aspx");
                }



                #region Days remaining
                if (Session["days_remaining"] == null)
                {
                    if (user.PaymentStatus == "unpaid")
                    {
                        int daysremaining = (user.ExpiryDate.Date - DateTime.Now.Date).Days;
                        if (daysremaining < 0)
                        {
                            daysremaining = 0;
                        }
                        Session["days_remaining"] = daysremaining;
                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are using '" + user.AccountType + "' account only '" + daysremaining + "' days is remaining !');", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Your trial " + user.AccountType + " account will expire in " + daysremaining + " days, please upgrade to paid plan.');", true);
                    }
                }

                #endregion



                #region for You can use only 30 days as Unpaid User

                if (user.PaymentStatus.ToLower() == "unpaid")
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



                    //#region check user Activation

                    //try
                    //{
                    //    if (objUserActivation != null)
                    //    {
                    //        if (objUserActivation.ActivationStatus == "0")
                    //        {
                    //            if (Request.QueryString["stat"] == "activate")
                    //            {
                    //                if (Request.QueryString["id"] != null)
                    //                {
                    //                    //objUserActivation = objUserActivationRepository.GetUserActivationStatusbyid(Request.QueryString["id"].ToString());
                    //                    if (objUserActivation.UserId.ToString() == Request.QueryString["id"].ToString())
                    //                    {
                    //                        objUserActivation.Id = objUserActivation.Id; //Guid.Parse(Request.QueryString["id"]);
                    //                        objUserActivation.UserId = Guid.Parse(Request.QueryString["id"]);// objUserActivation.UserId;
                    //                        objUserActivation.ActivationStatus = "1";
                    //                        UserActivationRepository.Update(objUserActivation);



                    //                    }
                    //                    else
                    //                    {
                    //                        Session["ActivationError"] = "Wrong Activation Link please contact Admin!";
                    //                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Wrong Activation Link please contact Admin!');", true);
                    //                        //Response.Redirect("ActivationLink.aspx");
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    Session["ActivationError"] = "Wrong Activation Link please contact Admin!";
                    //                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Wrong Activation Link please contact Admin!');", true);
                    //                    //Response.Redirect("ActivationLink.aspx");
                    //                }

                    //            }
                    //            else
                    //            {
                    //               // Response.Redirect("ActivationLink.aspx");
                    //            }


                    //        }
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message);
                    //    logger.Error(ex.StackTrace);
                    //}
                    //#endregion



                    #region Count Used Accounts
                    try
                    {
                        if (user.AccountType.ToString().ToLower() == AccountType.Deluxe.ToString().ToLower())
                            tot_acc = 20;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Standard.ToString().ToLower())
                            tot_acc = 10;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Premium.ToString().ToLower())
                            tot_acc = 50;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Free.ToString().ToLower())
                            tot_acc = 5;
                        profileCount = objSocioRepo.getAllSocialProfilesOfUser(user.Id).Count;
                        Session["ProfileCount"] = profileCount;
                        Session["TotalAccount"] = tot_acc;
                        usedAccount.InnerHtml = " using " + profileCount + " of " + tot_acc;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    #endregion




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

                    acrossProfile.InnerHtml = "Across " + user.UserName + "'s Twitter and Facebook accounts";
                    teamMem.InnerHtml = "managing " + user.UserName;
                    try
                    {
                        News nws = objNewsRepo.getNewsForHome();
                        divNews.InnerHtml = nws.NewsDetail;
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
                        List<Team> team = objTeamRepo.getAllTeamsOfUser(user.Id);
                        foreach (Team item in team)
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
                    getgrphData();
                    getNewFriends(7);
                    getNewFollowers(7);
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


                }
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
        }
        public void getgrphData()
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                FacebookAccountRepository objfb = new FacebookAccountRepository();
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                ArrayList alstfb = objfb.getFbMessageStatsHome(user.Id, 7);
                ArrayList alstTwt = objtwttatsRepo.gettwtMessageStatsHome(user.Id, 7);
                strArray = "[";
                if (alstfb.Count > 0 && alstTwt.Count > 0)
                {
                    int imcomMsg_Clounter = 0;
                    //int alstCount=0;
                    //if (alstfb.Count < alstTwt.Count)
                    //    alstCount = alstfb.Count;
                    //else
                    //    alstCount = alstTwt.Count;
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
                        imcomMsg_Clounter += (int.Parse(strTwtCnt) + int.Parse(strFbCnt));
                    }
                    spanIncoming.InnerHtml = (imcomMsg_Clounter).ToString();
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        strArray = strArray + "[0,0],";
                    }
                }
                strArray = strArray.Substring(0, strArray.Length - 1) + "]";

                ArrayList alstTwtFeed = objtwttatsRepo.gettwtFeedsStatsHome(user.Id, 7);
                ArrayList alstFBFeed = objfb.getFbFeedsStatsHome(user.Id);
                strSentArray = "[";

                if (alstFBFeed.Count > 0 && alstTwtFeed.Count > 0)
                {
                    int SentMsg_Counter = 0;
                    int alstSentCount = 0;
                    if (alstTwtFeed.Count < alstFBFeed.Count)
                        alstSentCount = alstTwtFeed.Count;
                    else
                        alstSentCount = alstFBFeed.Count;
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
                        // spanSent.InnerHtml = (int.Parse(strFbFeedCnt) + int.Parse(strTwtFeedCnt)).ToString();
                        SentMsg_Counter += (int.Parse(strFbFeedCnt) + int.Parse(strTwtFeedCnt));
                    }
                    spanSent.InnerHtml = (SentMsg_Counter).ToString();
                }
                else
                {
                    for (int i = 0; i < 7; i++)
                    {
                        strSentArray = strSentArray + "[0,0],";
                    }
                }
                strSentArray = strSentArray.Substring(0, strSentArray.Length - 1) + "]";

            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
                logger.Error(Err.StackTrace);
            }
        }

        public void getNewFriends(int days)
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                FacebookStatsRepository objfbStatsRepo = new FacebookStatsRepository();
                ArrayList arrFbStats = objfbStatsRepo.getAllFacebookStatsOfUser(user.Id, days);

                // Get facebook page like ...
                FacebookAccountRepository ObjAcFbAccount = new FacebookAccountRepository();
                int TotalLikes = ObjAcFbAccount.getPagelikebyUserId(user.Id);

                strFBArray = "[";
                int intdays = 1;
                foreach (var item in arrFbStats)
                {
                    Array temp = (Array)item;
                    strFBArray += (int.Parse(temp.GetValue(3).ToString()) + int.Parse(temp.GetValue(4).ToString())) + ",";
                    //spanFbFans.InnerHtml = (int.Parse(temp.GetValue(3).ToString()) + int.Parse(temp.GetValue(4).ToString())).ToString();
                    spanFbFans.InnerHtml = (TotalLikes).ToString();
                    intdays++;
                }
                if (intdays < 7)
                {
                    for (int i = 0; i < 7 - arrFbStats.Count; i++)
                    {
                        strFBArray = strFBArray + "0,";
                    }
                }
                strFBArray = strFBArray.Substring(0, strFBArray.Length - 1);
                strFBArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace);
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
                int NewTweet_Count = 0;
                foreach (var item in arrTwtStats)
                {
                    Array temp = (Array)item;
                    strTwtArray += (temp.GetValue(4)) + ",";
                    //spanNewTweets.InnerHtml = temp.GetValue(4).ToString();
                    NewTweet_Count += Convert.ToInt16(temp.GetValue(4));
                }
                spanNewTweets.InnerHtml = NewTweet_Count.ToString();

                if (arrTwtStats.Count > 0)
                    strTwtArray = strTwtArray.Substring(0, strTwtArray.Length - 1);
                if (arrTwtStats.Count < 7)
                {
                    for (int i = 0; i < 7 - arrTwtStats.Count; i++)
                    {
                        strTwtArray = strTwtArray + "0,";
                    }
                }
                strTwtArray = strTwtArray.Substring(0, strTwtArray.Length - 1) + "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
                logger.Error(Err.StackTrace);
            }
        }

        public void AuthenticateFacebook(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateTwitter(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateLinkedin(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
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
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AuthenticateInstagram(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
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

        public void fbPage_connect(object sender, EventArgs e)
        {
            try
            {
                int profilecount = (int)Session["ProfileCount"];
                int totalaccount = (int)Session["TotalAccount"];
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