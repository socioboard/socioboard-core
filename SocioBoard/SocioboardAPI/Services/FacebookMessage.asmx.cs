using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class FacebookMessage : System.Web.Services.WebService
    {
        ILog logger = LogManager.GetLogger(typeof(FacebookMessage));

        FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserHomeWithLimit(string UserId, string FacebookId, string count)
        {
            //try
            //{
            //    Guid userid = Guid.Parse(UserId);
            //    FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
            //    List<Domain.Socioboard.Domain.FacebookMessage> lstfbmsgs = fbmsgrepo.getFacebookUserWallPost(userid, FacebookId, Convert.ToInt32(count));
            //    return new JavaScriptSerializer().Serialize(lstfbmsgs);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return new JavaScriptSerializer().Serialize("Please Try Again");
            //    throw;
            //}
            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UserId) && t.ProfileId.Equals(FacebookId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                List<Domain.Socioboard.MongoDomain.FacebookMessage> fbfeeds = objfbfeeds.OrderByDescending(x => x.MessageDate).Take(Convert.ToInt32(count)).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserHome(string UserId, string FacebookId)
        {
            //try
            //{
            //    Guid userid = Guid.Parse(UserId);
            //    FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
            //    List<Domain.Socioboard.Domain.FacebookMessage> lstfbmsgs = fbmsgrepo.getFacebookUserWallPost(userid, FacebookId);
            //    return new JavaScriptSerializer().Serialize(lstfbmsgs);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return new JavaScriptSerializer().Serialize("Please Try Again");
            //    throw;
            //}

            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UserId) && t.ProfileId.Equals(FacebookId) && t.Type.Equals("fb_home")).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        //getAllFacebookMessagesOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookMessagesOfUser(string UserId, string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllFacebookMessagesOfUser(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(objFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UserId) && t.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //getAllSentMessages
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllSentMessages(string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllSentMessages(ProfileId);
            //    return new JavaScriptSerializer().Serialize(objFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t =>  t.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                List<Domain.Socioboard.MongoDomain.FacebookMessage> fbfeeds = objfbfeeds.OrderByDescending(t => t.MessageDate).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllWallpostsOfProfileAccordingtoGroup(string profileid, int count, string UserId)
        {
            //try
            //{
            //    logger.Error("USING index (MessageDate_ProfileId_UserId) >> FacebookMessage.asmx");
            //    List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.GetAllWallpostsOfProfileAccordingtoGroup(profileid, count, Guid.Parse(UserId));
            //    return new JavaScriptSerializer().Serialize(objFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.ProfileId.Equals(profileid) && t.UserId.Equals(UserId) && t.Type.Equals("fb_home")).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                List<Domain.Socioboard.MongoDomain.FacebookMessage> fbfeeds = objfbfeeds.OrderByDescending(t => t.MessageDate).Take(count).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        public string GetAllInboxMessage(string userid, string profileid, string days)
        {
            //List<Domain.Socioboard.Domain.FacebookMessage> lstmessage = new List<Domain.Socioboard.Domain.FacebookMessage>();
            int lstmessage = 0;
            try
            {
                int daycount = Convert.ToInt32(days);
                 lstmessage = objFacebookMessageRepository.getAllInboxMessagesByProfileid(Guid.Parse(userid), profileid, daycount);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return lstmessage.ToString();
        }

        //getAllFacebookMessagesOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookMessagesOfUserByProfileId(string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllMessageOfProfile(ProfileId);
            //    return new JavaScriptSerializer().Serialize(objFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.ProfileId.Equals(ProfileId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                List<Domain.Socioboard.MongoDomain.FacebookMessage> fbfeeds = objfbfeeds.OrderByDescending(t => t.MessageDate).ToList();
                return new JavaScriptSerializer().Serialize(fbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string getAllFacebookMessagesOfUserByProfileIdWithRange(string ProfileId, string noOfDataToSkip)
        //{
        //    try
        //    {
        //        List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllMessageOfProfileWithRange(ProfileId, noOfDataToSkip);
        //        return new JavaScriptSerializer().Serialize(objFacebookMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //        return "Something Went Wrong";
        //    }
        //}

        // Edited by Antima

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookTagOfUsers(string UserId, string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookTag = objFacebookMessageRepository.getAllFacebookTagOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstFacebookTag);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.ProfileId.Equals(ProfileId) && t.Type.Equals("fb_tag") && t.UserId.Equals(UserId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookstatusOfUsers(string UserId, string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookstatus = objFacebookMessageRepository.getAllFacebookstatusOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstFacebookstatus);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.ProfileId.Equals(ProfileId) && t.Type.Equals("fb_home") && t.UserId.Equals(UserId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookUserFeedOfUsers(string UserId, string ProfileId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookUserFeed = objFacebookMessageRepository.getAllFacebookUserFeedOfUsers(Guid.Parse(UserId), ProfileId);
            //    return new JavaScriptSerializer().Serialize(lstFacebookUserFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}



            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.ProfileId.Equals(ProfileId) && t.Type.Equals("fb_home") && t.UserId.Equals(UserId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetMessageDetailByMessageid(string MessageId)
        {
            //Domain.Socioboard.Domain.FacebookMessage objFacebookMessage = objFacebookMessageRepository.GetMessageDetailByMessageid(MessageId);
            //return new JavaScriptSerializer().Serialize(objFacebookMessage);


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.MessageId.Equals(MessageId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds.FirstOrDefault());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookMessageByMessageId(string userid, string msgid)
        {
            //Domain.Socioboard.Domain.FacebookMessage objFacebookMessage = objFacebookMessageRepository.GetFacebookMessageByMessageId(Guid.Parse(userid), msgid);
            //return new JavaScriptSerializer().Serialize(objFacebookMessage);
            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.MessageId.Equals(msgid) && t.UserId.Equals(userid)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;
                return new JavaScriptSerializer().Serialize(objfbfeeds.FirstOrDefault());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllMessageDetail(string profileid) 
        {
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = objFacebookMessageRepository.getAllMessageDetail(profileid);
            return new JavaScriptSerializer().Serialize(lstFacebookMessage);
        }
        //[WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        //public string GetAllMessageDetailWithRange(string profileid, string noOfDataToSkip)
        //{
        //    List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = objFacebookMessageRepository.getAllMessageDetail(profileid, noOfDataToSkip);
        //    return new JavaScriptSerializer().Serialize(lstFacebookMessage);
        //}

        //Added by Sumit Gupta [12-02-15]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookMessagesByUserIdWithRange(string UserId, string noOfDataToSkip)
        {
            //List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = new List<Domain.Socioboard.Domain.FacebookMessage>();
            //try
            //{
            //    //if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
            //    {
            //        lstFacebookMessage = objFacebookMessageRepository.getAllFacebookMessagesOfSBUserWithRange(UserId, noOfDataToSkip);
            //    }
            //    //else
            //    //{
            //    //    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookUserFeeds(ProfileId);
            //    //}
            //    return new JavaScriptSerializer().Serialize(lstFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UserId)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;

                return new JavaScriptSerializer().Serialize(objfbfeeds.Skip(Convert.ToInt32(noOfDataToSkip)).Take(20).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //Added by sumit gupta [13-02-2015]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllWallpostsOfProfileAccordingtoGroupByUserIdAndProfileId1WithRange(string UserId, string keyword, string ProfileId, string count)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.GetAllWallpostsOfProfileAccordingtoGroupByUserIdAndProfileId1WithRange(UserId, keyword, ProfileId, count);
            //    return new JavaScriptSerializer().Serialize(objFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UserId)&&t.ProfileId.Equals(ProfileId)&&t.Message.Contains(keyword)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;

                return new JavaScriptSerializer().Serialize(objfbfeeds.OrderByDescending(x=>x.MessageDate).Take(20).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        //Added by sumit gupta [13-02-2015]
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookUserFeedOfUsersByUserIdAndProfileId1WithRange(string UserId, string keyword, string ProfileId, string count)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookUserFeed = objFacebookMessageRepository.getAllFacebookUserFeedOfUsersByUserIdAndProfileId1WithRange(UserId, keyword, ProfileId, count);
            //    return new JavaScriptSerializer().Serialize(lstFacebookUserFeed);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}

            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UserId) && t.ProfileId.Equals(ProfileId) && t.ProfileId != t.FromId && t.Type.Equals("fb_home") && t.Message.Contains(keyword)).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;

                return new JavaScriptSerializer().Serialize(objfbfeeds.OrderByDescending(x => x.MessageDate).Take(20).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookMessagesOfUserByProfileIdWithRange(string ProfileId, string noOfDataToSkip, string UseId)
        {
            //try
            //{
            //    List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllMessageOfProfileWithRange(ProfileId, noOfDataToSkip, Guid.Parse(UseId));
            //    return new JavaScriptSerializer().Serialize(objFacebookMessage);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //    return "Something Went Wrong";
            //}


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {

                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.UserId.Equals(UseId) && t.ProfileId.Equals(ProfileId) && t.ProfileId != t.FromId && t.IsArchived.Equals("1")).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;

                return new JavaScriptSerializer().Serialize(objfbfeeds.OrderByDescending(x => x.MessageDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(15).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllMessageDetailWithRange(string profileid, string noOfDataToSkip, string UserId)
        {
            //List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = objFacebookMessageRepository.getAllMessageDetail(profileid, noOfDataToSkip, Guid.Parse(UserId));
            //return new JavaScriptSerializer().Serialize(lstFacebookMessage);


            MongoRepository boardrepo = new MongoRepository("FacebookMessage");
            try
            {
                string[] arrsrt = profileid.Split(',');
                var result = boardrepo.Find<Domain.Socioboard.MongoDomain.FacebookMessage>(t => t.ProfileId.Equals(profileid) && arrsrt.Contains(t.ProfileId) && !t.IsArchived.Equals("1")).ConfigureAwait(false);

                var task = Task.Run(async () =>
                {
                    return await result;
                });
                IList<Domain.Socioboard.MongoDomain.FacebookMessage> objfbfeeds = task.Result;

                return new JavaScriptSerializer().Serialize(objfbfeeds.OrderByDescending(x => x.MessageDate).Skip(Convert.ToInt32(noOfDataToSkip)).Take(15).ToList());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

    }
}
