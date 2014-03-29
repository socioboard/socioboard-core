using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Model;
using SocioBoard.Domain;
using Newtonsoft.Json.Linq;
using GlobusTwitterLib.App.Core;
using GlobusTwitterLib.Authentication;
using System.Collections;
using GlobusTwitterLib.Twitter.Core.UserMethods;
using System.Configuration;

namespace SocialSiteDataService
{
    public class TwitterData
    {
        public void getTwitterData(object UserId)
        {
            try
            {
                Guid userId = (Guid)UserId;
                oAuthTwitter OAuth = new oAuthTwitter(ConfigurationManager.AppSettings["consumerKey"], ConfigurationManager.AppSettings["consumerSecret"], ConfigurationManager.AppSettings["callbackurl"]);
                TwitterAccountRepository objTwtRepo = new TwitterAccountRepository();

                ArrayList arrTwtAcc = objTwtRepo.getAllTwitterAccountsOfUser(userId);
                foreach (TwitterAccount itemTwt in arrTwtAcc)
                {
                    OAuth.AccessToken = itemTwt.OAuthToken;
                    OAuth.AccessTokenSecret = itemTwt.OAuthSecret;

                    getUserProile(OAuth, itemTwt.TwitterUserId, userId);
                    getUserTweets(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId);
                    getUserFeed(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
        public void getUserProile(oAuthTwitter OAuth, string TwitterScreenName, Guid userId)
        {
            try
            {
                Users userinfo = new Users();
                TwitterAccount twitterAccount = new TwitterAccount();
                TwitterAccountRepository twtrepo = new TwitterAccountRepository();
                JArray profile = userinfo.Get_Users_LookUp(OAuth, TwitterScreenName);
                foreach (var item in profile)
                {
                    try
                    {
                        twitterAccount.FollowingCount = Convert.ToInt32(item["friends_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twitterAccount.FollowersCount = Convert.ToInt32(item["followers_count"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }
                    twitterAccount.IsActive = true;
                    twitterAccount.OAuthSecret = OAuth.AccessTokenSecret;
                    twitterAccount.OAuthToken = OAuth.AccessToken;
                    try
                    {
                        twitterAccount.ProfileImageUrl = item["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);

                    }
                    try
                    {
                        twitterAccount.ProfileUrl = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twitterAccount.TwitterUserId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception er)
                    {
                        try
                        {
                            twitterAccount.TwitterUserId = item["id"].ToString().TrimStart('"').TrimEnd('"');
                        }
                        catch (Exception err)
                        {
                            Console.WriteLine(err.StackTrace);

                        }
                        Console.WriteLine(er.StackTrace);

                    }
                    try
                    {
                        twitterAccount.TwitterScreenName = item["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    twitterAccount.UserId = userId;

                    if (twtrepo.checkTwitterUserExists(twitterAccount.TwitterUserId, userId))
                    {
                        twtrepo.updateTwitterUser(twitterAccount);
                    }
                    getTwitterStats(twitterAccount);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void getUserTweets(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId)
        {

            try
            {
                TwitterUser twtuser = new TwitterUser();
                JArray data = twtuser.GetStatuses_User_Timeline(OAuth);
                TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
                TwitterMessage twtmsg = new TwitterMessage();
                foreach (var item in data)
                {
                    twtmsg.UserId = userId;
                    twtmsg.Type = "twt_usertweets";
                    try
                    {
                        twtmsg.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.ScreenName = TwitterScreenName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.ProfileId = TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.MessageDate = SocioBoard.Helper.Extensions.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.Id = Guid.NewGuid();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    twtmsg.EntryDate = DateTime.Now;
                    try
                    {
                        twtmsg.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    if (!twtmsgrepo.checkTwitterMessageExists(twtmsg.ProfileId, twtmsg.UserId, twtmsg.MessageId))
                    {
                        twtmsgrepo.addTwitterMessage(twtmsg);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        public void getUserFeed(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId)
        {
            try
            {
                //User user = (User)Session["LoggedUser"];
                TwitterUser twtuser = new TwitterUser();
                JArray data = twtuser.GetStatuses_Home_Timeline(OAuth);

                TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
                TwitterFeed twtmsg = new TwitterFeed();
                foreach (var item in data)
                {
                    twtmsg.UserId = userId;
                    twtmsg.Type = "twt_feeds";
                    try
                    {
                        twtmsg.Feed = item["text"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.ScreenName = TwitterScreenName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.ProfileId = TwitterUserId;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FeedDate = SocioBoard.Helper.Extensions.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.Id = Guid.NewGuid();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    try
                    {
                        twtmsg.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    twtmsg.EntryDate = DateTime.Now;
                    try
                    {
                        twtmsg.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                    if (!twtmsgrepo.checkTwitterFeedExists(twtmsg.ProfileId, twtmsg.UserId, twtmsg.MessageId))
                    {
                        twtmsgrepo.addTwitterFeed(twtmsg);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }


        }
        public void getTwitterStats(TwitterAccount twitterAccount)
        {
            TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
            TwitterMessageRepository objTweetMsgRepo = new TwitterMessageRepository();
            TwitterStats objStats = new TwitterStats();
            Random rNum = new Random();
            objStats.Id = Guid.NewGuid();
            objStats.TwitterId = twitterAccount.TwitterUserId;
            objStats.UserId = twitterAccount.UserId;
            objStats.FollowingCount = twitterAccount.FollowingCount;
            objStats.FollowerCount = twitterAccount.FollowersCount;
            objStats.Age1820 = rNum.Next(twitterAccount.FollowersCount);
            objStats.Age2124 = rNum.Next(twitterAccount.FollowersCount);
            objStats.Age2534 = rNum.Next(twitterAccount.FollowersCount);
            objStats.Age3544 = rNum.Next(twitterAccount.FollowersCount);
            objStats.Age4554 = rNum.Next(twitterAccount.FollowersCount);
            objStats.Age5564 = rNum.Next(twitterAccount.FollowersCount);
            objStats.Age65 = rNum.Next(twitterAccount.FollowersCount);
            int replies = objTweetMsgRepo.getRepliesCount(twitterAccount.UserId, twitterAccount.TwitterUserId);
            int retweets = objTweetMsgRepo.getRetweetCount(twitterAccount.UserId, twitterAccount.TwitterUserId);
            if (twitterAccount.FollowersCount != 0)
                objStats.Engagement = (replies + retweets) / twitterAccount.FollowersCount;
            else
                objStats.Engagement = 0;
            //  objStats.Influence=
            // objStats.Engagement=
            objStats.EntryDate = DateTime.Now;
            if (!objTwtstats.checkTwitterStatsExists(twitterAccount.TwitterUserId, twitterAccount.UserId))
                objTwtstats.addTwitterStats(objStats);
        }

    }
}
