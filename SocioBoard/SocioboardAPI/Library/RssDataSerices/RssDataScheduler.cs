using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RssDataSerices
{
    class RssDataScheduler
    {
        public string PostRssFeed(string ProfileType)
        {
            string str = "";
            try
            {
                Api.RssFeeds.RssFeeds objRssFeed = new Api.RssFeeds.RssFeeds();
                str = objRssFeed.PostRssfeed(ProfileType);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }
    }
}
