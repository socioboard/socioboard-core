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
using System.Collections;

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
    public class LinkedinAccount : System.Web.Services.WebService
    {
        LinkedInAccountRepository objlinkedinaccrepo = new LinkedInAccountRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string LinkedinId)
        {
            try
            {
                Guid Userid = Guid.Parse(UserId);
                Domain.Socioboard.Domain.LinkedInAccount LinkedAccount = objlinkedinaccrepo.getUserInformation(Userid, LinkedinId);
                return new JavaScriptSerializer().Serialize(LinkedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllLinkedinAccountsOfUser(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.LinkedInAccount> objLinkedInAccount = objlinkedinaccrepo.GetAllLinkedinAccountsOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objLinkedInAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetLinkedinAccountDetailsById(string UserId, string LinkedinId)
        {
             Domain.Socioboard.Domain.LinkedInAccount LinkedAccount=new LinkedInAccount ();
            try
            {
                Guid Userid = Guid.Parse(UserId);
                if(objlinkedinaccrepo.checkLinkedinUserExists(LinkedinId,Userid))
                {
               LinkedAccount = objlinkedinaccrepo.getUserInformation(Userid, LinkedinId);
                }
                else
                {
                    LinkedAccount=objlinkedinaccrepo.getUserInformation(LinkedinId);
                }
                return new JavaScriptSerializer().Serialize(LinkedAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return new JavaScriptSerializer().Serialize("Please Try Again");
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteLinkedinAccount(string UserId, string ProfileId, string GroupId)
        {
            try
            {
                objlinkedinaccrepo.deleteLinkedinUser(ProfileId, Guid.Parse(UserId));
                objLinkedInFeedRepository.deleteAllFeedsOfUser(ProfileId,Guid.Parse(UserId));
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
        public string GetAllLinkedinAccountsByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                LinkedInAccountRepository _objLinkedInAccountRepository = new LinkedInAccountRepository();
                List<Domain.Socioboard.Domain.LinkedInAccount> lstLinkedInAccount = new List<Domain.Socioboard.Domain.LinkedInAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "linkedin");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        if(_objLinkedInAccountRepository.checkLinkedinUserExists(item.ProfileId,Guid.Parse(userid)))
                        {
                        lstLinkedInAccount.Add(objlinkedinaccrepo.getUserInformation(Guid.Parse(userid), item.ProfileId));
                        }
                        else
                        {
                            lstLinkedInAccount.Add(objlinkedinaccrepo.getUserInformation(item.ProfileId));
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }
                return new JavaScriptSerializer().Serialize(lstLinkedInAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllLinkedinAccounts()
        {
            try
            {
                ArrayList lstLiAcc = objlinkedinaccrepo.getAllLinkedinAccounts();
                return new JavaScriptSerializer().Serialize(lstLiAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

    }
}
