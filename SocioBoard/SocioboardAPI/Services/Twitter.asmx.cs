using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using Hammock.Authentication.OAuth;
using Hammock;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using Newtonsoft.Json.Linq;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Twitter.Core.TimeLineMethods;
using Api.Socioboard.Helper;
using GlobusTwitterLib.Twitter.Core.TweetMethods;
using log4net;
using Api.Socioboard.Model;
using SocioBoard.Model;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

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
    public class Twitter : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(Linkedin));
        GroupsRepository objGroupsRepository = new GroupsRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        TwitterAccountRepository objTwitterAccountRepository = new TwitterAccountRepository();
        Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
        Domain.Socioboard.MongoDomain.TwitterFeed objTwitterFeed;
        TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
        Domain.Socioboard.Domain.TwitterStats objTwitterStats;
        TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
        Domain.Socioboard.MongoDomain.TwitterMessage objTwitterMessage;
        TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();
        Domain.Socioboard.Domain.TwitterDirectMessages objTwitterDirectMessages;
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;
        InboxMessagesRepository objInboxMessagesRepository = new InboxMessagesRepository();
        MongoRepository twitterMessageRepo = new MongoRepository("TwitterMessage");
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterRedirectUrl(string consumerKey, string consumerSecret, string CallBackUrl)
        {
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;

            OAuthCredentials credentials = new OAuthCredentials()
            {
                Type = OAuthType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                CallbackUrl = CallBackUrl
            };

            // Use Hammock to create a rest client
            var client = new RestClient
            {
                Authority = "https://api.twitter.com/oauth",
                Credentials = credentials,
            };


            // Use Hammock to create a request
            var request = new RestRequest
            {
                Path = "request_token"
            };

            // Get the response from the request
            var response = client.Request(request);

            var collection = HttpUtility.ParseQueryString(response.Content);
            //string str = collection[1].ToString();
            //HttpContext.Current.Session["requestSecret"] = collection[1];
            string rest = "https://api.twitter.com/oauth/authorize?oauth_token=" + collection[0] + "~" + collection[1];

            return rest;
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddTwitterAccount(string client_id, string client_secret, string redirect_uri, string UserId, string GroupId, string requestToken, string requestSecret, string requestVerifier)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

                string ret = string.Empty;
                Users userinfo = new Users();
                oAuthTwitter OAuth = new oAuthTwitter(client_id, client_secret, redirect_uri);
                OAuth.AccessToken = requestToken;
                OAuth.AccessTokenSecret = requestVerifier;
                OAuth.AccessTokenGet(requestToken, requestVerifier);
                JArray profile = userinfo.Get_Users_LookUp_ByScreenName(OAuth, OAuth.TwitterScreenName);

                if (profile != null)
                {
                    logger.Error("Twitter.asmx >> AddTwitterAccount >> Twitter profile : " + profile);
                }
                else
                {
                    logger.Error("Twitter.asmx >> AddTwitterAccount >> NULL Twitter profile : " + profile);
                }

                objTwitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
                TwitterUser twtuser;
                foreach (var item in profile)
                {
                    #region Add Twitter Account
                    try
                    {
                        objTwitterAccount.FollowingCount = Convert.ToInt32(item["friends_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterAccount.FollowersCount = Convert.ToInt32(item["followers_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    objTwitterAccount.Id = Guid.NewGuid();
                    objTwitterAccount.IsActive = true;
                    objTwitterAccount.OAuthSecret = OAuth.AccessTokenSecret;
                    objTwitterAccount.OAuthToken = OAuth.AccessToken;
                    try
                    {
                        objTwitterAccount.ProfileImageUrl = item["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }
                    try
                    {
                        objTwitterAccount.ProfileUrl = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterAccount.TwitterUserId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception er)
                    {
                        try
                        {
                            objTwitterAccount.TwitterUserId = item["id"].ToString().TrimStart('"').TrimEnd('"');
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.StackTrace);
                        }
                        Console.WriteLine(er.StackTrace);

                    }

                    try
                    {
                        objTwitterAccount.TwitterScreenName = item["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    objTwitterAccount.UserId = Guid.Parse(UserId);
                    #endregion
                    if (!objTwitterAccountRepository.checkTwitterUserExists(objTwitterAccount.TwitterUserId, Guid.Parse(UserId)))
                    {
                        objTwitterAccountRepository.addTwitterkUser(objTwitterAccount);
                        #region Add TeamMemberProfile
                        Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                        objTeamMemberProfile.Id = Guid.NewGuid();
                        objTeamMemberProfile.TeamId = objTeam.Id;
                        objTeamMemberProfile.Status = 1;
                        objTeamMemberProfile.ProfileType = "twitter";
                        objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                        objTeamMemberProfile.ProfileId = objTwitterAccount.TwitterUserId;

                        objTeamMemberProfile.ProfileName = objTwitterAccount.TwitterScreenName;
                        objTeamMemberProfile.ProfilePicUrl = objTwitterAccount.ProfileImageUrl;

                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                        #endregion
                        #region SocialProfile
                        Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                        objSocialProfile.Id = Guid.NewGuid();
                        objSocialProfile.ProfileType = "twitter";
                        objSocialProfile.ProfileId = objTwitterAccount.TwitterUserId;
                        objSocialProfile.UserId = Guid.Parse(UserId);
                        objSocialProfile.ProfileDate = DateTime.Now;
                        objSocialProfile.ProfileStatus = 1;
                        #endregion
                        #region Add Twitter Stats
                        if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                        {
                            objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                        }
                        objTwitterStats = new Domain.Socioboard.Domain.TwitterStats();
                        Random rNum = new Random();
                        objTwitterStats.Id = Guid.NewGuid();
                        objTwitterStats.TwitterId = objTwitterAccount.TwitterUserId;
                        objTwitterStats.UserId = Guid.Parse(UserId);
                        objTwitterStats.FollowingCount = objTwitterAccount.FollowingCount;
                        objTwitterStats.FollowerCount = objTwitterAccount.FollowersCount;
                        objTwitterStats.Age1820 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.Age2124 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.Age2534 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.Age3544 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.Age4554 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.Age5564 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.Age65 = rNum.Next(objTwitterAccount.FollowersCount);
                        objTwitterStats.EntryDate = DateTime.Now;
                        if (!objTwtstats.checkTwitterStatsExists(objTwitterAccount.TwitterUserId, Guid.Parse(UserId)))
                        {
                            objTwtstats.addTwitterStats(objTwitterStats);
                        }
                        #endregion

                        #region AddTwitterFollowerCount
                        Domain.Socioboard.Domain.TwitterAccountFollowers objTwitterAccountFollowers = new Domain.Socioboard.Domain.TwitterAccountFollowers();
                        TwitterAccountFollowersRepository objTwitterAccountFollowersRepository = new TwitterAccountFollowersRepository();
                        objTwitterAccountFollowers.Id = Guid.NewGuid();
                        objTwitterAccountFollowers.UserId = Guid.Parse(UserId);
                        objTwitterAccountFollowers.EntryDate = DateTime.Now;
                        objTwitterAccountFollowers.FollowingsCount = Convert.ToInt32(item["friends_count"].ToString());
                        objTwitterAccountFollowers.FollowersCount = Convert.ToInt32(item["followers_count"].ToString());
                        try
                        {
                            objTwitterAccountFollowers.ProfileId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                        }
                        catch (Exception er)
                        {
                            try
                            {
                                objTwitterAccountFollowers.ProfileId = item["id"].ToString().TrimStart('"').TrimEnd('"');
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.StackTrace);
                            }
                            Console.WriteLine(er.StackTrace);

                        }
                        objTwitterAccountFollowersRepository.addTwitterAccountFollower(objTwitterAccountFollowers);
                        #endregion

                        ret = "Account Added Successfully";
                    }
                    else
                    {
                        ret = "Account already Exist !";
                    }
                }
                getTwitterMessages(UserId, OAuth);

                getUserRetweets(UserId, OAuth);


                //edited by avinash[24-03-15]

                //getTwitterEngagement(UserId, OAuth);

                getUserTweets(UserId, OAuth);

                getTwitterFeeds(UserId, OAuth);

                getTwitterDirectMessageSent(UserId, OAuth);
                getTwittwrDirectMessageRecieved(OAuth, UserId);
                getUserMentions(OAuth, objTwitterAccount.TwitterUserId, Guid.Parse(UserId));
                getUserFollowers(OAuth, objTwitterAccount.TwitterScreenName, objTwitterAccount.TwitterUserId, Guid.Parse(UserId));
                getUserRetweet(OAuth, objTwitterAccount.TwitterUserId, Guid.Parse(UserId));
                twitterrecentdetails(profile);
                //return ret;
                return new JavaScriptSerializer().Serialize(objTwitterAccount);
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterLogIn(string client_id, string client_secret, string redirect_uri, string requestToken, string requestSecret, string requestVerifier)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
            string ret = string.Empty;
            Users userinfo = new Users();
            oAuthTwitter OAuth = new oAuthTwitter(client_id, client_secret, redirect_uri);
            OAuth.AccessToken = requestToken;
            OAuth.AccessTokenSecret = requestVerifier;
            OAuth.AccessTokenGet(requestToken, requestVerifier);
            JArray profile = userinfo.Get_Users_LookUp_ByScreenName(OAuth, OAuth.TwitterScreenName);

            if (profile != null)
            {
                logger.Error("Twitter.asmx >> TwitterLogIn >> Twitter profile : " + profile);
            }
            else
            {
                logger.Error("Twitter.asmx >> TwitterLogIn >> NULL Twitter profile : " + profile);
            }
            string TwitterUserId = string.Empty;
            objTwitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
            Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
            foreach (var item in profile)
            {
                #region Login Twitter Account
                try
                {
                    objUser.ProfileUrl = item["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    TwitterUserId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception er)
                {
                    try
                    {
                        TwitterUserId = item["id"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    Console.WriteLine(er.StackTrace);
                }
                objUser.SocialLogin = "twitter_" + TwitterUserId;
                try
                {
                    objUser.UserName = item["name"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                }

                #endregion
            }
            return new JavaScriptSerializer().Serialize(objUser);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getTwitterFeeds(string UserId, oAuthTwitter OAuth)
        {
            //int J = 0;

            MongoRepository twitterfeedrepo = new MongoRepository("TwitterFeed");

            TwitterUser twtuser;
            #region Add Twitter User Feed

            twtuser = new TwitterUser();
            try
            {
                JArray Home_Timeline = twtuser.GetStatuses_Home_Timeline(OAuth);
                objTwitterFeed = new Domain.Socioboard.MongoDomain.TwitterFeed();
                foreach (var item in Home_Timeline)
                {
                    //objTwitterFeed.UserId = Guid.Parse(UserId);
                    objTwitterFeed.Type = "twt_feeds";
                    try
                    {
                        objTwitterFeed.Feed = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.ScreenName = objTwitterAccount.TwitterScreenName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.ProfileId = objTwitterAccount.TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FeedDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"')).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.Id = ObjectId.GenerateNewId();
                        objTwitterFeed.strId = ObjectId.GenerateNewId().ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                   
                    try
                    {
                        objTwitterFeed.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterFeed.MediaUrl = item["extended_entities"]["media"][0]["media_url"].ToString();
                    }
                    catch (Exception ex)
                    {
                    }

                    var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.MessageId.Equals(objTwitterFeed.MessageId) && t.ProfileId.Equals(objTwitterFeed.ProfileId));
                    var task = Task.Run(async () =>
                            {
                                return await ret;
                            });
                            int count = task.Result.Count;

                            if (count < 1)
                            {
                                twitterfeedrepo.Add(objTwitterFeed);
                            }
                            
                    //if (!objTwitterFeedRepository.checkTwitterFeedExists(objTwitterFeed.MessageId))
                    //{
                    //    try
                    //    {
                    //        J++;
                    //        objTwitterFeedRepository.addTwitterFeed(objTwitterFeed);
                    //        logger.Error("getTwitterFeedsCount>>>"+J);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Console.WriteLine(ex.Message);
                    //        Console.WriteLine(ex.StackTrace);
                    //    }
                    //}
                    // Edited by Antima[20/12/2014]

                    //SentimentalAnalysis _SentimentalAnalysis = new SentimentalAnalysis();
                    //FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new FeedSentimentalAnalysisRepository();
                    //try
                    //{
                    //    //if (_FeedSentimentalAnalysisRepository.checkFeedExists(objTwitterFeed.ProfileId.ToString(), Guid.Parse(UserId), objTwitterFeed.Id.ToString()))
                    //    //{
                    //    //    if (!string.IsNullOrEmpty(objTwitterFeed.Feed))
                    //    //    {
                    //    //        string Network = "twitter";
                    //    //        _SentimentalAnalysis.GetPostSentimentsFromUclassify(Guid.Parse(UserId), objTwitterFeed.ProfileId, objTwitterFeed.MessageId, objTwitterFeed.Feed, Network);
                    //    //    }
                    //    //}
                    //}
                    //catch (Exception)
                    //{

                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error("twtuser.GetStatuses_Home_Timeline ex.StackTrace >> " + ex.StackTrace);
                logger.Error("twtuser.GetStatuses_Home_Timeline ex.Message >> " + ex.Message);
            }
            #endregion
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        private void getUserTweets(string UserId, oAuthTwitter OAuth)
        {
            #region Add User Tweets
            try
            {
                TwitterUser twtuser = new TwitterUser();
                JArray Timeline = twtuser.GetStatuses_User_Timeline(OAuth);
                //TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                //TwitterMessage twtmsg = new TwitterMessage();
                objTwitterMessage = new Domain.Socioboard.MongoDomain.TwitterMessage();
                foreach (var item in Timeline)
                {
                    //objTwitterMessage.UserId = Guid.Parse(UserId);
                    objTwitterMessage.Id = ObjectId.GenerateNewId();
                    objTwitterMessage.Type = "twt_usertweets";
                    try
                    {
                        objTwitterMessage.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.ScreenName = objTwitterAccount.TwitterScreenName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.ProfileId = objTwitterAccount.TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.MessageDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"')).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    
                    try
                    {
                        objTwitterMessage.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    
                    try
                    {
                        objTwitterMessage.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.MessageId.Equals(objTwitterMessage.MessageId));
                    var task = Task.Run( async () => {
                        return await ret;
                    });
                    int count = task.Result.Count;
                    if (count < 1)
                    {
                        twitterMessageRepo.Add(objTwitterMessage);
                    }
                    //if (!objTwitterMessageRepository.checkTwitterMessageExists(objTwitterMessage.MessageId))
                    //{
                    //    objTwitterMessageRepository.addTwitterMessage(objTwitterMessage);
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error("twtuser.GetStatuses_User_Timeline ex.StackTrace >> " + ex.StackTrace);
                logger.Error("twtuser.GetStatuses_User_Timeline ex.Message >> " + ex.Message);
            }
            #endregion
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public void getTwitterEngagement(string UserId, oAuthTwitter OAuth)
        //{
        //    #region Add Twitter Engagement
        //    Tweet objTweet = new Tweet();
        //    TwitterEngagement objTwitterEngagement = new TwitterEngagement();
        //    TwitterEngagementRepository objTwitterEngagementRepository = new TwitterEngagementRepository();

        //    //TwitterMessageRepository objTwitterMessageRepository=new TwitterMessageRepository ();
        //    List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmessage = objTwitterMessageRepository.getAllTwitterMessagesRetweet(Guid.Parse(UserId), objTwitterAccount.TwitterUserId);
        //    List<Domain.Socioboard.Domain.TwitterMessage> tsttwtwmessagetweet = objTwitterMessageRepository.getAllTwitterMessagestweet(Guid.Parse(UserId), objTwitterAccount.TwitterUserId);
        //    foreach (var itemmsg in lsttwtmessage)
        //    {

        //        JArray EngagementData = objTweet.Get_Statuses_RetweetsById(OAuth, itemmsg.MessageId);
        //        foreach (var item in EngagementData)
        //        {
        //            objTwitterEngagement.Id = Guid.NewGuid();
        //            objTwitterEngagement.UserId = Guid.Parse(UserId);
        //            objTwitterEngagement.EntryDate = DateTime.Now;
        //            objTwitterEngagement.ProfileId = objTwitterAccount.TwitterUserId;
        //            try
        //            {
        //                objTwitterEngagement.RetweetCount = item["retweet_count"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                objTwitterEngagement.ReplyCount = objTwitterMessageRepository.getReplyCountbyProfileId(objTwitterAccount.TwitterUserId, Guid.Parse(UserId)).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                objTwitterEngagement.Engagement = ((Int32.Parse(objTwitterEngagement.ReplyCount) + Int32.Parse(objTwitterEngagement.RetweetCount)) * 100 / (objTwitterAccount.FollowersCount)).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                objTwitterEngagement.Engagement = "0".ToString();
        //                Console.WriteLine(ex.StackTrace);
        //            } try
        //            {
        //                objTwitterEngagement.Influence = (((tsttwtwmessagetweet.Count)) * 100 / (objTwitterAccount.FollowersCount)).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                objTwitterEngagement.Influence = "0".ToString();
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            objTwitterEngagementRepository.Add(objTwitterEngagement);

        //        }
        //    }
        //    #endregion
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserRetweets(string UserId, oAuthTwitter OAuth)
        {
            TwitterUser twtuser;
            #region Add User Retweet
            twtuser = new TwitterUser();
            try
            {
                JArray Retweet = null;
                try
                {
                    Retweet = twtuser.GetStatuses_Retweet_Of_Me(OAuth);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error("twtuser.GetStatuses_Retweet_Of_Me ex.StackTrace >> " + ex.StackTrace);
                    logger.Error("twtuser.GetStatuses_Retweet_Of_Me ex.Message >> " + ex.Message);
                }
                objTwitterMessage = new Domain.Socioboard.MongoDomain.TwitterMessage();
                foreach (var item in Retweet)
                {
                    //objTwitterMessage.UserId = Guid.Parse(UserId);
                    objTwitterMessage.Type = "twt_retweets";
                    objTwitterMessage.Id = ObjectId.GenerateNewId();

                    try
                    {
                        objTwitterMessage.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.MessageDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"')).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    } try
                    {
                        objTwitterMessage.ProfileId = objTwitterAccount.TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.MessageId.Equals(objTwitterMessage.MessageId));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int count = task.Result.Count;
                    if (count < 1)
                    {
                        twitterMessageRepo.Add(objTwitterMessage);
                    }

                    //if (!objTwitterMessageRepository.checkTwitterMessageExists(objTwitterMessage.MessageId))
                    //{
                    //    objTwitterMessageRepository.addTwitterMessage(objTwitterMessage);
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error("twtuser.GetStatuses_Retweet_Of_Me ex.StackTrace >> " + ex.StackTrace);
                logger.Error("twtuser.GetStatuses_Retweet_Of_Me ex.Message >> " + ex.Message);
            }
            #endregion
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getTwitterMessages(string UserId, oAuthTwitter OAuth)
        {
            int I = 0;
            TwitterUser twtuser;

            #region Add Twitter Messages
            twtuser = new TwitterUser();
            try
            {
                TimeLine tl = new TimeLine();
                JArray data = null;
                try
                {
                    data = tl.Get_Statuses_Mentions_Timeline(OAuth);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    logger.Error("tl.Get_Statuses_Mentions_Timeline ex.StackTrace >> " + ex.StackTrace);
                    logger.Error("tl.Get_Statuses_Mentions_Timeline ex.Message >> " + ex.Message);
                }
                objTwitterMessage = new Domain.Socioboard.MongoDomain.TwitterMessage();
                foreach (var item in data)
                {
                    //objTwitterMessage.UserId = Guid.Parse(UserId);
                    objTwitterMessage.Type = "twt_mentions";
                    objTwitterMessage.Id = ObjectId.GenerateNewId();

                    try
                    {
                        objTwitterMessage.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.MessageDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"')).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterMessage.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    } try
                    {
                        objTwitterMessage.ProfileId = objTwitterAccount.TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.ScreenName = item["user"]["screen_name"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.MessageId.Equals(objTwitterMessage.MessageId));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int count = task.Result.Count;
                    if (count < 1)
                    {
                        twitterMessageRepo.Add(objTwitterMessage);
                    }
         
                    //if (!objTwitterMessageRepository.checkTwitterMessageExists(objTwitterMessage.MessageId))
                    //{
                    //    I++;
                    //    objTwitterMessageRepository.addTwitterMessage(objTwitterMessage);
                    //    logger.Error("getTwitterMessagesCount>>>"+I);
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error("tl.Get_Statuses_Mentions_Timeline ex.StackTrace >> " + ex.StackTrace);
                logger.Error("tl.Get_Statuses_Mentions_Timeline ex.Message >> " + ex.Message);
            }
            #endregion
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public String getTwitterData(string UserId, string twitterid)
        {
            string ret = string.Empty;
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;

                Guid userId = Guid.Parse(UserId);
                oAuthTwitter OAuth = new oAuthTwitter(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                TwitterAccountRepository objTwtRepo = new TwitterAccountRepository();


                List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = objTwtRepo.getAllTwitterAccountsOfUser(userId);
                foreach (Domain.Socioboard.Domain.TwitterAccount itemTwt in lstTwitterAccount)
                {
                    string profileId = string.Empty;

                    OAuth.AccessToken = itemTwt.OAuthToken;
                    OAuth.AccessTokenSecret = itemTwt.OAuthSecret;

                    getUserProile(OAuth, itemTwt.TwitterUserId, userId);
                    //getUserTweets(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId);
                    getUserTweets(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId, itemTwt);
                    //gettwitterengagement(OAuth, userId, itemTwt);
                    getUserFeed(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId);

                    getTwitterDirectMessageSent(UserId, OAuth);
                    getTwittwrDirectMessageRecieved(OAuth, UserId);
                    getUserMentions(OAuth, itemTwt.TwitterUserId, Guid.Parse(UserId));
                    getUserFollowersData(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, Guid.Parse(UserId));
                    getUserRetweet(OAuth, itemTwt.TwitterUserId, Guid.Parse(UserId));
                    getTwitterFollowerData(OAuth, itemTwt.TwitterUserId);

                    AddTwitteruserFollower(OAuth, Guid.Parse(UserId), itemTwt.TwitterUserId, itemTwt.TwitterScreenName);
                    #region UpdateTeammemberprofile
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    objTeamMemberProfile.ProfileName = itemTwt.TwitterScreenName;
                    objTeamMemberProfile.ProfilePicUrl = itemTwt.ProfileImageUrl;
                    objTeamMemberProfile.ProfileId = itemTwt.TwitterUserId;
                    objTeamMemberProfileRepository.updateTeamMemberbyprofileid(objTeamMemberProfile);

                    #endregion
                    //Domain.Socioboard.Domain.TwitterAccount _TwitterAccount = objTwtRepo.GetUserInformation(itemTwt.UserId, itemTwt.TwitterUserId);
                    //if (_TwitterAccount != null)
                    //    getTwitterStats(_TwitterAccount);


                }
                return "twitter Info Updated Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.Message);
            }
            return ret;

        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string SheduleTwitterMessage(string TwitterId, string UserId, string sscheduledmsgguid)
        //{


        //    string str = string.Empty;
        //    string ret = string.Empty;
        //    bool rt = false;
        //    try
        //    {

        //        oAuthTwitter OAuthTwt = new oAuthTwitter();
        //        objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
        //        objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(UserId), TwitterId);


        //        OAuthTwt.CallBackUrl = System.Configuration.ConfigurationSettings.AppSettings["callbackurl"];
        //        OAuthTwt.ConsumerKey = System.Configuration.ConfigurationSettings.AppSettings["consumerKey"];
        //        OAuthTwt.ConsumerKeySecret = System.Configuration.ConfigurationSettings.AppSettings["consumerSecret"];
        //        OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
        //        OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
        //        OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
        //        OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;


        //        #region For Testing
        //        // For Testing 

        //        //OAuthTwt.ConsumerKey = "udiFfPxtCcwXWl05wTgx6w";
        //        //OAuthTwt.ConsumerKeySecret = "jutnq6N32Rb7cgbDSgfsrUVgRQKMbUB34yuvAfCqTI";
        //        //OAuthTwt.AccessToken = "1904022338-Ao9chvPouIU8ejE1HMG4yJsP3hOgEoXJoNRYUF7";
        //        //OAuthTwt.AccessTokenSecret = "Wj93a8csVFfaFS1MnHjbmbPD3V6DJbhEIf4lgSAefORZ5";
        //        //OAuthTwt.TwitterScreenName = "";
        //        //OAuthTwt.TwitterUserId = ""; 
        //        #endregion

        //        if (objTwitterAccount != null)
        //        {
        //            try
        //            {
        //                //TwitterUser twtuser = new TwitterUser();
        //                string message = objScheduledMessage.ShareMessage;
        //                string picurl = objScheduledMessage.PicUrl;
        //                //if (string.IsNullOrEmpty(objScheduledMessage.ShareMessage))
        //                //{
        //                //    objScheduledMessage.ShareMessage = "There is no data in Share Message !";
        //                //}
        //                if (string.IsNullOrEmpty(message) && string.IsNullOrEmpty(picurl))
        //                {
        //                    str = "There is no data in Share Message !";
        //                }
        //                else
        //                {
        //                    JArray post = new JArray();
        //                    try
        //                    {
        //                        Tweet twt = new Tweet();
        //                        if (!string.IsNullOrEmpty(picurl))
        //                        {
        //                            PhotoUpload ph = new PhotoUpload();
        //                            string res = string.Empty;
        //                            rt = ph.NewTweet(picurl, message, OAuthTwt, ref res);
        //                        }
        //                        else
        //                        {
        //                            post = twt.Post_Statuses_Update(OAuthTwt, message);
        //                            ret = post[0]["id_str"].ToString();
        //                        }
        //                        //post = twtuser.Post_Status_Update(OAuthTwt, objScheduledMessage.ShareMessage);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine(ex.StackTrace);
        //                        ret = "";
        //                    }
        //                    if (!string.IsNullOrEmpty(ret) || rt == true)
        //                    {
        //                        str = "Message post on twitter for Id :" + objTwitterAccount.TwitterUserId + " and Message: " + objScheduledMessage.ShareMessage;
        //                        ScheduledMessage schmsg = new ScheduledMessage();
        //                        schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
        //                    }
        //                    else
        //                    {
        //                        str = "Message not posted";
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //                str = ex.Message;
        //            }
        //        }
        //        else
        //        {
        //            str = "facebook account not found for id" + objScheduledMessage.ProfileId;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        str = ex.Message;
        //    }
        //    return str;
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SheduleTwitterMessage(string TwitterId, string UserId, string sscheduledmsgguid)
        {


            string str = string.Empty;
            string ret = string.Empty;
            bool rt = false;
            try
            {

                oAuthTwitter OAuthTwt = new oAuthTwitter();
                objScheduledMessage = objScheduledMessageRepository.GetScheduledMessageDetails(Guid.Parse(sscheduledmsgguid));
                objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(UserId), TwitterId);

                //OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
                //OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
                //OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
                //OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;

                str = PostTwitterMessage(objScheduledMessage.ShareMessage, objTwitterAccount.TwitterUserId, UserId, objScheduledMessage.PicUrl, true, sscheduledmsgguid);

                #region Commented

                //#region For Testing
                //// For Testing 

                ////OAuthTwt.ConsumerKey = "udiFfPxtCcwXWl05wTgx6w";
                ////OAuthTwt.ConsumerKeySecret = "jutnq6N32Rb7cgbDSgfsrUVgRQKMbUB34yuvAfCqTI";
                ////OAuthTwt.AccessToken = "1904022338-Ao9chvPouIU8ejE1HMG4yJsP3hOgEoXJoNRYUF7";
                ////OAuthTwt.AccessTokenSecret = "Wj93a8csVFfaFS1MnHjbmbPD3V6DJbhEIf4lgSAefORZ5";
                ////OAuthTwt.TwitterScreenName = "";
                ////OAuthTwt.TwitterUserId = ""; 
                //#endregion

                //if (objTwitterAccount != null)
                //{
                //    try
                //    {
                //        //TwitterUser twtuser = new TwitterUser();
                //        string message = objScheduledMessage.ShareMessage;
                //        string picurl = objScheduledMessage.PicUrl;
                //        //if (string.IsNullOrEmpty(objScheduledMessage.ShareMessage))
                //        //{
                //        //    objScheduledMessage.ShareMessage = "There is no data in Share Message !";
                //        //}
                //        if (string.IsNullOrEmpty(message) && string.IsNullOrEmpty(picurl))
                //        {
                //            str = "There is no data in Share Message !";
                //        }
                //        else
                //        {
                //            JArray post = new JArray();
                //            try
                //            {
                //                Tweet twt = new Tweet();
                //                if (!string.IsNullOrEmpty(picurl))
                //                {
                //                    PhotoUpload ph = new PhotoUpload();
                //                    string res = string.Empty;
                //                    rt = ph.NewTweet(picurl, message, OAuthTwt, ref res);
                //                }
                //                else
                //                {
                //                    post = twt.Post_Statuses_Update(OAuthTwt, message);
                //                    ret = post[0]["id_str"].ToString();
                //                }
                //                //post = twtuser.Post_Status_Update(OAuthTwt, objScheduledMessage.ShareMessage);
                //            }
                //            catch (Exception ex)
                //            {
                //                Console.WriteLine(ex.StackTrace);
                //                ret = "";
                //            }
                //            if (!string.IsNullOrEmpty(ret) || rt == true)
                //            {
                //                str = "Message post on twitter for Id :" + objTwitterAccount.TwitterUserId + " and Message: " + objScheduledMessage.ShareMessage;
                //                ScheduledMessage schmsg = new ScheduledMessage();
                //                schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                //            }
                //            else
                //            {
                //                str = "Message not posted";
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.StackTrace);
                //        str = ex.Message;
                //    }
                //}
                //else
                //{
                //    str = "facebook account not found for id" + objScheduledMessage.ProfileId;
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                str = ex.Message;
            }
            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserProile(oAuthTwitter OAuth, string TwitterScreenName, Guid userId)
        {
            try
            {
                Users userinfo = new Users();
                //TwitterAccount twitterAccount = new TwitterAccount();
                Domain.Socioboard.Domain.TwitterAccount twitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
                TwitterAccountRepository twtrepo = new TwitterAccountRepository();
                JArray profile = userinfo.Get_Users_LookUp(OAuth, TwitterScreenName);
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
                    twitterAccount.UserId = userId;

                    if (twtrepo.checkTwitterUserExists(twitterAccount.TwitterUserId, userId))
                    {
                        twtrepo.updateTwitterUser(twitterAccount);
                    }

                    try
                    {
                        twitterrecentdetails(profile);
                        //getTwitterStats(twitterAccount);
                    }
                    catch { }
                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public void getUserTweets(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId)
        //{

        //    try
        //    {
        //        TwitterUser twtuser = new TwitterUser();
        //        JArray data = twtuser.GetStatuses_User_Timeline(OAuth);
        //        TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
        //        // TwitterMessage twtmsg = new TwitterMessage();
        //        Domain.Socioboard.Domain.TwitterMessage ObjTwitterMessage = new Domain.Socioboard.Domain.TwitterMessage();
        //        foreach (var item in data)
        //        {
        //            ObjTwitterMessage.UserId = userId;
        //            ObjTwitterMessage.Type = "twt_usertweets";
        //            try
        //            {
        //                ObjTwitterMessage.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.ScreenName = TwitterScreenName;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.ProfileId = TwitterUserId;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.MessageDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.Id = Guid.NewGuid();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                ObjTwitterMessage.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            ObjTwitterMessage.EntryDate = DateTime.Now;
        //            try
        //            {
        //                ObjTwitterMessage.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            if (!twtmsgrepo.checkTwitterMessageExists(ObjTwitterMessage.ProfileId, ObjTwitterMessage.UserId, ObjTwitterMessage.MessageId))
        //            {
        //                twtmsgrepo.addTwitterMessage(ObjTwitterMessage);
        //            }
        //            #region Add Twitter Engagement
        //            Tweet objTweet = new Tweet();
        //            TwitterEngagement objTwitterEngagement = new TwitterEngagement();
        //            TwitterEngagementRepository objTwitterEngagementRepository = new TwitterEngagementRepository();
        //            //TwitterMessageRepository objTwitterMessageRepository=new TwitterMessageRepository ();
        //            List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmessage = objTwitterMessageRepository.getAllTwitterMessagesRetweet(userId, objTwitterAccount.TwitterUserId);
        //            foreach (var itemmsg in lsttwtmessage)
        //            {

        //                JArray EngagementData = objTweet.Get_Statuses_RetweetsById(OAuth, itemmsg.MessageId);
        //                foreach (var itemEngagementData in EngagementData)
        //                {
        //                    objTwitterEngagement.Id = Guid.NewGuid();
        //                    objTwitterEngagement.UserId = userId;
        //                    objTwitterEngagement.EntryDate = DateTime.Now;
        //                    objTwitterEngagement.ProfileId = objTwitterAccount.TwitterUserId;
        //                    try
        //                    {
        //                        objTwitterEngagement.RetweetCount = item["retweet_count"].ToString().TrimStart('"').TrimEnd('"');
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objTwitterEngagement.ReplyCount = objTwitterMessageRepository.getReplyCountbyProfileId(objTwitterAccount.TwitterUserId, userId).ToString();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine(ex.StackTrace);
        //                    }
        //                    try
        //                    {
        //                        objTwitterEngagement.Engagement = ((Int32.Parse(objTwitterEngagement.ReplyCount) + Int32.Parse(objTwitterEngagement.RetweetCount)) * 100 / (objTwitterAccount.FollowersCount)).ToString();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine(ex.StackTrace);
        //                    }
        //                    objTwitterEngagementRepository.Add(objTwitterEngagement);

        //                }
        //            }
        //            #endregion

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserTweets(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId, Domain.Socioboard.Domain.TwitterAccount objTwitterAccount)
        {

            try
            {
                Users userinfo = new Users();
                TwitterUser twtuser = new TwitterUser();
                JArray data = twtuser.GetStatuses_User_Timeline(OAuth);
                //TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                // TwitterMessage twtmsg = new TwitterMessage();
                objTwitterMessage = new Domain.Socioboard.MongoDomain.TwitterMessage();
                foreach (var item in data)
                {
                   
                    objTwitterMessage.Type = "twt_usertweets";
                    try
                    {
                        objTwitterMessage.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.ScreenName = TwitterScreenName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.ProfileId = TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.MessageDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"')).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.Id = ObjectId.GenerateNewId();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterMessage.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    
                    try
                    {
                        objTwitterMessage.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.MessageId.Equals(objTwitterMessage.MessageId));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int count = task.Result.Count;
                    if (count < 1)
                    {
                        twitterMessageRepo.Add(objTwitterMessage);
                    }

                    //if (!twtmsgrepo.checkTwitterMessageExists(ObjTwitterMessage.ProfileId, ObjTwitterMessage.UserId, ObjTwitterMessage.MessageId))
                    //{
                    //    twtmsgrepo.addTwitterMessage(ObjTwitterMessage);
                    //}


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public void gettwitterengagement(oAuthTwitter OAuth, Guid userId, Domain.Socioboard.Domain.TwitterAccount objTwitterAccount)
        //{
        //    #region Add Twitter Engagement
        //    Tweet objTweet = new Tweet();
        //    TwitterEngagement objTwitterEngagement = new TwitterEngagement();
        //    TwitterEngagementRepository objTwitterEngagementRepository = new TwitterEngagementRepository();

        //    //TwitterMessageRepository objTwitterMessageRepository=new TwitterMessageRepository ();
        //    List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmessage = objTwitterMessageRepository.getAllTwitterMessagesRetweet(userId, objTwitterAccount.TwitterUserId);
        //    List<Domain.Socioboard.Domain.TwitterMessage> tsttwtwmessagetweet = objTwitterMessageRepository.getAllTwitterMessagestweet(userId, objTwitterAccount.TwitterUserId);
        //    foreach (var itemmsg in tsttwtwmessagetweet)
        //    {

        //        JArray EngagementData = objTweet.Get_Statuses_RetweetsById(OAuth, itemmsg.InReplyToStatusUserId);
        //        foreach (var Engagementitem in EngagementData)
        //        {
        //            objTwitterEngagement.Id = Guid.NewGuid();
        //            objTwitterEngagement.UserId = userId;
        //            objTwitterEngagement.EntryDate = DateTime.Now;
        //            objTwitterEngagement.ProfileId = objTwitterAccount.TwitterUserId;
        //            try
        //            {
        //                objTwitterEngagement.RetweetCount = Engagementitem["retweet_count"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                objTwitterEngagement.RetweetCount = "0";
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                objTwitterEngagement.ReplyCount = objTwitterMessageRepository.getReplyCountbyProfileId(objTwitterAccount.TwitterUserId, userId).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                objTwitterEngagement.Engagement = ((Int32.Parse(objTwitterEngagement.ReplyCount) + Int32.Parse(objTwitterEngagement.RetweetCount)) * 100 / (objTwitterAccount.FollowersCount)).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                objTwitterEngagement.Engagement = "0".ToString();
        //                Console.WriteLine(ex.StackTrace);
        //            } try
        //            {
        //                objTwitterEngagement.Influence = (((tsttwtwmessagetweet.Count)) * 100 / (objTwitterAccount.FollowersCount)).ToString();
        //            }
        //            catch (Exception ex)
        //            {
        //                objTwitterEngagement.Influence = "0".ToString();
        //                Console.WriteLine(ex.StackTrace);
        //            }

        //            objTwitterEngagementRepository.Add(objTwitterEngagement);

        //        }
        //    }
        //    #endregion
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserFeed(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId)
        {
            MongoRepository twitterfeedrepo = new MongoRepository("TwitterFeed");
            try
            {
                //User user = (User)Session["LoggedUser"];
                TwitterUser twtuser = new TwitterUser();
                JArray data = twtuser.GetStatuses_Home_Timeline(OAuth);
                Domain.Socioboard.MongoDomain.TwitterFeed objTwitterFeed;
                //TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
                //TwitterFeed twtmsg = new TwitterFeed();

                foreach (var item in data)
                {
                    objTwitterFeed = new Domain.Socioboard.MongoDomain.TwitterFeed();
                    //objTwitterFeed.UserId = userId;
                    objTwitterFeed.Type = "twt_feeds";
                    try
                    {
                        objTwitterFeed.Feed = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.ScreenName = TwitterScreenName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.ProfileId = TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FeedDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"')).ToString("yyyy/MM/dd HH:mm:ss");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.Id = ObjectId.GenerateNewId();
                        objTwitterFeed.strId = ObjectId.GenerateNewId().ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterFeed.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    //objTwitterFeed.EntryDate = DateTime.Now;
                    try
                    {
                        objTwitterFeed.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    var ret = twitterfeedrepo.Find<Domain.Socioboard.MongoDomain.TwitterFeed>(t => t.MessageId.Equals(objTwitterFeed.MessageId));
                    var task = Task.Run(async () =>
                    {
                        return await ret;
                    });
                    int count = task.Result.Count;

                    if (count < 1)
                    {
                        twitterfeedrepo.Add(objTwitterFeed);
                    }


                    // Edited by Antima[20/12/2014]

                    //SentimentalAnalysis _SentimentalAnalysis = new SentimentalAnalysis();
                    //FeedSentimentalAnalysisRepository _FeedSentimentalAnalysisRepository = new FeedSentimentalAnalysisRepository();
                    //try
                    //{
                    //    if (_FeedSentimentalAnalysisRepository.checkFeedExists(objTwitterFeed.ProfileId.ToString(), userId, objTwitterFeed.Id.ToString()))
                    //    {
                    //        if (!string.IsNullOrEmpty(objTwitterFeed.Feed))
                    //        {
                    //            string Network = "twitter";
                    //            _SentimentalAnalysis.GetPostSentimentsFromUclassify(userId, objTwitterFeed.ProfileId, objTwitterFeed.MessageId, objTwitterFeed.Feed, Network);
                    //        }
                    //    }
                    //}
                    //catch (Exception)
                    //{

                    //}

                    //if (!twtmsgrepo.checkTwitterFeedExists(objTwitterFeed.ProfileId, objTwitterFeed.UserId, objTwitterFeed.MessageId))
                    //{
                    //    twtmsgrepo.addTwitterFeed(objTwitterFeed);
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }


        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public void getTwitterStats(Domain.Socioboard.Domain.TwitterAccount twitterAccount)
        //{
        //    //TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
        //    TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
        //    TwitterMessageRepository objTweetMsgRepo = new TwitterMessageRepository();
        //    //TwitterStats objStats = new TwitterStats();
        //    Domain.Socioboard.Domain.TwitterStats objStats = new Domain.Socioboard.Domain.TwitterStats();
        //    Random rNum = new Random();
        //    objStats.Id = Guid.NewGuid();
        //    objStats.TwitterId = twitterAccount.TwitterUserId;
        //    objStats.UserId = twitterAccount.UserId;
        //    objStats.FollowingCount = twitterAccount.FollowingCount;
        //    objStats.FollowerCount = twitterAccount.FollowersCount;
        //    objStats.Age1820 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age2124 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age2534 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age3544 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age4554 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age5564 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age65 = rNum.Next(twitterAccount.FollowersCount);
        //    int replies = objTweetMsgRepo.getRepliesCount(twitterAccount.UserId, twitterAccount.TwitterUserId);
        //    int retweets = objTweetMsgRepo.getRetweetCount(twitterAccount.UserId, twitterAccount.TwitterUserId);
        //    if (twitterAccount.FollowersCount != 0)
        //        objStats.Engagement = (replies + retweets) / twitterAccount.FollowersCount;
        //    else
        //        objStats.Engagement = 0;

        //    objStats.EntryDate = DateTime.Now;
        //    if (!objTwtstats.checkTwitterStatsExists(twitterAccount.TwitterUserId, twitterAccount.UserId, objStats.FollowerCount, objStats.FollowingCount))
        //        objTwtstats.addTwitterStats(objStats);
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterComposeMessage(String message, String profileid, string userid, string currentdatetime, string picurl)
        {
            return PostTwitterMessage(message, profileid, userid, picurl, false);
        }

        private string PostTwitterMessage(String message, String profileid, string userid, string picurl, bool isScheduled, string sscheduledmsgguid = "")
        {
            bool rt = false;
            string ret = "";
            string str = "";
            int Twtsc = 0;
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(userid), profileid);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            if (!string.IsNullOrEmpty(picurl))
            {
                PhotoUpload ph = new PhotoUpload();
                //ph.Tweet(file, message, OAuthTwt);
                string res = string.Empty;
                rt = ph.NewTweet(picurl, message, OAuthTwt, ref res);
            }
            else
            {
                try
                {
                    JArray post = twt.Post_Statuses_Update(OAuthTwt, message);
                    ret = post[0]["id_str"].ToString();
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(ret) || rt == true)
            {
                if (isScheduled)
                {
                    Twtsc++;
                    str = "Message post on twitter for Id :" + objTwitterAccount.TwitterUserId + " and Message: " + objScheduledMessage.ShareMessage;
                    ScheduledMessage schmsg = new ScheduledMessage();
                    schmsg.UpdateScheduledMessageByMsgId(Guid.Parse(sscheduledmsgguid));
                    logger.Error("PostTwitterMessageCount>>>"+Twtsc);
                }
            }
            else
            {
                str = "Message not posted";
            }

            return str;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterComposeMessageRss(string message, string profileid, string rssFeedId)
        {
            string ret = "";
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.getUserInformation(profileid);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            MongoRepository rssfeedRepo = new MongoRepository("RssFeed");
            try
            {
                JArray post = twt.Post_Statuses_Update(OAuthTwt, message);
                //RssFeedsRepository objrssfeed = new RssFeedsRepository();
                //objrssfeed.updateFeedStatus(Guid.Parse(userid), message);

                var builders = Builders<BsonDocument>.Filter;
                FilterDefinition<BsonDocument> filter = builders.Eq("strId", rssFeedId);
                var update = Builders<BsonDocument>.Update.Set("Status", true);
                rssfeedRepo.Update<Domain.Socioboard.MongoDomain.RssFeed>(update, filter);

                return ret = "Messages Posted Successfully";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ret = "Message Could Not Posted";
            }
        }

        public void SetCofigDetailsForTwitter(oAuthTwitter OAuth)
        {
            OAuth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"];
            OAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
            OAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterRecentFollower(string userid)
        {
            List<Domain.Socioboard.Helper.TwitterRecentFollower> lstTwitterRecentFollower = new List<Domain.Socioboard.Helper.TwitterRecentFollower>();
            List<Domain.Socioboard.Domain.TwitterAccount> lstAccRepo = objTwitterAccountRepository.getAllTwitterAccountsOfUser(Guid.Parse(userid));
            oAuthTwitter oauth = null;
            Users twtUsers = new Users();
            foreach (Domain.Socioboard.Domain.TwitterAccount itemTwt in lstAccRepo)
            {
                oauth = new oAuthTwitter();
                oauth.AccessToken = itemTwt.OAuthToken;
                oauth.AccessTokenSecret = itemTwt.OAuthSecret;
                oauth.TwitterScreenName = itemTwt.TwitterScreenName;
                oauth.TwitterUserId = itemTwt.TwitterUserId;
                SetCofigDetailsForTwitter(oauth);
                JArray jarresponse = twtUsers.Get_Followers_ById(oauth, itemTwt.TwitterUserId);
                foreach (var item in jarresponse)
                {
                    int resposecount = 0;
                    if (item["ids"] != null)
                    {
                        foreach (var child in item["ids"])
                        {
                            if (resposecount < 2)
                            {
                                JArray userprofile = twtUsers.Get_Users_LookUp(oauth, child.ToString());
                                foreach (var items in userprofile)
                                {
                                    Domain.Socioboard.Helper.TwitterRecentFollower objTwitterRecentFollower = new Domain.Socioboard.Helper.TwitterRecentFollower();
                                    resposecount++;
                                    objTwitterRecentFollower.screen_name = items["screen_name"].ToString();
                                    objTwitterRecentFollower.name = items["name"].ToString();
                                    objTwitterRecentFollower.profile_image_url = items["profile_image_url"].ToString();
                                    lstTwitterRecentFollower.Add(objTwitterRecentFollower);
                                }
                            }
                        }
                    }
                }
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRecentFollower);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterReplyUpdate(string message, string userid, string profileid, string statusid)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(userid), profileid);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            JArray replypost = twt.Post_StatusesUpdate(OAuthTwt, message, statusid);
            return new JavaScriptSerializer().Serialize(replypost);
        }

        //-------vikash-----------//


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterReteet_post(string userid, string profileid, string messageid)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(userid), profileid);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            JArray retweetpost = twt.Post_Statuses_RetweetsById(OAuthTwt, messageid, "");
            if (retweetpost.HasValues == true)
            {
                return "succeess";
            }

            else
            {
                return "failuer";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterFavorite_post(string userid, string profileid, string messageid)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(userid), profileid);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            JArray favoritepost = twt.Post_favorites(OAuthTwt, messageid);
            if (favoritepost.HasValues == true)
            {
                return "succeess";
            }

            else
            {
                return "failuer";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SpamUser_post(string userid, string SpammerScreanName, string UserProfileId)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(userid), UserProfileId);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            JArray spampost = twt.Post_report_as_spammer(OAuthTwt, SpammerScreanName);
            if (spampost.HasValues == true)
            {
                return "succeess";
            }

            else
            {
                return "failuer";
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string TwitterProfileDetails(string userid, string profileid)
        //{
        //    List<Domain.Socioboard.Helper.TwitterProfileDetails> lstTwitterRecentFollower = new List<Domain.Socioboard.Helper.TwitterProfileDetails>();
        //    List<Domain.Socioboard.Domain.TwitterAccount> listallTwtaccount = objTwitterAccountRepository.getAllTwitterAccountsOfUser(Guid.Parse(userid));
        //    Domain.Socioboard.Helper.TwitterProfileDetails objTwtProfileDescription = new Domain.Socioboard.Helper.TwitterProfileDetails();
        //    oAuthTwitter OAuthTwt = new oAuthTwitter();
        //    foreach (Domain.Socioboard.Domain.TwitterAccount childnoe in listallTwtaccount)
        //    {
        //        OAuthTwt.AccessToken = childnoe.OAuthToken;
        //        OAuthTwt.AccessTokenSecret = childnoe.OAuthSecret;
        //        OAuthTwt.TwitterScreenName = childnoe.TwitterScreenName;
        //        OAuthTwt.TwitterUserId = childnoe.TwitterUserId;
        //        this.SetCofigDetailsForTwitter(OAuthTwt);
        //        if (CheckTwitterTokenByUserId(OAuthTwt, profileid))
        //        {
        //            break;
        //        }
        //    }
        //    Users userinfo = new Users();
        //    JArray userlookup = userinfo.Get_Users_LookUp(OAuthTwt, profileid);
        //        foreach (var items in userlookup)
        //        {
        //            objTwtProfileDescription.screen_name = items["screen_name"].ToString();
        //            objTwtProfileDescription.name = items["name"].ToString();
        //            try
        //            {
        //                objTwtProfileDescription.profile_image_url = items["profile_image_url"].ToString();
        //            }
        //            catch (Exception)
        //            {

        //                objTwtProfileDescription.profile_image_url = null;
        //            }
        //            try
        //            {
        //                objTwtProfileDescription.profile_banner_url = items["profile_banner_url"].ToString();
        //            }
        //            catch (Exception)
        //            {

        //                objTwtProfileDescription.profile_banner_url = null;
        //            }
        //            try
        //            {
        //                objTwtProfileDescription.Status_Text = items["status"]["text"].ToString();
        //            }
        //            catch (Exception)
        //            {

        //                objTwtProfileDescription.Status_Text = null;
        //            }
        //            objTwtProfileDescription.Url = items["url"].ToString();
        //            objTwtProfileDescription.friends_count = items["friends_count"].ToString();
        //            objTwtProfileDescription.followers_count = items["followers_count"].ToString();
        //        }
        //        return new JavaScriptSerializer().Serialize(objTwtProfileDescription);
        //}

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TwitterProfileDetails(string userid, string profileid)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
            TwitterAccountRepository _objTwitterAccountRepository = new TwitterAccountRepository();
            if (_objTwitterAccountRepository.checkTwitterUserExists(profileid, Guid.Parse(userid)))
            {
                objTwitterAccount = _objTwitterAccountRepository.getUserInformation(profileid, Guid.Parse(userid));
            }
            else
            {
                objTwitterAccount = _objTwitterAccountRepository.getUserInformation(profileid);
            }
            return new JavaScriptSerializer().Serialize(objTwitterAccount);
        }

        public bool CheckTwitterTokenByUserId(oAuthTwitter objoAuthTwitter, string userid)
        {
            bool CheckTwitterTokenByUserId = false;
            //oAuthTwitter oAuthTwt = new oAuthTwitter();
            Users twtUser = new Users();
            try
            {
                Users userinfo = new Users();
                JArray userlookup = userinfo.Get_Users_LookUp(objoAuthTwitter, userid);
                CheckTwitterTokenByUserId = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return CheckTwitterTokenByUserId;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateTwitterAccountByAdmin(string ObjTwitter)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = (Domain.Socioboard.Domain.TwitterAccount)(new JavaScriptSerializer().Deserialize(ObjTwitter, typeof(Domain.Socioboard.Domain.TwitterAccount)));
            try
            {
                objTwitterAccountRepository.updateTwitterUser(objTwitterAccount);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Something went Wrong");
            }
        }

        // Edited by Antima[20/12/2014]

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string TicketTwitterReply(string message, string profileid, string statusid)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.getUserInformation(profileid);
            oAuthTwitter OAuthTwt = new oAuthTwitter();
            OAuthTwt.AccessToken = objTwitterAccount.OAuthToken;
            OAuthTwt.AccessTokenSecret = objTwitterAccount.OAuthSecret;
            OAuthTwt.TwitterScreenName = objTwitterAccount.TwitterScreenName;
            OAuthTwt.TwitterUserId = objTwitterAccount.TwitterUserId;
            this.SetCofigDetailsForTwitter(OAuthTwt);
            Tweet twt = new Tweet();
            JArray replypost = twt.Post_StatusesUpdate(OAuthTwt, message, statusid);
            return new JavaScriptSerializer().Serialize(replypost);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserMentions(oAuthTwitter OAuth, string TwitterUserId, Guid userId)
        {
            try
            {
                TimeLine _TimeLine = new TimeLine();
                JArray jdata = _TimeLine.Get_Statuses_Mentions_Timeline(OAuth);
                Domain.Socioboard.Domain.InboxMessages _InboxMessages;

                foreach (var item in jdata)
                {
                    try
                    {
                        _InboxMessages = new Domain.Socioboard.Domain.InboxMessages();
                        _InboxMessages.Id = Guid.NewGuid();
                        _InboxMessages.UserId = userId;
                        _InboxMessages.ProfileId = TwitterUserId;
                        _InboxMessages.ProfileType = "twt";
                        _InboxMessages.MessageType = "twt_mention";
                        _InboxMessages.EntryTime = DateTime.Now;
                        try
                        {
                            _InboxMessages.Message = item["text"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.MessageId = item["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.FromId = item["user"]["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.FromId = item["user"]["id"].ToString();
                        }
                        try
                        {
                            _InboxMessages.FromName = item["user"]["screen_name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.FromImageUrl = item["user"]["profile_image_url"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.FromImageUrl = item["user"]["profile_background_image_url_https"].ToString();
                        }
                        try
                        {
                            _InboxMessages.CreatedTime = Utility.ParseTwitterTime(item["created_at"].ToString());
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.RecipientId = item["entities"]["user_mentions"][0]["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.RecipientId = item["entities"]["user_mentions"][0]["id"].ToString();
                        }
                        try
                        {
                            _InboxMessages.RecipientName = item["entities"]["user_mentions"][0]["screen_name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
                        }

                        if (!objInboxMessagesRepository.checkInboxMessageExists(userId, _InboxMessages.MessageId, _InboxMessages.MessageType))
                        {
                            objInboxMessagesRepository.AddInboxMessages(_InboxMessages);
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error("Twitter.asmx => getUserMentions => " + ex.Message);
            }



        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserFollowers(oAuthTwitter OAuth, string screeenName, string TwitterUserId, Guid userId)
        {
            try
            {
                TimeLine _TimeLine = new TimeLine();
                JArray jdata = _TimeLine.Get_User_Followers(OAuth);
                JArray user_data = JArray.Parse(jdata[0]["users"].ToString());
                string curser = string.Empty;
                string curser_next = string.Empty;
                try
                {
                    curser_next = jdata[0]["next_cursor"].ToString();
                }
                catch (Exception ex)
                {
                    curser_next = "0";
                    logger.Error(ex.StackTrace);
                }
                do
                {
                    curser = curser_next;
                    Domain.Socioboard.Domain.InboxMessages _InboxMessages;
                    foreach (var item in user_data)
                    {
                        try
                        {
                            _InboxMessages = new Domain.Socioboard.Domain.InboxMessages();
                            _InboxMessages.Id = Guid.NewGuid();
                            _InboxMessages.UserId = userId;
                            _InboxMessages.ProfileId = TwitterUserId;
                            _InboxMessages.ProfileType = "twt";
                            _InboxMessages.MessageType = "twt_followers";
                            _InboxMessages.EntryTime = DateTime.Now;
                            _InboxMessages.MessageId = "";
                            _InboxMessages.Status = 1;
                            try
                            {
                                _InboxMessages.Message = item["description"].ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                            }
                            try
                            {
                                _InboxMessages.FromId = item["id_str"].ToString();
                            }
                            catch (Exception ex)
                            {
                                _InboxMessages.FromId = item["id"].ToString();
                            }
                            try
                            {
                                _InboxMessages.FromName = item["screen_name"].ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                            }
                            try
                            {
                                _InboxMessages.FollowerCount = item["followers_count"].ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                            }
                            try
                            {
                                _InboxMessages.FollowingCount = item["friends_count"].ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                            }
                            try
                            {
                                _InboxMessages.FromImageUrl = item["profile_image_url"].ToString();
                            }
                            catch (Exception ex)
                            {
                                _InboxMessages.FromImageUrl = item["profile_image_url_https"].ToString();
                            }
                            try
                            {
                                _InboxMessages.CreatedTime = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                            }
                            _InboxMessages.RecipientId = TwitterUserId;
                            _InboxMessages.RecipientName = screeenName;
                            if (!objInboxMessagesRepository.checkInboxMessageFriendExists(userId, _InboxMessages.FromId, _InboxMessages.RecipientId, _InboxMessages.MessageType))
                            {
                                objInboxMessagesRepository.AddInboxMessages(_InboxMessages);
                            }
                            else 
                            {
                                objInboxMessagesRepository.UpdateTwitterFollowerInfo(_InboxMessages);               
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                        }

                    }
                    if (curser != "0")
                    {
                        jdata = _TimeLine.Get_User_FollowersWithCurser(OAuth, curser);
                        user_data = JArray.Parse(jdata[0]["users"].ToString());
                        curser_next = jdata[0]["next_cursor"].ToString();
                    }
                }
                while (curser != "0");
            }
            catch (Exception ex)
            {
                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserRetweet(oAuthTwitter OAuth, string TwitterUserId, Guid userId)
        {
            try
            {
                TimeLine _TimeLine = new TimeLine();
                JArray jdata = _TimeLine.Get_Statuses_Retweet_Of_Me(OAuth);
                Domain.Socioboard.Domain.InboxMessages _InboxMessages;
                foreach (var item in jdata)
                {
                    try
                    {
                        _InboxMessages = new Domain.Socioboard.Domain.InboxMessages();

                        _InboxMessages.UserId = userId;
                        _InboxMessages.ProfileId = TwitterUserId;
                        _InboxMessages.ProfileType = "twt";
                        _InboxMessages.MessageType = "twt_retweet";
                        _InboxMessages.EntryTime = DateTime.Now;
                        try
                        {
                            _InboxMessages.MessageId = item["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.MessageId = item["id"].ToString();
                        }
                        try
                        {
                            _InboxMessages.Message = item["text"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.RecipientId = item["user"]["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.RecipientName = item["user"]["screen_name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.RecipientImageUrl = item["user"]["profile_image_url"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.RecipientImageUrl = item["user"]["profile_image_url_https"].ToString();
                        }
                        Tweet _tweet = new Tweet();
                        JArray retweet_data = _tweet.Get_Statuses_RetweetsById(OAuth, _InboxMessages.MessageId);
                        foreach (var item_retweet in retweet_data)
                        {
                            _InboxMessages.Id = Guid.NewGuid();
                            try
                            {
                                _InboxMessages.CreatedTime = Utility.ParseTwitterTime(item_retweet["created_at"].ToString());
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
                            }
                            try
                            {
                                _InboxMessages.FromId = item_retweet["user"]["id_str"].ToString();
                            }
                            catch (Exception ex)
                            {
                                _InboxMessages.FromId = item_retweet["user"]["id"].ToString();
                            }
                            try
                            {
                                _InboxMessages.FromName = item_retweet["user"]["screen_name"].ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
                            }
                            try
                            {
                                _InboxMessages.FromImageUrl = item_retweet["user"]["profile_image_url"].ToString();
                            }
                            catch (Exception ex)
                            {
                                _InboxMessages.FromImageUrl = item_retweet["user"]["profile_image_url_https"].ToString();
                            }

                            if (!objInboxMessagesRepository.checkInboxMessageRetweetExists(userId, _InboxMessages.MessageId, _InboxMessages.FromId, _InboxMessages.RecipientId, _InboxMessages.MessageType))
                            {
                                objInboxMessagesRepository.AddInboxMessages(_InboxMessages);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Twitter.asmx => getUserRetweets => " + ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getUserFollowersData(oAuthTwitter OAuth, string screeenName, string TwitterUserId, Guid userId)
        {
            try
            {
                TimeLine _TimeLine = new TimeLine();
                JArray jdata = _TimeLine.Get_User_Followers(OAuth);
                JArray user_data = JArray.Parse(jdata[0]["users"].ToString());
                Domain.Socioboard.Domain.InboxMessages _InboxMessages;
                foreach (var item in user_data)
                {
                    try
                    {
                        _InboxMessages = new Domain.Socioboard.Domain.InboxMessages();
                        _InboxMessages.Id = Guid.NewGuid();
                        _InboxMessages.UserId = userId;
                        _InboxMessages.ProfileId = TwitterUserId;
                        _InboxMessages.ProfileType = "twt";
                        _InboxMessages.MessageType = "twt_followers";
                        _InboxMessages.EntryTime = DateTime.Now;
                        _InboxMessages.MessageId = "";
                        _InboxMessages.Status = 0;
                        try
                        {
                            _InboxMessages.Message = item["description"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.FromId = item["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.FromId = item["id"].ToString();
                        }
                        try
                        {
                            _InboxMessages.FromName = item["screen_name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.FollowerCount = item["followers_count"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.FollowingCount = item["friends_count"].ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                        }
                        try
                        {
                            _InboxMessages.FromImageUrl = item["profile_image_url"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _InboxMessages.FromImageUrl = item["profile_image_url_https"].ToString();
                        }
                        try
                        {
                            _InboxMessages.CreatedTime = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                        }
                        _InboxMessages.RecipientId = TwitterUserId;
                        _InboxMessages.RecipientName = screeenName;
                        if (!objInboxMessagesRepository.checkInboxMessageFriendExists(userId, _InboxMessages.FromId, _InboxMessages.RecipientId, _InboxMessages.MessageType))
                        {
                            objInboxMessagesRepository.AddInboxMessages(_InboxMessages);
                        }
                        else
                        {
                            objInboxMessagesRepository.UpdateTwitterFollowerInfo(_InboxMessages);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Twitter.asmx => getUserFollowers => " + ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getTwitterDirectMessageSent(string UserId, oAuthTwitter OAuth)
        {
            #region Add Twitter Direct Message
            TwitterUser twtuser = new TwitterUser();
            try
            {
                JArray Messages_Sent = twtuser.GetDirect_Messages_Sent(OAuth, 20);

                objTwitterDirectMessages = new Domain.Socioboard.Domain.TwitterDirectMessages();
                foreach (var item in Messages_Sent)
                {
                    objTwitterDirectMessages.UserId = Guid.Parse(UserId);
                    objTwitterDirectMessages.Type = "twt_directmessages_sent";
                    objTwitterDirectMessages.Id = Guid.NewGuid();

                    try
                    {
                        objTwitterDirectMessages.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterDirectMessages.CreatedDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterDirectMessages.Message = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.RecipientId = item["recipient"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.RecipientScreenName = item["recipient"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.RecipientProfileUrl = item["recipient"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.SenderId = item["sender"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.SenderScreenName = item["sender"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    } try
                    {
                        objTwitterDirectMessages.SenderProfileUrl = item["sender"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    } try
                    {
                        objTwitterDirectMessages.EntryDate = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    if (!objTwitterDirectMessageRepository.checkExistsDirectMessages(objTwitterDirectMessages.MessageId))
                    {
                        try
                        {
                            objTwitterDirectMessageRepository.addNewDirectMessage(objTwitterDirectMessages);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error("twtuser.GetDirect_Messages_Sent ex.StackTrace >> " + ex.StackTrace);
                logger.Error("twtuser.GetDirect_Messages_Sent ex.Message >> " + ex.Message);
            }
            #endregion
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void getTwittwrDirectMessageRecieved(oAuthTwitter OAuth, string UserId)
        {
            #region Add Twitter Direct Message
            TwitterUser twtuser = new TwitterUser();
            try
            {
                JArray Messages_Sent = twtuser.GetDirect_Messages(OAuth, 20);

                objTwitterDirectMessages = new Domain.Socioboard.Domain.TwitterDirectMessages();
                foreach (var item in Messages_Sent)
                {
                    objTwitterDirectMessages.UserId = Guid.Parse(UserId);
                    objTwitterDirectMessages.Type = "twt_directmessages_received";
                    objTwitterDirectMessages.Id = Guid.NewGuid();

                    try
                    {
                        objTwitterDirectMessages.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterDirectMessages.CreatedDate = Utility.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterDirectMessages.Message = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.RecipientId = item["recipient"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.RecipientScreenName = item["recipient"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.RecipientProfileUrl = item["recipient"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.SenderId = item["sender"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }

                    try
                    {
                        objTwitterDirectMessages.SenderScreenName = item["sender"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    } try
                    {
                        objTwitterDirectMessages.SenderProfileUrl = item["sender"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    } try
                    {
                        objTwitterDirectMessages.EntryDate = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    if (!objTwitterDirectMessageRepository.checkExistsDirectMessages(objTwitterDirectMessages.MessageId))
                    {
                        try
                        {
                            objTwitterDirectMessageRepository.addNewDirectMessage(objTwitterDirectMessages);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error("twtuser.GetDirect_Messages_Sent ex.StackTrace >> " + ex.StackTrace);
                logger.Error("twtuser.GetDirect_Messages_Sent ex.Message >> " + ex.Message);
            }
            #endregion
        }

        public void getTwitterFollowerData(oAuthTwitter OAuth, string TwitterUserId)
        {
            try
            {
                TimeLine _TimeLine = new TimeLine();
                JArray jdata = _TimeLine.Get_User_Followers(OAuth);
                JArray user_data = JArray.Parse(jdata[0]["users"].ToString());
                Domain.Socioboard.Domain.TwitterFollowerNames _TwitterFollowerNames;
                TwitterFollowerNamesRepository objTwitterFollowerNamesRepository = new TwitterFollowerNamesRepository();
                foreach (var item in user_data)
                {
                    try
                    {
                        _TwitterFollowerNames = new Domain.Socioboard.Domain.TwitterFollowerNames();
                        _TwitterFollowerNames.Id = Guid.NewGuid();
                        _TwitterFollowerNames.TwitterProfileId = TwitterUserId;
                        try
                        {
                            _TwitterFollowerNames.FollowerId = item["id_str"].ToString();
                        }
                        catch (Exception ex)
                        {
                            _TwitterFollowerNames.FollowerId = item["id"].ToString();
                        }
                        try
                        {
                            _TwitterFollowerNames.Followerscrname = item["screen_name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        try
                        {
                            _TwitterFollowerNames.Name = item["name"].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (!objTwitterFollowerNamesRepository.IsFollowerExist(_TwitterFollowerNames.TwitterProfileId, _TwitterFollowerNames.FollowerId))
                        {
                            objTwitterFollowerNamesRepository.addTwitterAccountFollower(_TwitterFollowerNames);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Twitter.asmx => getTwitterFollowerData => " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Twitter.asmx => getTwitterFollowerData => " + ex.Message);
            }
        }

        public void AddTwitteruserFollower(oAuthTwitter OAuth, Guid userId, string TwitterUserId, string TwitterScreenName)
        {
            #region Add TwitterFollowerCount
            Users userinfo = new Users();
            TwitterAccountFollowersRepository objTwitterAccountFollowersRepository = new TwitterAccountFollowersRepository();
            Domain.Socioboard.Domain.TwitterAccountFollowers objTwitterAccountFollowers = new Domain.Socioboard.Domain.TwitterAccountFollowers();
            objTwitterAccountFollowers.Id = Guid.NewGuid();
            objTwitterAccountFollowers.UserId = userId;
            objTwitterAccountFollowers.EntryDate = DateTime.Now;
            objTwitterAccountFollowers.ProfileId = TwitterUserId;
            try
            {
                JArray profile = userinfo.Get_Users_LookUp_ByScreenName(OAuth, TwitterScreenName);
                foreach (var item_followerdata in profile)
                {

                    try
                    {
                        objTwitterAccountFollowers.FollowingsCount = Convert.ToInt32(item_followerdata["friends_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                    }
                    try
                    {
                        objTwitterAccountFollowers.FollowersCount = Convert.ToInt32(item_followerdata["followers_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }

                DateTime t1 = DateTime.Now.Date;
                DateTime t2 = DateTime.Now.Date.AddHours(12);
                DateTime t3 = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                if (DateTime.Now.TimeOfDay >= t1.TimeOfDay && DateTime.Now.TimeOfDay < t2.TimeOfDay)
                {
                    if (objTwitterAccountFollowersRepository.IsTwitterAccountExistsFirst(userId, TwitterUserId))
                    {
                        objTwitterAccountFollowersRepository.UpdateTwitterAccountFollowerFirst(objTwitterAccountFollowers);
                    }
                    else
                    {
                        objTwitterAccountFollowersRepository.addTwitterAccountFollower(objTwitterAccountFollowers);
                    }
                }
                if (DateTime.Now.TimeOfDay >= t2.TimeOfDay && DateTime.Now.TimeOfDay < t3.TimeOfDay)
                {
                    if (objTwitterAccountFollowersRepository.IsTwitterAccountExistsSecond(userId, TwitterUserId))
                    {
                        objTwitterAccountFollowersRepository.UpdateTwitterAccountFollowerSecond(objTwitterAccountFollowers);
                    }
                    else
                    {
                        objTwitterAccountFollowersRepository.addTwitterAccountFollower(objTwitterAccountFollowers);
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            #endregion
        }


        public void twitterrecentdetails(JArray data)
        {
            Domain.Socioboard.Domain.TwitterRecentDetails insertdata = new Domain.Socioboard.Domain.TwitterRecentDetails();
            TwitterAccountRepository objTwtRepo = new TwitterAccountRepository();


            string TwitterId = string.Empty;
            Guid Id = Guid.NewGuid();
            try
            {
                TwitterId = data[0]["id_str"].ToString();

            }
            catch (Exception)
            {

                TwitterId = string.Empty;

            }

            if (!string.IsNullOrEmpty(TwitterId))
            {
                string AccountCreationDate = string.Empty;
                string LastActivityDate = string.Empty;
                string lastfeed = string.Empty;
                string FeedId = string.Empty;
                string retweetcount = string.Empty;
                string favoritecount = string.Empty;

                try
                {
                    DateTime AccntCreationDate = Utility.ParseTwitterTime((data[0]["created_at"].ToString()));
                    AccountCreationDate = AccntCreationDate.ToString();
                }
                catch (Exception)
                {
                    AccountCreationDate = string.Empty;

                }


                try
                {
                    DateTime lastactivitydate = Utility.ParseTwitterTime((data[0]["status"]["created_at"].ToString()));
                    LastActivityDate = lastactivitydate.ToString();

                }
                catch (Exception)
                {


                    LastActivityDate = string.Empty;
                }

                try
                {
                    lastfeed = data[0]["status"]["text"].ToString();
                    FeedId = data[0]["status"]["id_str"].ToString();
                    retweetcount = data[0]["status"]["retweet_count"].ToString();
                    favoritecount = data[0]["status"]["favorite_count"].ToString();

                }
                catch (Exception)
                {
                    lastfeed = string.Empty;
                    FeedId = string.Empty;
                    retweetcount = string.Empty;
                    favoritecount = string.Empty;

                }

                insertdata.Id = Id;
                insertdata.TwitterId = TwitterId;
                insertdata.AccountCreationDate = AccountCreationDate;
                insertdata.LastActivityDate = LastActivityDate;
                insertdata.LastFeed = lastfeed;
                insertdata.FeedId = FeedId;
                insertdata.FeedRetweetCount = retweetcount;
                insertdata.FeedFavoriteCount = favoritecount;
                objTwtRepo.InsertTwitterRecentDetails(insertdata);



            }







        }




      





    }
}