using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections;
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
    public class YoutubeAccount : System.Web.Services.WebService
    {
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();
        YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetYoutubeAccountDetailsById(string UserId, string ProfileId)
        {
            Domain.Socioboard.Domain.YoutubeAccount objYoutubeAccount=new Domain.Socioboard.Domain.YoutubeAccount ();
            try
            {
                objYoutubeAccount.Ytuserid=ProfileId;
                objYoutubeAccount.UserId=Guid.Parse(UserId);
                if (objYoutubeAccountRepository.checkTubmlrUserExists(objYoutubeAccount))
                {
                    objYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(ProfileId, Guid.Parse(UserId));
                }
                else
                {
                    objYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(ProfileId);
                }
                return new JavaScriptSerializer().Serialize(objYoutubeAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllYoutubeAccountDetailsById(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.YoutubeAccount> lstYoutubeAccount = objYoutubeAccountRepository.getYoutubeAccountDetailsById(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstYoutubeAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteYoutubeAccount(string UserId, string ProfileId, string GroupId)
        {
            try
            {
                objYoutubeAccountRepository.deleteYoutubeUser(Guid.Parse(UserId), ProfileId);
                objYoutubeChannelRepository.DeleteProfileDataByUserid(ProfileId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
                objGroupProfileRepository.DeleteGroupProfile(Guid.Parse(UserId), ProfileId, Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(ProfileId, objTeam.Id);
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId);
                return new JavaScriptSerializer().Serialize("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllYoutubeAccountsByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.YoutubeAccount> lstYoutubeAccount = new List<Domain.Socioboard.Domain.YoutubeAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "youtube");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstYoutubeAccount.Add(objYoutubeAccountRepository.getYoutubeAccountDetailsById(item.ProfileId, Guid.Parse(userid)));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstYoutubeAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllYoutubeAccounts()
        {
            try
            {
                YoutubeAccountRepository objyoutube = new YoutubeAccountRepository();
                List<Domain.Socioboard.Domain.YoutubeAccount> lstYoutubeAcc = objyoutube.getAllYoutubeAccounts();
                return new JavaScriptSerializer().Serialize(lstYoutubeAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }




    }
}
