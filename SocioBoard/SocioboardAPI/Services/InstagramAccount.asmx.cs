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
        InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();
        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
        SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
        TeamRepository objTeamRepository = new TeamRepository();
        InstagramCommentRepository objInstagramCommentRepository = new InstagramCommentRepository();
        InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
        Domain.Socioboard.Domain.InstagramAccount objInstagramAccount;
        GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
        UserRepository objUserRepository = new UserRepository();
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllInstagramAccounts(string Userid)
        {
            List<Domain.Socioboard.Domain.InstagramAccount> objInstagramAccount = objInstagramAccountRepository.GetAllInstagramAccountsOfUser(Guid.Parse(Userid));
            return new JavaScriptSerializer().Serialize(objInstagramAccount);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UserInformation(string UserId, string InstagramId)
        {
            objInstagramAccount = new Domain.Socioboard.Domain.InstagramAccount();
            try
            {
                Guid Userid = Guid.Parse(UserId);
                if (objInstagramAccountRepository.checkInstagramUserExists(InstagramId, Guid.Parse(UserId)))
                {
                    objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId, Userid);
                }
                else
                {
                    objInstagramAccount = objInstagramAccountRepository.getInstagramAccountDetailsById(InstagramId);
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
                objInstagramAccountRepository.deleteInstagramUser(ProfileId, Guid.Parse(UserId));
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
                        lstInstagramAccount.Add(objInstagramAccountRepository.getInstagramAccountDetailsById(item.ProfileId,Guid.Parse(userid)));
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

        [WebMethod]
        public string AddInstagramAccountFromInstaBoard(string UserId, string GroupId, string ProfileId, string AccessToken, string FriendsCount, string Name, string EmailId,string ProfilePicUrl, string TotalImage, string Followers, string Followings)
        {
            if (objUserRepository.IsUserExist(Guid.Parse(UserId)))
            {
                if (!objInstagramAccountRepository.checkInstagramUserExists(objInstagramAccount.InstagramId, Guid.Parse(UserId)))
                {
                    objInstagramAccount = new Domain.Socioboard.Domain.InstagramAccount();
                    objInstagramAccount.Id = Guid.NewGuid();
                    objInstagramAccount.InstagramId = ProfileId;
                    objInstagramAccount.InsUserName = Name;
                    objInstagramAccount.IsActive = true;
                    objInstagramAccount.ProfileUrl = ProfilePicUrl;
                    objInstagramAccount.TotalImages = Int32.Parse(TotalImage);
                    objInstagramAccount.Followers = Int32.Parse(Followers);
                    objInstagramAccount.FollowedBy = Int32.Parse(Followings);
                    objInstagramAccount.UserId = Guid.Parse(UserId);
                    objInstagramAccountRepository.addInstagramUser(objInstagramAccount);
                    #region Add TeamMemberProfile
                    Domain.Socioboard.Domain.Team objTeam = objTeamRepository.GetTeamByGroupId(Guid.Parse(GroupId));
                    Domain.Socioboard.Domain.TeamMemberProfile objTeamMemberProfile = new Domain.Socioboard.Domain.TeamMemberProfile();
                    if (objTeamMemberProfileRepository.checkTeamMemberProfile(objTeam.Id, ProfileId))
                    {
                        objTeamMemberProfile.Id = Guid.NewGuid();
                        objTeamMemberProfile.TeamId = objTeam.Id;
                        objTeamMemberProfile.Status = 1;
                        objTeamMemberProfile.ProfileType = "instagram";
                        objTeamMemberProfile.StatusUpdateDate = DateTime.Now;
                        objTeamMemberProfile.ProfileId = ProfileId;
                        objTeamMemberProfile.ProfilePicUrl = ProfilePicUrl;
                        objTeamMemberProfile.ProfileName = Name;
                        objTeamMemberProfileRepository.addNewTeamMember(objTeamMemberProfile);
                    }
                    #endregion
                    #region SocialProfile
                    Domain.Socioboard.Domain.SocialProfile objSocialProfile = new Domain.Socioboard.Domain.SocialProfile();
                    objSocialProfile.Id = Guid.NewGuid();
                    objSocialProfile.ProfileType = "instagram";
                    objSocialProfile.ProfileId = ProfileId;
                    objSocialProfile.UserId = Guid.Parse(UserId);
                    objSocialProfile.ProfileDate = DateTime.Now;
                    objSocialProfile.ProfileStatus = 1;
                    #endregion
                    #region Add SocialProfile
                    if (!objSocialProfilesRepository.checkUserProfileExist(objSocialProfile))
                    {
                        objSocialProfilesRepository.addNewProfileForUser(objSocialProfile);
                    }
                    #endregion
                    return "account added";
                }
                else
                {
                    return "account already exist";
                }
            }
            else {
                return "user not exist";
            }
        }

        [WebMethod]
        public string GetInstagramVideosCount(string ProfileIds, string days)
        {
            int videoscout = objInstagramAccountRepository.GetInstagramVideosCount(ProfileIds,Int32.Parse(days));
            return videoscout.ToString();
        }
        [WebMethod]
        public string GetInstagramImageCount(string ProfileIds, string days)
        {
            int imagecount = objInstagramAccountRepository.GetInstagramImagesCount(ProfileIds, Int32.Parse(days));
            return imagecount.ToString();
        }
        [WebMethod]
        public string GetInstagramLikesCount(string ProfileIds, string days)
        {
            int likecount = objInstagramAccountRepository.GetInstagramLikesCount(ProfileIds,Int32.Parse(days));
            return likecount.ToString();
        }
        [WebMethod]
        public string GetInstagramCommentCount(string ProfileIds, string days)
        {
            int commentCount = objInstagramAccountRepository.GetInstagramCommentCount(ProfileIds,Int32.Parse(days));
            return commentCount.ToString();
        }
    }
}
