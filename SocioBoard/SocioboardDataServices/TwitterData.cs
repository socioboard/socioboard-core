using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Socioboard.Domain;
using System.Collections;
using System.Configuration;
using System.Web.Script.Serialization;


namespace SocioboardDataServices
{
    public class TwitterData : ISocialSiteData
    {
        //public String getTwitterData(object UserId)
        //{
        //    try
        //    {
        //        Guid userId = (Guid)UserId;
        //        string ret = string.Empty;
        //        Api.TwitterAccount.TwitterAccount ApiObjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
        //        List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = (List<Domain.Socioboard.Domain.TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiObjTwitterAccount.getAllTwitterAccountsOfUser(UserId.ToString()), typeof(List<Domain.Socioboard.Domain.TwitterAccount>)));
        //        //List<TwitterAccount> lstTwitterAccount = new List<TwitterAccount>();
        //        foreach (TwitterAccount itemTwt in lstTwitterAccount)
        //        {
        //           // OAuth.AccessToken = itemTwt.OAuthToken;
        //           // OAuth.AccessTokenSecret = itemTwt.OAuthSecret;
        //           //itemTwt.OAuthToken
        //           // getUserProile(OAuth, itemTwt.TwitterUserId, userId);
        //           // getUserTweets(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId);
        //           // getUserFeed(OAuth, itemTwt.TwitterScreenName, itemTwt.TwitterUserId, userId);
        //           //itemTwt.UserId=ApiObjTwitterAccount.
        //            Api.TwitterAccount.TwitterAccount ApiobjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
        //            ret=ApiobjTwitterAccount.getTwitterData(

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }

