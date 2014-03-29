using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Configuration;
using Facebook;
using SocioBoard.Model;
using SocioBoard.Helper;

using log4net;

namespace SocialSuitePro
{
    public partial class FacebookManager : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(FacebookManager));

        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            if (!IsPostBack)
            {
                if (Session["login"] == null)
                {
                    if (user == null)
                    { Response.Redirect("Default.aspx"); }
                }

                try
                {
                    GetAccessToken();


                    Session["profilesforcomposemessage"] = null;
                    Session["InboxMessages"] = null;
                    if (Session["UserAndGroupsForFacebook"] != null)
                    {
                        if (Session["UserAndGroupsForFacebook"].ToString() == "facebook")
                        {
                            try
                            {

                                Session["UserAndGroupsForFacebook"] = null;
                                Response.Redirect("/Settings/UsersAndGroups.aspx");
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);
                                Session["UserAndGroupsForFacebook"] = null;
                            }
                        }
                    }
                    else if (Session["login"] != null)
                    {
                        if (Session["login"].ToString() == "facebook")
                        {
                            UserRepository userrepo = new UserRepository();
                            user = (User)Session["LoggedUser"];

                            if (!string.IsNullOrEmpty(user.Password))
                            {

                                if (user.UserStatus == 1)
                                {
                                    Response.Redirect("Home.aspx");
                                }
                                else
                                {
                                    //check user is block or not
                                    Session["fblogout"] = "NOTACTIVATED";

                                    Response.Redirect("Default.aspx");
                                }
                            }
                            else
                            {
                                //Response.Redirect("Pricing.aspx");
                                Response.Redirect("NetworkLogin.aspx?type=Free");
                            }
                        }

                    }

                    else
                    {
                        try
                        {
                            Response.Redirect("Home.aspx");
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            Session["profilesforcomposemessage"] = null;
                            Response.Redirect("Home.aspx");
                        }

                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.StackTrace);
                }

            }

        }


        //Getting AccessToken,FacebookMessages,FacebookFeeds and UserProfile for  Authenticated user.

        private void GetAccessToken()
        {
            string code = Request.QueryString["code"];
            if (code == null)
            {
                Response.Redirect("Home.aspx");
            }
            User user = null;
            if (Session["login"] != null)
            {
                if (Session["login"].ToString() == "facebook")
                {
                    user = new User();

                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Id = Guid.NewGuid();
                    user.PaymentStatus = "unpaid";
                }
            }
            else
            {
                /*User class in SocioBoard.Domain to check authenticated user*/
                user = (User)Session["LoggedUser"];

            }
            /*Replacing Code With AccessToken*/
            // Facebook.dll using for 
            FacebookHelper fbhelper = new FacebookHelper();


            FacebookClient fb = new FacebookClient();
            string profileId = string.Empty;
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("client_id", ConfigurationManager.AppSettings["ClientId"]);
            parameters.Add("redirect_uri", ConfigurationManager.AppSettings["RedirectUrl"]);
            parameters.Add("client_secret", ConfigurationManager.AppSettings["ClientSecretKey"]);
            parameters.Add("code", code);

            JsonObject result = (JsonObject)fb.Get("/oauth/access_token", parameters);
            string accessToken = result["access_token"].ToString();
            fb.AccessToken = accessToken;

            // This code is used for posting Begin

            ManageReferrals(fb);

            // This code is used for posting End

            // For long Term Fb access_token

            // GET /oauth/access_token?  
            //grant_type=fb_exchange_token&           
            //client_id={app-id}&
            //client_secret={app-secret}&
            //fb_exchange_token={short-lived-token}


            parameters.Clear();

            parameters.Add("grant_type", "fb_exchange_token");
            parameters.Add("client_id", ConfigurationManager.AppSettings["ClientId"]);
            parameters.Add("client_secret", ConfigurationManager.AppSettings["ClientSecretKey"]);
            parameters.Add("fb_exchange_token", accessToken);

            result = (JsonObject)fb.Get("/oauth/access_token", parameters);
            accessToken = result["access_token"].ToString();
            fb.AccessToken = accessToken;

            dynamic profile = fb.Get("me");

            int res = UpdateFbToken(profile["id"], accessToken);
            bool isfbemailexist = false;
            if (Session["login"] != null)
            {
                if (Session["login"].ToString() == "facebook")
                {
                    try
                    {
                        user.EmailId = profile["email"].ToString();
                    }
                    catch (Exception ex)
                    {
                        isfbemailexist = true;
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    if (isfbemailexist)
                    {
                        Session["isemailexist"] = "emailnotexist";
                        Response.Redirect("Default.aspx");
                    }
                    try
                    {

                        user.UserName = profile["name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        user.ProfileUrl = "https://graph.facebook.com/" + profile["id"] + "/picture?type=small";
                        profileId = profile["id"];
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    user.UserStatus = 1;

                    UserRepository userrepo = new UserRepository();

                    if (userrepo.IsUserExist(user.EmailId))
                    {
                        string emailid = user.EmailId;
                        user = null;

                        user = userrepo.getUserInfoByEmail(emailid);

                    }
                    else
                    {
                        UserRepository.Add(user);
                    }

                    Session["LoggedUser"] = user;
                }

            }




            var feeds = fb.Get("/me/feed");

            var home = fb.Get("/me/home");
            var messages = fb.Get("/me/inbox");

            long friendscount = 0;
            try
            {
                fbhelper.getInboxMessages(messages, profile, user.Id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }

            try
            {
                dynamic friedscount = fb.Get("fql", new { q = "SELECT friend_count FROM user WHERE uid=me()" });

                foreach (var friend in friedscount.data)
                {
                    friendscount = friend.friend_count;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);

            }


            try
            {

                fbhelper.getFacebookUserProfile(profile, accessToken, friendscount, user.Id);

            }
            catch (Exception exx)
            {
                logger.Error(exx.StackTrace);
                Console.WriteLine(exx.StackTrace);

            }

            try
            {

                fbhelper.getFacebookUserFeeds(feeds, profile);

            }
            catch (Exception exxx)
            {
                logger.Error(exxx.StackTrace);
                Console.WriteLine(exxx.StackTrace);

            }

            try
            {

                fbhelper.getFacebookUserHome(home, profile);

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);

            }

            try
            {

                var friendsgenderstats = fb.Get("me/friends?fields=gender");
                fbhelper.getfbFriendsGenderStats(friendsgenderstats, profile, user.Id);

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            try
            {
                FacebookInsightStatsHelper fbiHelper = new FacebookInsightStatsHelper();
                fbiHelper.getPageImpresion(profile["id"], user.Id, 15);
                fbiHelper.getFanPageLikesByGenderAge(profile["id"], user.Id, 15);
                fbiHelper.getLocation(profile["id"], user.Id, 15);
                //  fbiHelper.getFanPost("459630637383010", user.Id, 10);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
            }
        }

        private void ManageReferrals(FacebookClient fb)
        {
            try
            {
                if (Session["facemsg"] != null)
                {
                    try
                    {
                        var args = new Dictionary<string, object>();
                        args["message"] = Session["facemsg"].ToString();
                        fb.Post("/me/feed", args).ToString();
                        Response.Redirect("Referrals.aspx");
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public int UpdateFbToken(string fbUserId, string fbAccessToken)
        {
            int res = 0;
            try
            {
                FacebookAccountRepository objfbAR = new FacebookAccountRepository();

                res = objfbAR.UpdateFBAccessTokenByFBUserId(fbUserId, fbAccessToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);

            }
            return res;
        }
    }
}