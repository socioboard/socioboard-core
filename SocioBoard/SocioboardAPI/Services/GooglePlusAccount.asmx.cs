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
    public class GooglePlusAccount : System.Web.Services.WebService
    {
        GooglePlusAccountRepository ObjGooglePlusAccountsRepo = new GooglePlusAccountRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllGooglePlusAccounts()
        {
            try
            {
                ArrayList lstGooglePlusAcc = ObjGooglePlusAccountsRepo.getAllGooglePlusAccounts();
                return new JavaScriptSerializer().Serialize(lstGooglePlusAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetGooglePlusAccountDetailsById(string UserId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.GooglePlusAccount objGpAccount = ObjGooglePlusAccountsRepo.getGooglePlusAccountDetailsById(ProfileId, Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objGpAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateGooglePlusAccountByAdmin(string ObjGooglePlus)
        {
            Domain.Socioboard.Domain.GooglePlusAccount ObjGooglePlusAccount = (Domain.Socioboard.Domain.GooglePlusAccount)(new JavaScriptSerializer().Deserialize(ObjGooglePlus, typeof(Domain.Socioboard.Domain.GooglePlusAccount)));
            try
            {
                ObjGooglePlusAccountsRepo.updateGooglePlusUser(ObjGooglePlusAccount);
                return new JavaScriptSerializer().Serialize("Update Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Something went Wrong");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllBloggerAccountByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.GooglePlusAccount> lstGooglePlusAccount = new List<Domain.Socioboard.Domain.GooglePlusAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "gplus");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstGooglePlusAccount.Add(ObjGooglePlusAccountsRepo.getGooglePlusAccountDetailsById(item.ProfileId,Guid.Parse(userid)));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstGooglePlusAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void DeleteGplusAccount(string UserId, string ProfileId, string GroupId)
        {
            try
            {
                ObjGooglePlusAccountsRepo.deleteGooglePlusUser(ProfileId, Guid.Parse(UserId));
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                objTeamMemberProfileRepository.DeleteTeamMemberProfileByTeamIdProfileId(ProfileId, objTeam.Id);
                objSocialProfilesRepository.deleteProfile(Guid.Parse(UserId), ProfileId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteGplusAccount => " + ex.Message);
            }
        }

    }
}
