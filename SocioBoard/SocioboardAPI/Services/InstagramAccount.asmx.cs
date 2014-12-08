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
    public class InstagramAccount : System.Web.Services.WebService
    {
        InstagramAccountRepository objLinkedInAccountRepository = new InstagramAccountRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        InstagramCommentRepository objInstagramCommentRepository = new InstagramCommentRepository();
        InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
        Domain.Socioboard.Domain.InstagramAccount objInstagramAccount;
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllInstagramAccounts(string Userid)
        {
            List<Domain.Socioboard.Domain.InstagramAccount> objInstagramAccount = objLinkedInAccountRepository.GetAllInstagramAccountsOfUser(Guid.Parse(Userid));
            return new JavaScriptSerializer().Serialize(objInstagramAccount);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string LinkedinId)
        {
            objInstagramAccount = new Domain.Socioboard.Domain.InstagramAccount();
            try
            {
                Guid Userid = Guid.Parse(UserId);
                if (objLinkedInAccountRepository.checkInstagramUserExists(LinkedinId, Guid.Parse(UserId)))
                {
                    objInstagramAccount = objLinkedInAccountRepository.getInstagramAccountDetailsById(LinkedinId, Userid);
                }
                else
                {
                    objInstagramAccount = objLinkedInAccountRepository.getInstagramAccountDetailsById(LinkedinId);
                }
                return new JavaScriptSerializer().Serialize(objInstagramAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteInstagramAccount(string UserId, string ProfileId, string GroupId)
        {
            try
            {
                objLinkedInAccountRepository.deleteInstagramUser(ProfileId, Guid.Parse(UserId));
                objInstagramCommentRepository.deleteAllCommentsOfUser(ProfileId, Guid.Parse(UserId));
                objInstagramFeedRepository.deleteAllFeedsOfUser(ProfileId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objGroupProfileRepository.DeleteGroupProfile(Guid.Parse(UserId), ProfileId, Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(ProfileId, objTeam.Id);
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllInstagramAccountsByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.InstagramAccount> lstInstagramAccount = new List<Domain.Socioboard.Domain.InstagramAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "instagram");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstInstagramAccount.Add(objLinkedInAccountRepository.getInstagramAccountDetailsById(item.ProfileId,Guid.Parse(userid)));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstInstagramAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
    }
}
