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
    /// Summary description for InboxMessages
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class InboxMessages : System.Web.Services.WebService
    {
        InboxMessagesRepository objInboxMessagesRepository = new InboxMessagesRepository();
      

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInboxMessage(string UserId, string ProfileIds, string MessageType, string noOfDataToSkip, string noOfDataFromTop)
        {
            List<Domain.Socioboard.Domain.InboxMessages> lstmsg = objInboxMessagesRepository.getInboxMessageByGroupandMessageType(Guid.Parse(UserId), ProfileIds, MessageType, noOfDataToSkip, noOfDataFromTop);
            return new JavaScriptSerializer().Serialize(lstmsg);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getInboxMessageByMessageId(string UserId, string MessageId)
        {
            Domain.Socioboard.Domain.InboxMessages _InboxMessages = objInboxMessagesRepository.getInboxMessageByMessageId(Guid.Parse(UserId), Guid.Parse(MessageId));
            return new JavaScriptSerializer().Serialize(_InboxMessages);
        }
        [WebMethod]
        public string getInboxMessageCount(string UserId, string ProfileId)
        {
            int count = objInboxMessagesRepository.GetInboxMessageCount(Guid.Parse(UserId), ProfileId);
            return count.ToString();
        }
         [WebMethod]
        public string GetAllFollowersOfUser(string userid, string profileid)
        {
            return new JavaScriptSerializer().Serialize(objInboxMessagesRepository.GetAllFollowersOfUser(Guid.Parse(userid), profileid));
        }

        //vikash 20-08-2015
         [WebMethod]
         public string GetTwitterMentionCount(string UserId, string profileids, string days)
         {
             try
             {
                 int mention = objInboxMessagesRepository.GetTwitterMentionCount(Guid.Parse(UserId), profileids, Int32.Parse(days));
                 return mention.ToString();
             }
             catch (Exception ex)
             {
                 return "0";
             }
         }
         [WebMethod]
         public string GetTwitterRetweetCount(string UserId, string profileids, string days)
         {
             try
             {
                 int retweet = objInboxMessagesRepository.GetTwitterRetweetCount(Guid.Parse(UserId), profileids, Int32.Parse(days));
                 return retweet.ToString();
             }
             catch (Exception ex)
             {
                 return "0";
             }
         }
         [WebMethod]
         public string GetTwitterFollowersCount(string UserId, string profileids, string days)
         {
             try
             {
                 int follower = objInboxMessagesRepository.GetTwitterFollowersCount(Guid.Parse(UserId), profileids, Int32.Parse(days));
                 return follower.ToString();
             }
             catch (Exception ex)
             {
                 return "0";
             }
         }

         [WebMethod]
         public string GetTwitterFollowers(string UserId, string profileids, string days)
         {
             try
             {
                 List<Domain.Socioboard.Domain.InboxMessages> lstfollowers = objInboxMessagesRepository.GetTwitterFollowers(Guid.Parse(UserId), profileids, Int32.Parse(days));
                 return new JavaScriptSerializer().Serialize(lstfollowers);
             }
             catch (Exception ex)
             {
                 return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.InboxMessages>());
                 
             }
         }

         [WebMethod]
         public string GetInstagramFollower(string UserId, string ProfileIds, string days)
         {
             int followers = objInboxMessagesRepository.GetInsagramFollowerCount(Guid.Parse(UserId), ProfileIds,Int32.Parse(days));
             return followers.ToString();
         }

         [WebMethod]
         public string GetInstagramFollowing(string UserId, string ProfileIds, string days)
         {
             int following = objInboxMessagesRepository.GetInsagramFollowingCount(Guid.Parse(UserId), ProfileIds, Int32.Parse(days));
             return following.ToString();
         }
    }
}