        //}
        //public void getUserProile(oAuthTwitter OAuth, string TwitterScreenName, Guid userId)
        //{
        //    try
        //    {
        //        Users userinfo = new Users();
        //        TwitterAccount twitterAccount = new TwitterAccount();
        //        TwitterAccountRepository twtrepo = new TwitterAccountRepository();
        //        JArray profile = userinfo.Get_Users_LookUp(OAuth, TwitterScreenName);
        //        foreach (var item in profile)
        //        {
        //            try
        //            {
        //                twitterAccount.FollowingCount = Convert.ToInt32(item["friends_count"].ToString());
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twitterAccount.FollowersCount = Convert.ToInt32(item["followers_count"].ToString());
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);

        //            }
        //            twitterAccount.IsActive = true;
        //            twitterAccount.OAuthSecret = OAuth.AccessTokenSecret;
        //            twitterAccount.OAuthToken = OAuth.AccessToken;
        //            try
        //            {
        //                twitterAccount.ProfileImageUrl = item["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);

        //            }
        //            try
        //            {
        //                twitterAccount.ProfileUrl = string.Empty;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twitterAccount.TwitterUserId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception er)
        //            {
        //                try
        //                {
        //                    twitterAccount.TwitterUserId = item["id"].ToString().TrimStart('"').TrimEnd('"');
        //                }
        //                catch (Exception err)
        //                {
        //                    Console.WriteLine(err.StackTrace);

        //                }
        //                Console.WriteLine(er.StackTrace);

        //            }
        //            try
        //            {
        //                twitterAccount.TwitterScreenName = item["screen_name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            twitterAccount.UserId = userId;

        //            if (twtrepo.checkTwitterUserExists(twitterAccount.TwitterUserId, userId))
        //            {
        //                twtrepo.updateTwitterUser(twitterAccount);
        //            }
        //            getTwitterStats(twitterAccount);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}
        //public void getUserTweets(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId)
        //{

        //    try
        //    {
        //        TwitterUser twtuser = new TwitterUser();
        //        JArray data = twtuser.GetStatuses_User_Timeline(OAuth);
        //        TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
        //        TwitterMessage twtmsg = new TwitterMessage();
        //        foreach (var item in data)
        //        {
        //            twtmsg.UserId = userId;
        //            twtmsg.Type = "twt_usertweets";
        //            try
        //            {
        //                twtmsg.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.ScreenName = TwitterScreenName;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.ProfileId = TwitterUserId;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.MessageDate = SocioBoard.Helper.Extensions.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.Id = Guid.NewGuid();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            twtmsg.EntryDate = DateTime.Now;
        //            try
        //            {
        //                twtmsg.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            if (!twtmsgrepo.checkTwitterMessageExists(twtmsg.ProfileId, twtmsg.UserId, twtmsg.MessageId))
        //            {
        //                twtmsgrepo.addTwitterMessage(twtmsg);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }
        //}
        //public void getUserFeed(oAuthTwitter OAuth, string TwitterScreenName, string TwitterUserId, Guid userId)
        //{
        //    try
        //    {
        //        User user = (User)Session["LoggedUser"];
        //        TwitterUser twtuser = new TwitterUser();
        //        JArray data = twtuser.GetStatuses_Home_Timeline(OAuth);

        //        TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
        //        TwitterFeed twtmsg = new TwitterFeed();
        //        foreach (var item in data)
        //        {
        //            twtmsg.UserId = userId;
        //            twtmsg.Type = "twt_feeds";
        //            try
        //            {
        //                twtmsg.Feed = item["text"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.SourceUrl = item["source"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.ScreenName = TwitterScreenName;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.ProfileId = TwitterUserId;
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.MessageId = item["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FeedDate = SocioBoard.Helper.Extensions.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.Id = Guid.NewGuid();
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FromProfileUrl = item["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FromName = item["user"]["name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            try
        //            {
        //                twtmsg.FromId = item["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            twtmsg.EntryDate = DateTime.Now;
        //            try
        //            {
        //                twtmsg.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.StackTrace);
        //            }
        //            if (!twtmsgrepo.checkTwitterFeedExists(twtmsg.ProfileId, twtmsg.UserId, twtmsg.MessageId))
        //            {
        //                twtmsgrepo.addTwitterFeed(twtmsg);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.StackTrace);
        //    }


        //}
        //public void getTwitterStats(TwitterAccount twitterAccount)
        //{
        //    TwitterStatsRepository objTwtstats = new TwitterStatsRepository();
        //    TwitterMessageRepository objTweetMsgRepo = new TwitterMessageRepository();
        //    TwitterStats objStats = new TwitterStats();
        //    Random rNum = new Random();
        //    objStats.Id = Guid.NewGuid();
        //    objStats.TwitterId = twitterAccount.TwitterUserId;
        //    objStats.UserId = twitterAccount.UserId;
        //    objStats.FollowingCount = twitterAccount.FollowingCount;
        //    objStats.FollowerCount = twitterAccount.FollowersCount;
        //    objStats.Age1820 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age2124 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age2534 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age3544 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age4554 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age5564 = rNum.Next(twitterAccount.FollowersCount);
        //    objStats.Age65 = rNum.Next(twitterAccount.FollowersCount);
        //    int replies = objTweetMsgRepo.getRepliesCount(twitterAccount.UserId, twitterAccount.TwitterUserId);
        //    int retweets = objTweetMsgRepo.getRetweetCount(twitterAccount.UserId, twitterAccount.TwitterUserId);
        //    if (twitterAccount.FollowersCount != 0)
        //        objStats.Engagement = (replies + retweets) / twitterAccount.FollowersCount;
        //    else
        //        objStats.Engagement = 0;
        //    objStats.Influence =
        //   objStats.Engagement =
        //  objStats.EntryDate = DateTime.Now;
        //    if (!objTwtstats.checkTwitterStatsExists(twitterAccount.TwitterUserId, twitterAccount.UserId))
        //        objTwtstats.addTwitterStats(objStats);
        //}


        // public void GetData(object UserId)
        public string GetData(object UserId, string profileid)
        {
            string ret = string.Empty;
            try
            {
                Guid userId = (Guid)UserId;
                //string ret = string.Empty;
                Api.TwitterAccount.TwitterAccount ApiObjTwitterAccount = new Api.TwitterAccount.TwitterAccount();
                List<Domain.Socioboard.Domain.TwitterAccount> lstTwitterAccount = (List<Domain.Socioboard.Domain.TwitterAccount>)(new JavaScriptSerializer().Deserialize(ApiObjTwitterAccount.getAllTwitterAccountsOfUser(userId.ToString()), typeof(List<Domain.Socioboard.Domain.TwitterAccount>)));

                foreach (TwitterAccount itemTwt in lstTwitterAccount)
                {

                    Api.Twitter.Twitter ApiObjTwitter = new Api.Twitter.Twitter();
                    ret = ApiObjTwitter.getTwitterData(itemTwt.UserId.ToString(), itemTwt.TwitterUserId);

                }
                return ret;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }


        public void GetSearchData(object parameters)
        {
            //#region Twitter

            //Array arrayParams = (Array)parameters;

            //DiscoverySearch dissearch = (DiscoverySearch)arrayParams.GetValue(0);
            //DiscoverySearchRepository dissearchrepo=(DiscoverySearchRepository)arrayParams.GetValue(1);
            //DiscoverySearch discoverySearch = (DiscoverySearch)arrayParams.GetValue(2);

            //oAuthTwitter oauth = new oAuthTwitter();
            //oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
            //oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
            //oauth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"].ToString();


            //TwitterAccountRepository twtAccRepo = new TwitterAccountRepository();
            ////ArrayList alst = twtAccRepo.getAllTwitterAccounts();
            //ArrayList alst = twtAccRepo.getAllTwitterAccountsOfUser(discoverySearch.UserId);
            //foreach (TwitterAccount item in alst)
            //{
            //    oauth.AccessToken = item.OAuthToken;
            //    oauth.AccessTokenSecret = item.OAuthSecret;
            //    oauth.TwitterUserId = item.TwitterUserId;
            //    oauth.TwitterScreenName = item.TwitterScreenName;
            //    if (TwitterHelper.CheckTwitterToken(oauth, discoverySearch.SearchKeyword))
            //    {
            //        break;
            //    }
            //    else
            //    {

            //    }


            //}

            //GlobusTwitterLib.Twitter.Core.SearchMethods.Search search = new GlobusTwitterLib.Twitter.Core.SearchMethods.Search();
            //Newtonsoft.Json.Linq.JArray twitterSearchResult = search.Get_Search_Tweets(oauth, discoverySearch.SearchKeyword);

            //foreach (var item in twitterSearchResult)
            //{
            //    var results = item["statuses"];

            //    foreach (var chile in results)
            //    {
            //        try
            //        {
            //            dissearch.CreatedTime = SocioBoard.Helper.Extensions.ParseTwitterTime(chile["created_at"].ToString().TrimStart('"').TrimEnd('"')); ;
            //            dissearch.EntryDate = DateTime.Now;
            //            dissearch.FromId = chile["user"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.FromName = chile["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.ProfileImageUrl = chile["user"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.SearchKeyword = discoverySearch.SearchKeyword;
            //            dissearch.Network = "twitter";
            //            dissearch.Message = chile["text"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.MessageId = chile["id_str"].ToString().TrimStart('"').TrimEnd('"');
            //            dissearch.Id = Guid.NewGuid();
            //            dissearch.UserId = discoverySearch.UserId;//user.Id;


            //            if (!dissearchrepo.isKeywordPresent(dissearch.SearchKeyword, dissearch.MessageId))
            //            {
            //                dissearchrepo.addNewSearchResult(dissearch);
            //            }


            //        }
            //        catch (Exception ex)
            //        {
            //            //logger.Error(ex.StackTrace);
            //            Console.WriteLine(ex.StackTrace);
            //        }
            //    }
            //}
            //#endregion
        }



        //public string GetData(object UserId, string profileid)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
