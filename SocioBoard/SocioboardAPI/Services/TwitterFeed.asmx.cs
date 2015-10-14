using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
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
        //TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
        MongoRepository twitterfeedrepo = new MongoRepository("TwitterFeed");
        private CultureInfo provider = CultureInfo.InvariantCulture;
        private string format = "yyyy/MM/dd HH:mm:ss";
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId(string UserId, string ProfileId)
        {


            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();

            try
            {
                var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(ProfileId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                lstTwitterFeed = _lstTwitterFeed.OrderByDescending(t => DateTime.ParseExact(t.FeedDate, format, provider)).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstTwitterFeed);

            //try
            //{
            //    if (objTwitterFeedRepository.checkTwitteUserExists(ProfileId, Guid.Parse(UserId)))
            //    {
            //        lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId);
            //    }
            //    else
            //    {
            //        lstTwitterFeed = objTwitterFeedRepository.getAllTwitterUserFeeds(ProfileId);
            //    }
            //    return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFeedsByUserIdAndProfileIdUsingLimit(string UserId, string ProfileId, string noOfDataToSkip, string noOfResultsFromTop)
        {
            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();

            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.TwitterFeed>.Sort;
                var sort = builder.Descending(t => t.FeedDate);

                var ret = twitterfeedrepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(ProfileId), sort, Int32.Parse(noOfDataToSkip), Int32.Parse(noOfResultsFromTop));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                lstFeed = _lstTwitterFeed.ToList();
            }
            catch (Exception ex)
            {
                lstFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstFeed);


            //try
            //{
            //    if (objTwitterFeedRepository.checkTwitteUserExists(ProfileId, Guid.Parse(UserId)))
            //    {
            //        lstFeed = objTwitterFeedRepository.getAllFeedsOfSBUserWithRangeAndProfileId(UserId, ProfileId, noOfDataToSkip, noOfResultsFromTop);
            //    }
            //    else
            //    {
            //        lstFeed = objTwitterFeedRepository.getAllFeedsOfSBUserWithRangeByProfileId(ProfileId, noOfDataToSkip, noOfResultsFromTop);
            //    }
            //    return new JavaScriptSerializer().Serialize(lstFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId1(string UserId, string ProfileId, int count)
        {

            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();

            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.TwitterFeed>.Sort;
                var sort = builder.Descending(t => t.FeedDate);
                var ret = twitterfeedrepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(ProfileId),sort,count,10);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                lstFeed = _lstTwitterFeed.ToList();

            }
            catch (Exception ex)
            {
                lstFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();
            }
            return new JavaScriptSerializer().Serialize(lstFeed);

            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId, count);
            //    return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }
        //getAllTwitterFeedOfUsers
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterFeedOfUsers(string UserId, string ProfileId)
        {

            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();

            try
            {
                var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(ProfileId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                lstTwitterFeed = _lstTwitterFeed.OrderByDescending(t => DateTime.ParseExact(t.FeedDate, format, provider)).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstTwitterFeed);

            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }


        [WebMethod]
        public string TwitterInboxMessagecount(string userid, string profileid, string days)
        {
            int daycount = Convert.ToInt32(days);
            //List<Domain.Socioboard.Domain.TwitterFeed> lstfeed = new List<Domain.Socioboard.Domain.TwitterFeed>();
            int lstfeed=0;

            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstTwitterFeed;

            try
            {
                var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(profileid));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                lstfeed = _lstTwitterFeed.Where(t => DateTime.ParseExact(t.FeedDate, format, provider) < DateTime.Now.AddDays(1).Date && DateTime.ParseExact(t.FeedDate, format, provider) > DateTime.Now.AddDays(-Int32.Parse(days)).Date).ToList().Count;
            }
            catch (Exception ex)
            {
                lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();
            }
                   
            //try
            //{

            //    lstfeed = objTwitterFeedRepository.getAllInboxMessagesByProfileid(Guid.Parse(userid), profileid, daycount);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //}

            return lstfeed.ToString();

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterFeedById(string id)
        {
            Domain.Socioboard.MongoDomain.TwitterFeed _TwitterFeed = new Domain.Socioboard.MongoDomain.TwitterFeed();
            try
            {
                var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.strId.Equals(id));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                _TwitterFeed = (Domain.Socioboard.MongoDomain.TwitterFeed)_lstTwitterFeed[0];
            }
            catch (Exception ex)
            {
                _TwitterFeed = new Domain.Socioboard.MongoDomain.TwitterFeed();
            }

            return new JavaScriptSerializer().Serialize(_TwitterFeed);

            //try
            //{
            //    Domain.Socioboard.Domain.TwitterFeed twtfeed = objTwitterFeedRepository.getTwitterFeed(id);
            //    return new JavaScriptSerializer().Serialize(twtfeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.Write(ex.StackTrace);
            //    return null;
            //}
        }

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getTwitterFeedByProfileId(string ProfileId, string MessageId)
        {
            Domain.Socioboard.MongoDomain.TwitterFeed twtfeed;
            try
            {
                var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(ProfileId) && t.MessageId.Equals(MessageId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                twtfeed = (Domain.Socioboard.MongoDomain.TwitterFeed)_lstTwitterFeed[0];
            }
            catch (Exception ex)
            {
                twtfeed = new Domain.Socioboard.MongoDomain.TwitterFeed();
            }
            return new JavaScriptSerializer().Serialize(twtfeed);
            //try
            //{
            //    Domain.Socioboard.Domain.TwitterFeed twtfeed = objTwitterFeedRepository.getTwitterFeedByProfileId(ProfileId, MessageId);
            //    return new JavaScriptSerializer().Serialize(twtfeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return null;
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterFeedsByUserIdAndProfileId1ByKeyword(string UserId, string ProfileId, string keyword, int count)
        {

            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();

            try
            {
                var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.ProfileId.Equals(ProfileId) && t.Feed.Contains(keyword));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstTwitterFeed = task.Result;
                lstTwitterFeed = _lstTwitterFeed.OrderByDescending(t => DateTime.ParseExact(t.FeedDate, format, provider)).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterFeed = new List<Domain.Socioboard.MongoDomain.TwitterFeed>();
            }

            return new JavaScriptSerializer().Serialize(lstTwitterFeed);

            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterFeed> lstTwitterFeed = objTwitterFeedRepository.getAllTwitterFeedOfUsersByKeyword(UserId, ProfileId, keyword, count);
            //    return new JavaScriptSerializer().Serialize(lstTwitterFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }
    }
}
