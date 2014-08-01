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
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
            TeamRepository objTeamRepository = new TeamRepository();
            GroupRepository objGroupRepository = new GroupRepository();

            try
            {
                User user = (User)Session["LoggedUser"];
               // SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)Session["GroupName"];

                #region check user Activation

                UserRepository objUserRepository = new UserRepository();

                try
                {
                    if (user != null)
                    {
                        if (user.ActivationStatus == "0" || user.ActivationStatus ==null)
                        {
                            actdiv.InnerHtml = "<marquee behavior=\"scroll\" direction=\"left\"><span >Your account is not yet activated.Check your E-mail to activate your account.</span><a id=\"resendmail\" uid=\""+user.Id+"\" href=\"#\">Resend Mail</a></marquee>";
                            if (Request.QueryString["stat"] == "activate")
                            {
                                if (Request.QueryString["id"] != null)
                                {
                                    //objUserActivation = objUserActivationRepository.GetUserActivationStatusbyid(Request.QueryString["id"].ToString());
                                    if (user.Id.ToString() == Request.QueryString["id"].ToString())
                                    {
                                        user.Id = user.Id; //Guid.Parse(Request.QueryString["id"]);
                                        //objUserActivation.UserId = Guid.Parse(Request.QueryString["id"]);// objUserActivation.UserId;
                                        user.ActivationStatus = "1";
                                        //UserActivationRepository.Update(objUserActivation);

                                        int res = objUserRepository.UpdateActivationStatusByUserId(user);

                                        actdiv.Attributes.CssStyle.Add("display", "none");
                                        Console.WriteLine("before");
                                        #region to check/update user Reference Relation
                                        IsUserReferenceActivated(Request.QueryString["id"].ToString());
                                        Console.WriteLine("after");
                                        #endregion
                                       
                                       
                                       


                                    }
                                    else
                                    {
                                        Session["ActivationError"] = "Wrong Activation Link please contact Admin!";
                                        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Wrong Activation Link please contact Admin!');", true);
                                        //Response.Redirect("ActivationLink.aspx");


                                    }
                                }
                                else
                                {
                                    Session["ActivationError"] = "Wrong Activation Link please contact Admin!";
                                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Wrong Activation Link please contact Admin!');", true);
                                    //Response.Redirect("ActivationLink.aspx");
                                }

                            }
                            else
                            {
                                // Response.Redirect("ActivationLink.aspx");
                            }


                        }

                        if (user.ActivationStatus == "1")
                        {
                            actdiv.Attributes.CssStyle.Add("display", "none");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    logger.Error(ex.StackTrace);
                }
                #endregion

                if (!IsPostBack)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", " logout();", true);

                    if (user == null)
                        Response.Redirect("/Default.aspx");
                    else
                    {


                        #region for You can use only 30 days as Unpaid User

                        if (user.PaymentStatus.ToLower() == "unpaid")
                        {
                            if (!SBUtils.IsUserWorkingDaysValid(user.ExpiryDate))
                            {
                               // cmposecontainer.Attributes.Add("display", "none");
                                cmposecontainer.Attributes.CssStyle.Add("display", "none");
                                //topbtn.Attributes.CssStyle.Add("display", "none");
                                cmposecontainer.Attributes.CssStyle.Add("display", "none");
                                searchcontainer.Attributes.CssStyle.Add("display", "none");
                            }
                        }
                        #endregion




                        try
                        {
                            if (Session["IncomingTasks"] != null)
                            {
                                //incom_tasks.InnerHtml = Convert.ToString((int)Session["IncomingTasks"]);
                                //incomTasks.InnerHtml = Convert.ToString((int)Session["IncomingTasks"]);
                            }
                            else
                            {
                                //TaskRepository taskRepo = new TaskRepository();
                                //ArrayList alst = taskRepo.getAllIncompleteTasksOfUser(user.Id,team.GroupId);
                                //Session["IncomingTasks"] = alst.Count;
                                //incom_tasks.InnerHtml = Convert.ToString(alst.Count);
                                //incomTasks.InnerHtml = Convert.ToString(alst.Count);
                            }
                        }
                        catch (Exception es)
                        {
                            logger.Error(es.Message);
                            Console.WriteLine(es.Message);
                        }
                        try
                        {

                            FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
                            int alstfeedfb = objFacebookFeedRepository.countUnreadMessages(user.Id);
                            TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
                            int alstMsgtwt = objTwitterMessageRepository.getCountUnreadMessages(user.Id);
                            incom_messages.InnerHtml = (alstfeedfb + alstMsgtwt).ToString();


                            if (Session["CountMessages"] != null)
                            {
                                //incom_messages.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                                //incomMessages.InnerHtml = Convert.ToString((int)Session["CountMessages"]);
                            }
                            else
                            {
                                //incom_messages.InnerHtml = "0";
                                //incomMessages.InnerHtml = "0";
                            }
                        }
                        catch (Exception sx)
                        {
                            logger.Error(sx.Message);
                            Console.WriteLine(sx.Message);
                        }

                      
//======================================================================================================                                      

                        try
                        {

                            ArrayList totalAccuount = objTeamRepository.getAllAccountUser(user.EmailId,user.Id);
                            if (totalAccuount.Count != 0)
                            {
                                string drodownselectedvalue = string.Empty;
                                try
                                {
                                    foreach (Guid item in totalAccuount)
                                    {
                                        Guid GroupIde = (Guid)item;

                                        List<Groups> GetData = objGroupRepository.getAllGroupsDetail(GroupIde);
                                        if (GetData.Count != 0)
                                        {
                                            string val = string.Empty;
                                            foreach (var items in GetData)
                                            {
                                                try
                                                {
                                                    if (items.GroupName == "Socioboard")
                                                    {
                                                        drodownselectedvalue = items.Id.ToString();
                                                    }
                                                    groupsselection.Items.Add(new ListItem((string)items.GroupName, items.Id.ToString()));
                                                }
                                                catch { }
                                            }

                                        }

                                    }

                                    if (Session["groupcheck"] == null)
                                    {
                                        int i = 0;
                                        groupsselection.SelectedIndex = groupsselection.Items.IndexOf(groupsselection.Items.FindByText("Socioboard"));
                                        //groupsselection.SelectedIndex = groupsselection.Items.IndexOf(groupsselection.Items.FindByValue("Socioboard"));
                                        //ListItem li =  groupsselection.Items.FindByText(drodownselectedvalue);
                                        //li.Selected = true;
                                        Session["groupcheck"] = groupsselection.Items.FindByText("Socioboard").Value;
                                    }
                                    else
                                    {
                                        groupsselection.SelectedIndex = groupsselection.Items.IndexOf(groupsselection.Items.FindByValue((string)Session["groupcheck"]));
                                        //groupsselection.se.SelectedIndex = Session["groupcheck"];
                                    }

                                    //if (Session["groupcheck"] == null)
                                    //{

                                    //    groupsselection.SelectedValue = (drodownselectedvalue);
                                    //    Session["groupcheck"] = groupsselection.SelectedValue;
                                    //}
                                    //else
                                    //{
                                    //    groupsselection.SelectedIndex = Session["groupcheck"];
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    logger.Error("Error : " + ex.Message);
                                    logger.Error("Error : " + ex.StackTrace);
                                }
                            }




                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            logger.Error("Error : " + ex.Message);
                            logger.Error("Error : " + ex.StackTrace);
                        }

//=====================================================================================================================================                                            
                        usernm.InnerHtml = "Hello, " + user.UserName + " ";
                        if (!string.IsNullOrEmpty(user.ProfileUrl))
                        {
                            // Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                            //userimg.InnerHtml = "<a href=\"../Settings/PersonalSettings.aspx\"><img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" /></a></a>";
                            userimg.InnerHtml = "<img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" />";
                            userimg.InnerHtml = "<img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" />";
                            //userinf.InnerHtml = Datetime;
                            //{ 
                                //userimg.InnerHtml = "<a href=\"../Settings/PersonalSettings.aspx\"><img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" height=\"100\" width=\"100\"/></a></a>";
                                userimg.InnerHtml = "<img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" height=\"100\" width=\"100\"/>";
                                userimg.InnerHtml = "<img id=\"loggeduserimg\" src=\"" + user.ProfileUrl + "\" alt=\"" + user.UserName + "\" height=\"100\" width=\"100\"/>";
                            if (user.TimeZone != null)
                            {
                                Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                                //userinf.InnerHtml = Datetime;
                            }
                            if (user.TimeZone == null)
                            {
                                Datetime = DateTime.Now.ToString();
                                //userinf.InnerHtml = Datetime;
                            }
                        }
                        else
                        {
                            //Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                            //userimg.InnerHtml = "<a href=\"../Settings/PersonalSettings.aspx\"><img id=\"loggeduserimg\" src=\"../Contents/img/blank_img.png\" alt=\"" + user.UserName + "\"/></a>";
                            userimg.InnerHtml = "<img id=\"loggeduserimg\" src=\"../Contents/img/blank_img.png\" alt=\"" + user.UserName + "\"/>";
                            userimg.InnerHtml = "<img id=\"loggeduserimg\" src=\"../Contents/img/blank_img.png\" alt=\"" + user.UserName + "\"/>";

                            //userinf.InnerHtml = Datetime;
                            if (user.TimeZone != null)
                            {
                                Datetime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToLongDateString() + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, user.TimeZone).ToShortTimeString() + " (" + user.TimeZone + ")";
                                //userinf.InnerHtml = Datetime;
                            }
                            if (user.TimeZone == null)
                            {
                                Datetime = DateTime.Now.ToString();
                                //userinf.InnerHtml = Datetime;
                            }
                        }

                        try
                        {

                            GroupRepository grouprepo = new GroupRepository();
                            List<Groups> lstgroups = grouprepo.getAllGroupsCompleteDetails(user.Id);
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

                //Team lstDetails = objTeamRepository.getAllGroupsDetails(user.EmailId.ToString(), Guid.Parse(groupsselection.SelectedItem.Value));
                //Session["GroupName"] = lstDetails;


            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
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


        /// <summary>
        /// to check user reference and update user expiry , userrefrencerelation status
        /// created by Abhay Kr 5-3-2014
        /// </summary>
        /// <param name="RefereeId"></param>
        /// <returns></returns>
        public bool IsUserReferenceActivated(string RefereeId)
        {
            //testing
            Console.WriteLine("Inside " + RefereeId);

            bool ret = false;
            try
            {
                User objUser = new User();
                Package objPackage=new Package ();
                UserPackageRelation objUserPackageRelation=new UserPackageRelation ();
                UserRepository objUserRepository = new UserRepository();
                UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
                UserRefRelation objUserRefRelation = new UserRefRelation();
                UserRefRelationRepository objUserRefRelationRepository = new UserRefRelationRepository();
                PackageRepository objPackageRepository = new PackageRepository();
                objUserRefRelation.ReferenceUserId = (Guid.Parse(RefereeId));

                //testing
                List<UserRefRelation> check =objUserRefRelationRepository.GetUserRefRelationInfo();
                //testing

                List<UserRefRelation> lstUserRefRelation = objUserRefRelationRepository.GetUserRefRelationInfoByRefreeId(objUserRefRelation);
                if (lstUserRefRelation.Count > 0)
                {
                    if (lstUserRefRelation[0].Status == "0")
                    {
                        objUserRefRelation = lstUserRefRelation[0];
                        objUserRefRelation.Status = "1";
                        objUser = objUserRepository.getUsersById(lstUserRefRelation[0].ReferenceUserId);
                        if (objUser.ExpiryDate <= DateTime.Now)
                        {
                            objUser.ExpiryDate = DateTime.Now.AddDays(30);
                        }
                        else if (objUser.ExpiryDate > DateTime.Now)
                        {
                            objUser.ExpiryDate = objUser.ExpiryDate.AddDays(30);
                        }
                        else
                        {
                            objUser.ExpiryDate = objUser.ExpiryDate.AddDays(30);
                        }
                        objUser.AccountType = "Premium";
                        objPackage = objPackageRepository.getPackageDetails("Premium");

                        objUserPackageRelation.Id=Guid.NewGuid();
                        objUserPackageRelation.UserId=objUser.Id;
                        objUserPackageRelation.PackageId=objPackage.Id;
                        objUserPackageRelation.ModifiedDate=DateTime.Now;
                        objUserPackageRelation.PackageStatus=true;
                        objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);
                        int objUserRepositoryresponse = objUserRepository.UpdateUserExpiryDateById(objUser);
                        int objUserRefRelationRepositoryresponse = objUserRefRelationRepository.UpdateStatusById(objUserRefRelation);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return ret;
        }

        //protected void groupsselection_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    SocioBoard.Domain.User user = (User)Session["LoggedUser"];
        //    string GroupNames =string.Empty;

        //    groupsselection.SelectedItem.ToString();
        //    TeamRepository objTeamRepository = new TeamRepository();
        //    Team lstDetails = objTeamRepository.getAllGroupsDetails(user.EmailId.ToString(),Guid.Parse(groupsselection.SelectedValue));
           
        //    Session["GroupName"] = lstDetails;
        //    Session["groupcheck"] = groupsselection.SelectedValue;
         
        //}




    }
}