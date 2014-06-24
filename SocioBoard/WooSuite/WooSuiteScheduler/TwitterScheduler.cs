using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using SocioBoard.Model;
using SocioBoard.Domain;
using GlobusTwitterLib.Authentication;
using System.Configuration;
using GlobusTwitterLib.App.Core;
using Newtonsoft.Json.Linq;

namespace WooSuiteScheduler
{
  public  class TwitterScheduler:Scheduler
    {
       

        public override void PostScheduleMessage(dynamic data)
        {
            try
            {

                oAuthTwitter OAuthTwt = new oAuthTwitter();
                TwitterAccountRepository fbaccrepo = new TwitterAccountRepository();
                TwitterAccount twtaccount = fbaccrepo.getUserInformation(data.UserId, data.ProfileId);
                OAuthTwt.CallBackUrl = System.Configuration.ConfigurationSettings.AppSettings["callbackurl"];
                OAuthTwt.ConsumerKey = System.Configuration.ConfigurationSettings.AppSettings["consumerKey"];
                OAuthTwt.ConsumerSecret = System.Configuration.ConfigurationSettings.AppSettings["consumerSecret"];
                OAuthTwt.Token = twtaccount.OAuthToken;
                OAuthTwt.TokenSecret = twtaccount.OAuthSecret;
                OAuthTwt.TwitterUserName = twtaccount.TwitterScreenName;
                TwitterUser twtuser = new TwitterUser();
                JArray post = twtuser.Post_Status_Update(OAuthTwt, data.ShareMessage);
             
                Console.WriteLine("Message post on twitter for Id :" + twtaccount.TwitterUserId + " and Message: " + data.ShareMessage);
                ScheduledMessageRepository schrepo = new ScheduledMessageRepository();
                schrepo.updateMessage(data.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }

        public override void PostScheduleMessageWithImage(dynamic data)
        {
            throw new NotImplementedException();
        }
    }
}
