using System;
using System.Collections.Generic;
using System.Text;

namespace GlobusTwitterLib.Twitter.Core
{

  public  static class Globals
    {
       public static string authUrl = "http://twitter.com/account/verify_credentials.xml";
       public static string getFollowersUrl = "http://twitter.com/followers/ids/";
       public static string getSearchUrl = "http://search.twitter.com/search.atom?q=";
       public static string getFriendsUrl = "http://twitter.com/friends/ids/";
       public static string getUserStatusUrl = "http://twitter.com/users/show.xml?screen_name=";
       public static string getStatusUrl="http://twitter.com/statuses/update.xml";
       public static string getFollowUserUrl = "http://twitter.com/friendships/create.xml?user_id=";
       public static string getUnfollowUserUrl = "http://twitter.com/friendships/destroy.xml?user_id=";
       public static string UserFileName = "/twusers.txt";
       public static string CrusherFileName = "/crusher.txt";
       public static string CrusherMemberFileName = "/crushermembers.txt";
       public static string VipFileName = "/vip.txt";
       public static string ProxyFileName = "/proxy.txt";
       public static string FollowsFileName = "/follows.txt";
       public static string UnfollowsFileName = "/unfollows.txt";
       public static string NewFollowersName = "/newfollowers.txt";
       private static int _RequestCount;
       public static int RequestCount
       {
           set
           {
               _RequestCount = value;
           }
           get
           {
               return _RequestCount;
           }
       }
    }
}
