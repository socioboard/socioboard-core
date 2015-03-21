using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using GlobusMailLib;
using System.IO;
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
    public class Invitation : System.Web.Services.WebService
    {
        InvitationRepository InvitationRepo = new InvitationRepository();
        UserRepository userRepo = new UserRepository();
         ILog logger = LogManager.GetLogger(typeof(Invitation));
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendInvitationMail(string SenderEmail, string SenderName, string FriendsEmail)
        {
            string ret = string.Empty;
            string mailbody = string.Empty;
            //string code = Utility.GenerateRandomUniqueString();
            Domain.Socioboard.Domain.User _user = userRepo.getUserInfoByEmail(SenderEmail);
            string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/Invitationmailer_template.html");
            mailbody = File.ReadAllText(mailpath);
            mailbody = mailbody.Replace("[FriendName]", SenderName);
            if (_user.UserCode == null || _user.UserCode == "")
            {
                string code = Utility.GenerateRandomUniqueString();
                int retint = userRepo.UpdateCode(_user.Id,code);
                _user = userRepo.getUsersById(_user.Id);

            }
            mailbody = mailbody.Replace("[CODE]", _user.UserCode);
            mailbody = mailbody.Replace("[DomainName]", ConfigurationManager.AppSettings["DomainName"]);
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            ret = objMailHelper.SendInvitationMailByMandrill(SenderEmail, SenderName, FriendsEmail, pass, mailbody);
            //Domain.Socioboard.Domain.Invitation objInvite = new Domain.Socioboard.Domain.Invitation();
            //Domain.Socioboard.Domain.User _user = userRepo.getUserInfoByEmail(SenderEmail);
            //objInvite.Id=Guid.NewGuid();
            //objInvite.SenderEmail=_user.EmailId;
            //objInvite.SenderUserId=_user.Id;
            //objInvite.FriendEmail=FriendsEmail;
            //objInvite.SendInvitationDate = DateTime.Now;
            //objInvite.InvitationCode = code;
            //InvitationRepo.Add(objInvite);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public bool IsFriendAlreadydInvited(string UserId, string FriendsEmail)
        {
            return InvitationRepo.IsFriendAlreadydInvited(Guid.Parse(UserId), FriendsEmail);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInvitationInfoBycode(string code)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(InvitationRepo.GetInvitationInfoBycode(code));
            }
            catch (Exception ex)
            {
                return "Somthing went wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateInvitatoinStatus(string invitationid, string userid)
        {
            try
            {
                int ret = InvitationRepo.UpdateInvitatoinStatus(Guid.Parse(invitationid), Guid.Parse(userid), DateTime.Now);
                return "success";
            }
            catch (Exception ex)
            {
                return "Something went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllInvitedDataOfUser(string UserId)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(InvitationRepo.GetAllInvitedDataOfUser(Guid.Parse(UserId)));
            }
            catch (Exception ex)
            {
                return "Somthing Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInvitedInfo(string UserId)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(InvitationRepo.UserInvitedInfo(Guid.Parse(UserId)));
            }
            catch (Exception ex)
            {
                return "Somthing Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetInvitedDataOfAcceotedUser(string UserId)
        {
            try
            {
                return new JavaScriptSerializer().Serialize(InvitationRepo.GetInvitedDataOfAcceptedUser(Guid.Parse(UserId)));
            }
            catch (Exception ex)
            {
                return "Somthing Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void AddInvitationInfoBycode(string code, string Email, string UserId)
        {
             try
                {
                    Domain.Socioboard.Domain.Invitation _Invitation=new Domain.Socioboard.Domain.Invitation();
                    Domain.Socioboard.Domain.User _User = userRepo.GetUserInfoByCode(code);
                    if (_User!=null)
                    {
                        _Invitation.Id = Guid.NewGuid();
                        _Invitation.SenderEmail = _User.EmailId;
                        _Invitation.SenderUserId = _User.Id;
                        _Invitation.FriendEmail = Email;
                        _Invitation.FriendUserId = Guid.Parse(UserId);
                        _Invitation.InvitationCode = code;
                        _Invitation.AcceptInvitationDate = DateTime.Now;
                        _Invitation.Status = 1;
                        InvitationRepo.Add(_Invitation);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("AddInvitationInfoBycode : "+ex.StackTrace);
                    logger.Error("AddInvitationInfoBycode : "+ex.Message);
                }
          
        }


    }
}
