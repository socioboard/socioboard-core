using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Model;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;

namespace SocioBoard.Helper
{
    public class LinkedInHelper
    {
        public void GetUserProfile(oAuthLinkedIn OAuth, string LinkedinUserId, Guid user)
        {
            LinkedInProfile objLinkedInProfile = new LinkedInProfile();
            LinkedInProfile.UserProfile objUserProfile = new LinkedInProfile.UserProfile();
            objUserProfile = objLinkedInProfile.GetUserProfile(OAuth);
            GetLinkedInUserProfile(objUserProfile, OAuth, user, LinkedinUserId);
        }

        public void GetLinkedInUserProfile(dynamic data, oAuthLinkedIn _oauth, Guid user, string LinkedinUserId)
        {
            SocialProfile socioprofile = new SocialProfile();
            SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
            LinkedInAccount objLinkedInAccount = new LinkedInAccount();
            LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
            try
            {
                objLinkedInAccount.UserId = user;
                objLinkedInAccount.LinkedinUserId = data.id.ToString();
                try
                {
                    objLinkedInAccount.EmailId = data.email.ToString();
                }
                catch { }
                objLinkedInAccount.LinkedinUserName = data.first_name.ToString() + data.last_name.ToString();
                objLinkedInAccount.OAuthToken = _oauth.Token;
                objLinkedInAccount.OAuthSecret = _oauth.TokenSecret;
                objLinkedInAccount.OAuthVerifier = _oauth.Verifier;
                try
                {
                    objLinkedInAccount.ProfileImageUrl = data.picture_url.ToString();
                }
                catch { }
                try
                {
                    objLinkedInAccount.ProfileUrl = data.profile_url.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                objLinkedInAccount.Connections = data.connections;
                objLinkedInAccount.IsActive = true;

                socioprofile.UserId = user;
                socioprofile.ProfileType = "linkedin";
                socioprofile.ProfileId = LinkedinUserId;
                socioprofile.ProfileStatus = 1;
                socioprofile.ProfileDate = DateTime.Now;
                socioprofile.Id = Guid.NewGuid();

            }
            catch
            {
            }
            try
            {
                if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                {
                    socioprofilerepo.addNewProfileForUser(socioprofile);

                    GroupRepository objGroupRepository = new GroupRepository();
                    SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)HttpContext.Current.Session["GroupName"];
                    Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                    if (lstDetails.GroupName == "Socioboard")
                    {
                        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                        TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                        teammemberprofile.Id = Guid.NewGuid();
                        teammemberprofile.TeamId = team.Id;
                        teammemberprofile.ProfileId = socioprofile.ProfileId;
                        teammemberprofile.ProfileType = "linkedin";
                        teammemberprofile.StatusUpdateDate = DateTime.Now;

                        objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);

                    }

                }
                else
                {
                    socioprofile.ProfileId = data.id.ToString();
                    socioprofilerepo.updateSocialProfile(socioprofile);
                }
                if (!objLiRepo.checkLinkedinUserExists(LinkedinUserId, user))
                {
                    objLiRepo.addLinkedinUser(objLinkedInAccount);
                }
                else
                {
                    objLinkedInAccount.LinkedinUserId = LinkedinUserId;
                    objLiRepo.updateLinkedinUser(objLinkedInAccount);
                }
                // GetLinkedInFeeds(_oauth, data.id, user.Id);
            }
            catch
            {

            }
        }


