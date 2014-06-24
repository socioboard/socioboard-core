using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using System.Web.Services;
using SocioBoard.Model;
using System.Collections;
using GlobusTwitterLib.Authentication;
using GlobusLinkedinLib.Authentication;
using GlobusInstagramLib.Authentication;
using System.Data;
using Newtonsoft.Json.Linq;
using SocioBoard.Helper;
using Facebook;

namespace SocioBoard
{
    public partial class Home : System.Web.UI.Page
    {
        public int male = 0, female = 0;
        public decimal twtmale = 0, twtfemale = 0;
        public string strArray = string.Empty;
        public string strSentArray = string.Empty;
        public string strTwtArray = "[" + 0 + "]";
        public string strFBArray = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUser"] == null)
                Response.Redirect("/Login.aspx");
 
            if (!IsPostBack)
            {              
                User user = (User)Session["LoggedUser"];
                profiles.InnerText = "Connected To "+user.UserName+ "";

                #region Home 
                SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
                List<SocialProfile> lstsocio = socioprofilerepo.getAllSocialProfilesOfUser(user.Id);
                usedAccount.InnerHtml = lstsocio.Count + " of 20"; 
                #endregion

                #region Team
             
                try
                {

                    //GroupRepository grouprepo = new GroupRepository();
                    //string groupsofhome = string.Empty;
                    //List<Groups> lstgroups = grouprepo.getAllGroups(user.Id);
                    
                }
                catch
                {
                }
                

                #endregion


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
                }
                #endregion


                try
                {
                    decimal malecount = 0, femalecount = 0, cnt = 0;

                    FacebookStatsRepository objfbStatsRepo = new FacebookStatsRepository();
                    double daysSub = (DateTime.Now - user.CreateDate).TotalDays;
                    int userdays = (int)daysSub;
                    ArrayList arrFbStats = objfbStatsRepo.getAllFacebookStatsOfUser(user.Id, userdays);
                    Random rNum = new Random();
                    foreach (FacebookStats item in arrFbStats)
                    {
                        cnt += item.FemaleCount + item.MaleCount;
                        malecount += item.MaleCount;
                        femalecount += item.FemaleCount;
                    }
                    try
                    {
                        decimal mc = (malecount / cnt) * 100;
                        male = Convert.ToInt16(mc);
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    try
                    {
                        decimal fc = (femalecount / cnt) * 100;
                        female = Convert.ToInt16(fc);
                    }
                    catch (Exception Err)
                    {
                        Console.Write(Err.StackTrace);
                    }
                    twtmale = rNum.Next(100);
                    twtfemale = 100 - twtmale;
                    Session["twtGender"] = male + "," + female;
                }
                catch (Exception Err)
                {
                    Console.Write(Err.Message.ToString());
                }
                getgrphData();

                getNewFriends(7);
                getNewFollowers(7);
            }
        }


        public void AuthenticateFacebook(object sender, EventArgs e)
        {
            fb_account.HRef = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,user_groups,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
           // fb_cont.HRef = fb_account.HRef;
            Response.Redirect(fb_account.HRef);
      
        }

        public void AuthenticateTwitter(object sender, EventArgs e)
        {

            TwitterHelper twthelper = new TwitterHelper();
            string twtredirecturl = twthelper.TwitterRedirect(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
            Response.Redirect(twtredirecturl);

            oAuthTwitter OAuth = new oAuthTwitter();

            if (Request["oauth_token"] == null)
            {
                OAuth.AccessToken = string.Empty;
                OAuth.AccessTokenSecret = string.Empty;
                OAuth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"].ToString();
                this.TwitterOAuth.HRef = OAuth.AuthorizationLinkGet();
                Response.Redirect(OAuth.AuthorizationLinkGet());
            }

        
        }
        public void AuthenticateLinkedin(object sender, EventArgs e)
        {
            oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
            string authLink = Linkedin_oauth.AuthorizationLinkGet();
            Session["reuqestToken"] = Linkedin_oauth.Token;
            Session["reuqestTokenSecret"] = Linkedin_oauth.TokenSecret;

            this.LinkedInLink.HRef = "";
            this.LinkedInLink.HRef = authLink;
            Response.Redirect(authLink);
        }

        public void AuthenticateInstagram(object sender, EventArgs e)
        {
            GlobusInstagramLib.Authentication.ConfigurationIns config = new GlobusInstagramLib.Authentication.ConfigurationIns("https://instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "https://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");

            oAuthInstagram _api = oAuthInstagram.GetInstance(config);

            InstagramConnect.HRef = _api.AuthGetUrl("likes+comments+basic+relationships");
            Response.Redirect(_api.AuthGetUrl("likes+comments+basic+relationships"));
        }
        public void getgrphData()
        {
            try
            {
                SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                FacebookAccountRepository objfb = new FacebookAccountRepository();
                TwitterMessageRepository objtwttatsRepo = new TwitterMessageRepository();
                ArrayList alstfb = objfb.getFbMessageStats(user.Id,15);
                ArrayList alstTwt = objtwttatsRepo.gettwtMessageStats(user.Id, 15);
                strArray = "[";
                if (alstfb.Count > 0 && alstTwt.Count > 0)
                {
                    int alstCount = 0;
                    if (alstfb.Count < alstTwt.Count)
                        alstCount = alstfb.Count;
                    else
                        alstCount = alstTwt.Count;
                    for (int i = 0; i < alstCount; i++)
                    {
                        strArray = strArray + "[" + alstfb[i].ToString() + "," + alstTwt[i].ToString() + "]";
                    }
                }
                strArray += "]";

                ArrayList alstTwtFeed = objtwttatsRepo.gettwtFeedsStatsHome(user.Id,7);
                ArrayList alstFBFeed = objfb.getFbFeedsStatsHome(user.Id);
                strSentArray = "[";

                if (alstFBFeed.Count > 0 && alstTwtFeed.Count > 0)
                {
                    int alstSentCount = 0;
                    if (alstTwtFeed.Count < alstFBFeed.Count)
                        alstSentCount = alstTwtFeed.Count;
                    else
                        alstSentCount = alstFBFeed.Count;
                    for (int i = 0; i < alstSentCount; i++)
                    {
                        strSentArray = strSentArray + "[" + alstFBFeed[i].ToString() + "," + alstTwtFeed[i].ToString() + "]";
                    }
                }
                strSentArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
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
                foreach (FacebookStats item in arrFbStats)
                {
                    strFBArray += (item.MaleCount + item.FemaleCount) + ",";
                    intdays++;
                }
                strFBArray = strFBArray.Substring(0, strFBArray.Length - 1);
                strFBArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
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
                foreach (TwitterStats item in arrTwtStats)
                {
                    strTwtArray += (item.FollowerCount) + ",";
                }
                if (arrTwtStats.Count > 0)
                    strTwtArray = strTwtArray.Substring(0, strTwtArray.Length - 1);
                if (arrTwtStats.Count < 7)
                {
                    for (int i = 0; i < 7 - arrTwtStats.Count; i++)
                    {
                        strTwtArray = strTwtArray + "0,";
                    }
                }
                strTwtArray += "]";
            }
            catch (Exception Err)
            {
                Console.Write(Err.Message.ToString());
            }
        }
        public void fbPage_connect(object sender, EventArgs e)
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
            }
        }
    }
}