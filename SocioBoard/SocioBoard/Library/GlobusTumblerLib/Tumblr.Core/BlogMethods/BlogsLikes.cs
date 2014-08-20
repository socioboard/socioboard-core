using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GlobusTumblrLib.Authentication;
using System.Configuration;

namespace GlobusTumblerLib.Tumblr.Core.BlogMethods
{
   public class BlogsLikes
    {
                   
        public void likeBlog(string accesstoken, string accesstokensecret, string blogId, string reblogkey, int like)
       {
           oAuthTumbler.TumblrConsumerKey =  ConfigurationManager.AppSettings["TumblrClientKey"];
           oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];

           {
               KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accesstoken, accesstokensecret);
               var prms = new Dictionary<string, object>();
               prms.Add("id", blogId);
               prms.Add("reblog_key", reblogkey);
               var postUrl = "";

               if (like == 0)
               {
                   postUrl = "https://api.tumblr.com/v2/user/unlike/";
               }
               else
               {
                   postUrl = "https://api.tumblr.com/v2/user/like/";
               }
               string result = oAuthTumbler.OAuthData(postUrl, "POST", LoginDetails.Key, LoginDetails.Value, prms);
           }

          
       }


    }
}
