using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;

namespace RssDataSerices
{
    class RssDataService
    {
        public string UpdateRss()
        {
            string str = "";
            try
            {
                Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
                str = objRssFeed.GetRssData();
                List<Domain.Socioboard.MongoDomain.Rss> lstRss = (List<Domain.Socioboard.MongoDomain.Rss>)new JavaScriptSerializer().Deserialize(str, typeof(List<Domain.Socioboard.MongoDomain.Rss>));
                foreach (var item in lstRss)
                {
                    string rt = objRssFeed.ParseFeedUrl(item.RssFeedUrl, item.ProfileType,item.ProfileId,item.UserId,item.ProfileName,item.ProfileImageUrl);
                    if (rt == "ok")
                    {
                        Console.WriteLine("Rss Url Added");
                    }
                    Thread.Sleep(5*1000);
                }
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return "Success";
        }
    }
}
