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

    public class TwitterAccount : System.Web.Services.WebService
    {
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        TwitterAccountRepository objTwitterAccountRepository = new TwitterAccountRepository();
        TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
        TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
        TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
        TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetTwitterAccountDetailsById(string UserId, string ProfileId)
        {
             Domain.Socioboard.Domain.TwitterAccount objTwitterAccount=new Domain.Socioboard.Domain.TwitterAccount ();
            try
            {
                if (objTwitterAccountRepository.checkTwitterUserExists(ProfileId, Guid.Parse(UserId)))
                {
                    objTwitterAccount = objTwitterAccountRepository.GetUserInformation(Guid.Parse(UserId), ProfileId);
                }
                else
                {
                    objTwitterAccount = objTwitterAccountRepository.getUserInformation(ProfileId);
                }
                return new JavaScriptSerializer().Serialize(objTwitterAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string DeleteTwitterAccount(string UserId, string ProfileId, string GroupId)
        {
            try
            {
                objTwitterAccountRepository.deleteTwitterUser(Guid.Parse(UserId), ProfileId);
                objTwitterFeedRepository.deleteTwitterFeed(ProfileId, Guid.Parse(UserId));
                objTwtstats.deleteTwitterStats(Guid.Parse(UserId), ProfileId);
                objTwitterMessageRepository.deleteTwitterMessage(ProfileId, Guid.Parse(UserId));
                objTwitterDirectMessageRepository.deleteDirectMessage(Guid.Parse(UserId), ProfileId);
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
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

        //getAllTwitterAccountsOfUser
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]

        public string getAllTwitterAccountsOfUser(string UserId)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterAccount> objTwitterAccount = objTwitterAccountRepository.getAllTwitterAccountsOfUser(Guid.Parse(UserId));
                return new JavaScriptSerializer().Serialize(objTwitterAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterAccountsByUserIdAndGroupId(string userid, string groupid)
        {
            try
            {
                List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = new List<Domain.Socioboard.Domain.TwitterAccount>();
                Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(groupid));
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberProfile = objTeamMemberProfileRepository.GetTeamMemberProfileByTeamIdAndProfileType(objTeam.Id, "twitter");
                foreach (var item in lstTeamMemberProfile)
                {
                    try
                    {
                        lstTwitterAccount.Add(objTwitterAccountRepository.GetUserInformation(Guid.Parse(userid), item.ProfileId));
                    }
                    catch (Exception)
                    {

                    }
                }
                return new JavaScriptSerializer().Serialize(lstTwitterAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }
 
        //vikash
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllAccountDetailsByProfileId(string profileid, string userid)
        {
            try
            {

                List<Domain.Socioboard.Domain.TwitterAccount> lstAllTwtAccount = objTwitterAccountRepository.getAllAccountDetail(profileid, Guid.Parse(userid));
                return new JavaScriptSerializer().Serialize(lstAllTwtAccount);
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);
                return "Something Went Wrong";
            }

        }
     
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string getUserInformation(string TwtUserId)
        {
            try
            {
                Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = objTwitterAccountRepository.getUserInformation(TwtUserId);
                return new JavaScriptSerializer().Serialize(objTwitterAccount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



        public Domain.Socioboard.Domain.TwitterAccount AcccountDetails(string profileid, Guid userid)
        {
            Domain.Socioboard.Domain.TwitterAccount objTwitterAccount = new Domain.Socioboard.Domain.TwitterAccount();
            try
            {
                objTwitterAccount = objTwitterAccountRepository.GetUserInformation(userid, profileid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return (objTwitterAccount);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllTwitterAccounts()
        {
            try
            {
                ArrayList lstTwtAcc = objTwitterAccountRepository.getAllTwitterAccounts();
                return new JavaScriptSerializer().Serialize(lstTwtAcc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Something Went Wrong";
            }
        }



    }




}
