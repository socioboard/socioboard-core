/*
 Desc:Manages all Twitter related methods
 Date:10/5/2013
 Change Log : Sumit Ghosh, initial code
 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using Newtonsoft.Json.Linq;
using log4net;
using System.Configuration;

using SocioBoard.Helper;
using SocioBoard;
using GlobusTwitterLib.Twitter.Core.TweetMethods;

namespace SocialSuitePro
{

    public partial class TwitterManager : System.Web.UI.Page
    {

        ILog logger = LogManager.GetLogger(typeof(TwitterManager));

        //Manages oAuth Related Functionality of Twitter, part of GlobusTwitterLib
        oAuthTwitter OAuth = new oAuthTwitter(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);

        oAuthTwitter OAuthNew = new oAuthTwitter(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);

        //SocioBoard Domain Class to store Twitter Data
        TwitterAccount twitterAccount = new TwitterAccount();
        TwitterAccount newtwitterAccount = new TwitterAccount();
        //GlobusLib Class for managing Twitter Users
        TwitterUserController twtUserController = new TwitterUserController();

        //GlobusLib Class to manage Twitter User Data
        //  TwitterUser twtUser = new TwitterUser();
        TwitterAccountRepository twtrepo = new TwitterAccountRepository();

        //Creates new instance of Users Class to Model Twitter User
        Users userinfo = new Users();



        protected void Page_Load(object sender, EventArgs e)
        {

            User user = (User)Session["LoggedUser"];
            if (!IsPostBack)
            {
           
                //if (Session["login"] == null)
                //{
                //    if (user == null)
                //    { Response.Redirect("Default.aspx"); }
                //}

                try
                {
                    getAccessToken();
                    Session["profilesforcomposemessage"] = null;

                    if (Session["UserAndGroupsForTwitter"] != null)
                    {
                        if (Session["UserAndGroupsForTwitter"].ToString() == "twitter")
                        {
                            try
                            {

                                Session["UserAndGroupsForTwitter"] = null;
                                Response.Redirect("/Settings/UsersAndGroups.aspx");
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                                Console.WriteLine(ex.StackTrace);
                                Session["UserAndGroupsForTwitter"] = null;
                                Response.Redirect("/Settings/UsersAndGroups.aspx");
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            Response.Redirect("Home.aspx", false);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                            Console.WriteLine(ex.StackTrace);
                            Session["profilesforcomposemessage"] = null;
                            Response.Redirect("Home.aspx");

                        }

                    }
                }

                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);

                }

            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void getAccessToken()
        {
            User user = (User)Session["LoggedUser"];
            TwitterHelper twthelper = new TwitterHelper();

            if (Request["oauth_token"] != null)
            {


                try
                {
                    getTwitterUserProfile();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twthelper.getMentions(OAuth, twitterAccount, user.Id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    twthelper.getReTweetsOfUser(OAuth, twitterAccount, user.Id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }


                try
                {
                    twthelper.getUserTweets(OAuth, twitterAccount, user.Id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twthelper.getUserFeed(OAuth, twitterAccount, user.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.StackTrace);
                }
                try
                {
                    twthelper.getSentDirectMessages(OAuth, twitterAccount, user.Id);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    Console.WriteLine(ex.StackTrace);
                }

            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void getTwitterUserProfile()
        {


            var requestToken = (String)Request.QueryString["oauth_token"];
            var requestSecret = (String)Session["requestSecret"];
            var requestVerifier = (String)Request.QueryString["oauth_verifier"];


            OAuth.AccessToken = requestToken;
            OAuth.AccessTokenSecret = requestVerifier;
            OAuth.AccessTokenGet(requestToken, requestVerifier);

            JArray profile = userinfo.Get_Users_LookUp_ByScreenName(OAuth, OAuth.TwitterScreenName);
            User user = (User)Session["LoggedUser"];
            SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
            SocialProfile socioprofile = new SocialProfile();

            #region for managing referrals
            ManageReferrals(OAuth); 
            #endregion
         
            foreach (var item in profile)
            {
                try
                {
                    twitterAccount.FollowingCount = Convert.ToInt32(item["friends_count"].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twitterAccount.FollowersCount = Convert.ToInt32(item["followers_count"].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                twitterAccount.Id = Guid.NewGuid();
                twitterAccount.IsActive = true;
                twitterAccount.OAuthSecret = OAuth.AccessTokenSecret;
                twitterAccount.OAuthToken = OAuth.AccessToken;
                try
                {
                    twitterAccount.ProfileImageUrl = item["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);

                }
                try
                {
                    twitterAccount.ProfileUrl = string.Empty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twitterAccount.TwitterUserId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception er)
                {
                    try
                    {
                        twitterAccount.TwitterUserId = item["id"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.StackTrace);
                    }
                    Console.WriteLine(er.StackTrace);

                }

                try
                {
                    twitterAccount.TwitterScreenName = item["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                twitterAccount.UserId = user.Id;

                socioprofile.Id = Guid.NewGuid();
                socioprofile.ProfileDate = DateTime.Now;
                socioprofile.ProfileId = twitterAccount.TwitterUserId;
                socioprofile.ProfileType = "twitter";
                socioprofile.UserId = user.Id;

                if (HttpContext.Current.Session["login"] != null)
                {
                    if (HttpContext.Current.Session["login"].ToString().Equals("twitter"))
                    {
                        User usr = new User();
                        UserRepository userrepo = new UserRepository();
                        Registration regObject = new Registration();
                        usr.AccountType = "free";
                        usr.CreateDate = DateTime.Now;
                        usr.ExpiryDate = DateTime.Now.AddMonths(1);
                        usr.Id = Guid.NewGuid();
                        usr.UserName = twitterAccount.TwitterName;
                        usr.Password = regObject.MD5Hash(twitterAccount.TwitterName);
                        usr.EmailId = "";
                        usr.UserStatus = 1;
                        if (!userrepo.IsUserExist(usr.EmailId))
                        {
                            UserRepository.Add(usr);
                        }
                    }
                }

                TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
                TwitterStats objStats = new TwitterStats();
                Random rNum = new Random();
                objStats.Id = Guid.NewGuid();
                objStats.TwitterId = twitterAccount.TwitterUserId;
                objStats.UserId = user.Id;
                objStats.FollowingCount = twitterAccount.FollowingCount;
                objStats.FollowerCount = twitterAccount.FollowersCount;
                objStats.Age1820 = rNum.Next(twitterAccount.FollowersCount);
                objStats.Age2124 = rNum.Next(twitterAccount.FollowersCount);
                objStats.Age2534 = rNum.Next(twitterAccount.FollowersCount);
                objStats.Age3544 = rNum.Next(twitterAccount.FollowersCount);
                objStats.Age4554 = rNum.Next(twitterAccount.FollowersCount);
                objStats.Age5564 = rNum.Next(twitterAccount.FollowersCount);
                objStats.Age65 = rNum.Next(twitterAccount.FollowersCount);
                objStats.EntryDate = DateTime.Now;
                if (!objTwtstats.checkTwitterStatsExists(twitterAccount.TwitterUserId, user.Id))
                    objTwtstats.addTwitterStats(objStats);
                if (!twtrepo.checkTwitterUserExists(twitterAccount.TwitterUserId, user.Id))
                {
                    twtrepo.addTwitterkUser(twitterAccount);
                    if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                    {
                        socioprofilerepo.addNewProfileForUser(socioprofile);
                    }
                    else
                    {
                        socioprofilerepo.updateSocialProfile(socioprofile);
                    }
                }
                else
                {
                    twtrepo.updateTwitterUser(twitterAccount);
                    TwitterMessageRepository twtmsgreponew = new TwitterMessageRepository();
                    twtmsgreponew.updateScreenName(twitterAccount.TwitterUserId, twitterAccount.TwitterScreenName);
                    if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                    {
                        socioprofilerepo.addNewProfileForUser(socioprofile);
                    }
                    else
                    {
                        socioprofilerepo.updateSocialProfile(socioprofile);
                    }

                }
                if (Session["UserAndGroupsForTwitter"] != null)
                {
                    if (Session["UserAndGroupsForTwitter"].ToString() == "twitter")
                    {
                        try
                        {
                            if (Session["GroupName"] != null)
                            {
                                Groups group = (Groups)Session["GroupName"];
                                GroupProfile groupprofile = new GroupProfile();
                                GroupProfileRepository groupprofilerepo = new GroupProfileRepository();
                                groupprofile.Id = Guid.NewGuid();
                                groupprofile.ProfileId = socioprofile.ProfileId;
                                groupprofile.ProfileType = "twitter";
                                groupprofile.GroupOwnerId = user.Id;
                                groupprofile.EntryDate = DateTime.Now;
                                groupprofile.GroupId = group.Id;
                                if (!groupprofilerepo.checkGroupProfileExists(user.Id, group.Id, groupprofile.ProfileId))
                                {
                                    groupprofilerepo.AddGroupProfile(groupprofile);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.StackTrace);
                        }
                    }
                }

            }
        }

        private void ManageReferrals(oAuthTwitter OAuth)
        {
            try
            {
                if (Session["twittermsg"] != null)
                {
                    TwitterHelper twthelper = new TwitterHelper();
                    Tweet twt = new Tweet();
                    JArray post = twt.Post_Statuses_Update(OAuth, Session["twittermsg"].ToString());
                    Response.Redirect("Referrals.aspx");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }



    }
}