        public void GetLinkedInUserProfile(dynamic data, oAuthLinkedIn _oauth, User user)
        {
            SocialProfile socioprofile = new SocialProfile();
            SocialProfilesRepository socioprofilerepo = new SocialProfilesRepository();
            LinkedInAccount objLinkedInAccount = new LinkedInAccount();
            LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
            try
            {
                objLinkedInAccount.UserId = user.Id;
                objLinkedInAccount.LinkedinUserId = data.id.ToString();
                try
                {
                    objLinkedInAccount.EmailId = data.email.ToString();
                }
                catch { }
                objLinkedInAccount.LinkedinUserName = data.first_name.ToString() + data.last_name.ToString();
                objLinkedInAccount.OAuthToken = _oauth.Token;
                objLinkedInAccount.OAuthSecret = _oauth.TokenSecret;
                objLinkedInAccount.OAuthVerifier = _oauth.Verifier;
                try
                {
                    objLinkedInAccount.ProfileImageUrl = data.picture_url.ToString();
                }
                catch { }
                try
                {
                    objLinkedInAccount.ProfileUrl = data.profile_url.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                objLinkedInAccount.Connections = data.connections;
                objLinkedInAccount.IsActive = true;
                socioprofile.UserId = user.Id;
                socioprofile.ProfileType = "linkedin";
                socioprofile.ProfileId = data.id.ToString();
                socioprofile.ProfileStatus = 1;
                socioprofile.ProfileDate = DateTime.Now;
                socioprofile.Id = Guid.NewGuid();

            }
            catch
            {


            }


            try
            {
                if (!string.IsNullOrEmpty(objLinkedInAccount.LinkedinUserId))
                {
                    if (objLiRepo.checkLinkedinUserExists(objLinkedInAccount.LinkedinUserId,user.Id))
                    {
                        if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                        {
                            socioprofilerepo.addNewProfileForUser(socioprofile);
                             GroupRepository objGroupRepository = new GroupRepository();
                    SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)HttpContext.Current.Session["GroupName"];
                    Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                    if (lstDetails.GroupName == "Socioboard")
                    {
                        TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                        TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                        teammemberprofile.Id = Guid.NewGuid();
                        teammemberprofile.TeamId = team.Id;
                        teammemberprofile.ProfileId = socioprofile.ProfileId;
                        teammemberprofile.ProfileType = "linkedin";
                        teammemberprofile.StatusUpdateDate = DateTime.Now;

                        objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);

                    }
                        }
                        objLiRepo.updateLinkedinUser(objLinkedInAccount);
                        HttpContext.Current.Session["alreadyexist"] = objLinkedInAccount;
                    }
                    else
                    {
                        if (!socioprofilerepo.checkUserProfileExist(socioprofile))
                        {

                            socioprofilerepo.addNewProfileForUser(socioprofile);
                            GroupRepository objGroupRepository = new GroupRepository();
                            SocioBoard.Domain.Team team = (SocioBoard.Domain.Team)HttpContext.Current.Session["GroupName"];
                            Groups lstDetails = objGroupRepository.getGroupName(team.GroupId);
                            if (lstDetails.GroupName == "Socioboard")
                            {
                                TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
                                TeamMemberProfile teammemberprofile = new TeamMemberProfile();
                                teammemberprofile.Id = Guid.NewGuid();
                                teammemberprofile.TeamId = team.Id;
                                teammemberprofile.ProfileId = socioprofile.ProfileId;
                                teammemberprofile.ProfileType = "linkedin";
                                teammemberprofile.StatusUpdateDate = DateTime.Now;

                                objTeamMemberProfileRepository.addNewTeamMember(teammemberprofile);

                            }



                        }
                        objLiRepo.addLinkedinUser(objLinkedInAccount);
                    }
                }
                GetLinkedInFeeds(_oauth, data.id, user.Id);

            }
            catch
            {
            }
        }

        public void GetLinkedInFeeds(oAuthLinkedIn _oauth,string profileId,Guid userId)
        {
            LinkedInNetwork objln = new LinkedInNetwork();
            LinkedInFeedRepository objliFeedsRepo = new LinkedInFeedRepository();
            List<LinkedInNetwork.Network_Updates> userUPdate = objln.GetNetworkUpdates(_oauth, 20);
            LinkedInFeed lnkfeeds=new LinkedInFeed();
            foreach (var item in userUPdate)
	        {
                lnkfeeds.Feeds = item.Message;
                lnkfeeds.FromId = item.PersonId;
                lnkfeeds.FromName = item.PersonFirstName + " " + item.PersonLastName;
                lnkfeeds.FeedsDate =Convert.ToDateTime(item.DateTime);
                lnkfeeds.EntryDate = DateTime.Now;
                lnkfeeds.ProfileId = profileId;
                lnkfeeds.Type = item.UpdateType;
                lnkfeeds.UserId = userId;
                lnkfeeds.FromPicUrl = item.PictureUrl;
                objliFeedsRepo.addLinkedInFeed(lnkfeeds);
	        }
           
        }

        public void JobUpdates(oAuthLinkedIn _oauth, string profileId, Guid UserId)
        { 
        
        }
    }
}