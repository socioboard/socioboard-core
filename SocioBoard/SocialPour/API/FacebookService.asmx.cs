using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using SocioBoard.Model;
using SocioBoard.Domain;
using System.Web.Script.Serialization;
using SocioBoard.Helper;

namespace SocialSuitePro.API
{
    /// <summary>
    /// Summary description for Facebook
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class FacebookService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string FacebookId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(facebook);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserHome(string UserId, string FacebookId, string count)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                List<FacebookMessage> lstfbmsgs = fbmsgrepo.getFacebookUserWallPost(userid, FacebookId, Convert.ToInt32(count));
                return new JavaScriptSerializer().Serialize(lstfbmsgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
                throw;
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserFeeds(string UserId, string FacebookId)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();
                List<FacebookFeed> lstfbfeed = fbfeedrepo.getAllFacebookUserFeeds(FacebookId);
                return new JavaScriptSerializer().Serialize(lstfbfeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");

            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void FacebookUserHomeAPI(string UserId, string FacebookId)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                FacebookHelper fbhelper = new FacebookHelper();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(FacebookId, userid);
                Facebook.FacebookClient fb = new Facebook.FacebookClient(fbAccount.AccessToken);
                var home = fb.Get("/me/home");
                dynamic profile = fb.Get("me");
                fbhelper.getFacebookUserHome(home, profile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void FacebookUserFeedAPI(string UserId, string FacebookId)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                FacebookHelper fbhelper = new FacebookHelper();
                FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
                FacebookAccount fbAccount = fbAccRepo.getFacebookAccountDetailsById(FacebookId, userid);
                Facebook.FacebookClient fb = new Facebook.FacebookClient(fbAccount.AccessToken);
                var feeds = fb.Get("/me/feed");
                dynamic profile = fb.Get("me");
                fbhelper.getFacebookUserFeeds(feeds, profile);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void DeleteFacebookUser(string UserId, string FacebookId)
        {
            Guid userid = Guid.Parse(UserId);
            FacebookAccountRepository fbAccRepo = new FacebookAccountRepository();
            int i = fbAccRepo.deleteFacebookUser(FacebookId, userid);
            if (i != 0)
            {
                FacebookFeedRepository fbfeedRepo = new FacebookFeedRepository();
                try
                {
                    fbfeedRepo.deleteAllFeedsOfUser(FacebookId, userid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                FacebookMessageRepository fbmsgRepo = new FacebookMessageRepository();
                try
                {
                    fbmsgRepo.deleteAllMessagesOfUser(FacebookId, userid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }


            }

        }



    }
}
