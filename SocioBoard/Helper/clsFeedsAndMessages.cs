using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Collections;

namespace SocioBoard.Helper
{
    public class clsFeedsAndMessages
    {
        public static int messagescount = 0;
        string facebookid = string.Empty;
        string twitterid = string.Empty;
        string googleplusid = string.Empty;
   
        public DataSet bindMessagesIntoDataTable(Guid id)
        {
            //SocialProfilesRepository socioprofrepo = new SocialProfilesRepository();
            //List<SocialProfile> alstprofiles = socioprofrepo.getAllSocialProfilesOfUser(user.Id);

            TeamMemberProfileRepository objTeamMemberProfileRepository = new TeamMemberProfileRepository();
            List<TeamMemberProfile> alstprofiles = objTeamMemberProfileRepository.getAllTeamMemberProfilesOfTeam(id);



            // DataTableGenerator datatablegenerepo = new DataTableGenerator();
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
            //  DataSet ds = datatablegenerepo.CreateDataSetForTable(mstable);

            foreach (TeamMemberProfile item in alstprofiles)
            {
                if (item.ProfileType == "facebook")
                {
                    FacebookFeedRepository fbfeedRepo = new FacebookFeedRepository();
                    List<FacebookFeed> alstfeedfb = fbfeedRepo.getUnreadMessages(item.ProfileId);
                    foreach (FacebookFeed facebookmsg in alstfeedfb)
                    {
                        ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type, facebookmsg.ReadStatus);
                    }
                }
                else if (item.ProfileType == "twitter")
                {
                    TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                    List<TwitterMessage> lstmsgtwtuser = twtmsgrepo.getUnreadMessages(item.ProfileId);
                    foreach (TwitterMessage lst in lstmsgtwtuser)
                    {
                        ds.Tables[0].Rows.Add(lst.ProfileId, "twitter", lst.FromId, lst.FromScreenName, lst.FromProfileUrl, lst.MessageDate, lst.TwitterMsg, "", "", lst.MessageId, lst.Type, lst.ReadStatus);
                    }
                }
                else if (item.ProfileType == "googleplus")
                {

                }


            }


