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
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendInvitationMail(string SenderEmail, string SenderName, string FriendsEmail)
        {
            string ret = string.Empty;
            string mailbody = string.Empty;
            string code = Utility.GenerateRandomUniqueString();
            string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/Invitationmailer_template.html");
            mailbody = File.ReadAllText(mailpath);
            mailbody = mailbody.Replace("[FriendName]", SenderName);
            mailbody = mailbody.Replace("[CODE]",code);
            mailbody = mailbody.Replace("[DomainName]", ConfigurationManager.AppSettings["DomainName"]);
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            ret = objMailHelper.SendInvitationMailByMandrill(SenderEmail, SenderName, FriendsEmail, pass, mailbody);
            Domain.Socioboard.Domain.Invitation objInvite = new Domain.Socioboard.Domain.Invitation();
            Domain.Socioboard.Domain.User _user = userRepo.getUserInfoByEmail(SenderEmail);
            objInvite.Id=Guid.NewGuid();
            objInvite.SenderEmail=_user.EmailId;
            objInvite.SenderUserId=_user.Id;
            objInvite.FriendEmail=FriendsEmail;
            objInvite.SendInvitationDate = DateTime.Now;
            objInvite.InvitationCode = code;
            InvitationRepo.Add(objInvite);
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

    }
}
