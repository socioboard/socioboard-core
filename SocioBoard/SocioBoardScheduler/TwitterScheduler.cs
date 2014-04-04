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

namespace SocioBoardScheduler
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
                OAuthTwt.ConsumerKeySecret = System.Configuration.ConfigurationSettings.AppSettings["consumerSecret"];
                OAuthTwt.AccessToken = twtaccount.OAuthToken;
                OAuthTwt.AccessTokenSecret = twtaccount.OAuthSecret;
                OAuthTwt.TwitterScreenName = twtaccount.TwitterScreenName;
                OAuthTwt.TwitterUserId = twtaccount.TwitterUserId;


                #region For Testing
                // For Testing 

                //OAuthTwt.ConsumerKey = "udiFfPxtCcwXWl05wTgx6w";
                //OAuthTwt.ConsumerKeySecret = "jutnq6N32Rb7cgbDSgfsrUVgRQKMbUB34yuvAfCqTI";
                //OAuthTwt.AccessToken = "1904022338-Ao9chvPouIU8ejE1HMG4yJsP3hOgEoXJoNRYUF7";
                //OAuthTwt.AccessTokenSecret = "Wj93a8csVFfaFS1MnHjbmbPD3V6DJbhEIf4lgSAefORZ5";
                //OAuthTwt.TwitterScreenName = "";
                //OAuthTwt.TwitterUserId = ""; 
                #endregion

                TwitterUser twtuser = new TwitterUser();

                if (string.IsNullOrEmpty(data.ShareMessage))
                {
                    data.ShareMessage = "There is no data in Share Message !";
                }

                JArray post = twtuser.Post_Status_Update(OAuthTwt, data.ShareMessage);

                
             
                Console.WriteLine("Message post on twitter for Id :" + twtaccount.TwitterUserId + " and Message: " + data.ShareMessage);
                ScheduledMessageRepository schrepo = new ScheduledMessageRepository();
                schrepo.updateMessage(data.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Log log = new Log();
                log.CreatedDate = DateTime.Now;
                log.Exception = ex.Message;
                log.Id = Guid.NewGuid();
                log.ModuleName = "TwitterScheduler";
                log.ProfileId = data.ProfileId;
                log.Status = false;
                LogRepository logRepo = new LogRepository();
                logRepo.AddLog(log);
            }

        }

        public override void PostScheduleMessageWithImage(dynamic data)
        {
            throw new NotImplementedException();
        }
    }
}
