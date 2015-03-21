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
    public class FacebookMessage : System.Web.Services.WebService
    {
        FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserHomeWithLimit(string UserId, string FacebookId, string count)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                List<Domain.Socioboard.Domain.FacebookMessage> lstfbmsgs = fbmsgrepo.getFacebookUserWallPost(userid, FacebookId, Convert.ToInt32(count));
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
        public string UserHome(string UserId, string FacebookId)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                List<Domain.Socioboard.Domain.FacebookMessage> lstfbmsgs = fbmsgrepo.getFacebookUserWallPost(userid, FacebookId);
                return new JavaScriptSerializer().Serialize(lstfbmsgs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
                throw;
            }

        }

        //getAllFacebookMessagesOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookMessagesOfUser(string UserId, string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllFacebookMessagesOfUser(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(objFacebookMessage);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllSentMessages(ProfileId);
                return new JavaScriptSerializer().Serialize(objFacebookMessage);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.GetAllWallpostsOfProfileAccordingtoGroup(profileid, count, Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objFacebookMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        public string GetAllInboxMessage(string userid, string profileid, string days)
        {
            List<Domain.Socioboard.Domain.FacebookMessage> lstmessage = new List<Domain.Socioboard.Domain.FacebookMessage>();
            try
            {
                int daycount = Convert.ToInt32(days);
                 lstmessage = objFacebookMessageRepository.getAllInboxMessagesByProfileid(Guid.Parse(userid), profileid, daycount);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return lstmessage.Count.ToString();
        }

        //getAllFacebookMessagesOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllFacebookMessagesOfUserByProfileId(string ProfileId)
        {
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllMessageOfProfile(ProfileId);
                return new JavaScriptSerializer().Serialize(objFacebookMessage);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookTag = objFacebookMessageRepository.getAllFacebookTagOfUsers(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookTag);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookstatus = objFacebookMessageRepository.getAllFacebookstatusOfUsers(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookstatus);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookUserFeed = objFacebookMessageRepository.getAllFacebookUserFeedOfUsers(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize(lstFacebookUserFeed);
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
            Domain.Socioboard.Domain.FacebookMessage objFacebookMessage = objFacebookMessageRepository.GetMessageDetailByMessageid(MessageId);
            return new JavaScriptSerializer().Serialize(objFacebookMessage);

        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetFacebookMessageByMessageId(string userid, string msgid)
        {
            Domain.Socioboard.Domain.FacebookMessage objFacebookMessage = objFacebookMessageRepository.GetFacebookMessageByMessageId(Guid.Parse(userid), msgid);
            return new JavaScriptSerializer().Serialize(objFacebookMessage);
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
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = new List<Domain.Socioboard.Domain.FacebookMessage>();
            try
            {
                //if (objFacebookFeedRepository.checkFacebookUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    lstFacebookMessage = objFacebookMessageRepository.getAllFacebookMessagesOfSBUserWithRange(UserId, noOfDataToSkip);
                }
                //else
                //{
                //    lstFacebookFeed = objFacebookFeedRepository.getAllFacebookUserFeeds(ProfileId);
                //}
                return new JavaScriptSerializer().Serialize(lstFacebookMessage);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.GetAllWallpostsOfProfileAccordingtoGroupByUserIdAndProfileId1WithRange(UserId, keyword, ProfileId, count);
                return new JavaScriptSerializer().Serialize(objFacebookMessage);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookUserFeed = objFacebookMessageRepository.getAllFacebookUserFeedOfUsersByUserIdAndProfileId1WithRange(UserId, keyword, ProfileId, count);
                return new JavaScriptSerializer().Serialize(lstFacebookUserFeed);
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
            try
            {
                List<Domain.Socioboard.Domain.FacebookMessage> objFacebookMessage = objFacebookMessageRepository.getAllMessageOfProfileWithRange(ProfileId, noOfDataToSkip, Guid.Parse(UseId));
                return new JavaScriptSerializer().Serialize(objFacebookMessage);
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
            List<Domain.Socioboard.Domain.FacebookMessage> lstFacebookMessage = objFacebookMessageRepository.getAllMessageDetail(profileid, noOfDataToSkip, Guid.Parse(UserId));
            return new JavaScriptSerializer().Serialize(lstFacebookMessage);
        }

    }
}
