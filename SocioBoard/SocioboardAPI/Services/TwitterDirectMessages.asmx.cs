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
    public class TwitterDirectMessages : System.Web.Services.WebService
    {
        TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //getAllDirectMessagesById
        [WebMethod]
        public string getAllDirectMessagesById(string Profileid)
        {
            List<Domain.Socioboard.Domain.TwitterDirectMessages> lsttwtmsg = objTwitterDirectMessageRepository.getAllDirectMessagesById(Profileid);
            return new JavaScriptSerializer().Serialize(lsttwtmsg);
        }
        [WebMethod]
        public string GetDistinctTwitterDirectMessagesByProfilesAndUserId(string UserId, string Profiles)
        {
            List<Domain.Socioboard.Domain.TwitterDirectMessages> lstTDM = objTwitterDirectMessageRepository.GetDistinctTwitterDirectMessagesByProfilesAndUserId(Guid.Parse(UserId), Profiles);
            return new JavaScriptSerializer().Serialize(lstTDM);
        }
        [WebMethod]
        public string GetConversation(string UserId, string SenderId, string RecipientId)
        {
            List<Domain.Socioboard.Domain.TwitterDirectMessages> lstTDM = objTwitterDirectMessageRepository.GetConversation(Guid.Parse(UserId), SenderId, RecipientId);
            return new JavaScriptSerializer().Serialize(lstTDM);
        }
        [WebMethod]
        public string GetTwitterDirectMessageSentCount(string UserId, string profileids, string days)
        {
            try
            {
                int dmsent = objTwitterDirectMessageRepository.GetTwitterDirectMessageSentCount(Guid.Parse(UserId), profileids, Int32.Parse(days));
                return dmsent.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        [WebMethod]
        public string GetTwitterDirectMessageRecievedCount(string UserId, string profileids, string days)
        {
            try
            {
                int dmrecieved = objTwitterDirectMessageRepository.GetTwitterDirectMessageRecievedCount(Guid.Parse(UserId), profileids, Int32.Parse(days));
                return dmrecieved.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
    }
}
