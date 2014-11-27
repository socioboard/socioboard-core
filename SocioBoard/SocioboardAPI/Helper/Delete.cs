using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Services;
using SocioBoard.Model;
using Api.Socioboard.Model;

namespace Api.Socioboard.Helper
{
    public class Delete
    {
        public static string DeleteUserByUserId(string UserId)
        {
            UserRepository objUserRepository = new UserRepository();
            ArchiveMessageRepository objArchiveMessageRepository = new ArchiveMessageRepository();
            BusinessSettingRepository objBusinessSettingRepository = new BusinessSettingRepository(); 
            DiscoverySearchRepository objDiscoverySearchRepository = new DiscoverySearchRepository();
            DraftsRepository objDraftsRepository = new DraftsRepository();
            DropboxAccountRepository objDropboxAccountRepository = new DropboxAccountRepository(); 
            FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();
            FacebookFanPageRepository objFacebookFanPageRepository = new FacebookFanPageRepository();
            FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
            //FacebookInsightPostStatsRepository objFacebookInsightPostStatsRepository = new FacebookInsightPostStatsRepository();
            //FacebookInsightStatsRepository objFacebookInsightStatsRepository = new FacebookInsightStatsRepository();
            FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
            FacebookStatsRepository objFacebookStatsRepository = new FacebookStatsRepository();
            FbaPageSharerRepository objFbaPageSharerRepository = new FbaPageSharerRepository();
            FbPageCommentRepository objFbPageCommentRepository = new FbPageCommentRepository();
            FbPageLikerRepository objFbPageLikerRepository = new FbPageLikerRepository();
            FbPagePostCommentLikerRepository objFbPagePostCommentLikerRepository = new FbPagePostCommentLikerRepository();
            FbPagePostRepository objFbPagePostRepository = new FbPagePostRepository();
            //GoogleAnalyticsAccountRepository objGoogleAnalyticsAccountRepository = new GoogleAnalyticsAccountRepository();
            //GoogleAnalyticsStatsRepository objGoogleAnalyticsStatsRepository = new GoogleAnalyticsStatsRepository();
            GooglePlusAccountRepository objGooglePlusAccountRepository = new GooglePlusAccountRepository();
            //GooglePlusActivitiesRepository objGooglePlusActivitiesRepository = new GooglePlusActivitiesRepository();
            GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
            GroupsRepository objGroupRepository = new GroupsRepository();
            InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();
            InstagramCommentRepository objInstagramCommentRepository = new InstagramCommentRepository();
            InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
            LinkedInAccountRepository objLinkedInAccountRepository = new LinkedInAccountRepository();
            LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
            //LogRepository objLogRepository = new LogRepository();
            NewsLetterRepository objNewsLetterRepository = new NewsLetterRepository();
            //RssFeedsRepository objRssFeedsRepository = new RssFeedsRepository();
            //RssReaderRepository objRssReaderRepository = new RssReaderRepository();
            ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
            SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
            TaskCommentRepository objTaskCommentRepository = new TaskCommentRepository();
            TaskRepository objTaskRepository = new TaskRepository();
            TeamRepository objTeamRepository = new TeamRepository();
            TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
            TumblrAccountRepository objTumblrAccountRepository = new TumblrAccountRepository();
            TumblrFeedRepository objTumblrFeedRepository = new TumblrFeedRepository();
            TwitterAccountRepository objTwitterAccountRepository = new TwitterAccountRepository();
            TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();
            TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
            TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
            TwitterStatsRepository objTwitterStatsRepository = new TwitterStatsRepository();
            UserActivationRepository objUserActivationRepository = new UserActivationRepository();
            UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
            YoutubeAccountRepository objYoutubeAccountRepository = new YoutubeAccountRepository();
            YoutubeChannelRepository objYoutubeChannelRepository = new YoutubeChannelRepository();
            try
            {
                Guid userid = Guid.Parse(UserId);
                objArchiveMessageRepository.DeleteArchiveMessageByUserid(userid);
                objBusinessSettingRepository.DeleteBusinessSettingByUserid(userid);
                objDiscoverySearchRepository.DeleteDiscoverySearchByUserid(userid);
                objDraftsRepository.DeleteDraftsByUserid(userid);
                objDropboxAccountRepository.DeleteDropboxAccountByUserid(userid);
                objFacebookAccountRepository.DeleteFacebookAccountByUserid(userid);
                objFacebookFeedRepository.DeleteFacebookFeedByUserid(userid);
                //objFacebookInsightPostStatsRepository.DeleteFacebookInsightPostStatsByUserid(userid);
                //objFacebookInsightStatsRepository.DeleteFacebookInsightStatsByUserid(userid);
                objFacebookMessageRepository.DeleteFacebookMessageByUserid(userid);
                objFacebookStatsRepository.DeleteFacebookStatsByUserid(userid);
                objFbaPageSharerRepository.DeleteFbPageSharerByUserid(userid);
                objFbPageCommentRepository.DeleteFbPageCommentByUserid(userid);
                objFbPageLikerRepository.DeleteFbPageLikerByUserid(userid);
                objFbPagePostCommentLikerRepository.DeleteFbPagePostCommentLikerByUserid(userid);
                objFbPagePostRepository.DeleteFbPagePostByUserid(userid);
                //objGoogleAnalyticsAccountRepository.DeleteGoogleAnalyticsAccountByUserid(userid);
                //objGoogleAnalyticsStatsRepository.DeleteGoogleAnalyticsStatsByUserid(userid);
                //objGooglePlusAccountRepository.DeleteGooglePlusAccountByUserid(userid);
                //objGooglePlusActivitiesRepository.DeleteGooglePlusActivitiesByUserid(userid);
                objGroupProfileRepository.DeleteGroupProfileByUserid(userid);
                objGroupRepository.DeleteGroupsByUserid(userid);
                objInstagramAccountRepository.DeleteInstagramAccountByUserid(userid);
                objInstagramCommentRepository.DeleteInstagramCommentByUserid(userid);
                objInstagramFeedRepository.DeleteInstagramFeedByUserid(userid);
                objLinkedInAccountRepository.DeleteLinkedInAccountByUserid(userid);
                objLinkedInFeedRepository.DeleteLinkedInFeedByUserid(userid);
                //objLogRepository.DeleteLogByUserid(userid);
                //objRssFeedsRepository.DeleteRssFeedsByUserid(userid);
                //objRssReaderRepository.DeleteRssReaderByUserid(userid);
                objScheduledMessageRepository.DeleteScheduledMessageByUserid(userid);
                objSocialProfilesRepository.DeleteSocialProfileByUserid(userid);
                objTaskCommentRepository.DeleteTaskCommentByUserid(userid);
                objTaskRepository.DeleteTasksByUserid(userid);
                objTeamRepository.DeleteTeamByUserid(userid);
                //objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(userid);
                objTumblrAccountRepository.DeleteTumblrAccountByUserid(userid);
                objTumblrFeedRepository.DeleteTumblrFeedByUserid(userid);
                objTwitterAccountRepository.DeleteTwitterAccountByUserid(userid);
                objTwitterDirectMessageRepository.DeleteTwitterDirectMessagesByUserid(userid);
                objTwitterFeedRepository.DeleteTwitterFeedByUserid(userid);
                objTwitterMessageRepository.DeleteTwitterMessageByUserid(userid);
                objTwitterStatsRepository.DeleteTwitterStatsByUserid(userid);
                objUserActivationRepository.DeleteUserActivationByUserid(userid);
                objUserPackageRelationRepository.DeleteuserPackageRelationByUserid(userid);
                objUserRepository.DeleteUserByUserid(userid);
                objYoutubeAccountRepository.DeleteYoutubeAccountByUserid(userid);
                objYoutubeChannelRepository.DeleteYoutubeChannelByUserid(userid);
                return "Account Deleted Successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return "Somthing Went Wrong!";
            }
        }
    }
}