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

    }
}
