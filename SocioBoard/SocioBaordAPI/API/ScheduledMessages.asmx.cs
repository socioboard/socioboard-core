using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using SocioBoard.Domain;
using SocioBoard.Model;
using SocioBaordAPI.Helper;
using System.IO;

namespace SocioBaordAPI.API
{
    /// <summary>
    /// Summary description for ScheduledMessages
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [ScriptService]
    public class ScheduledMessages : System.Web.Services.WebService
    {

        [WebMethod]
        public string UploadFile(byte[] f, string fileName)
        {
            string ret = string.Empty;
            // the byte array argument contains the content of the file
            // the string argument contains the name and extension
            // of the file passed in the byte array
            try
            {
                // instance a memory stream and pass the
                // byte array to its constructor
                MemoryStream ms = new MemoryStream(f);

                // instance a filestream pointing to the
                // storage folder, use the original file name
                // to name the resulting file
                // FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.MapPath("~/Contents/img/upload/") +fileName, FileMode.Create);

                if (!File.Exists(Server.MapPath("~/Contents/img/upload/") + fileName))
                {
                    FileStream fs = new FileStream(Server.MapPath("~/Contents/img/upload/") + fileName, FileMode.Create);

                    // write the memory stream containing the original
                    // file as a byte array to the filestream
                    ms.WriteTo(fs);
                    // clean up
                    ms.Close();
                    fs.Close();
                    fs.Dispose();
                    //ret = Server.MapPath("~/Contents/img/upload/") + fileName;
                    ret = "http://api.socioboard.com/Contents/img/upload/"+fileName;
                }
                else
                {
                    ret = "File Already Exist";
                }
                // return OK if we made it this far


            }
            catch (Exception ex)
            {
                // return the error message if the operation fails
                return ex.Message.ToString();
            }
            return ret;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string ADDScheduledMessage(string typeandid, string ShareMessage, DateTime ClientTime, DateTime ScheduleTime, bool Status, string UserId, string PicUrl, DateTime CreateTime)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                List<string> type = new List<string>();
                List<string> profileid = new List<string>();
                string[] TypeandId = typeandid.Split(',');
                for (int i = 0; i < TypeandId.Length; i = i + 2)
                {
                    type.Add(TypeandId[i]);
                    profileid.Add(TypeandId[i + 1]);

                }
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                ScheduledMessage objScheduledMessage = new ScheduledMessage();

                try
                {

                    for (int i = 0; i < type.Count; i++)
                    {
                        objScheduledMessage.Id = Guid.NewGuid();
                        objScheduledMessage.ShareMessage = ShareMessage;
                        objScheduledMessage.ClientTime = ClientTime;
                        objScheduledMessage.ScheduleTime = ScheduleTime;
                        objScheduledMessage.CreateTime = CreateTime;
                        objScheduledMessage.Status = Status;
                        objScheduledMessage.UserId = userid;
                        objScheduledMessage.ProfileType = type[i];
                        objScheduledMessage.PicUrl = PicUrl;
                        objScheduledMessage.ProfileId = profileid[i];
                        objScheduledMessageRepository.addNewMessage(objScheduledMessage);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                ScheduledMessage objScheduledMessages = new ScheduledMessage();
                return new JavaScriptSerializer().Serialize(typeandid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateScheduledMessage(string typeidandmsgid, string ShareMessage,DateTime scheduledTime,string picurl)
        {
            try
            {
                //Guid msgid = Guid.Parse(MsgId);
                List<string> type = new List<string>();
                List<string> profileid = new List<string>();
                List<string> msgid = new List<string>();
                string[] TypeandId = typeidandmsgid.Split(',');
                for (int i = 0; i < TypeandId.Length; i = i + 3)
                {
                    type.Add(TypeandId[i]);
                    profileid.Add(TypeandId[i + 1]);
                    msgid.Add(TypeandId[i + 2]);

                }
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                ScheduledMessage objScheduledMessage = new ScheduledMessage();

                try
                {

                    for (int i = 0; i < type.Count; i++)
                    {

                        objScheduledMessageRepository.UpdateProfileScheduleMessage(Guid.Parse(msgid[i]), profileid[i], ShareMessage, type[i],scheduledTime,picurl);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                ScheduledMessage objScheduledMessages = new ScheduledMessage();
                return new JavaScriptSerializer().Serialize(typeidandmsgid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllScheduledMessageByUserId(string UserId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<ScheduledMessage> lstScheduledMessages = new List<ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getAllMessagesOfUser(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                //FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
                //return lstScheduledMessages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllUnSentMessagesOfUser(string UserId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<ScheduledMessage> lstScheduledMessages = new List<ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getAllIUnSentMessagesOfUser(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetWooQueueMessageByUserId(string UserId)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<ScheduledMessage> lstScheduledMessages = new List<ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getWooQueueMessage(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetScheduleMessageByMessageId(string MessageId)
        {
            try
            {

                Guid userid = Guid.Parse(MessageId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                ScheduledMessage ScheduledMessages = new ScheduledMessage();
                ScheduledMessages = objScheduledMessageRepository.getScheduleMessageByMessageId(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(ScheduledMessages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string CheckMessageExistsAtTime(string UserId, DateTime schetime)
        {
            bool isexist;
            try
            {

                Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                //ScheduledMessage ScheduledMessages = new ScheduledMessage();

                isexist = objScheduledMessageRepository.checkMessageExistsAtTime(userid, schetime);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(isexist);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllMessagesOfUser(string UserId, string profileid)
        {
            try
            {
                Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<ScheduledMessage> lstScheduledMessages = new List<ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getAllMessagesOfUser(userid, profileid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

    }
}
