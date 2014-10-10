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
    public class GroupProfile : System.Web.Services.WebService
    {
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        Domain.Socioboard.Domain.GroupProfile objGroupProfile = new Domain.Socioboard.Domain.GroupProfile();
        TeamRepository objTeamRepository = new TeamRepository();
        Domain.Socioboard.Domain.Team objTeam = new Domain.Socioboard.Domain.Team();
        Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllProfilesConnectedWithGroup(string UserId, string GroupId)
        {
            try
            {
                List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfile = objGroupProfileRepository.getAllGroupProfiles(Guid.Parse(UserId), Guid.Parse(GroupId));
                return new JavaScriptSerializer().Serialize(lstGroupProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddProfileToGroup(string profileid, string network, string groupid, string userid)
        {
            objGroupProfile = new Domain.Socioboard.Domain.GroupProfile();
            objGroupProfile.Id = Guid.NewGuid();
            objGroupProfile.GroupId = Guid.Parse(groupid);
            objGroupProfile.ProfileId = profileid;
            objGroupProfile.GroupOwnerId = Guid.Parse(userid);
            objGroupProfile.ProfileType = network;
            objGroupProfile.EntryDate = DateTime.Now;
            objGroupProfileRepository.AddGroupProfile(objGroupProfile);
            objTeam = new Domain.Socioboard.Domain.Team();
            objTeam=objTeamRepository.GetAllTeam(Guid.Parse(groupid), Guid.Parse(userid));
            objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
            objTeamMemberProfile.Id = Guid.NewGuid();
            objTeamMemberProfile.TeamId = objTeam.Id;
            objTeamMemberProfile.ProfileId = profileid;
            objTeamMemberProfile.ProfileType = network;
            objTeamMemberProfile.Status = 1;
            objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
            objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
            return "";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteProfileFromGroup(string profileid, string groupid, string userid)
        {
            objGroupProfileRepository.DeleteGroupProfile(Guid.Parse(userid), profileid, Guid.Parse(groupid));
            objTeam = new Domain.Socioboard.Domain.Team();
            objTeam = objTeamRepository.GetAllTeam(Guid.Parse(groupid), Guid.Parse(userid));
            objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(profileid, objTeam.Id);
            return "";
        }
    }
}
