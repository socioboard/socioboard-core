using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]

    public class TwitterFeed : System.Web.Services.WebService
    {
        TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId(string UserId, string ProfileId)
        { 
            List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed=new List<Domain.Socioboard.Domain.TwitterFeed> ();
            try
            {
                if (objTwitterFeedRepository.checkTwitteUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId);
                }
                else
                {
                    lstTwitterFeed = objTwitterFeedRepository.getAllTwitterUserFeeds(ProfileId);
                }
                return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId1(string UserId, string ProfileId, int count)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId, count);
                return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        //getAllTwitterFeedOfUsers
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterFeedOfUsers(string UserId, string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        public string TwitterInboxMessagecount(string userid, string profileid, string days)
        {
            int daycount = Convert.ToInt32(days);
            List<Domain.Socioboard.Domain.TwitterFeed> lstfeed = new List<Domain.Socioboard.Domain.TwitterFeed>();

            try
            {

                lstfeed = objTwitterFeedRepository.getAllInboxMessagesByProfileid(Guid.Parse(userid), profileid, daycount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return lstfeed.Count.ToString();

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterFeedById(string id)
        {
            try
            {
                Domain.Socioboard.Domain.TwitterFeed twtfeed = objTwitterFeedRepository.getTwitterFeed(id);
                return new JavaScriptSerializer().Serialize(twtfeed);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return null;
            }
        }

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getTwitterFeedByProfileId(string ProfileId, string MessageId)
        {
            try
            {
                Domain.Socioboard.Domain.TwitterFeed twtfeed = objTwitterFeedRepository.getTwitterFeedByProfileId(ProfileId, MessageId);
                return new JavaScriptSerializer().Serialize(twtfeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId1ByKeyword(string UserId, string ProfileId, string keyword, int count)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsersByKeyword(UserId, ProfileId, keyword, count);
                return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}