            foreach (TeamMemberProfile item in alstprofiles)
            {
                if (item.ProfileType == "facebook")
                {
                    try
                    {
                        //FacebookFeedRepository fbfeedrepo = new FacebookFeedRepository();
                        //List<FacebookFeed> alstfbmsgs = fbfeedrepo.getAllReadFacebookFeeds(user.Id, item.ProfileId);
                        //foreach (FacebookFeed facebookmsg in alstfbmsgs)
                        //{
                        //    ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type,facebookmsg.ReadStatus);
                        //}
                        facebookid += item.ProfileId + ",";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                else if (item.ProfileType == "twitter")
                {
                    try
                    {
                        //TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                        //List<TwitterMessage> lstmsgtwtuser = twtmsgrepo.getAllReadMessagesOfUser(user.Id, item.ProfileId);
                        //foreach (TwitterMessage lst in lstmsgtwtuser)
                        //{
                        //    ds.Tables[0].Rows.Add(lst.ProfileId, "twitter", lst.FromId, lst.FromScreenName, lst.FromProfileUrl, lst.MessageDate, lst.TwitterMsg, "", "", lst.MessageId, lst.Type,lst.ReadStatus);
                        //}
                        twitterid += item.ProfileId + ",";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                else if (item.ProfileType == "googleplus")
                {
                    try
                    {
                        //GooglePlusActivitiesRepository objActRepo = new GooglePlusActivitiesRepository();
                        //List<GooglePlusActivities> lstmsggauser = objActRepo.getAllgoogleplusActivityOfUser(user.Id, item.ProfileId);
                        //foreach (GooglePlusActivities lst in lstmsggauser)
                        //{
                        //    ds.Tables[0].Rows.Add(lst.GpUserId, "googleplus", lst.FromId, lst.FromUserName, lst.FromProfileImage, lst.PublishedDate, lst.Content, "", "", lst.ActivityId, "activities");
                        //}
                        googleplusid += item.ProfileId + ",";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }

            }
            if (facebookid != "")
            {
                facebookid = facebookid.Substring(0, facebookid.Length - 1);
            }
            if (twitterid != "")
            {
                twitterid = twitterid.Substring(0, twitterid.Length - 1);
            }
            if (googleplusid != "")
            {
                googleplusid = googleplusid.Substring(0, googleplusid.Length - 1);
            }

            FacebookFeedRepository fbfeedRepository = new FacebookFeedRepository();
            List<FacebookFeed> alstfbmsgs = fbfeedRepository.getAllReadFbFeeds(facebookid);
            try
            {
                foreach (FacebookFeed facebookmsg in alstfbmsgs)
                {
                    try
                    {
                        ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type, facebookmsg.ReadStatus);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }



            TwitterMessageRepository twtmsgRepository = new TwitterMessageRepository();
            List<TwitterMessage> lstmsgtwt = twtmsgRepository.getAlltwtMessagesOfUser(twitterid);
            try
            {
                foreach (TwitterMessage lst in lstmsgtwt)
                {
                    try
                    {
                        ds.Tables[0].Rows.Add(lst.ProfileId, "twitter", lst.FromId, lst.FromScreenName, lst.FromProfileUrl, lst.MessageDate, lst.TwitterMsg, "", "", lst.MessageId, lst.Type, lst.ReadStatus);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }



            GooglePlusActivitiesRepository objActRepository = new GooglePlusActivitiesRepository();
            List<GooglePlusActivities> lstmsggpl = objActRepository.getAllgplusOfUser(googleplusid);
            try
            {
                foreach (GooglePlusActivities lst in lstmsggpl)
                {
                    try
                    {
                        ds.Tables[0].Rows.Add(lst.GpUserId, "googleplus", lst.FromId, lst.FromUserName, lst.FromProfileImage, lst.PublishedDate, lst.Content, "", "", lst.ActivityId, "activities");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return ds;
        }

        public DataSet bindFeedsIntoDataTable(User user, string network)
        {
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);



            if (!string.IsNullOrEmpty(network))
            {
                /*Facebook region*/
                if (network == "facebook")
                {
                    FacebookAccountRepository fbaccount = new FacebookAccountRepository();
                    FacebookMessageRepository fbmsg = new FacebookMessageRepository();
                    ArrayList alstfbaccount = fbaccount.getAllFacebookAccountsOfUser(user.Id);
                    foreach (FacebookAccount item in alstfbaccount)
                    {
                        List<FacebookMessage> lstfbmsg = fbmsg.getAllFacebookMessagesOfUser(user.Id, item.FbUserId);
                        foreach (FacebookMessage facebookmsg in lstfbmsg)
                        {
                            ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type);
                        }
                    }
                }
                else if (network == "twitter")
                {
                    TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
                    TwitterFeedRepository twtfeedrepo = new TwitterFeedRepository();
                    ArrayList alsttwtaccount = twtaccountrepo.getAllTwitterAccountsOfUser(user.Id);
                    foreach (TwitterAccount item in alsttwtaccount)
                    {
                        List<TwitterFeed> lsttwtmsg = twtfeedrepo.getAllTwitterFeedOfUsers(user.Id, item.TwitterUserId);
                        foreach (TwitterFeed twtmsg in lsttwtmsg)
                        {
                            ds.Tables[0].Rows.Add(twtmsg.ProfileId, "twitter", twtmsg.FromId, twtmsg.FromScreenName, twtmsg.FromProfileUrl, twtmsg.FeedDate, twtmsg.Feed, "", "", twtmsg.MessageId, twtmsg.Type);
                        }
                    }

                }
                else if (network == "linkedin")
                {
                    LinkedInAccountRepository liaccountrepo = new LinkedInAccountRepository();
                    LinkedInFeedRepository lifeedrepo = new LinkedInFeedRepository();
                    ArrayList alstliaccount = liaccountrepo.getAllLinkedinAccountsOfUser(user.Id);
                    foreach (LinkedInAccount item in alstliaccount)
                    {
                        List<LinkedInFeed> lsttwtmsg = lifeedrepo.getAllLinkedInFeedsOfUser(user.Id, item.LinkedinUserId);
                        foreach (LinkedInFeed limsg in lsttwtmsg)
                        {
                            ds.Tables[0].Rows.Add(limsg.ProfileId, "linkedin", limsg.FromId, limsg.FromName, limsg.FromPicUrl, limsg.FeedsDate, limsg.Feeds, "", "", "", limsg.Type);
                        }
                    }

                }
                else if (network == "instagram")
                {
                    InstagramAccountRepository insAccRepo = new InstagramAccountRepository();
                    InstagramFeedRepository insFeedRepo = new InstagramFeedRepository();
                    ArrayList alstlistaccount = insAccRepo.getAllInstagramAccountsOfUser(user.Id);
                    foreach (InstagramAccount item in alstlistaccount)
                    {
                        List<InstagramFeed> lstFeeed = insFeedRepo.getAllInstagramFeedsOfUser(user.Id, item.InstagramId);
                        foreach (InstagramFeed insFeed in lstFeeed)
                        {
                            ds.Tables[0].Rows.Add(insFeed.InstagramId, "instagram", "", "", "", insFeed.FeedDate, insFeed.FeedImageUrl, "", "", insFeed.FeedId, "");
                        }
                    }
                }

            }
            return ds;
        }

        public DataSet bindSentMessagesToDataTable(User user, string network)
        {
            SocialProfilesRepository socioprofrepo = new SocialProfilesRepository();
            List<SocialProfile> alstprofiles = socioprofrepo.getAllSocialProfilesOfUser(user.Id);
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
            foreach (SocialProfile item in alstprofiles)
            {
                if (item.ProfileType == "facebook")
                {
                    FacebookMessageRepository fbmsgrepo = new FacebookMessageRepository();
                    List<FacebookMessage> alstfbmsgs = fbmsgrepo.getAllSentMessages(item.ProfileId);

                    if (alstfbmsgs != null)
                    {
                        if (alstfbmsgs.Count != 0)
                        {
                            foreach (FacebookMessage facebookmsg in alstfbmsgs)
                            {
                                ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type);

                            }

                        }
                    }
                }
                else if (item.ProfileType == "twitter")
                {
                    TwitterDirectMessageRepository twtmsgrepo = new TwitterDirectMessageRepository();
                    List<TwitterDirectMessages> lstmsgtwtuser = twtmsgrepo.getAllDirectMessagesById(item.ProfileId);
                    foreach (TwitterDirectMessages lst in lstmsgtwtuser)
                    {
                        ds.Tables[0].Rows.Add(lst.SenderId, "twitter", lst.SenderId, lst.SenderScreenName, lst.SenderProfileUrl, lst.CreatedDate, lst.Message, "", "", lst.MessageId, lst.Type);
                    }
                }
                else if (item.ProfileType == "googleplus")
                {

                }

            }
            return ds;
        }
    }

}