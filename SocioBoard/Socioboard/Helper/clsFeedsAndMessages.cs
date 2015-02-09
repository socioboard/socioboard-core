using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Domain.Socioboard.Domain;
using Domain.Socioboard.Factory;
using System.Collections;
using System.Web.Script.Serialization;
using log4net;

namespace Socioboard.Helper
{
    public class clsFeedsAndMessages
    {
        ILog logger = LogManager.GetLogger(typeof(clsFeedsAndMessages));

        public static int messagescount = 0;
        string facebookid = string.Empty;
        string twitterid = string.Empty;
        string googleplusid = string.Empty;

        Api.FacebookFeed.FacebookFeed objApiFacebookFeed = new Api.FacebookFeed.FacebookFeed();
        Api.TwitterMessage.TwitterMessage objApiTwitterMessage = new Api.TwitterMessage.TwitterMessage();

        Api.FacebookAccount.FacebookAccount objApiFacebookAccount = new Api.FacebookAccount.FacebookAccount();
        Api.TwitterAccount.TwitterAccount objApiTwitterAccount = new Api.TwitterAccount.TwitterAccount();

        Api.FacebookMessage.FacebookMessage objApiFacebookMessage = new Api.FacebookMessage.FacebookMessage();
        Api.TwitterFeed.TwitterFeed objApiTwitterFeed = new Api.TwitterFeed.TwitterFeed();

        Api.SocialProfile.SocialProfile objApiSocialProfile = new Api.SocialProfile.SocialProfile();

        Api.TwitterDirectMessages.TwitterDirectMessages objApiTwitterDirectMessages = new Api.TwitterDirectMessages.TwitterDirectMessages();
   
        public DataSet bindMessagesIntoDataTable(Guid id)
        {           
            Api.TeamMemberProfile.TeamMemberProfile objTeamMemberProfileRepository = new Api.TeamMemberProfile.TeamMemberProfile();
            List<TeamMemberProfile> alstprofiles = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objTeamMemberProfileRepository.GetTeamMemberProfilesByTeamId(id.ToString()), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));
            Api.FacebookFeed.FacebookFeed objApiFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            Api.TwitterMessage.TwitterMessage objApiTwitterMessage = new Api.TwitterMessage.TwitterMessage();          
          
            Messages mstable = new Messages();
           
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
          
