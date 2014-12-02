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
    public class ArchiveMessage : System.Web.Services.WebService
    {

        ArchiveMessageRepository objArchiveMessageRepository = new ArchiveMessageRepository();



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void AddArchiveMessage(string UserId, string ProfileId, string SocialGroup, string UserName, string MessageId, string Message, string CreatedDateTime, string ImgUrl)
        {
            Domain.Socioboard.Domain.ArchiveMessage ApiobjArchiveMessage = new Domain.Socioboard.Domain.ArchiveMessage();
            try
            {
                ApiobjArchiveMessage.Id = Guid.NewGuid();
                ApiobjArchiveMessage.UserId = Guid.Parse(UserId);
                ApiobjArchiveMessage.ProfileId = ProfileId;
                ApiobjArchiveMessage.SocialGroup = SocialGroup;
                ApiobjArchiveMessage.UserName = UserName;
                ApiobjArchiveMessage.MessageId = MessageId;
                ApiobjArchiveMessage.Message = Message;
                ApiobjArchiveMessage.CreatedDateTime = Convert.ToDateTime(CreatedDateTime);
                ApiobjArchiveMessage.ImgUrl = ImgUrl;
                objArchiveMessageRepository.AddArchiveMessage(ApiobjArchiveMessage);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool CheckArchiveMessageExists(string userid, string messageid)
        {
            bool status = objArchiveMessageRepository.checkArchiveMessageExists(Guid.Parse(userid), messageid);
            return status;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void DeleteArchiveMessage(string UserId, string ProfileId, string SocialGroup, string UserName, string MessageId, string Message, string CreatedDateTime, string ImgUrl)
        {
            Domain.Socioboard.Domain.ArchiveMessage ApiobjArchiveMessage = new Domain.Socioboard.Domain.ArchiveMessage();
            try
            {
                if (SocialGroup == "facebook")
                {
                    FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
                    objFacebookMessageRepository.DeleteFacebookMessagebymessageid(Message, MessageId, Guid.Parse(UserId));

                }
                if (SocialGroup == "twitter")
                {
                    TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
                    objTwitterMessageRepository.DeleteTwitterMessagebymessageid(Message, MessageId, Guid.Parse(UserId));
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
            }
        }

       

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllArchiveMessagesDetails(string userid, string profileid)
        {
            List<Domain.Socioboard.Domain.ArchiveMessage> lstAllArchive = objArchiveMessageRepository.getAllArchiveMessageDetail(profileid, Guid.Parse(userid));
            return new JavaScriptSerializer().Serialize(lstAllArchive);
        }
     
    }
}
