using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Web.Script.Serialization;
using GlobusTwitterLib.Authentication;
using SocioBoard.Helper;
using System.Configuration;

namespace SocialSuitePro.API
{
    /// <summary>
    /// Summary description for Twitter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class TwitterService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string TwitterId)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                TwitterAccountRepository twtAccrepo = new TwitterAccountRepository();
                TwitterAccount twtAccount = twtAccrepo.getUserInformation(userid, TwitterId);
                return new JavaScriptSerializer().Serialize(twtAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetTwitterFeedsAPI(string UserId, string TwitterId)
        {
            try
            {
                Guid userId = Guid.Parse(UserId);
                UserRepository userrepo = new UserRepository();
                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                TwitterAccount twtAcc = twtAccRepo.getUserInformation(userId, TwitterId);
                oAuthTwitter oAuth = new oAuthTwitter();
                oAuth.AccessToken = twtAcc.OAuthToken;
                oAuth.AccessTokenSecret = twtAcc.OAuthSecret;
                oAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                oAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                oAuth.TwitterUserId = twtAcc.TwitterUserId;
                oAuth.TwitterScreenName = twtAcc.TwitterScreenName;
                TwitterHelper twtHelper = new TwitterHelper();
                twtHelper.getUserFeed(oAuth, twtAcc, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetUserTweetsAPI(string UserId, string TwitterId)
        {
            try
            {

                Guid userId = Guid.Parse(UserId);
                UserRepository userrepo = new UserRepository();
                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                TwitterAccount twtAcc = twtAccRepo.getUserInformation(userId, TwitterId);
                oAuthTwitter oAuth = new oAuthTwitter();
                oAuth.AccessToken = twtAcc.OAuthToken;
                oAuth.AccessTokenSecret = twtAcc.OAuthSecret;
                oAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                oAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                oAuth.TwitterUserId = twtAcc.TwitterUserId;
                oAuth.TwitterScreenName = twtAcc.TwitterScreenName;
                TwitterHelper twtHelper = new TwitterHelper();
                twtHelper.getUserTweets(oAuth, twtAcc, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetUserReTweetsAPI(string UserId, string TwitterId)
        {
            try
            {
                Guid userId = Guid.Parse(UserId);
                UserRepository userrepo = new UserRepository();
                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                TwitterAccount twtAcc = twtAccRepo.getUserInformation(userId, TwitterId);
                oAuthTwitter oAuth = new oAuthTwitter();
                oAuth.AccessToken = twtAcc.OAuthToken;
                oAuth.AccessTokenSecret = twtAcc.OAuthSecret;
                oAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                oAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                oAuth.TwitterUserId = twtAcc.TwitterUserId;
                oAuth.TwitterScreenName = twtAcc.TwitterScreenName;
                TwitterHelper twtHelper = new TwitterHelper();
                twtHelper.getReTweetsOfUser(oAuth, twtAcc, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void GetDirectMessagesSentByUserAPI(string UserId, string TwitterId)
        {
            try
            {
                Guid userId = Guid.Parse(UserId);
                UserRepository userrepo = new UserRepository();
                TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
                TwitterAccount twtAcc = twtAccRepo.getUserInformation(userId, TwitterId);
                oAuthTwitter oAuth = new oAuthTwitter();
                oAuth.AccessToken = twtAcc.OAuthToken;
                oAuth.AccessTokenSecret = twtAcc.OAuthSecret;
                oAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
                oAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
                oAuth.TwitterUserId = twtAcc.TwitterUserId;
                oAuth.TwitterScreenName = twtAcc.TwitterScreenName;
                TwitterHelper twtHelper = new TwitterHelper();
                twtHelper.getSentDirectMessages(oAuth, twtAcc, userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        /* Below function might be change in Future   */

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterFeeds(string TwitterUserId)
        {
            try
            {
              
                TwitterFeedRepository twtFeedRepo = new TwitterFeedRepository();
                List<TwitterFeed> lsttwtmessage = twtFeedRepo.getTwitterFeedOfProfile(TwitterUserId);
                return new JavaScriptSerializer().Serialize(lsttwtmessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterMessages(string TwitterId)
        {
            TwitterMessageRepository twtMsgRepo = new TwitterMessageRepository();
            List<TwitterMessage> lsttwtmsg = twtMsgRepo.getAllTwitterMessagesOfProfile(TwitterId);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        private void InitializeComponent()
        {

        }


    }
}
