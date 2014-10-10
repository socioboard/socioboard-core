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
using System.Configuration;
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
    public class Messages : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddReplyMessage(string FromUserId, string Name, string UserId, string MessageId, string Message, string type)
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

                try
                {
                    RplyMsgRepository.AddReplyMessage(RplMsg);
                    status = "Success";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    status = ex.Message;
                }
            }
            catch (Exception)
            {

            }
            return new JavaScriptSerializer().Serialize(status);
        }
    }
}
