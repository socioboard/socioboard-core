using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Api.Socioboard.Model;
using Domain.Socioboard.Domain;
using log4net;
using System.Globalization;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Api.Socioboard.Helper;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for SentimentalAnalysis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SentimentalAnalysis : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(SentimentalAnalysis));
        Domain.Socioboard.Domain.FeedSentimentalAnalysis _FeedSentimentalAnalysis = new FeedSentimentalAnalysis();
        FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new FeedSentimentalAnalysisRepository();

        InboxMessagesRepository _InboxMessagesRepository = new InboxMessagesRepository();
        MongoRepository TwitterFeedRepo = new MongoRepository("TwitterFeed");
        MongoRepository TwitteMessageRepo = new MongoRepository("TwitterMessage");
        MongoRepository FacebookMessageRepo = new MongoRepository("FacebookMessage");
        MongoRepository FacebookFeedRepo = new MongoRepository("MongoFacebookFeed");
        MongoRepository ArticlesAndBlogsRepo = new MongoRepository("ArticlesAndBlogs");

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetPostSentimentsFromUclassify(Guid Userid, string profileid, string feedId, string Message, string Network)
        {
            string responce = string.Empty;
            try
            {
                Guid _Userid = Guid.Parse(Userid.ToString());
                responce = GetSentimentsOfText(_Userid, profileid, feedId, Message, Network);
            }
            catch (Exception ex)
            {
                responce = "";
            }
            return responce;
        }

        public string GetSentimentsOfText(Guid Userid, string profileid, string feedId, string Message, string Network)
        {
            string _Strreturn = string.Empty;
            try
            {
                string _SentimentalRestUrl = "http://uclassify.com/browse/uClassify/Sentiment/ClassifyText?readkey=" + ConfigurationManager.AppSettings["ReadKey"] + "&text=" + Message + "&output=json&version=1.01";

                GlobusLinkedinLib.Authentication.oAuthLinkedIn _oAuthLinkedIn = new GlobusLinkedinLib.Authentication.oAuthLinkedIn();
                string response = _oAuthLinkedIn.WebRequest(GlobusLinkedinLib.Authentication.oAuthLinkedIn.Method.GET, Uri.EscapeUriString(_SentimentalRestUrl), "");

                var JData = Newtonsoft.Json.Linq.JObject.Parse(response);

                string negative = JData["cls1"]["negative"].ToString();
                string positive = JData["cls1"]["positive"].ToString();


                Domain.Socioboard.Domain.FeedSentimentalAnalysis _FeedSentimentalAnalysis = new FeedSentimentalAnalysis();
                _FeedSentimentalAnalysis.Id = Guid.NewGuid();
                _FeedSentimentalAnalysis.UserId = Userid;
                _FeedSentimentalAnalysis.ProfileId = profileid;
                _FeedSentimentalAnalysis.FeedId = feedId;
                _FeedSentimentalAnalysis.Positive = positive;
                _FeedSentimentalAnalysis.Negative = negative;
                _FeedSentimentalAnalysis.EntryDate = DateTime.Now;
                _FeedSentimentalAnalysis.Network = Network;

                Model.FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new Model.FeedSentimentalAnalysisRepository();
                _FeedSentimentalAnalysisRepository.Add(_FeedSentimentalAnalysis);

                _Strreturn = "Success";
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                _Strreturn = "failure";
            }


            return _Strreturn;
        }

        /// <summary>
        /// Get All Negative Feed of All Profile.
        /// </summary>
        /// <param name="ProfileId"></param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllNegativeFeedsOfProfile()
        {
            try
            {
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstAllProfiles = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfProfile = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfAllProfile = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

                try
                {
                    lstAllProfiles = _FeedSentimentalAnalysisRepository.getAllProfiles();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }

                foreach (var item in lstAllProfiles)
                {
                    try
                    {
                        lstNegativeFeedsOfProfile = _FeedSentimentalAnalysisRepository.getAllNegativeFeedsOfProfile(item.ProfileId);
                        lstNegativeFeedsOfAllProfile.AddRange(lstNegativeFeedsOfProfile);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                return new JavaScriptSerializer().Serialize(lstNegativeFeedsOfAllProfile);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string updateAssignedStatus(string Id, string AssignedUserId)
        {
            int ret = 0;
            try
            {
                ret = _FeedSentimentalAnalysisRepository.updateAssignedStatus(Guid.Parse(Id), Guid.Parse(AssignedUserId));
                if (ret == 1)
                {
                    return "Updated";
                }
                else
                {
                    return "Failure";
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getNegativeFeedsOfUser(string UserId)
        {
            List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfUser = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
            try
            {
                lstNegativeFeedsOfUser = _FeedSentimentalAnalysisRepository.getNegativeFeedsOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstNegativeFeedsOfUser);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTicketsofGroup(string GroupId, string UserId)
        {
            string AssignedUser = string.Empty;
            try
            {
                GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
                FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
                TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
                List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfUser = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();
                List<Domain.Socioboard.Domain.GroupProfile> objGroupProfile = new List<Domain.Socioboard.Domain.GroupProfile>();
                List<FBTwitterFeeds> objListFBTwitterFeeds = new List<FBTwitterFeeds>();

                objGroupProfile = objGroupProfileRepository.getAllGroupProfiles(Guid.Parse(UserId), Guid.Parse(GroupId));

                if (objGroupProfile.Count > 0)
                {
                    lstNegativeFeedsOfUser = _FeedSentimentalAnalysisRepository.getAllNegativeFeedsOfUser(Guid.Parse(UserId));
                }
                else
                {
                    lstNegativeFeedsOfUser = _FeedSentimentalAnalysisRepository.getNegativeFeedsOfUser(Guid.Parse(UserId));
                }
                if (lstNegativeFeedsOfUser != null)
                {
                    foreach (var item in lstNegativeFeedsOfUser)
                    {
                        FBTwitterFeeds objFBTwitterFeeds = new FBTwitterFeeds();
                        UserRepository objUserRepository = new UserRepository();
                        Domain.Socioboard.Domain.User user = objUserRepository.getUsersById(item.AssigneUserId);
                        if (objGroupProfileRepository.checkProfileExistsingroup(Guid.Parse(GroupId), item.ProfileId))
                        {
                            string Network = item.Network;
                            if (Network == "facebook")
                            {
                                Domain.Socioboard.Domain.FacebookFeed facebookfeed = objFacebookFeedRepository.getFacebookFeedByProfileId(item.ProfileId, item.FeedId);
                                if (facebookfeed != null)
                                {
                                    objFBTwitterFeeds.FacebookFeed = facebookfeed;
                                }
                            }
                            if (Network == "twitter")
                            {
                                Domain.Socioboard.Domain.TwitterFeed twtfeed = objTwitterFeedRepository.getTwitterFeedByProfileId(item.ProfileId, item.FeedId);
                                if (twtfeed != null)
                                {
                                    objFBTwitterFeeds.TwitterFeed = twtfeed;
                                }
                            }
                            if (objFBTwitterFeeds.TwitterFeed != null)
                            {
                                try
                                {
                                    objFBTwitterFeeds.TicketNo = item.TicketNo;
                                    if (user != null)
                                    {
                                        objFBTwitterFeeds.AssignedUser = user.UserName;
                                    }
                                    else
                                    {
                                        objFBTwitterFeeds.AssignedUser = "";
                                    }
                                    objListFBTwitterFeeds.Add(objFBTwitterFeeds);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                            if (objFBTwitterFeeds.FacebookFeed != null)
                            {
                                try
                                {
                                    objFBTwitterFeeds.TicketNo = item.TicketNo;
                                    if (user != null)
                                    {
                                        objFBTwitterFeeds.AssignedUser = user.UserName;
                                    }
                                    else
                                    {
                                        objFBTwitterFeeds.AssignedUser = "";
                                    }
                                    objListFBTwitterFeeds.Add(objFBTwitterFeeds);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.StackTrace);
                                }
                            }
                        }
                    } 
                }
               
                return new JavaScriptSerializer().Serialize(objListFBTwitterFeeds);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "SomethingWentWrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string FeedSentiment(string startedIndex, string count)
        {

            List<Domain.Socioboard.Domain.InboxMessages> lstMessages = _InboxMessagesRepository.GetMentionAndRetweets(Int32.Parse(startedIndex),Int32.Parse(count));

            if (lstMessages.Count == 0)
            {
                return "no_data";
            }

            string readKeys = ConfigurationManager.AppSettings["ReadKey"];
            string[] arrReadKey = readKeys.Split(',');
            Random r = new Random();


            foreach (var item in lstMessages)
            {
                try
                {
                    int i = r.Next(0, arrReadKey.Length);

                    string Message = item.Message;

                    var JData = GetSentiments(arrReadKey[i], Message);

                    string negative = JData["cls1"]["negative"].ToString();
                    string positive = JData["cls1"]["positive"].ToString();

                    item.Negative = double.Parse(negative, CultureInfo.InvariantCulture.NumberFormat);
                    item.Positive = double.Parse(positive, CultureInfo.InvariantCulture.NumberFormat);

                    int j = _InboxMessagesRepository.UpdateMessageSentiments(item);
                }
                catch (Exception wx)
                {
                    logger.Error(wx.StackTrace);
                    logger.Error(wx.Message);
                    return "";
                }

            }

            return "Successfully updated";

        }

        [WebMethod]
        public string FacebookFeedSentimet(string startedIndex, string count)
        {
            var ret = FacebookFeedRepo.Find<Domain.Socioboard.Domain.MongoFacebookFeed>(t => !string.IsNullOrEmpty(t.FeedDescription));
            var task =Task.Run(async () => 
            {
                return await ret;
            });
            IList<Domain.Socioboard.Domain.MongoFacebookFeed> _lstMongoFacebookFeed = task.Result;
            List<Domain.Socioboard.Domain.MongoFacebookFeed> lstMongoFacebookFeed = _lstMongoFacebookFeed.Where(t => t.Positive == 0).GroupBy(x => x.FeedId).Select(g => g.First()).ToList();
             lstMongoFacebookFeed =lstMongoFacebookFeed.OrderByDescending(t => t.FeedDate).Skip(Int32.Parse(startedIndex)).Take(Int32.Parse(count)).ToList();
            string readKeys = ConfigurationManager.AppSettings["ReadKey"];
            string[] arrReadKey = readKeys.Split(',');
            Random r = new Random();
            foreach (var item in lstMongoFacebookFeed)
            {
                try
                {
                    int i = r.Next(0, arrReadKey.Length);

                    string Message = item.FeedDescription;

                    var JData = GetSentiments(arrReadKey[i], Message);

                    string negative = JData["cls1"]["negative"].ToString();
                    string positive = JData["cls1"]["positive"].ToString();

                    item.Negative = double.Parse(negative, CultureInfo.InvariantCulture.NumberFormat);
                    item.Positive = double.Parse(positive, CultureInfo.InvariantCulture.NumberFormat);

                    FilterDefinition<BsonDocument> filter = new BsonDocument("FeedId", item.FeedId);
                    var update = Builders<BsonDocument>.Update.Set("Positive", item.Positive).Set("Negative", item.Negative);
                    FacebookFeedRepo.Update<Domain.Socioboard.Domain.MongoFacebookFeed>(update, filter);

                }
                catch (Exception wx)
                {
                    logger.Error(wx.StackTrace);
                    logger.Error(wx.Message);
                    return "";
                }

            }

            return "Successfully updated";
        }
        [WebMethod]
        public string FacebookMessageSentiment(string startedIndex, string count)
        {
            var ret = FacebookMessageRepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => !string.IsNullOrEmpty(t.Message));
            var task = Task.Run(async () => {

                return await ret;
            });
            IList<Domain.Socioboard.MongoDomain.FacebookMessage> _lstFacebookMessage = task.Result;
            List<Domain.Socioboard.MongoDomain.FacebookMessage> lstFacebookMessage = _lstFacebookMessage.Where(t => t.Positive == 0).GroupBy(t => t.MessageId).Select(g =>g.First()).ToList();
            lstFacebookMessage =lstFacebookMessage.OrderByDescending(t => t.MessageDate).Skip(Int32.Parse(startedIndex)).Take(Int32.Parse(count)).ToList();
            string readKeys = ConfigurationManager.AppSettings["ReadKey"];
            string[] arrReadKey = readKeys.Split(',');
            Random r = new Random();
            foreach (var item in lstFacebookMessage)
            {
                try
                {
                    int i = r.Next(0, arrReadKey.Length);

                    string Message = item.Message;

                    var JData = GetSentiments(arrReadKey[i], Message);

                    string negative = JData["cls1"]["negative"].ToString();
                    string positive = JData["cls1"]["positive"].ToString();

                    item.Negative = double.Parse(negative, CultureInfo.InvariantCulture.NumberFormat);
                    item.Positive = double.Parse(positive, CultureInfo.InvariantCulture.NumberFormat);

                    FilterDefinition<BsonDocument> filter = new BsonDocument("MessageId", item.MessageId);
                    var update = Builders<BsonDocument>.Update.Set("Positive", item.Positive).Set("Negative", item.Negative);
                    FacebookMessageRepo.Update<Domain.Socioboard.MongoDomain.FacebookMessage>(update, filter);

                }
                catch (Exception wx)
                {
                    logger.Error(wx.StackTrace);
                    logger.Error(wx.Message);
                    return "";
                }

            }
            return "Successfully updated";
        }
        [WebMethod]
        public string TwitterMessageSentiment(string startedIndex, string count)
        {
            var ret = TwitteMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => !string.IsNullOrEmpty(t.TwitterMsg));
            var task = Task.Run(async () =>
            {
                return await ret;
            });
            IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lstMongoFacebookFeed = task.Result;
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstMongoFacebookFeed = _lstMongoFacebookFeed.Where(t => t.Positive == 0).GroupBy(x => x.MessageId).Select(g => g.First()).ToList();
            lstMongoFacebookFeed = lstMongoFacebookFeed.OrderByDescending(t => t.MessageDate).Skip(Int32.Parse(startedIndex)).Take(Int32.Parse(count)).ToList();
            string readKeys = ConfigurationManager.AppSettings["ReadKey"];
            string[] arrReadKey = readKeys.Split(',');
            Random r = new Random();
            foreach (var item in lstMongoFacebookFeed)
            {
                try
                {
                    int i = r.Next(0, arrReadKey.Length);

                    string Message = item.TwitterMsg;

                    var JData = GetSentiments(arrReadKey[i], Message);

                    string negative = JData["cls1"]["negative"].ToString();
                    string positive = JData["cls1"]["positive"].ToString();

                    item.Negative = double.Parse(negative, CultureInfo.InvariantCulture.NumberFormat);
                    item.Positive = double.Parse(positive, CultureInfo.InvariantCulture.NumberFormat);

                    FilterDefinition<BsonDocument> filter = new BsonDocument("MessageId", item.MessageId);
                    var update = Builders<BsonDocument>.Update.Set("Positive", item.Positive).Set("Negative", item.Negative);
                    TwitteMessageRepo.Update<Domain.Socioboard.MongoDomain.TwitterMessage>(update, filter);
                }
                catch (Exception wx)
                {
                    logger.Error(wx.StackTrace);
                    logger.Error(wx.Message);
                    return "";
                }
                
            }
            return "Successfully updated";
        }

        [WebMethod]
        public string TwitterFeedSentiment(string startedIndex, string count)
        {
            var builder = Builders<Domain.Socioboard.MongoDomain.TwitterFeed>.Sort;
            var sort = builder.Descending(t => t.FeedDate);
            var ret = TwitterFeedRepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.Positive == 0, sort, 0, 20);
            var task = Task.Run(async () =>
            {
                return await ret;
            });
            IList<Domain.Socioboard.MongoDomain.TwitterFeed> _lstMongoFacebookFeed = task.Result;
            List<Domain.Socioboard.MongoDomain.TwitterFeed> lstMongoFacebookFeed = _lstMongoFacebookFeed.Where(t => t.Positive == 0).GroupBy(x => x.MessageId).Select(g => g.First()).ToList();
            lstMongoFacebookFeed = lstMongoFacebookFeed.OrderByDescending(t => t.FeedDate).Skip(Int32.Parse(startedIndex)).Take(Int32.Parse(count)).ToList();
            string readKeys = ConfigurationManager.AppSettings["ReadKey"];
            string[] arrReadKey = readKeys.Split(',');
            Random r = new Random();
            foreach (var item in lstMongoFacebookFeed)
            {
                try
                {
                    int i = r.Next(0, arrReadKey.Length);

                    string Message = item.Feed;

                    var JData = GetSentiments(arrReadKey[i], Message);

                    string negative = JData["cls1"]["negative"].ToString();
                    string positive = JData["cls1"]["positive"].ToString();

                    item.Negative = double.Parse(negative, CultureInfo.InvariantCulture.NumberFormat);
                    item.Positive = double.Parse(positive, CultureInfo.InvariantCulture.NumberFormat);

                    FilterDefinition<BsonDocument> filter = new BsonDocument("MessageId", item.MessageId);
                    var update = Builders<BsonDocument>.Update.Set("Positive", item.Positive).Set("Negative", item.Negative);
                    TwitterFeedRepo.Update<Domain.Socioboard.MongoDomain.TwitterFeed>(update, filter);
                    
                }
                catch (Exception wx)
                {
                    logger.Error(wx.StackTrace);
                    logger.Error(wx.Message);
                    return "";
                }
                
            }
            return "Successfully updated";
        }
        public string GoogleSearchData(string Url)
        {
            var jdata =GetGoogleSearchData(Url);
            foreach (var item in jdata["responseData"]["results"])
            {
                Domain.Socioboard.MongoDomain.ArticlesAndBlogs _ArticlesAndBlogs = new Domain.Socioboard.MongoDomain.ArticlesAndBlogs();
                _ArticlesAndBlogs.Id = ObjectId.GenerateNewId();
                try
                {
                    _ArticlesAndBlogs.VideoId = item["unescapedUrl"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.VideoId = "";
                }
                try
                {
                    _ArticlesAndBlogs.VideoUrl = item["cacheUrl"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.VideoUrl = "";
                }
                try
                {
                    _ArticlesAndBlogs.Title = item["titleNoFormatting"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.Title = "";

                }
                try
                {
                    _ArticlesAndBlogs.Description = item["content"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.Description = "";
                }
                try
                {
                    _ArticlesAndBlogs.Created_Time = (DateTime.Parse(item["publishedDate"].ToString())).ToUnixTimestamp();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.Created_Time = DateTime.UtcNow.ToUnixTimestamp();
                }
              
                _ArticlesAndBlogs.Url = Url;


                var ret = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t=>t.VideoId.Equals(_ArticlesAndBlogs.VideoId));
                var task = Task.Run(async () =>
                {

                    return await ret;
                });
                int count = task.Result.Count;
                if (count < 1)
                {
                    ArticlesAndBlogsRepo.Add(_ArticlesAndBlogs);
                }
            }
            return "Successfully updated";
        
        }

        public string GetYouTubeVideoData(string Url) 
        {
            var Jdata = GetYoutubeVideo(Url);
            foreach (var item in Jdata["responseData"]["results"])
            {
                Domain.Socioboard.MongoDomain.ArticlesAndBlogs _ArticlesAndBlogs = new Domain.Socioboard.MongoDomain.ArticlesAndBlogs();
                _ArticlesAndBlogs.Id = ObjectId.GenerateNewId();
              
                _ArticlesAndBlogs.Url = Url;
                try
                {
                    _ArticlesAndBlogs.Title = item["titleNoFormatting"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                    _ArticlesAndBlogs.Title = "";
                }
                try
                {
                    _ArticlesAndBlogs.Created_Time = (DateTime.Parse(item["published"].ToString())).ToUnixTimestamp();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.Created_Time = DateTime.UtcNow.ToUnixTimestamp();
                }
                try
                {
                    _ArticlesAndBlogs.Description = item["content"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.Description = "";
                }

                try
                {
                    _ArticlesAndBlogs.VideoId = item["url"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.VideoId = "";
                }
                try
                {
                    _ArticlesAndBlogs.VideoUrl = item["url"].ToString();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    logger.Error(ex.Message);
                    _ArticlesAndBlogs.VideoUrl = "";
                }
                var ret = ArticlesAndBlogsRepo.Find<Domain.Socioboard.MongoDomain.ArticlesAndBlogs>(t => t.VideoId.Equals(_ArticlesAndBlogs.VideoId));
                var task = Task.Run(async () =>
                {
                    return await ret;

                });
                int count = task.Result.Count;
                if (count < 1)
                {

                    ArticlesAndBlogsRepo.Add(_ArticlesAndBlogs);
                }
            }
            return "Successfully updated";
        }

        public Newtonsoft.Json.Linq.JObject GetYoutubeVideo(string url) {

            string youtubeposturl = "http://ajax.googleapis.com/ajax/services/search/video?v=1.0&gl=de&q="+url+"&rsz=8&start=5";
            string response = WebRequst(youtubeposturl);
            var jdata = Newtonsoft.Json.Linq.JObject.Parse(response);
            return jdata;
        }

        public Newtonsoft.Json.Linq.JObject GetGoogleSearchData(string url)
        {
            string _GetGoogleSearchRestUrl = "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&gl=de&q="+url+"&rsz=8&start=5";


            string response = WebRequst(_GetGoogleSearchRestUrl);

            var JData = Newtonsoft.Json.Linq.JObject.Parse(response);
            return JData;
        }

        public Newtonsoft.Json.Linq.JObject GetSentiments(string _ReadKey, string Message)
        {
            string _SentimentalRestUrl = "http://uclassify.com/browse/uClassify/Sentiment/ClassifyText?readkey=" + _ReadKey + "&text=" + HttpUtility.UrlEncode(Message) + "&removeHtml=true&output=json&version=1.01";

            string response = WebRequst(_SentimentalRestUrl);

            var JData = Newtonsoft.Json.Linq.JObject.Parse(response);
            return JData;
        }
        
        public string WebRequst(string Url)
        {

            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(Url);
                httpRequest.Method = "GET";
                httpRequest.ContentType = "application/json; charset=utf-8";
                HttpWebResponse httResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = httResponse.GetResponseStream();
                StreamReader responseStreamReader = new StreamReader(responseStream, System.Text.Encoding.Default);
                string pageContent = responseStreamReader.ReadToEnd();
                responseStreamReader.Close();
                responseStream.Close();
                httResponse.Close();
                return pageContent;
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
                return "";
            }
        }

    }
}
