using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using GlobusTumblrLib.Authentication;

namespace GlobusTumblerLib.Tumblr.Core.BlogMethods
{
    public class BlogsFollowers
    {

        public void Unfollowblog(string accesstoken, string accesstokensecret,string blogname)
        {
            oAuthTumbler.TumblrConsumerKey = ConfigurationManager.AppSettings["TumblrClientKey"];
            oAuthTumbler.TumblrConsumerSecret = ConfigurationManager.AppSettings["TumblrClientSec"];
            var postUrl="";
             var prms = new Dictionary<string, object>();


             try
             {
                 KeyValuePair<string, string> LoginDetails = new KeyValuePair<string, string>(accesstoken, accesstokensecret);

                 prms.Add("url", "http://" + blogname + ".tumblr.com/");

                 postUrl = "https://api.tumblr.com/v2/user/unfollow/";

                 string result = oAuthTumbler.OAuthData(postUrl, "POST", LoginDetails.Key, LoginDetails.Value, prms);
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.StackTrace);
             }
                       
             
	           
        }

    }
}
