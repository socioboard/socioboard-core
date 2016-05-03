using Api.Socioboard.Helper;
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
    [App_Start.AllowCrossSiteJson]
    public class ApiUserController : ApiController
    {
        ILog logger = LogManager.GetLogger(typeof(ApiShareathonController));
        UserRepository userrepo = new UserRepository();
        [HttpGet]
        public IHttpActionResult Login(string EmailId, string PasswordHash)
        {
            try
            {
                
                Domain.Socioboard.Domain.User objUser = new Domain.Socioboard.Domain.User();
                objUser = userrepo.getUserInfoByEmail(EmailId);
                if (objUser == null)
                {
                    return BadRequest("Email Not Exist");
                }

                Domain.Socioboard.Domain.User user = userrepo.GetUserInfo(EmailId, PasswordHash);
                if (user != null)
                {
                    if (user.UserCode == null || user.UserCode == "")
                    {
                        string code = Utility.GenerateRandomUniqueString();
                        int retint = userrepo.UpdateCode(user.Id, code);
                        user = userrepo.getUsersById(user.Id);
                    }
                    try
                    {
                        userrepo.UpdateLastLoginTime(user.Id);
                    }
                    catch { }
                    return Ok(user);

                }
                else
                {
                    return BadRequest("Not Exist");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error : " + ex.Message);
                logger.Error("Error : " + ex.StackTrace);
                return BadRequest();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteUserDetails(string user)
        {
            GroupsRepository _GroupsRepository=new GroupsRepository();
            GroupMembersRepository _GroupMembersRepository = new GroupMembersRepository();
            GroupProfileRepository _GroupProfileRepository = new GroupProfileRepository();
            TaskRepository _TaskRepository = new TaskRepository();
            TaskCommentRepository _TaskCommentRepository = new TaskCommentRepository();
            InboxMessagesRepository _InboxMessagesRepository=new InboxMessagesRepository();
            FacebookAccountRepository _FacebookAccountRepository=new FacebookAccountRepository();
            GoogleAnalyticsAccountRepository _GoogleAnalyticsAccountRepository=new GoogleAnalyticsAccountRepository();
            GooglePlusAccountRepository _GooglePlusAccountRepository=new GooglePlusAccountRepository();
            InstagramAccountRepository _InstagramAccountRepository=new InstagramAccountRepository();
            LinkedInAccountRepository _LinkedInAccountRepository=new LinkedInAccountRepository();
            LinkedinCompanyPageRepository _LinkedinCompanyPageRepository=new LinkedinCompanyPageRepository();
            ScheduledMessageRepository _ScheduledMessageRepository=new ScheduledMessageRepository();
            SocialProfilesRepository _SocialProfilesRepository = new SocialProfilesRepository();
            TwitterAccountRepository _TwitterAccountRepository=new TwitterAccountRepository();
            TumblrAccountRepository _TumblrAccountRepository = new TumblrAccountRepository();
            YoutubeAccountRepository _YoutubeAccountRepository = new YoutubeAccountRepository();
            YoutubeChannelRepository _YoutubeChannelRepository = new YoutubeChannelRepository();
            try
            {
                Domain.Socioboard.Domain.User _User = userrepo.getUserInfoByEmail(user);
                if (_User != null)
                {
                    List<Domain.Socioboard.Domain.Groups> lstGroups = _GroupsRepository.getAllGroups(_User.Id);
                    foreach (Domain.Socioboard.Domain.Groups item_group in lstGroups)
                    {
                        int i = _GroupMembersRepository.DeleteGroupMember(item_group.Id.ToString());
                        int j = _GroupProfileRepository.DeleteAllGroupProfile(item_group.Id);
                        bool rt = _GroupProfileRepository.DeleteGroupReport(item_group.Id);
                        int k = _TaskRepository.DeleteTaskOfGroup(item_group.Id);
                    }
                    int g = _GroupMembersRepository.DeleteGroupMemberByUserId(user);
                    int h = _GroupsRepository.DeleteGroupsByUserid(_User.Id);
                    int l = _TaskCommentRepository.DeleteTaskCommentByUserid(_User.Id);
                    int m = _InboxMessagesRepository.DeleteInboxMessages(_User.Id);
                    int n = _FacebookAccountRepository.DeleteAllFacebookAccount(_User.Id);
                    int o = _GoogleAnalyticsAccountRepository.DeleteGoogleAnalyticsAccountByUserid(_User.Id);
                    int p = _GooglePlusAccountRepository.DeleteGooglePlusAccountByUserid(_User.Id);
                    int q = _InstagramAccountRepository.DeleteInstagramAccountByUserid(_User.Id);
                    int r = _LinkedInAccountRepository.DeleteLinkedInAccountByUserid(_User.Id);
                    int s = _LinkedinCompanyPageRepository.DeleteLinkedinCompanyPage(_User.Id);
                    int t = _ScheduledMessageRepository.DeleteScheduledMessageByUserid(_User.Id);
                    int u = _SocialProfilesRepository.DeleteSocialProfileByUserid(_User.Id);
                    int v = _TwitterAccountRepository.DeleteTwitterAccountByUserid(_User.Id);
                    int w = _TumblrAccountRepository.DeletetumblraccountByUserid(_User.Id);
                    int x = _YoutubeAccountRepository.DeleteYoutubeAccount(_User.Id);
                    int y = _YoutubeChannelRepository.DeleteYoutubeChannelByUserid(_User.Id);
                    int z = userrepo.DeleteUser(_User.Id);
                }
                else {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
            return Ok(true);
        }


          
    }
}
