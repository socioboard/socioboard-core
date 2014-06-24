using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using GlobusGooglePlusLib.Authentication;
using GlobusInstagramLib.Authentication;
using GlobusLinkedinLib.Authentication;
using SocioBoard.Helper;
using System.Configuration;
using log4net;
using Facebook;
using System.Data;
using System.Collections;


namespace letTalkNew.MasterPage
{
    public partial class Home : System.Web.UI.MasterPage
    {
        public int tot_acc = 0;
        public int profileCount = 0;
        ILog logger = LogManager.GetLogger(typeof(Home));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["LoggedUser"] == null)
                    {
                        Response.Redirect("/Default.aspx");
                        return;
                    }

                    SocioBoard.Domain.User user = (User)Session["LoggedUser"];
                    SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();
                    profileName.InnerHtml = user.UserName;
                    if(user.ProfileUrl!=null)
                        profileImg.Src= user.ProfileUrl;
                    else
                        profileImg.Src="~/Contents/img/blank_user.png";
                    try
                    {
                        if (Session["IncomingTasks"] != null)
                        {
                            incom_tasks.InnerHtml = "You have " + Convert.ToString((int)Session["IncomingTasks"]) + " Tasks";
                           // incom_tasks.InnerHtml = Convert.ToString((int)Session["IncomingTasks"]);
                        }
                        else
                        {
                            TaskRepository taskRepo = new TaskRepository();
                            ArrayList alst = taskRepo.getAllIncompleteTasksOfUser(user.Id);
                            incom_tasks.InnerHtml = "You have " + alst.Count + " Tasks";
                            Session["IncomingTasks"] = alst.Count;
                        }
                    }
                    catch (Exception es)
                    {
                        logger.Error(es.Message);
                        Console.WriteLine(es.Message);
                    }
                    try
                    {
                        if (Session["CountMessages"] != null)
                        {
                            incom_messages.InnerHtml = "You have " +  Convert.ToString((int)Session["CountMessages"]) + " Messages";
                            //incomMessages.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                        }
                        else
                        {
                            incom_messages.InnerHtml = "You have 0 Messages";
                           // incomMessages.InnerHtml = "0";
                        }
                    }
                    catch (Exception sx)
                    {
                        logger.Error(sx.Message);
                        Console.WriteLine(sx.Message);
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
                    #region Count Used Accounts
                    try
                    {
                        if (user.AccountType.ToString().ToLower() == AccountType.Deluxe.ToString().ToLower())
                            tot_acc = 20;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Standard.ToString().ToLower())
                            tot_acc = 10;
                        else if (user.AccountType.ToString().ToLower() == AccountType.Premium.ToString().ToLower())
                            tot_acc = 50;
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
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void AuthenticateTwitter(object sender, EventArgs e)
        {
            try
            {
                //  int profilecount = (int)Session["ProfileCount"];
                //   int totalaccount = (int)Session["TotalAccount"];
                //   if (profilecount < totalaccount)
                {
                    TwitterHelper twthelper = new TwitterHelper();
                    string twtredirecturl = twthelper.TwitterRedirect(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                    Response.Redirect(twtredirecturl);
                }
                //  else
                {
                    //        Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                //  int profilecount = (int)Session["ProfileCount"];
                // int totalaccount = (int)Session["TotalAccount"];
                // if (profilecount < totalaccount)
                {
                    oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                    string authLink = Linkedin_oauth.AuthorizationLinkGet();
                    Session["reuqestToken"] = Linkedin_oauth.Token;
                    Session["reuqestTokenSecret"] = Linkedin_oauth.TokenSecret;
                    Response.Redirect(authLink);
                }
                //  else
                {
                    //      Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                //   int profilecount = (int)Session["ProfileCount"];
                //   int totalaccount = (int)Session["TotalAccount"];
                //   if (profilecount < totalaccount)
                {
                    GlobusInstagramLib.Authentication.ConfigurationIns config = new GlobusInstagramLib.Authentication.ConfigurationIns("https://instagram.com/oauth/authorize/", ConfigurationManager.AppSettings["InstagramClientKey"].ToString(), ConfigurationManager.AppSettings["InstagramClientSec"].ToString(), ConfigurationManager.AppSettings["InstagramCallBackURL"].ToString(), "https://api.instagram.com/oauth/access_token", "https://api.instagram.com/v1/", "");
                    oAuthInstagram _api = oAuthInstagram.GetInstance(config);
                    Response.Redirect(_api.AuthGetUrl("likes+comments+basic+relationships"));
                }
                //    else
                {
                    //  Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                //  int profilecount = (int)Session["ProfileCount"];
                //  int totalaccount = (int)Session["TotalAccount"];
                // if (profilecount < totalaccount)
                {

                    oAuthToken objToken = new oAuthToken();
                    Response.Redirect(objToken.GetAutherizationLink("https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/plus.login"));
                }
                //   else
                {
                    //      Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                // int profilecount = (int)Session["ProfileCount"];
                // int totalaccount = (int)Session["TotalAccount"];
                // if (profilecount < totalaccount)
                {
                    oAuthTokenGa obj = new oAuthTokenGa();
                    Response.Redirect(obj.GetAutherizationLinkGa("https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/analytics.readonly"));
                }
                //   else
                {
                    //      Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        


        public void AuthenticateFacebook(object sender, EventArgs e)
        {
            try
            {
                //      int profilecount = (int)Session["ProfileCount"];
                //     int totalaccount = (int)Session["TotalAccount"];
                //     if (profilecount < totalaccount)
                {
                    Session["fbSocial"] = "a";
                    string fb = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access,create_event,rsvp_event,user_events,friends_events&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                    // fb_cont.HRef = fb_account.HRef;
                    Response.Redirect(fb);
                }
                //     else
                {
                    //         Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                //  int profilecount = (int)Session["ProfileCount"];
                //  int totalaccount = (int)Session["TotalAccount"];
                //  if (profilecount < totalaccount)
                {
                    Session["fbSocial"] = "p";
                    string fbpageconnectClick = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                    Response.Redirect(fbpageconnectClick);
                }
                //     else
                {
                    //       Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message.ToString());
            }
        }

        protected void Page_Render(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterForEventValidation("fb_account");
        } 
    }
}