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
    public class TumblrAccount : System.Web.Services.WebService
    {
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
        TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
       
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTumblrAccountDetailsById(string UserId, string ProfileId)
        {
            try
            {
                Domain.Socioboard.Domain.TumblrAccount objTumblrAccount=new Domain.Socioboard.Domain.TumblrAccount ();
                objTumblrAccount.UserId = Guid.Parse(UserId);
                objTumblrAccount.tblrUserName = ProfileId;
                if(objTumblrAccountRepository.checkTubmlrUserExists(objTumblrAccount))
                {
                objTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(ProfileId, Guid.Parse(UserId));
                }
                else
                {
                    objTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(ProfileId);
            }
                return new JavaScriptSerializer().Serialize(objTumblrAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTumblrAccountsOfUser(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TumblrAccount> lstTumblrAccount = objTumblrAccountRepository.getTumblrAccountDetailsById(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(lstTumblrAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteTumblrAccount(string UserId, string ProfileId, string GroupId)
        {
            try
            {
                objTumblrAccountRepository.deleteTumblrUser(ProfileId,Guid.Parse(UserId));
                objTumblrFeedRepository.DeleteTumblrDataByUserid(Guid.Parse(UserId), ProfileId);
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
        public string GetAllTumblrAccountsByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.TumblrAccount> lstTumblrAccount = new List<Domain.Socioboard.Domain.TumblrAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "tumblr");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstTumblrAccount.Add(objTumblrAccountRepository.getTumblrAccountDetailsById(item.ProfileId, Guid.Parse(userid)));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstTumblrAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTumblrAccounts()
        {
            try
            {
                ArrayList lstTumblrAcc = objTumblrAccountRepository.getAllTumblrAccounts();
                return new JavaScriptSerializer().Serialize(lstTumblrAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }


    }
}
