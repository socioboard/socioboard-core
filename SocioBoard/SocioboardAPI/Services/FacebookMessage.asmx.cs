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


    }
}
