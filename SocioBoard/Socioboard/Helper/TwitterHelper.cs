using GlobusTwitterLib.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{
    public static class TwitterHelper
    {
        public static bool FollowAccount(string AccessToken,string AccessTokenSecret ,string Screen_name, string user_id) 
        {
            bool IsFollowed = false;
            oAuthTwitter oauth = new oAuthTwitter();

            oauth.AccessToken = AccessToken;
            oauth.AccessTokenSecret = AccessTokenSecret;
            oauth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
            oauth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
            string RequestUrl = "https://api.twitter.com/1.1/friendships/create.json";
            SortedDictionary<string, string> strdic = new SortedDictionary<string, string>();
            if (!string.IsNullOrEmpty(Screen_name)) 
            {
                strdic.Add("screen_name", Screen_name);
            }
            else if (!string.IsNullOrEmpty(user_id))
            {
                strdic.Add("user_id", user_id);
            }
            else 
            {
                return false;
            }
            strdic.Add("follow", "true");
            string response = oauth.oAuthWebRequest(oAuthTwitter.Method.POST, RequestUrl, strdic);
            if (!string.IsNullOrEmpty(response)) 
            {
                IsFollowed = true;
            }
            return IsFollowed;
        }
    }
}