using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using SocioBoard.Model;
using SocioBoard.Domain;
using SocioBoard.Helper;
using GlobusTwitterLib.Authentication;
using GlobusTwitterLib.Twitter.Core.TweetMethods;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace RssService
{
   public class RssPosts
    {

       public int getRssFeeds(object url)
       {
           try
           {
               //Fetch the subscribed RSS Feed
               XmlDocument RSSXml = new XmlDocument();
               RSSXml.Load(url.ToString());

               XmlNodeList RSSNodeList = RSSXml.SelectNodes("rss/channel/item");
               XmlNode RSSDesc = RSSXml.SelectSingleNode("rss/channel/title");
               RssReaderRepository rssReaderRepo = new RssReaderRepository();
               StringBuilder sb = new StringBuilder();

               foreach (XmlNode RSSNode in RSSNodeList)
               {
                   RssReader rssReader = new RssReader();

                   XmlNode RSSSubNode;
                   RSSSubNode = RSSNode.SelectSingleNode("title");
                   string title = RSSSubNode != null ? RSSSubNode.InnerText : "";

                   RSSSubNode = RSSNode.SelectSingleNode("link");
                   string link = RSSSubNode != null ? RSSSubNode.InnerText : "";

                   RSSSubNode = RSSNode.SelectSingleNode("description");
                   string desc = RSSSubNode != null ? RSSSubNode.InnerText : "";

                   RSSSubNode = RSSNode.SelectSingleNode("pubDate");
                   string publishDate = RSSSubNode != null ? RSSSubNode.InnerText : "";

                   rssReader.Id = Guid.NewGuid();
                   rssReader.Description = desc;
                   rssReader.Link = link;
                   rssReader.PublishedDate = publishDate;
                   rssReader.Title = title;
                   rssReader.FeedsUrl = url.ToString();
                   rssReader.CreatedDate = DateTime.Now;
                   rssReader.Status = false;

                   if (!rssReaderRepo.CheckFeedExists(url.ToString(), desc, publishDate))
                   {
                       rssReaderRepo.AddRssReader(rssReader);
                   }
               }
               return 0;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.StackTrace);
               return 0;
           }
       }

       public int postRssFeeds(object data)
       {
           try
           {
               TwitterAccountRepository twtAccountRepo = new TwitterAccountRepository();
               RssFeedsRepository rssFeedsRepo = new RssFeedsRepository();
               RssReaderRepository rssReadRepo = new RssReaderRepository();
               RssFeeds objrss = (RssFeeds)data;

               TwitterAccountRepository twtaccountrepo = new TwitterAccountRepository();
               SocioBoard.Domain.TwitterAccount twtaccount = twtaccountrepo.getUserInformation(objrss.ProfileScreenName,objrss.UserId);
               TwitterHelper twthelper = new TwitterHelper();
               oAuthTwitter OAuthTwt = new oAuthTwitter();
               OAuthTwt.AccessToken = twtaccount.OAuthToken;
               OAuthTwt.AccessTokenSecret = twtaccount.OAuthSecret;
               OAuthTwt.TwitterScreenName = twtaccount.TwitterScreenName;
               OAuthTwt.TwitterUserId = twtaccount.TwitterUserId;
               OAuthTwt.ConsumerKey = ConfigurationManager.AppSettings["consumerKey"];
               OAuthTwt.ConsumerKeySecret = ConfigurationManager.AppSettings["consumerSecret"];
               twthelper.SetCofigDetailsForTwitter(OAuthTwt);
               Tweet twt = new Tweet();
               List<RssReader> lstRssReader= rssReadRepo.geturlRssFeed(objrss.FeedUrl);
               foreach (RssReader rss in lstRssReader)
               {
                     JArray post = twt.Post_Statuses_Update(OAuthTwt, rss.Title);
                     rssReadRepo.UpdateStatus(rss.Id);
               }
               return 0;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.StackTrace);
               return 0;
           }
       }
    }
}
