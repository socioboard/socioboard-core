using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Domain.Socioboard.Domain;
using System.Text.RegularExpressions;
using log4net;

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
    public class ScheduledMessage : System.Web.Services.WebService
    {
        ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
        Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage;
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        ILog logger = LogManager.GetLogger(typeof(ScheduledMessage));

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
                    ret = Server.MapPath("~/Contents/img/upload/") + fileName;
                    //ret = "http://api.socioboard.com/Contents/img/upload/" + fileName;
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
                objScheduledMessage = new Domain.Socioboard.Domain.ScheduledMessage();

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
                    logger.Error(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                ScheduledMessage objScheduledMessages = new ScheduledMessage();
                return new JavaScriptSerializer().Serialize(typeandid);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddGroupScheduleMessages(String ScheduleTime, string CreateTime, string ProfileType, string ProfileId, string PicUrl, string ClientTime, string ShareMessage, string UserId, string Status)
        {
            try
            {
                DateTime scheduleddatetime = Convert.ToDateTime(CompareDateWithclient(ClientTime, (ScheduleTime).ToString()));
                
                Guid userid = Guid.Parse(UserId);
                Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage = new Domain.Socioboard.Domain.ScheduledMessage();

                objScheduledMessage.Id = Guid.NewGuid();
                objScheduledMessage.ShareMessage = ShareMessage;
                logger.Error("ScheduledMessage.asmx >> AddGroupScheduleMessages >> ClientTime = " + ClientTime);
                objScheduledMessage.ClientTime = Convert.ToDateTime(ClientTime);
                //objScheduledMessage.ScheduleTime = Convert.ToDateTime(ScheduleTime);
                objScheduledMessage.ScheduleTime = scheduleddatetime.ToLocalTime();
                //objScheduledMessage.CreateTime = Convert.ToDateTime(CreateTime);
                objScheduledMessage.CreateTime = DateTime.Now;
                objScheduledMessage.Status = Convert.ToBoolean(Status);
                objScheduledMessage.UserId = userid;
                objScheduledMessage.ProfileType = ProfileType;
                objScheduledMessage.PicUrl = PicUrl;
                objScheduledMessage.ProfileId = ProfileId;
                objScheduledMessageRepository.addNewMessage(objScheduledMessage);

                return new JavaScriptSerializer().Serialize(objScheduledMessage);
            }
            catch (Exception ex)
            {
                logger.Error("AddGroupScheduleMessages : " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateScheduledMessage(string typeidandmsgid, string ShareMessage, DateTime scheduledTime, string picurl)
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

                        objScheduledMessageRepository.UpdateProfileScheduleMessage(Guid.Parse(msgid[i]), profileid[i], ShareMessage, type[i], scheduledTime, picurl);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                ScheduledMessage objScheduledMessages = new ScheduledMessage();
                return new JavaScriptSerializer().Serialize(typeidandmsgid);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getAllMessagesOfUser(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                //FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
                //return lstScheduledMessages;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getAllIUnSentMessagesOfUser(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllUnSentMessagesAccordingToGroup(string UserId, string profileid, string network)
        {
            try
            {

                Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.GetAllUnSentMessagesAccordingToGroup(userid, profileid, network);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getWooQueueMessage(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                Domain.Socioboard.Domain.ScheduledMessage ScheduledMessages = new Domain.Socioboard.Domain.ScheduledMessage();
                ScheduledMessages = objScheduledMessageRepository.getScheduleMessageByMessageId(userid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(ScheduledMessages);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
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
                logger.Error(ex.Message);
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
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                lstScheduledMessages = objScheduledMessageRepository.getAllMessagesOfUser(userid, profileid);

                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateScheduledMessageByMsgId(Guid msgId)
        {
            ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
            objScheduledMessageRepository.UpdateScheduledMessage(msgId);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getScheduledMessageByProfileType(string profileType)
        {
            try
            {
                //Guid userid = Guid.Parse(UserId);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = objScheduledMessageRepository.GetUnsentSchdeuledMessageByProfileType(profileType);

                if (lstScheduledMessages == null)
                {
                    lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                }

                return new JavaScriptSerializer().Serialize(lstScheduledMessages);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize(new List<Domain.Socioboard.Domain.ScheduledMessage>());
            }

        }



        public int GetAllScheduledMessage(string userid, string profileid, string days)
        {
            List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            try
            {
                _ScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetailsbyDate(Guid.Parse(userid), profileid, Convert.ToInt32(days));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return _ScheduledMessage.Count;
        }

        public string GetAllScheduleMsgDetailsForReport(string userid, string profileid, string days)
        {
            int count = 0;
            string countdetails = string.Empty;
            int piccount = 0;
            try
            {
               List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage = new List<Domain.Socioboard.Domain.ScheduledMessage>();
              _ScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetailsbyDate(Guid.Parse(userid), profileid, Convert.ToInt32(days));

              foreach (Domain.Socioboard.Domain.ScheduledMessage item in _ScheduledMessage)
              {
                  if (string.IsNullOrEmpty(item.PicUrl))
                  {
                      count++;
                  }                
                  else 
                  {
                      piccount++;
                  }
              }
              countdetails = "plaintext_" + count;
              countdetails = countdetails + "@" + piccount;           
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return countdetails;
        }
        //vikash
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getAllSentMessageDetails(string profileid, string userid)
        {
            try
            {
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                //List<Domain.Socioboard.Domain.ScheduledMessage> lstallScheduledMessage = objScheduledMessageRepository.getAllMessagesDetail(profileid, Guid.Parse(userid));
                List<Domain.Socioboard.Domain.ScheduledMessage> lstallScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetails(profileid, Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(lstallScheduledMessage);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("plese try again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetSociaoQueueMessageByUserIdAndGroupId(string UserId, string GroupId)
        {
            string profileid = string.Empty;

            TeamRepository objTeamRepository = new TeamRepository();
            try
            {
                Guid userid = Guid.Parse(UserId);
                Guid groupid = Guid.Parse(GroupId);
                List<Domain.Socioboard.Domain.ScheduledMessage> lstfianlscoailqueue = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(groupid);
                List<Domain.Socioboard.Domain.TeamMemberProfile> allprofiles = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(objTeam.Id);
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduledMessages = new List<Domain.Socioboard.Domain.ScheduledMessage>();
                Dictionary<string, List<Domain.Socioboard.Domain.ScheduledMessage>> objdic = new Dictionary<string, List<Domain.Socioboard.Domain.ScheduledMessage>>();
                foreach (var item in allprofiles)
                {
                    lstScheduledMessages = objScheduledMessageRepository.getAllMessagesDetail(item.ProfileId, userid);
                    if (lstScheduledMessages.Count > 0)
                    {
                        objdic.Add(item.ProfileId, lstScheduledMessages);
                    }

                }
                foreach (var item in objdic)
                {
                    foreach (var ScheduledMessage in item.Value)
                    {
                        lstfianlscoailqueue.Add(ScheduledMessage);
                    }
                }
                //FacebookAccountRepository facebookAccountRepo = new FacebookAccountRepository();
                // FacebookAccount facebook = facebookAccountRepo.getFacebookAccountDetailsById(FacebookId, userid);
                return new JavaScriptSerializer().Serialize(lstfianlscoailqueue);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteSchecduledMessage(string id)
        {
            try
            {
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                objScheduledMessage = new Domain.Socioboard.Domain.ScheduledMessage();
                objScheduledMessage.Id = Guid.Parse(id);
                objScheduledMessageRepository.deleteMessage(objScheduledMessage);
                return new JavaScriptSerializer().Serialize("");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string EditSchecduledMessage(string id, string msg)
        {
            try
            {
                ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
                objScheduledMessageRepository.UpdateProfileScheduleMessage(Guid.Parse(id), msg);
                return new JavaScriptSerializer().Serialize("");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please try Again");
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
    
        public string AddAllScheduledMessage(string typeandid, string ShareMessage, string ClientTime, string scheduleddate, string scheduletime, string UserId, string PicUrl)
        {
            string[] datearr = scheduleddate.Split(',');
            foreach (var date in datearr)
            {
                DateTime scheduleddatetime = Convert.ToDateTime(CompareDateWithclient(ClientTime, (date + " " + scheduletime).ToString()));
                string[] profileandidarr = Regex.Split(typeandid, "<:>");
                foreach (var item in profileandidarr)
                {
                    string[] profileandid = item.Split('~');
                    string profiletype = profileandid[1];
                    string profileid = profileandid[0];
                    objScheduledMessage = new Domain.Socioboard.Domain.ScheduledMessage();
                    objScheduledMessage.CreateTime = DateTime.Now;
                    objScheduledMessage.ProfileType = profiletype;
                    objScheduledMessage.ProfileId = profileid;
                    objScheduledMessage.Id = Guid.NewGuid();
                    objScheduledMessage.PicUrl = PicUrl;
                    DateTime client = Convert.ToDateTime(ClientTime); 
                    objScheduledMessage.ClientTime = client;
                    objScheduledMessage.ScheduleTime = scheduleddatetime;
                    objScheduledMessage.ShareMessage = ShareMessage;
                    objScheduledMessage.UserId = Guid.Parse(UserId);
                    objScheduledMessage.Status = false;
                    if (!objScheduledMessageRepository.checkMessageExistsAtTime(objScheduledMessage.UserId, objScheduledMessage.ShareMessage, objScheduledMessage.ScheduleTime, objScheduledMessage.ProfileId))
                    {
                        objScheduledMessageRepository.addNewMessage(objScheduledMessage);
                    }
                }
            }
            return new JavaScriptSerializer().Serialize("Scheduled"); ;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddComposeMessage(string UserId, string ProfileId, string ProfileType, string Message, string Imageurl)
        {
            ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();

            Domain.Socioboard.Domain.ScheduledMessage objScheduledMessage = new Domain.Socioboard.Domain.ScheduledMessage();
            objScheduledMessage.ShareMessage = Message;
            objScheduledMessage.ClientTime = DateTime.Now;
            objScheduledMessage.ScheduleTime = DateTime.Now;
            objScheduledMessage.CreateTime = DateTime.Now;
            objScheduledMessage.Status = true;
            objScheduledMessage.UserId = Guid.Parse(UserId);
            objScheduledMessage.ProfileType = ProfileType;
            objScheduledMessage.ProfileId = ProfileId;
            objScheduledMessage.PicUrl = Imageurl;
            objScheduledMessageRepository.addNewMessage(objScheduledMessage);
            return "success";
        }
        //vikash
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllScheduledMessageforADay(string userid, string profileid, string days)
        {
            List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            try
            {
                _ScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetailsforADay(Guid.Parse(userid), profileid, Convert.ToInt32(days));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return new JavaScriptSerializer().Serialize(_ScheduledMessage);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllScheduledMessageByDays(string userid, string profileid, string days)
        {
            List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            try
            {
                _ScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetailsByDays(Guid.Parse(userid), profileid, Convert.ToInt32(days));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return new JavaScriptSerializer().Serialize(_ScheduledMessage);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllScheduledMessageByMonth(string userid, string profileid, string month)
        {
            List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            try
            {
                _ScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetailsByMonth(Guid.Parse(userid), profileid, Convert.ToInt32(month));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return new JavaScriptSerializer().Serialize(_ScheduledMessage);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllSentMessageDetailsForCustomrange(string userid, string profileid, string sdate, string ldate)
        {
            List<Domain.Socioboard.Domain.ScheduledMessage> _ScheduledMessage = new List<Domain.Socioboard.Domain.ScheduledMessage>();
            try
            {
                _ScheduledMessage = objScheduledMessageRepository.getAllSentMessageDetailsForCustomrange(Guid.Parse(userid), profileid, Convert.ToDateTime(sdate), Convert.ToDateTime(ldate));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return new JavaScriptSerializer().Serialize(_ScheduledMessage);
        }

        public string CompareDateWithclient(string clientdate, string scheduletime)
        {
            try
            {
                DateTime client = Convert.ToDateTime(clientdate);
         
                DateTime server = DateTime.Now;
                DateTime schedule = Convert.ToDateTime(scheduletime);
                {
                    var kind = schedule.Kind; // will equal DateTimeKind.Unspecified
                    if (DateTime.Compare(client, server) > 0)
                    {
                        double minutes = (server - client).TotalMinutes;
                        schedule = schedule.AddMinutes(minutes);
                    }
                    else if (DateTime.Compare(client, server) == 0)
                    {
                    }
                    else if (DateTime.Compare(client, server) < 0)
                    {
                        double minutes = (server - client).TotalMinutes;
                        schedule = schedule.AddMinutes(minutes);
                    }
                }
                return schedule.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return "";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllScheduledDetails()
        {
            List<Domain.Socioboard.Helper.ScheduledTracker> _AllScheduledMessage = new List<Domain.Socioboard.Helper.ScheduledTracker>();
            try
            {
                _AllScheduledMessage = objScheduledMessageRepository.GetAllScheduledDetails();

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return new JavaScriptSerializer().Serialize(_AllScheduledMessage);
        }

        [WebMethod]
        //[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public int GetSentMessageCountByProfileIdAndUserId(string UserId, string ProfileIds)
        {
            try
            {
                return objScheduledMessageRepository.GetSentMessageCountByProfileIdAndUserId(Guid.Parse(UserId), ProfileIds);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        //vikash 20-08-2015

        [WebMethod]
        public string GetSentMessageCount(string Userid, string profileids, string days)
        {
            try
            {
                int sentmessage = objScheduledMessageRepository.GetSentMessageCount(Guid.Parse(Userid), profileids, Int32.Parse(days));
                return sentmessage.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        [WebMethod]
        public string GetClickCount(string UserId, string profileids, string days)
        {
            try
            {
                int clicks = objScheduledMessageRepository.GetClickCount(Guid.Parse(UserId), profileids, Int32.Parse(days));
                return clicks.ToString();
            }
            catch (Exception ex)
            {
                return "0";
            }
        }


    }
}
