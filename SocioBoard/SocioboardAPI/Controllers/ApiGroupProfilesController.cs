using Api.Socioboard.Model;
using Api.Socioboard.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Socioboard.Controllers
{
    [Api.Socioboard.App_Start.AllowCrossSiteJson]
    public class ApiGroupProfilesController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiGroupMembersController));
        private GroupProfileRepository grpProfilesRepo = new GroupProfileRepository();
        [HttpGet]
        public IHttpActionResult GetGroupProfiles(string GroupId) 
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch(Exception ex)
            {
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles  =grpProfilesRepo.GetAllGroupProfiles(grpId);
           
            return Ok(lstGroupProfiles);
        }

        [HttpGet]
        public IHttpActionResult AddGroupProfile(string profileid, string network, string groupid, string userid,string profileName) 
        {
            try 
            {
                Domain.Socioboard.Domain.GroupProfile objGroupProfile = new Domain.Socioboard.Domain.GroupProfile();
                objGroupProfile = new Domain.Socioboard.Domain.GroupProfile();
                objGroupProfile.Id = Guid.NewGuid();
                objGroupProfile.GroupId = Guid.Parse(groupid);
                objGroupProfile.ProfileId = profileid;
                objGroupProfile.GroupOwnerId = Guid.Parse(userid);
                objGroupProfile.ProfileType = network;
                objGroupProfile.EntryDate = DateTime.UtcNow;
                objGroupProfile.ProfileName = profileName;
                grpProfilesRepo.AddGroupProfile(objGroupProfile);
                return Ok("GroupProfile Added");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid Inputs");
            }
            

        }

        [HttpDelete]
        public IHttpActionResult DeleteProfileFromGroup([FromUri]string profileid, [FromUri]string groupid, [FromUri]string userid, [FromUri]string profiletype) 
        {
            if (string.IsNullOrEmpty(profileid)) 
            {
                profileid = "";
            }
            try 
            {
                grpProfilesRepo.DeleteGroupProfile(Guid.Parse(userid), profileid, Guid.Parse(groupid), profiletype);
                return Ok("Deleted");         
            }
            catch (Exception ex) 
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Wrong Inputs");
            }
        }

        [HttpGet]
        public IHttpActionResult GetGroupProfileByProfileType(string GroupId, string ProfileType) 
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId); 
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, ProfileType);
            return Ok(lstGroupProfiles);
        }


        [HttpGet]
        public IHttpActionResult GetGroupFacebookProfiles(string GroupId, string UserId) 
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "facebook");
            List<Domain.Socioboard.Domain.FacebookAccount> lstFbAccounts = new List<Domain.Socioboard.Domain.FacebookAccount>();
            FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();

            foreach (var profile in lstGroupProfiles) 
            {
                try
                {
                    //if (objFacebookAccountRepository.checkFacebookUserExists(profile.ProfileId, Guid.Parse(UserId)))
                    //{
                    //    lstFbAccounts.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(profile.ProfileId, Guid.Parse(UserId)));
                    //}
                    //else
                    //{
                        lstFbAccounts.Add(objFacebookAccountRepository.getFacebookAccountDetailsById(profile.ProfileId));
                    //}
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstFbAccounts);
        }

        [HttpGet]
        public IHttpActionResult GetGroupTwitterProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "twitter");
            List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = new List<Domain.Socioboard.Domain.TwitterAccount>();
            TwitterAccountRepository objTwitterAccountRepository = new TwitterAccountRepository();

            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    lstTwitterAccount.Add(objTwitterAccountRepository.getUserInformation( profile.ProfileId));
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstTwitterAccount);
        }

        [HttpGet]
        public IHttpActionResult GetGroupLinkedinProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "linkedin");
            List<Domain.Socioboard.Domain.LinkedInAccount> lstLinkedInAccount = new List<Domain.Socioboard.Domain.LinkedInAccount>();
            LinkedInAccountRepository _objLinkedInAccountRepository = new LinkedInAccountRepository();


            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    //if (_objLinkedInAccountRepository.checkLinkedinUserExists(profile.ProfileId, Guid.Parse(UserId)))
                    //{
                    //    lstLinkedInAccount.Add(_objLinkedInAccountRepository.getUserInformation(Guid.Parse(UserId), profile.ProfileId));
                    //}
                    //else
                    //{
                        lstLinkedInAccount.Add(_objLinkedInAccountRepository.getUserInformation(profile.ProfileId));
                    //}
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstLinkedInAccount);
        }

        [HttpGet]
        public IHttpActionResult GetGroupLinkedinComanyPageProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "linkedincompanypage");
            List<Domain.Socioboard.Domain.LinkedinCompanyPage> lstLinkedInCompanyPage = new List<Domain.Socioboard.Domain.LinkedinCompanyPage>();
            LinkedinCompanyPageRepository objLinkedCmpnyPgeRepo = new LinkedinCompanyPageRepository();



            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    lstLinkedInCompanyPage.Add(objLinkedCmpnyPgeRepo.getCompanyPageInformation(profile.ProfileId));
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstLinkedInCompanyPage);
        }


        [HttpGet]
        public IHttpActionResult GetGroupInstagramProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "instagram");
            List<Domain.Socioboard.Domain.InstagramAccount> lstInstagramAccount = new List<Domain.Socioboard.Domain.InstagramAccount>();
            InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();



            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    lstInstagramAccount.Add(objInstagramAccountRepository.getInstagramAccountById(profile.ProfileId));
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstInstagramAccount);
        }



        [HttpGet]
        public IHttpActionResult GetGroupTumblrProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "tumblr");
            List<Domain.Socioboard.Domain.TumblrAccount> lstTumblrAccount = new List<Domain.Socioboard.Domain.TumblrAccount>();
            TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();




            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    lstTumblrAccount.Add(objTumblrAccountRepository.getTumblrAccountDetailsById(profile.ProfileId));
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstTumblrAccount);
        }


        [HttpGet]
        public IHttpActionResult GetGroupYoutubeProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "youtube");
            List<Domain.Socioboard.Domain.YoutubeAccount> lstYoutubeAccount = new List<Domain.Socioboard.Domain.YoutubeAccount>();
            YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();


            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    lstYoutubeAccount.Add(objYoutubeAccountRepository.getYoutubeAccountDetailsById(profile.ProfileId));
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstYoutubeAccount);
        }


        [HttpGet]
        public IHttpActionResult GetGroupGPlusProfiles(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "gplus");
            List<Domain.Socioboard.Domain.GooglePlusAccount> lstGooglePlusAccount = new List<Domain.Socioboard.Domain.GooglePlusAccount>();
            GooglePlusAccountRepository ObjGooglePlusAccountsRepo = new GooglePlusAccountRepository();



            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    lstGooglePlusAccount.Add(ObjGooglePlusAccountsRepo.getUserDetails(profile.ProfileId));
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstGooglePlusAccount);
        }

        [HttpGet]
        public IHttpActionResult GetGroupFacebookPage(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "facebook_page");
            List<Domain.Socioboard.Domain.FacebookAccount> lstFacebookAccount =new List<Domain.Socioboard.Domain.FacebookAccount>();
            FacebookAccountRepository _FacebookAccountRepository = new FacebookAccountRepository();
            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    Domain.Socioboard.Domain.FacebookAccount _FacebookAccount = _FacebookAccountRepository.getFacebookAccountDetailsById(profile.ProfileId, Guid.Parse(UserId));
                    if (_FacebookAccount.Type.ToLower()=="page" && !string.IsNullOrEmpty(_FacebookAccount.AccessToken))
                    {
                        lstFacebookAccount.Add(_FacebookAccount);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstFacebookAccount);
        }

        [HttpGet]
        public IHttpActionResult GetGroupGoogleAnalytics(string GroupId, string UserId)
        {
            Guid grpId = Guid.Empty;
            try
            {
                grpId = Guid.Parse(GroupId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return BadRequest("Invalid GroupId");
            }
            List<Domain.Socioboard.Domain.GroupProfile> lstGroupProfiles = grpProfilesRepo.GetAllGroupProfilesByProfileType(grpId, "googleanalytics");
            List<Domain.Socioboard.Domain.GoogleAnalyticsAccount> lstGoogleAnalyticsAccount = new List<Domain.Socioboard.Domain.GoogleAnalyticsAccount>();
            GoogleAnalyticsAccountRepository _GoogleAnalyticsAccountRepository = new GoogleAnalyticsAccountRepository();
            foreach (var profile in lstGroupProfiles)
            {
                try
                {
                    Domain.Socioboard.Domain.GoogleAnalyticsAccount _GoogleAnalyticsAccount = _GoogleAnalyticsAccountRepository.getGoogleAnalyticsAccountDetailsById(profile.ProfileId, Guid.Parse(UserId));
                    lstGoogleAnalyticsAccount.Add(_GoogleAnalyticsAccount);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
            return Ok(lstGoogleAnalyticsAccount);
        }


        [HttpGet]
        public IHttpActionResult TeamProfileTOGroupProfileService() 
        {
            TeamRepository Tr = new TeamRepository();
                GroupProfileRepository GpR = new GroupProfileRepository();
            TeamMemberProfileRepository TMPR = new TeamMemberProfileRepository();

            List<Domain.Socioboard.Domain.Team> lstTeam = Tr.GetAllActiveTeam();
            foreach(var item in lstTeam)
            {
                List<Domain.Socioboard.Domain.TeamMemberProfile> lstTeamMemberprofiles = TMPR.getAllTeamMemberProfilesOfTeam(item.Id);
                foreach (var memProfile in lstTeamMemberprofiles) 
                {
                    Domain.Socioboard.Domain.GroupProfile grpProfile = new Domain.Socioboard.Domain.GroupProfile();
                    grpProfile.Id = Guid.NewGuid();
                    grpProfile.EntryDate = memProfile.StatusUpdateDate;
                    grpProfile.GroupId = item.GroupId;
                    grpProfile.GroupOwnerId = item.UserId;
                    grpProfile.ProfileId = memProfile.ProfileId;
                    grpProfile.ProfileName = memProfile.ProfileName;
                    grpProfile.ProfileType = memProfile.ProfileType;
                    grpProfile.ProfilePic = memProfile.ProfilePicUrl;
                    if (!GpR.checkProfileExistsingroup(grpProfile.GroupId, grpProfile.ProfileId)) 
                    {
                        GpR.AddGroupProfile(grpProfile);
                    }
                }

            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GrpMemService() 
        {
            UserRepository userRepo = new UserRepository();
            List<Domain.Socioboard.Domain.User> lstUser = userRepo.getAllUsers();
            foreach (var user in lstUser) 
            {
                try 
                {
                    GrpMem(user);
                }
                catch { }
            }
            return Ok();


        }


        public bool GrpMem(Domain.Socioboard.Domain.User user) 
        {
            GroupsRepository grpRep = new GroupsRepository();
            GroupMembersRepository grpMemRep = new GroupMembersRepository();
            UserRepository userRepo = new UserRepository();
           // Domain.Socioboard.Domain.User user = userRepo.getUserInfoByEmail(Email);
            //foreach (var user in lstUser) 
            //{
                try 
                {
                    List<Domain.Socioboard.Domain.Groups> lstGroups = grpRep.getAlluserGroups(user.Id);
                    foreach (var item in lstGroups)
                    {
                        try
                        {
                            if (user != null)
                            {
                                Domain.Socioboard.Domain.Groupmembers grpMember = new Domain.Socioboard.Domain.Groupmembers();
                                grpMember.Id = Guid.NewGuid();
                                grpMember.Emailid = user.EmailId;
                                grpMember.Groupid = item.Id.ToString();
                                grpMember.IsAdmin = true;
                                grpMember.Status = Domain.Socioboard.Domain.GroupUserStatus.Accepted;
                                grpMember.Userid = item.UserId.ToString();
                                if (!grpMemRep.checkMemberExistsingroup(grpMember.Groupid, grpMember.Userid))
                                {
                                    grpMemRep.AddGroupMemeber(grpMember);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                            logger.Error(ex.StackTrace);
                        }


                    }
                }
                catch (Exception e) { }
               

            //}
            
            return true;

        }
    }
}
