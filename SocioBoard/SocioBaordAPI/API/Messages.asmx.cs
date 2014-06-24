using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using SocioBoard.Model;
using SocioBoard.Domain;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using GlobusTwitterLib.Authentication;
using System.Configuration;
using SocioBoard.Helper;

namespace SocioBaordAPI.API
{
    /// <summary>
    /// Summary description for Messages
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Messages : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddReplyMessage(String FromUserId, String Name, String UserId,String MessageId, String Message, string type)
        {
            String status = "Failed";
            try
            {
                ReplyMessageRepository RplyMsgRepository = new ReplyMessageRepository();
                ReplyMessage RplMsg = new ReplyMessage();
                
                Guid _Id = Guid.NewGuid();
                Guid _UserId = Guid.Parse(UserId);
                Guid _MessageId = Guid.Parse(MessageId);

                RplMsg.Id = _Id;
                RplMsg.FromUserId = FromUserId;
                RplMsg.Name = Name;
                RplMsg.UserId = _UserId;
                RplMsg.MessageId = _MessageId;
                RplMsg.Message = Message;
                RplMsg.Type = type;

                int result = RplyMsgRepository.AddReplyMessage(RplMsg);

                if (result == 1)
                {
                    status="Success";
                }
            }
            catch (Exception)
            {
              
            }
            return new JavaScriptSerializer().Serialize(status);
        }
    }
}
