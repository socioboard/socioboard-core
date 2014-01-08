using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using GlobusLinkedinLib.Authentication;
using GlobusGooglePlusLib.Authentication;
using GlobusTwitterLib.Authentication;
using GlobusInstagramLib.Authentication;
using SocioBoard.Model;
using SocioBoard.Helper;
using System.Collections;
using log4net;

namespace SocialSuitePro.MasterPage
{
    public partial class Site : System.Web.UI.MasterPage
    {
        ILog logger = LogManager.GetLogger(typeof(Site));
        SocialProfilesRepository objSocioRepo = new SocialProfilesRepository();
        public int tot_acc = 0;
        public int profileCount = 0;
        string Datetime = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];

            if (!IsPostBack)
            {

                if (user == null)
                    Response.Redirect("/Default.aspx");
                else
                {

                    try
                    {
                        if (Session["IncomingTasks"] != null)
                        {
                            incom_tasks.InnerHtml = Convert.ToString((int)Session["IncomingTasks"]);
                            incomTasks.InnerHtml = Convert.ToString((int)Session["IncomingTasks"]);
                        }
                        else
                        {
                            TaskRepository taskRepo = new TaskRepository();
                            ArrayList alst = taskRepo.getAllIncompleteTasksOfUser(user.Id);
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
                            incom_messages.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                            incomMessages.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                        }
                        else
                        {
                            incom_messages.InnerHtml = "0";
                            incomMessages.InnerHtml = "0";
                        }
                    }
                    catch (Exception sx)
                    {
                        logger.Error(sx.Message);
                        Console.WriteLine(sx.Message);
                    }

                    usernm.InnerHtml = "Hello, <a href=\"../Settings/PersonalSettings.aspx\"> " + user.UserName + "</a> ";
                    if (!string.IsNullOrEmpty(user.ProfileUrl))
                    {
                       // Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                        userimg.InnerHtml = "<a href=\"../Settings/PersonalSettings.aspx\"><img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" height=\"100\" width=\"100\"/></a></a>";
                        //userinf.InnerHtml = Datetime;
                    //{ 
                    //    userimg.InnerHtml = "<a href=\"../Settings/PersonalSettings.aspx\"><img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" height=\"100\" width=\"100\"/></a></a>";
                        if (user.TimeZone != null)
                        {
                            Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                            userinf.InnerHtml = Datetime;
                        }
                        if (user.TimeZone == null)
                        {
                            Datetime = DateTime.Now.ToString();
                            userinf.InnerHtml = Datetime;
                        }
                    }
                    else
                    {
                        //Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                        userimg.InnerHtml = "<a href=\"../Settings/PersonalSettings.aspx\"><img id=\"loggeduserimg\" src=\"../Contents/img/blank_img.png\" alt=\"" + user.UserName + "\"/></a>";

                        userinf.InnerHtml = Datetime;
                        if (user.TimeZone != null)
                        {
                            Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                            userinf.InnerHtml = Datetime;
                        }
                        if (user.TimeZone == null)
                        {
                            Datetime = DateTime.Now.ToString();
                            userinf.InnerHtml = Datetime;
                        }
                    }

                    try
                    {

                        GroupRepository grouprepo = new GroupRepository();
                        List<Groups> lstgroups = grouprepo.getAllGroups(user.Id);
                        string totgroups = string.Empty;
                        if (lstgroups.Count != 0)
                        {
                            foreach (Groups item in lstgroups)
                            {
                                totgroups += "<li><a href=\"../Settings/InviteMember.aspx?q=" + item.Id + "\" id=\"group_" + item.Id + "\"><img src=\"../Contents/img/groups_.png\"  alt=\"\"  style=\" margin-right:5px;\"/>" + item.GroupName + "</a></li>";
                            }
                            inviteRedirect.InnerHtml = totgroups;
                        }
                        if (user.AccountType == AccountType.Deluxe.ToString().ToLower())
                            tot_acc = 10;
                        else if (user.AccountType == AccountType.Standard.ToString().ToLower())
                            tot_acc = 20;
                        else if (user.AccountType == AccountType.Premium.ToString().ToLower())
                            tot_acc = 50;
                        profileCount = objSocioRepo.getAllSocialProfilesOfUser(user.Id).Count;

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        Console.WriteLine(ex.Message);
                    }
                }
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
                    string fb = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access,user_groups&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                    // fb_cont.HRef = fb_account.HRef;
                    Response.Redirect(fb);
                }
                else
                {
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                    Response.Redirect(authLink);
                }
                else
                {
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                    Response.Redirect(_api.AuthGetUrl("likes+comments+basic+relationships"));
                }
                else
                {
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
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
                    Session["fbSocial"] = "p";
                    string fbpageconnectClick = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                    Response.Redirect(fbpageconnectClick);
                }
                else
                {
                    Response.Write("<script>SimpleMessageAlert('Change the Plan to Add More Accounts');</script>");
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message.ToString());
            }
        }

    }
}