            foreach (TeamMemberProfile item in alstprofiles)
            {
                try
                {
                    if (item.ProfileType == "facebook")
                    {
                        //List<Domain.Socioboard.Domain.FacebookFeed> alstfeedfb = (List<Domain.Socioboard.Domain.FacebookFeed>)new JavaScriptSerializer().Deserialize(objApiFacebookFeed.getUnreadMessages(item.ProfileId), typeof(List<Domain.Socioboard.Domain.FacebookFeed>)); ;

                        //foreach (FacebookFeed facebookmsg in alstfeedfb)
                        //{
                        //    ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type, facebookmsg.ReadStatus);
                        //}

                        List<FacebookMessage> lstfbmsg = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.getAllFacebookMessagesOfUserByProfileId(item.ProfileId), typeof(List<FacebookMessage>));
                        foreach (FacebookMessage facebookmsg in lstfbmsg)
                        {
                            try
                            {
                                ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type, 1);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Exception Message : " + ex.Message);
                                logger.Error("Exception Message : " + ex.StackTrace);
                            }
                        }

                    }
                    else if (item.ProfileType == "twitter")
                    {
                        List<Domain.Socioboard.Domain.TwitterMessage> lstmsgtwtuser = (List<TwitterMessage>)new JavaScriptSerializer().Deserialize(objApiTwitterMessage.getUnreadMessages(item.ProfileId), typeof(List<Domain.Socioboard.Domain.TwitterMessage>));

                        foreach (TwitterMessage lst in lstmsgtwtuser)
                        {
                            try
                            {
                                ds.Tables[0].Rows.Add(lst.ProfileId, "twitter", lst.FromId, lst.FromScreenName, lst.FromProfileUrl, lst.MessageDate, lst.TwitterMsg, "", "", lst.MessageId, lst.Type, lst.ReadStatus);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Exception Message : " + ex.Message);
                                logger.Error("Exception Message : " + ex.StackTrace);
                            }
                        }
                    }
                    else if (item.ProfileType == "googleplus")
                    {

                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Exception Message : " + ex.Message);
                    logger.Error("Exception Message : " + ex.StackTrace);
                }
            }
            foreach (TeamMemberProfile item in alstprofiles)
            {
                if (item.ProfileType == "facebook")
                {
                    try
                    {
                        
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

// suraj===============================================================================
            //List<FacebookFeed> alstfbmsgs = (List<FacebookFeed>)new JavaScriptSerializer().Deserialize(objApiFacebookFeed.getAllReadFbFeeds(facebookid), typeof(List<FacebookFeed>));

            //try
            //{
            //    foreach (FacebookFeed facebookmsg in alstfbmsgs)
            //    {
            //        try
            //        {
            //            ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type, facebookmsg.ReadStatus);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //}

//===============================================================================

           
            List<TwitterMessage> lstmsgtwt = (List<TwitterMessage>)new JavaScriptSerializer().Deserialize(objApiTwitterMessage.getAlltwtMessagesOfUser(twitterid), typeof(List<TwitterMessage>));

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



            #region Commented G+ code, to be used later
            //GooglePlusActivitiesRepository objActRepository = new GooglePlusActivitiesRepository();
            //List<GooglePlusActivities> lstmsggpl = objActRepository.getAllgplusOfUser(googleplusid);
            //try
            //{
            //    foreach (GooglePlusActivities lst in lstmsggpl)
            //    {
            //        try
            //        {
            //            ds.Tables[0].Rows.Add(lst.GpUserId, "googleplus", lst.FromId, lst.FromUserName, lst.FromProfileImage, lst.PublishedDate, lst.Content, "", "", lst.ActivityId, "activities");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //} 
            #endregion

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

                    List<FacebookAccount> alstfbaccount = (List<FacebookAccount>)new JavaScriptSerializer().Deserialize(objApiFacebookAccount.getAllFacebookAccountsOfUser(user.Id.ToString()), typeof(List<FacebookAccount>));

                    foreach (FacebookAccount item in alstfbaccount)
                    {
                      
                        List<FacebookMessage> lstfbmsg = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.getAllFacebookMessagesOfUser(user.Id.ToString(), item.FbUserId), typeof(List<FacebookMessage>));
                        foreach (FacebookMessage facebookmsg in lstfbmsg)
                        {
                            ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type);
                        }
                    }
                }
                else if (network == "twitter")
                {
                  
                    List<TwitterAccount> alsttwtaccount = (List<TwitterAccount>)new JavaScriptSerializer().Deserialize(objApiTwitterAccount.getAllTwitterAccountsOfUser(user.Id.ToString()), typeof(List<TwitterAccount>));
                    foreach (TwitterAccount item in alsttwtaccount)
                    {
                     
                        List<TwitterFeed> lsttwtmsg = (List<TwitterFeed>)new JavaScriptSerializer().Deserialize(objApiTwitterFeed.getAllTwitterFeedOfUsers(user.Id.ToString(), item.TwitterUserId), typeof(List<TwitterFeed>));
                        foreach (TwitterFeed twtmsg in lsttwtmsg)
                        {
                            ds.Tables[0].Rows.Add(twtmsg.ProfileId, "twitter", twtmsg.FromId, twtmsg.FromScreenName, twtmsg.FromProfileUrl, twtmsg.FeedDate, twtmsg.Feed, "", "", twtmsg.MessageId, twtmsg.Type);
                        }
                    }

                }
                #region To be used later
                //else if (network == "linkedin")
                //{
                //    LinkedInAccountRepository liaccountrepo = new LinkedInAccountRepository();
                //    LinkedInFeedRepository lifeedrepo = new LinkedInFeedRepository();
                //    ArrayList alstliaccount = liaccountrepo.getAllLinkedinAccountsOfUser(user.Id);
                //    foreach (LinkedInAccount item in alstliaccount)
                //    {
                //        List<LinkedInFeed> lsttwtmsg = lifeedrepo.getAllLinkedInFeedsOfUser(user.Id, item.LinkedinUserId);
                //        foreach (LinkedInFeed limsg in lsttwtmsg)
                //        {
                //            ds.Tables[0].Rows.Add(limsg.ProfileId, "linkedin", limsg.FromId, limsg.FromName, limsg.FromPicUrl, limsg.FeedsDate, limsg.Feeds, "", "", "", limsg.Type);
                //        }
                //    }

                //}
                //else if (network == "instagram")
                //{
                //    InstagramAccountRepository insAccRepo = new InstagramAccountRepository();
                //    InstagramFeedRepository insFeedRepo = new InstagramFeedRepository();
                //    ArrayList alstlistaccount = insAccRepo.getAllInstagramAccountsOfUser(user.Id);
                //    foreach (InstagramAccount item in alstlistaccount)
                //    {
                //        List<InstagramFeed> lstFeeed = insFeedRepo.getAllInstagramFeedsOfUser(user.Id, item.InstagramId);
                //        foreach (InstagramFeed insFeed in lstFeeed)
                //        {
                //            ds.Tables[0].Rows.Add(insFeed.InstagramId, "instagram", "", "", "", insFeed.FeedDate, insFeed.FeedImageUrl, "", "", insFeed.FeedId, "");
                //        }
                //    }
                //} 
                #endregion

            }
            return ds;
        }

        public DataSet bindSentMessagesToDataTable(User user, string network)
        {
          
            List<SocialProfile> alstprofiles = (List<SocialProfile>)new JavaScriptSerializer().Deserialize(objApiSocialProfile.GetSocialProfileByUserId(user.Id.ToString()), typeof(List<SocialProfile>));
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
            foreach (SocialProfile item in alstprofiles)
            {
                if (item.ProfileType == "facebook")
                {
                   
                    List<FacebookMessage> alstfbmsgs = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.getAllSentMessages(item.ProfileId), typeof(List<FacebookMessage>)); ;

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
                   
                    List<TwitterDirectMessages> lstmsgtwtuser = (List<TwitterDirectMessages>)new JavaScriptSerializer().Deserialize(objApiTwitterDirectMessages.getAllDirectMessagesById(item.ProfileId), typeof(List<TwitterDirectMessages>));
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

        public DataSet bindFeedMessageIntoDataTable(string[] profid)
        {
            string fbProfileid = string.Empty;
            string twtProfileid = string.Empty;
            string profilId = string.Empty;
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
            try
            {
                foreach (var item in profid)
                {
                    if (item.Contains("fb_"))
                    {
                        profilId = item.ToString().Split('_')[1];
                        fbProfileid += profilId + ',';
                    }
                    else if (item.Contains("twt_"))
                    {
                        profilId = item.ToString().Split('_')[1];
                        twtProfileid += profilId + ',';
                    }
                    else
                    {
                        try
                        {

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

                try
                {
                    fbProfileid = fbProfileid.Substring(0, fbProfileid.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtProfileid = twtProfileid.Substring(0, twtProfileid.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

               
                //List<FacebookFeed> alstfbmsgs = (List<FacebookFeed>)new JavaScriptSerializer().Deserialize(objApiFacebookFeed.getAllFeedDetail(fbProfileid), typeof(List<FacebookFeed>));
                List<FacebookMessage> alstfbmsgs = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.GetAllMessageDetail(fbProfileid),typeof(List<FacebookMessage>));
                try
                {
                    foreach (FacebookMessage facebookmsg in alstfbmsgs)
                    {
                        try
                        {
                           ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type);
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


            
                List<TwitterMessage> lstmsgtwt = (List<TwitterMessage>)new JavaScriptSerializer().Deserialize(objApiTwitterMessage.getAlltwtMessages(twtProfileid), typeof(List<TwitterMessage>));
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
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ds;
        }

        public DataSet bindMessagesIntoDataTable(Guid id, int noOfDataToSkip)
        {
            Api.TeamMemberProfile.TeamMemberProfile objTeamMemberProfileRepository = new Api.TeamMemberProfile.TeamMemberProfile();
            List<TeamMemberProfile> alstprofiles = (List<Domain.Socioboard.Domain.TeamMemberProfile>)new JavaScriptSerializer().Deserialize(objTeamMemberProfileRepository.GetTeamMemberProfilesByTeamId(id.ToString()), typeof(List<Domain.Socioboard.Domain.TeamMemberProfile>));
            Api.FacebookFeed.FacebookFeed objApiFacebookFeed = new Api.FacebookFeed.FacebookFeed();
            Api.TwitterMessage.TwitterMessage objApiTwitterMessage = new Api.TwitterMessage.TwitterMessage();

            Messages mstable = new Messages();

            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);

            foreach (TeamMemberProfile item in alstprofiles)
            {
                try
                {
                    if (item.ProfileType == "facebook")
                    {
                        //List<Domain.Socioboard.Domain.FacebookFeed> alstfeedfb = (List<Domain.Socioboard.Domain.FacebookFeed>)new JavaScriptSerializer().Deserialize(objApiFacebookFeed.getUnreadMessages(item.ProfileId), typeof(List<Domain.Socioboard.Domain.FacebookFeed>)); ;

                        //foreach (FacebookFeed facebookmsg in alstfeedfb)
                        //{
                        //    ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type, facebookmsg.ReadStatus);
                        //}

                        //Updated by SumitGupta [09-02-2015]
                        //List<FacebookMessage> lstfbmsg = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.getAllFacebookMessagesOfUserByProfileId(item.ProfileId), typeof(List<FacebookMessage>));
                        List<FacebookMessage> lstfbmsg = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.getAllFacebookMessagesOfUserByProfileIdWithRange(item.ProfileId, noOfDataToSkip.ToString()), typeof(List<FacebookMessage>));
                        foreach (FacebookMessage facebookmsg in lstfbmsg)
                        {
                            try
                            {
                                ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type, 1);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Exception Message : " + ex.Message);
                                logger.Error("Exception Message : " + ex.StackTrace);
                            }
                        }

                    }
                    else if (item.ProfileType == "twitter")
                    {
                        List<Domain.Socioboard.Domain.TwitterMessage> lstmsgtwtuser = (List<TwitterMessage>)new JavaScriptSerializer().Deserialize(objApiTwitterMessage.getUnreadMessages(item.ProfileId), typeof(List<Domain.Socioboard.Domain.TwitterMessage>));

                        foreach (TwitterMessage lst in lstmsgtwtuser)
                        {
                            try
                            {
                                ds.Tables[0].Rows.Add(lst.ProfileId, "twitter", lst.FromId, lst.FromScreenName, lst.FromProfileUrl, lst.MessageDate, lst.TwitterMsg, "", "", lst.MessageId, lst.Type, lst.ReadStatus);
                            }
                            catch (Exception ex)
                            {
                                logger.Error("Exception Message : " + ex.Message);
                                logger.Error("Exception Message : " + ex.StackTrace);
                            }
                        }
                    }
                    else if (item.ProfileType == "googleplus")
                    {

                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Exception Message : " + ex.Message);
                    logger.Error("Exception Message : " + ex.StackTrace);
                }
            }
            foreach (TeamMemberProfile item in alstprofiles)
            {
                if (item.ProfileType == "facebook")
                {
                    try
                    {

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

            // suraj===============================================================================
            //List<FacebookFeed> alstfbmsgs = (List<FacebookFeed>)new JavaScriptSerializer().Deserialize(objApiFacebookFeed.getAllReadFbFeeds(facebookid), typeof(List<FacebookFeed>));

            //try
            //{
            //    foreach (FacebookFeed facebookmsg in alstfbmsgs)
            //    {
            //        try
            //        {
            //            ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.FeedDate, facebookmsg.FeedDescription, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.FeedId, facebookmsg.Type, facebookmsg.ReadStatus);
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //}

            //===============================================================================


            List<TwitterMessage> lstmsgtwt = (List<TwitterMessage>)new JavaScriptSerializer().Deserialize(objApiTwitterMessage.getAlltwtMessagesOfUser(twitterid), typeof(List<TwitterMessage>));

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



            #region Commented G+ code, to be used later
            //GooglePlusActivitiesRepository objActRepository = new GooglePlusActivitiesRepository();
            //List<GooglePlusActivities> lstmsggpl = objActRepository.getAllgplusOfUser(googleplusid);
            //try
            //{
            //    foreach (GooglePlusActivities lst in lstmsggpl)
            //    {
            //        try
            //        {
            //            ds.Tables[0].Rows.Add(lst.GpUserId, "googleplus", lst.FromId, lst.FromUserName, lst.FromProfileImage, lst.PublishedDate, lst.Content, "", "", lst.ActivityId, "activities");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.StackTrace);
            //} 
            #endregion

            return ds;
        }

        public DataSet bindFeedMessageIntoDataTable(string[] profid, int noOfDataToSkip)
        {
            string fbProfileid = string.Empty;
            string twtProfileid = string.Empty;
            string profilId = string.Empty;
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
            try
            {
                foreach (var item in profid)
                {
                    if (item.Contains("fb_"))
                    {
                        profilId = item.ToString().Split('_')[1];
                        fbProfileid += profilId + ',';
                    }
                    else if (item.Contains("twt_"))
                    {
                        profilId = item.ToString().Split('_')[1];
                        twtProfileid += profilId + ',';
                    }
                    else
                    {
                        try
                        {

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

                try
                {
                    fbProfileid = fbProfileid.Substring(0, fbProfileid.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtProfileid = twtProfileid.Substring(0, twtProfileid.Length - 1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                //Updated by SumitGupta [09-02-2015]
                //List<FacebookMessage> alstfbmsgs = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.GetAllMessageDetail(fbProfileid), typeof(List<FacebookMessage>));
                List<FacebookMessage> alstfbmsgs = (List<FacebookMessage>)new JavaScriptSerializer().Deserialize(objApiFacebookMessage.GetAllMessageDetailWithRange(fbProfileid, noOfDataToSkip.ToString()), typeof(List<FacebookMessage>));
                try
                {
                    foreach (FacebookMessage facebookmsg in alstfbmsgs)
                    {
                        try
                        {
                            ds.Tables[0].Rows.Add(facebookmsg.ProfileId, "facebook", facebookmsg.FromId, facebookmsg.FromName, facebookmsg.FromProfileUrl, facebookmsg.MessageDate, facebookmsg.Message, facebookmsg.FbComment, facebookmsg.FbLike, facebookmsg.MessageId, facebookmsg.Type);
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



                List<TwitterMessage> lstmsgtwt = (List<TwitterMessage>)new JavaScriptSerializer().Deserialize(objApiTwitterMessage.getAlltwtMessages(twtProfileid), typeof(List<TwitterMessage>));
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return ds;
        }

        // Edited by Antima[20/12/2014]

        public DataSet bindMyTickets(Guid UserId)
        {
            Messages mstable = new Messages();
            DataSet ds = DataTableGenerator.CreateDataSetForTable(mstable);
            Api.SentimentalAnalysis.SentimentalAnalysis ApiobjSentimentalAnalysis = new Api.SentimentalAnalysis.SentimentalAnalysis();
            List<Domain.Socioboard.Domain.FeedSentimentalAnalysis> lstNegativeFeedsOfUser = new List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>();

            lstNegativeFeedsOfUser = (List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>)(new JavaScriptSerializer().Deserialize(ApiobjSentimentalAnalysis.getNegativeFeedsOfUser(UserId.ToString()), typeof(List<Domain.Socioboard.Domain.FeedSentimentalAnalysis>)));
            if (lstNegativeFeedsOfUser != null)
            {
                foreach (var item in lstNegativeFeedsOfUser)
                {

                    string Network = item.Network;
                    if (Network == "facebook")
                    {
                        Api.FacebookFeed.FacebookFeed ApiobjFacebookFeed = new Api.FacebookFeed.FacebookFeed();
                        Domain.Socioboard.Domain.FacebookFeed facebookfeed = (Domain.Socioboard.Domain.FacebookFeed)(new JavaScriptSerializer().Deserialize(ApiobjFacebookFeed.getFacebookFeedByProfileId(item.ProfileId, item.FeedId), typeof(Domain.Socioboard.Domain.FacebookFeed)));
                        if (facebookfeed != null)
                        {
                            ds.Tables[0].Rows.Add(facebookfeed.ProfileId, "facebook", facebookfeed.FromId, facebookfeed.FromName, facebookfeed.FromProfileUrl, facebookfeed.FeedDate, facebookfeed.FeedDescription, facebookfeed.FbComment, facebookfeed.FbLike, facebookfeed.FeedId, facebookfeed.Type, "");
                        }
                    }
                    if (Network == "twitter")
                    {
                        Api.TwitterFeed.TwitterFeed ApiobjTwitterFeed = new Api.TwitterFeed.TwitterFeed();
                        Domain.Socioboard.Domain.TwitterFeed twtfeed = (Domain.Socioboard.Domain.TwitterFeed)(new JavaScriptSerializer().Deserialize(ApiobjTwitterFeed.getTwitterFeedByProfileId(item.ProfileId, item.FeedId), typeof(Domain.Socioboard.Domain.TwitterFeed)));
                        if (twtfeed != null)
                        {
                            ds.Tables[0].Rows.Add(twtfeed.ProfileId, item.Network, twtfeed.FromId, twtfeed.FromScreenName, twtfeed.FromProfileUrl, twtfeed.FeedDate, twtfeed.Feed, "", "", twtfeed.MessageId, twtfeed.Type, "");
                        }
                    }
                } 
            }
            return ds;
        }

    }

}