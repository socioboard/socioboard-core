using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using System.Collections;
using System.Threading.Tasks;
using System.Globalization;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using MongoDB.Bson;
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
    public class TwitterMessage : System.Web.Services.WebService
    {
        TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
        MongoRepository twitterMessageRepo = new MongoRepository("TwitterMessage");
        private CultureInfo provider = CultureInfo.InvariantCulture;
        private string format = "yyyy/MM/dd HH:mm:ss";
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterMessages(string TwitterId, string Userid)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;
            try
            {

                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.MessageId.Equals(TwitterId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lsttwtmsg = _lsttwtmsg.OrderByDescending(t => DateTime.ParseExact(t.MessageDate, format, provider)).ToList();
            }
            catch (Exception ex)
            {
                lsttwtmsg = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }

            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAllTwitterMessagesOfUser(Guid.Parse(Userid), TwitterId);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterMessages1(string TwitterId, string Userid, int count)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;
            var builder = Builders<Domain.Socioboard.MongoDomain.TwitterMessage>.Sort;
            var sort = builder.Descending(t=> t.MessageDate);
            var ret = twitterMessageRepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(TwitterId),sort,count,10);
            var task = Task.Run(async () =>
            {
                return await ret;
            });
            IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
            lsttwtmsg = _lsttwtmsg.ToList();

            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAllTwitterMessagesOfUser(Guid.Parse(Userid), TwitterId, count);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUnreadMessages(string Profileid)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;

            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(Profileid) && t.ReadStatus == 0);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lsttwtmsg = _lsttwtmsg.ToList();
            }
            catch (Exception ex)
            {
                lsttwtmsg = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getUnreadMessages(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        //getAlltwtMessagesOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAlltwtMessagesOfUser(string Profileid)
        {
            string [] arr=Profileid.Split(',');
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ReadStatus == 1 && arr.Contains(t.ProfileId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lsttwtmsg = _lsttwtmsg.OrderByDescending(t => t.MessageDate).ToList();
            }
            catch (Exception ex)
            {
                lsttwtmsg = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAlltwtMessagesOfUser(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        //getAlltwtMessages
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAlltwtMessages(string Profileid)
        {
            string [] arr=Profileid.Split(',');
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;

            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => arr.Contains(t.ProfileId));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lsttwtmsg = _lsttwtmsg.OrderByDescending(t => t.MessageDate).ToList();
            }
            catch (Exception ex)
            {
                lsttwtmsg = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }


            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAlltwtMessages(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string getAlltwtMessages1(string Profileid, string userid)
        //{
        //    List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAlltwtMessages1(Profileid, Guid.Parse(userid));
        //    return new JavaScriptSerializer().Serialize(lsttwtmsg);
        //}

        public string getTwtMention(string profileId, Guid userid, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();
            string count = string.Empty;
            try
            {
                ArrayList alstTwt = objretwt.getMentionStatsByProfileId(profileId, userid, days);
                count = alstTwt[0].ToString();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return count;
        }

        public string GetMentionStatsCountByProfileIdAndUserId(string profileId, Guid userid, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();
            int count = 0;
            try
            {
                count = objretwt.GetMentionStatsCountByProfileIdAndUserId(userid, profileId, days);
            }
            catch (Exception Err)
            {
                count = 0;
                Console.Write(Err.StackTrace);
            }
            return count.ToString();
        }

        public string getRetweets(string profileId, Guid userid, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();

            string strcount = string.Empty;
            try
            {

                ArrayList alstTwt = objretwt.getRetweetStatsByProfileId(profileId, userid, days);
                strcount = alstTwt[0].ToString();
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            return strcount;

        }
        public string GetRetweetStatsCountByProfileIdAndUserId(string profileId, Guid userid, int days)
        {
            TwitterMessageRepository objretwt = new TwitterMessageRepository();

            int strcount = 0;
            try
            {

                strcount = objretwt.GetRetweetStatsCountByProfileIdAndUserId(userid, profileId, days);
               
            }
            catch (Exception Err)
            {
                strcount = 0;
                Console.Write(Err.StackTrace);
            }
            return strcount.ToString();
        }
     

        public string GetAllRetweetMentionBydays(string userid, string profileId, string days)
        { 
             Guid UserId=Guid.Parse(userid);
             string data = string.Empty;

             try
             {
                           
                 string usertweets = string.Empty;
                 string usertweetsDates = string.Empty;
                 string retweets = string.Empty;
                 string retweetsDates = string.Empty;
                 string mentions = string.Empty;
                 string mentionsDates = string.Empty;

                 int _TwitterMention = objTwitterMessageRepository.getTotalMentionBydays(UserId, profileId, Convert.ToInt32(days));
                 int _TwitterRetweet = objTwitterMessageRepository.getTotalRetweetBydays(UserId, profileId, Convert.ToInt32(days));
                 //int _TwitterMention = objTwitterMessageRepository.GetMentionStatsCountByProfileIdAndUserId(UserId, profileId, Convert.ToInt32(days));
                 //int _TwitterRetweet = objTwitterMessageRepository.GetRetweetStatsCountByProfileIdAndUserId(UserId, profileId, Convert.ToInt32(days));

                 dynamic _TwitterMessage = objTwitterMessageRepository.getAllRetweetMentionBydays(UserId, profileId, Convert.ToInt32(days));

                 if (_TwitterMessage != null)
                 {

                     foreach (var item in _TwitterMessage)
                     {
                         if (item[3] == "twt_usertweets")
                         {
                             usertweets += item[0].ToString() + ',';
                             usertweetsDates += item[1].ToString() + ',';
                         }
                         else if (item[3] == "twt_retweets")
                         {
                             retweets += item[0].ToString() + ',';
                             retweetsDates += item[1].ToString() + ',';
                         }
                         else if (item[3] == "twt_mentions")
                         {
                             mentions += item[0].ToString() + ',';
                             mentionsDates += item[1].ToString() + ',';
                         }
                     }


                   





                     try
                     {

                         usertweets = usertweets.Substring(0, usertweets.Length - 1);
                         usertweetsDates = usertweetsDates.Substring(0, usertweetsDates.Length - 1);
                         usertweets = "usrtwet^" + usertweets + "^" + usertweetsDates;

                     }
                     catch (Exception ex)
                     {
                         int Day = Convert.ToInt32(days);
                         string[] Days = new string[Day];
                         for (int i = 0; i < Day; i++)
                         {
                             DateTime d1 = DateTime.Now.AddDays(-i);
                             Days[i] = Convert.ToString(d1);
                         }
                         foreach (string item in Days)
                         {
                             usertweets += "0" + ',';
                             usertweetsDates += item.ToString() + ',';
                             usertweets = "usrtwet^" + usertweets + "^" + usertweetsDates;
                         }
                     }
                     try
                     {
                         mentions = mentions.Substring(0, mentions.Length - 1);
                         mentionsDates = mentionsDates.Substring(0, mentionsDates.Length - 1);
                         mentions = "mention^" + mentions + "^" + mentionsDates;
                     }
                     catch (Exception ex)
                     {
                         int Day = Convert.ToInt32(days);
                         string[] Days = new string[Day];
                         for (int i = 0; i < Day; i++)
                         {
                             DateTime d1 = DateTime.Now.AddDays(-i);
                             try
                             {
                                 Days[i] = Convert.ToString(d1);
                             }
                             catch (Exception ex1)
                             {
                                 
                                 throw;
                             }
                         }
                         foreach (string item in Days)
                         {
                             mentions += "0" + ',';
                             mentionsDates += item.ToString() + ',';
                             mentions = "mention^" + mentions + "^" + mentionsDates;
                         }
                     }
                     try
                     {
                         retweets = retweets.Substring(0, retweets.Length - 1);
                         retweetsDates = retweetsDates.Substring(0, retweetsDates.Length - 1);
                         retweets = "retwet^" + retweets + "^" + retweetsDates;
                     }
                     catch (Exception ex)
                     {
                         int Day = Convert.ToInt32(days);
                         string[] Days = new string[Day];
                         for (int i = 0; i < Day; i++)
                         {
                             DateTime d1 = DateTime.Now.AddDays(-i);
                             Days[i] = Convert.ToString(d1);
                         }
                         foreach (string item in Days)
                         {
                             retweets += "0" + ',';
                             retweetsDates += item.ToString() + ',';
                             retweets = "retwet^" + retweets + "^" + retweetsDates;
                         }
                     }

                 }

                 else
                 {
                     usertweets = (0).ToString();
                     mentions = (0).ToString();
                     retweets = (0).ToString();
                     _TwitterMention = 0;
                     _TwitterRetweet = 0;
                 }

                 String totalmention = "metion"+_TwitterMention;
                 string totalretweet = "retwet" + _TwitterRetweet;

                 data = usertweets + "@" + mentions + "@" + retweets + "@" + totalmention + "@" + totalretweet;
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.StackTrace);
             }
            
             return data;
        
        }

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterUsertweetOfUsers(string UserId, string ProfileId)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterUsertweet;

            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(ProfileId) && t.Type == "twt_usertweets");
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterUsertweet = _lsttwtmsg.OrderByDescending(t => t.MessageDate).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterUsertweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterUsertweet);
            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterMessage> lstTwitterUsertweet = objTwitterMessageRepository.getAllTwitterUsertweetOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstTwitterUsertweet);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterRetweetOfUsers(string UserId, string ProfileId)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterRetweet;
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(ProfileId) && t.Type == "twt_retweets");
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterRetweet = _lsttwtmsg.OrderByDescending(t => t.MessageDate).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterRetweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRetweet);
            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterMessage> lstTwitterRetweet = objTwitterMessageRepository.getAllTwitterRetweetOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstTwitterRetweet);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterMentionsOfUsers(string UserId, string ProfileId)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterRetweet;
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(ProfileId) && t.Type == "twt_mentions");
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterRetweet = _lsttwtmsg.OrderByDescending(t => t.MessageDate).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterRetweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRetweet);
            
            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterMessage> lstTwitterMentions = objTwitterMessageRepository.getAllTwitterMentionsOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstTwitterMentions);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterMessageByMessageId(string userid, string Msgid)
        {
            Domain.Socioboard.MongoDomain.TwitterMessage _TwitterRetweet;
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.MessageId.Equals(Msgid));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                _TwitterRetweet = _lsttwtmsg[0];
            }
            catch (Exception ex)
            {
                _TwitterRetweet = new Domain.Socioboard.MongoDomain.TwitterMessage();
            }
            return new JavaScriptSerializer().Serialize(_TwitterRetweet);


            //try
            //{
            //    Domain.Socioboard.Domain.TwitterMessage _TwitterMessage = objTwitterMessageRepository.GetTwitterMessageByMessageId(Guid.Parse(userid), Msgid);
            //    return new JavaScriptSerializer().Serialize(_TwitterMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterUsertweetOfUsersByKeyword(string UserId, string ProfileId, string keyword)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterRetweet;
            try
            {

                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(ProfileId) && t.TwitterMsg.Contains(keyword) && t.Type.Equals("twt_usertweets"));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterRetweet = _lsttwtmsg.OrderByDescending(t => t.MessageDate).Take(20).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterRetweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRetweet);


            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterMessage> lstTwitterUsertweet = objTwitterMessageRepository.getAllTwitterUsertweetOfUsersByKeyword(UserId, ProfileId, keyword);
            //    return new JavaScriptSerializer().Serialize(lstTwitterUsertweet);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterRetweetOfUsersByKeyword(string UserId, string ProfileId, string keyword)
        {

            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterRetweet;
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(ProfileId) && t.TwitterMsg.Contains(keyword) && t.Type.Equals("twt_retweets"));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterRetweet = _lsttwtmsg.OrderByDescending(t => t.MessageDate).Take(20).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterRetweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRetweet);

            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterMessage> lstTwitterRetweet = objTwitterMessageRepository.getAllTwitterRetweetOfUsersByKeyword(UserId, ProfileId, keyword);
            //    return new JavaScriptSerializer().Serialize(lstTwitterRetweet);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterMentionsOfUsersByKeyword(string UserId, string ProfileId, string keyword)
        {

            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterRetweet;
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(ProfileId) && t.TwitterMsg.Contains(keyword) && t.Type.Equals("twt_mentions"));
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterRetweet = _lsttwtmsg.OrderByDescending(t => t.MessageDate).Take(20).ToList();
            }
            catch (Exception ex)
            {
                lstTwitterRetweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRetweet);

            //try
            //{
            //    List<Domain.Socioboard.Domain.TwitterMessage> lstTwitterMentions = objTwitterMessageRepository.getAllTwitterMentionsOfUsersByKeyword(UserId, ProfileId, keyword);
            //    return new JavaScriptSerializer().Serialize(lstTwitterMentions);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterMessages1ByKeyword(string TwitterId, string Userid, string keyword, int count)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lstTwitterRetweet;
            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.TwitterMessage>.Sort;
                var sort = builder.Descending(t => t.MessageDate);
                var ret = twitterMessageRepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(TwitterId) && t.TwitterMsg.Contains(keyword),sort,count,20);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lstTwitterRetweet = _lsttwtmsg.ToList();
            }
            catch (Exception ex)
            {
                lstTwitterRetweet = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }
            return new JavaScriptSerializer().Serialize(lstTwitterRetweet);

            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAllTwitterMessagesOfUserByKeyword(Userid, TwitterId, keyword, count);
            //return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int GetFeedCountByProfileIdAndUserId(string UserId, string ProfileIds)
        {
            int count = 0;
            string[] arr = ProfileIds.Split(',');
            try
            {
                var ret = twitterMessageRepo.Find<Domain.Socioboard.MongoDomain.TwitterMessage>(t => arr.Contains(t.ProfileId) );
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                count = _lsttwtmsg.Count;
            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
            //try
            //{
            //    return objTwitterMessageRepository.GetMessageCountByProfileIdAndUserId(Guid.Parse(UserId), ProfileIds);
            //}
            //catch (Exception ex)
            //{
            //    return 0;
            //}
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllTwitterkMessagesOfUserByProfileIdWithRange(string UserId, string profileid, string noOfDataToSkip)
        {
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;
            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.TwitterMessage>.Sort;
                var sort = builder.Descending(t => t.MessageDate);
                var ret = twitterMessageRepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterMessage>(t => t.ProfileId.Equals(profileid), sort, Int32.Parse(noOfDataToSkip), 15);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lsttwtmsg = _lsttwtmsg.ToList();
            }
            catch (Exception ex)
            {
                lsttwtmsg = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }

            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.getAllTwitterkMessagesOfUserByProfileIdWithRange(Guid.Parse(UserId), profileid, noOfDataToSkip);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllMessageDetailWithRange(string UserId, string profileid, string noOfDataToSkip)
        {
            string[] arr = profileid.Split(',');
            List<Domain.Socioboard.MongoDomain.TwitterMessage> lsttwtmsg;
            try
            {
                var builder = Builders<Domain.Socioboard.MongoDomain.TwitterMessage>.Sort;
                var sort = builder.Descending(t => t.MessageDate);
                var ret = twitterMessageRepo.FindWithRange<Domain.Socioboard.MongoDomain.TwitterMessage>(t => arr.Contains(t.ProfileId), sort, Int32.Parse(noOfDataToSkip), 15);
                var task = Task.Run(async () =>
                {
                    return await ret;
                });
                IList<Domain.Socioboard.MongoDomain.TwitterMessage> _lsttwtmsg = task.Result;
                lsttwtmsg = _lsttwtmsg.ToList();
            }
            catch (Exception ex)
            {
                lsttwtmsg = new List<Domain.Socioboard.MongoDomain.TwitterMessage>();
            }

            return new JavaScriptSerializer().Serialize(lsttwtmsg);

            //List<Domain.Socioboard.Domain.TwitterMessage> lsttwtmsg = objTwitterMessageRepository.GetAllMessageDetailWithRange(Guid.Parse(UserId), profileid, noOfDataToSkip);
            //return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }
    }
}
