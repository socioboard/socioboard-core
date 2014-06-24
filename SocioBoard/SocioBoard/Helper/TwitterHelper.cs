using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using GlobusTwitterLib.Authentication;
using SocioBoard.Model;
using SocioBoard.Domain;
using Newtonsoft.Json.Linq;
using GlobusTwitterLib.App.Core;
using Hammock.Authentication.OAuth;
using Hammock;
using GlobusTwitterLib.Twitter.Core.TimeLineMethods;

namespace SocioBoard.Helper
{
    public class TwitterHelper
    {
        public void SetCofigDetailsForTwitter(oAuthTwitter OAuth)
        {
            OAuth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"];
            OAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
            OAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
        }

        public List<TwitterMessage> getTwitterMesssage(Guid userid, string profileid)
        {
            TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
            List<TwitterMessage> lsttwtmsg = twtmsgrepo.getAllTwitterMessagesOfUser(userid, profileid);
            return lsttwtmsg;
        }


        public List<TwitterFeed> getTwitterFeed(Guid userid, string profileid)
        {
            TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
            List<TwitterFeed> lsttwtmsg = twtmsgrepo.getAllTwitterFeedOfUsers(userid, profileid);
            return lsttwtmsg;
        }

        public void getUserFeeds(string profileid)
        {
            TwitterUser twtuser = new TwitterUser();
            User user = (User)HttpContext.Current.Session["LoggedUser"];
            oAuthTwitter OAuth = new oAuthTwitter();
            JArray data = twtuser.GetStatuses_User_Timeline(OAuth);
            TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
             TwitterAccountRepository twtrepo = new TwitterAccountRepository();
             TwitterAccount twtaccount = twtrepo.getUserInformation(user.Id, profileid);

             OAuth.CallBackUrl = ConfigurationManager.AppSettings["callbackurl"];
            OAuth.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
            OAuth.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
            OAuth.TwitterScreenName = twtaccount.TwitterScreenName;
            OAuth.AccessTokenSecret = twtaccount.OAuthSecret;
            OAuth.AccessToken = twtaccount.OAuthToken;
            OAuth.ProfileImage = twtaccount.ProfileImageUrl;
         

            TwitterMessage twtmsg = new TwitterMessage();
            foreach (var item in data)
            {
                twtmsg.UserId = user.Id;
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
                    twtmsg.ScreenName = twtaccount.TwitterScreenName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtmsg.ProfileId = twtaccount.TwitterUserId;
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
                if (!twtmsgrepo.checkTwitterMessageExists(twtmsg.MessageId))
                {
                    twtmsgrepo.addTwitterMessage(twtmsg);
                }

            }
        }


        public string TwitterRedirect(string consumerKey,string consumerSecret,string CallBackUrl)
        {
            OAuthCredentials credentials = new OAuthCredentials()
            {
                Type = OAuthType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                CallbackUrl = CallBackUrl
            };

            // Use Hammock to create a rest client
            var client = new RestClient
            {
                Authority = "https://api.twitter.com/oauth",
                Credentials = credentials
            };

            // Use Hammock to create a request
            var request = new RestRequest
            {
                Path = "request_token"
            };

            // Get the response from the request
            var response = client.Request(request);

            var collection = HttpUtility.ParseQueryString(response.Content);
            HttpContext.Current.Session["requestSecret"] = collection[1];
            string rest = "https://api.twitter.com/oauth/authorize?oauth_token=" + collection[0];

            return rest;
        }

        public void getUserTweets(oAuthTwitter OAuth, TwitterAccount twitterAccount,Guid userid)
        {
         
         
            TwitterUser twtuser = new TwitterUser();


            JArray data = twtuser.GetStatuses_User_Timeline(OAuth);
            TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
            TwitterMessage twtmsg = new TwitterMessage();
            foreach (var item in data)
            {
                twtmsg.UserId = userid;
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
                    twtmsg.ScreenName = twitterAccount.TwitterScreenName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtmsg.ProfileId = twitterAccount.TwitterUserId;
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
                if (!twtmsgrepo.checkTwitterMessageExists(twtmsg.MessageId))
                {
                    twtmsgrepo.addTwitterMessage(twtmsg);
                }

            }
        }

