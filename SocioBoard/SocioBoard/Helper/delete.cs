using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocioBoard.Helper
{
    public class delete
    {

        public string DeleteAllUsersByCreateDate(string date)
        {
            int i = 0;
            int count = 0;
            UserRepository objUserRepository = new UserRepository();
            List<User> lstuser = objUserRepository.GetAllUsersByCreateDate(date);
            ArchiveMessageRepository objArchiveMessageRepository = new ArchiveMessageRepository();
            DiscoverySearchRepository objDiscoverySearchRepository = new DiscoverySearchRepository();
            DraftsRepository objDraftsRepository = new DraftsRepository();
            FacebookAccountRepository objFacebookAccountRepository = new FacebookAccountRepository();
            FacebookFeedRepository objFacebookFeedRepository = new FacebookFeedRepository();
            FacebookInsightPostStatsRepository objFacebookInsightPostStatsRepository = new FacebookInsightPostStatsRepository();
            FacebookInsightStatsRepository objFacebookInsightStatsRepository = new FacebookInsightStatsRepository();
            FacebookMessageRepository objFacebookMessageRepository = new FacebookMessageRepository();
            FacebookStatsRepository objFacebookStatsRepository = new FacebookStatsRepository();
            GoogleAnalyticsAccountRepository objGoogleAnalyticsAccountRepository = new GoogleAnalyticsAccountRepository();
            GoogleAnalyticsStatsRepository objGoogleAnalyticsStatsRepository = new GoogleAnalyticsStatsRepository();
            GooglePlusAccountRepository objGooglePlusAccountRepository = new GooglePlusAccountRepository();
            GooglePlusActivitiesRepository objGooglePlusActivitiesRepository = new GooglePlusActivitiesRepository();
            GroupProfileRepository objGroupProfileRepository = new GroupProfileRepository();
            GroupRepository objGroupRepository = new GroupRepository();
            InstagramAccountRepository objInstagramAccountRepository = new InstagramAccountRepository();
            InstagramCommentRepository objInstagramCommentRepository = new InstagramCommentRepository();
            InstagramFeedRepository objInstagramFeedRepository = new InstagramFeedRepository();
            LinkedInAccountRepository objLinkedInAccountRepository = new LinkedInAccountRepository();
            LinkedInFeedRepository objLinkedInFeedRepository = new LinkedInFeedRepository();
            LogRepository objLogRepository = new LogRepository();
            RssFeedsRepository objRssFeedsRepository = new RssFeedsRepository();
            RssReaderRepository objRssReaderRepository = new RssReaderRepository();
            ScheduledMessageRepository objScheduledMessageRepository = new ScheduledMessageRepository();
            SocialProfilesRepository objSocialProfilesRepository = new SocialProfilesRepository();
            TaskCommentRepository objTaskCommentRepository = new TaskCommentRepository();
            TaskRepository objTaskRepository = new TaskRepository();
            TeamRepository objTeamRepository = new TeamRepository();
            TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
            TwitterAccountRepository objTwitterAccountRepository = new TwitterAccountRepository();
            TwitterDirectMessageRepository objTwitterDirectMessageRepository = new TwitterDirectMessageRepository();
            TwitterFeedRepository objTwitterFeedRepository = new TwitterFeedRepository();
            TwitterMessageRepository objTwitterMessageRepository = new TwitterMessageRepository();
            TwitterStatsRepository objTwitterStatsRepository = new TwitterStatsRepository();
            UserActivationRepository objUserActivationRepository = new UserActivationRepository();
            UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();






            count = lstuser.Count();


            foreach (var item in lstuser)
            {
                i++;
                try
                {

                    if (item.EmailId == "pbpraveen@globussoft.com" || item.EmailId == "stephen@globussoft.com" || item.EmailId == "jotytiwari@yahoo.com" || item.EmailId == "pravin.huske@globussoft.com" || item.EmailId == "amankeepscool@gmail.com" || item.EmailId == "nikiyjaswall@yahoo.com" || item.EmailId == "RosanaGlass883@yahoo.com" || item.EmailId == "EmilieWestmoreland741@yahoo.com" || item.EmailId == "ReidMccaslin656@yahoo.com" || item.EmailId == "abhishek@globussoft.com")
                    {

                    }
                    else
                    {
                        objArchiveMessageRepository.DeleteArchiveMessageByUserid(item.Id);
                        objDiscoverySearchRepository.DeleteDiscoverySearchByUserid(item.Id);
                        objDraftsRepository.DeleteDraftsByUserid(item.Id);
                        objFacebookAccountRepository.DeleteFacebookAccountByUserid(item.Id);
                        objFacebookFeedRepository.DeleteFacebookFeedByUserid(item.Id);
                        objFacebookInsightPostStatsRepository.DeleteFacebookInsightPostStatsByUserid(item.Id);
                        objFacebookInsightStatsRepository.DeleteFacebookInsightStatsByUserid(item.Id);
                        objFacebookMessageRepository.DeleteFacebookMessageByUserid(item.Id);
                        objFacebookStatsRepository.DeleteFacebookStatsByUserid(item.Id);
                        objGoogleAnalyticsAccountRepository.DeleteGoogleAnalyticsAccountByUserid(item.Id);
                        objGoogleAnalyticsStatsRepository.DeleteGoogleAnalyticsStatsByUserid(item.Id);
                        objGooglePlusAccountRepository.DeleteGooglePlusAccountByUserid(item.Id);
                        objGooglePlusActivitiesRepository.DeleteGooglePlusActivitiesByUserid(item.Id);
                        objGroupProfileRepository.DeleteGroupProfileByUserid(item.Id);
                        objGroupRepository.DeleteGroupsByUserid(item.Id);
                        objInstagramAccountRepository.DeleteInstagramAccountByUserid(item.Id);
                        objInstagramCommentRepository.DeleteInstagramCommentByUserid(item.Id);
                        objInstagramFeedRepository.DeleteInstagramFeedByUserid(item.Id);
                        objLinkedInAccountRepository.DeleteLinkedInAccountByUserid(item.Id);
                        objLinkedInFeedRepository.DeleteLinkedInFeedByUserid(item.Id);
                        objLogRepository.DeleteLogByUserid(item.Id);
                        objRssFeedsRepository.DeleteRssFeedsByUserid(item.Id);
                        objRssReaderRepository.DeleteRssReaderByUserid(item.Id);
                        objScheduledMessageRepository.DeleteScheduledMessageByUserid(item.Id);
                        objSocialProfilesRepository.DeleteSocialProfileByUserid(item.Id);
                        objTaskCommentRepository.DeleteTaskCommentByUserid(item.Id);
                        objTaskRepository.DeleteTasksByUserid(item.Id);
                        objTeamRepository.DeleteTeamByUserid(item.Id);
                        objTeamMemberProfileRepository.DeleteTeamMemberProfileByUserid(item.Id);
                        objTwitterAccountRepository.DeleteTwitterAccountByUserid(item.Id);
                        objTwitterDirectMessageRepository.DeleteTwitterDirectMessagesByUserid(item.Id);
                        objTwitterFeedRepository.DeleteTwitterFeedByUserid(item.Id);
                        objTwitterMessageRepository.DeleteTwitterMessageByUserid(item.Id);
                        objTwitterStatsRepository.DeleteTwitterStatsByUserid(item.Id);
                        objUserActivationRepository.DeleteUserActivationByUserid(item.Id);
                        objUserPackageRelationRepository.DeleteuserPackageRelationByUserid(item.Id);
                        objUserRepository.DeleteUserByUserid(item.Id);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
           return i +" "+count;
        }
        

    }


}