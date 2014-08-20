using System;
using System.Collections.Generic;
using System.Text;

namespace GlobusTwitterLib.App.Core
{

  public  static class Globals
    {

       #region Search API Methods
       /// <summary>
       /// Search Trends
       /// </summary>
       public static string TrendsUrl = "http://search.twitter.com/trends.json";
       public static string SearchUrl = "http://search.twitter.com/search.atom?q=";
       #endregion

       #region Timeline Methods Urls
       /// <summary>
       /// TimeLine 
       /// </summary>
       public static string MentionUrl = "http://api.twitter.com/1/statuses/mentions.xml?count=";
       public static string HomeTimeLineUrl = "http://api.twitter.com/1/statuses/home_timeline.xml?count=";
       public static string UserTimeLineUrl = "http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=";
       public static string RetweetedByMeUrl = "http://api.twitter.com/1/statuses/retweeted_by_me.xml?count=";

       #endregion

       #region Status Methods Urls
       /// <summary>
       /// User Status Demo
       /// </summary>
       public static string ShowStatusUrl = "http://api.twitter.com/1/statuses/show/";
       public static string UpdateStatusUrl = " http://api.twitter.com/1/statuses/update.xml";
       public static string ShowStatusUrlByScreenName = "http://twitter.com/users/show.xml?screen_name=";
       public static string ReTweetStatus = "http://api.twitter.com/1/statuses/retweet/";
       #endregion

       #region User Methods Urls
       /// <summary>
       /// User Status Demo
       /// </summary>
       public static string FriendStatusUrl = "http://api.twitter.com/1/statuses/friends/";
       public static string FollowerStatusUrl = "http://api.twitter.com/1/statuses/followers/";

       #endregion

       #region Direct Message Methods 
       /// <summary>
       /// Direct Message
       /// </summary>
       public static string DirectMessageGetByUserUrl = "http://api.twitter.com/1/direct_messages.xml?count=";
       public static string DirectMessageSentByUserUrl = "http://api.twitter.com/1/direct_messages/sent.xml?count=";
       public static string NewDirectMessage = "http://api.twitter.com/1/direct_messages/new.xml";
       public static string DeleteDirectMessage = "http://api.twitter.com/1/direct_messages/destroy/";
       #endregion

       #region Account Methods
       /// <summary>
       /// Account 
       /// </summary>
       public static string VerifyCredentialsUrl = "http://twitter.com/account/verify_credentials.xml";
       public static string RateLimitStatusUrl = "http://api.twitter.com/1/account/rate_limit_status.xml";
       #endregion

       #region Friendship Methods
       /// <summary>
       /// Followers
       /// </summary>
       public static string FollowerUrl = "http://api.twitter.com/1/friendships/create.xml?screen_name=";
       public static string UnFollowUrl = "http://api.twitter.com/1/friendships/destroy.xml?screen_name=";
       public static string FollowerUrlById = "http://api.twitter.com/1/friendships/create.xml?user_id=";
       public static string UnFollowUrlById = "http://api.twitter.com/1/friendships/destroy.xml?user_id=";
       #endregion

       #region Social Graph Methods
       /// <summary>
       /// Social Graph Methods
       /// </summary>
       public static string FriendsIdUrl = "http://api.twitter.com/1/friends/ids.xml?screen_name=";
       public static string FollowersIdUrl = "http://api.twitter.com/1/followers/ids.xml?screen_name="; 
       #endregion



       //private static int _RequestCount;
       //public static int RequestCount
       //{
       //    set
       //    {
       //        _RequestCount = value;
       //    }
       //    get
       //    {
       //        return _RequestCount;
       //    }
       //}
    }
}