        public void getUserFeed(oAuthTwitter OAuth, TwitterAccount twitterAccount,Guid userid)
        {
            
           
            TwitterUser twtuser = new TwitterUser();
            JArray data = twtuser.GetStatuses_Home_Timeline(OAuth);

            TwitterFeedRepository twtmsgrepo = new TwitterFeedRepository();
            TwitterFeed twtmsg = new TwitterFeed();
            foreach (var item in data)
            {
                twtmsg.UserId = userid;
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
                    twtmsg.ScreenName = twitterAccount.TwitterScreenName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtmsg.ProfileId = twitterAccount.TwitterUserId;
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
                if (!twtmsgrepo.checkTwitterFeedExists(twtmsg.MessageId))
                {
                    twtmsgrepo.addTwitterFeed(twtmsg);
                }

            }


        }

        public void getSentDirectMessages(oAuthTwitter OAuth,TwitterAccount twitterAccount,Guid userid)
        {
           
          
            TwitterUser twtuser = new TwitterUser();
            JArray data = twtuser.GetDirect_Messages_Sent(OAuth, 20);

            TwitterDirectMessageRepository twtmsgrepo = new TwitterDirectMessageRepository();
            TwitterDirectMessages twtmsg = new TwitterDirectMessages();
            foreach (var item in data)
            {
                twtmsg.UserId = userid;
                twtmsg.Type = "twt_directmessages_sent";
                twtmsg.Id = Guid.NewGuid();

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
                    twtmsg.CreatedDate = SocioBoard.Helper.Extensions.ParseTwitterTime(item["created_at"].ToString().TrimStart('"').TrimEnd('"'));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtmsg.Message = item["text"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    twtmsg.RecipientId = item["recipient"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    twtmsg.RecipientScreenName = item["recipient"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    twtmsg.RecipientProfileUrl = item["recipient"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    twtmsg.SenderId = item["sender"]["id_str"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

                try
                {
                    twtmsg.SenderScreenName = item["sender"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                } try
                {
                    twtmsg.SenderProfileUrl = item["sender"]["profile_image_url"].ToString().TrimStart('"').TrimEnd('"');
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                } try
                {
                    twtmsg.EntryDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                if (!twtmsgrepo.checkExistsDirectMessages(twtmsg.MessageId))
                {
                    twtmsgrepo.addNewDirectMessage(twtmsg);
                }
            }


        }

        public void getReTweetsOfUser(oAuthTwitter OAuth, TwitterAccount twitterAccount,Guid userid)
        {
            
          
            TwitterUser twtuser = new TwitterUser();
            JArray data = twtuser.GetStatuses_Retweet_Of_Me(OAuth);

            TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
            TwitterMessage twtmsg = new TwitterMessage();
            foreach (var item in data)
            {
                twtmsg.UserId = userid;
                twtmsg.Type = "twt_retweets";
                twtmsg.Id = Guid.NewGuid();

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
                    twtmsg.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
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

                try
                {
                    twtmsg.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
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
                    twtmsg.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
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
                } try
                {
                    twtmsg.ProfileId = twitterAccount.TwitterUserId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                } try
                {
                    twtmsg.EntryDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                if (!twtmsgrepo.checkTwitterMessageExists(twtmsg.MessageId))
                {
                    twtmsgrepo.addTwitterMessage(twtmsg);
                }
            }



        }

        public void getMentions(oAuthTwitter oauth,TwitterAccount twitterAccount,Guid userid)
        {
            TwitterUser twtuser = new TwitterUser();
            TimeLine tl = new TimeLine();
            JArray data = tl.Get_Statuses_Mentions_Timeline(oauth);

            TwitterMessageRepository twtmsgrepo = new TwitterMessageRepository();
            TwitterMessage twtmsg = new TwitterMessage();
            foreach (var item in data)
            {
                twtmsg.UserId = userid;
                twtmsg.Type = "twt_mentions";
                twtmsg.Id = Guid.NewGuid();

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
                    twtmsg.TwitterMsg = item["text"].ToString().TrimStart('"').TrimEnd('"');
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

                try
                {
                    twtmsg.FromScreenName = item["user"]["screen_name"].ToString().TrimStart('"').TrimEnd('"');
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
                    twtmsg.InReplyToStatusUserId = item["in_reply_to_status_id_str"].ToString().TrimStart('"').TrimEnd('"');
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
                } try
                {
                    twtmsg.ProfileId = twitterAccount.TwitterUserId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                try
                {
                    twtmsg.ScreenName = item["user"]["screen_name"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                
                try
                {
                    twtmsg.EntryDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                if (!twtmsgrepo.checkTwitterMessageExists(twtmsg.MessageId))
                {
                    twtmsgrepo.addTwitterMessage(twtmsg);
                }
               
            }

        }

    }
}