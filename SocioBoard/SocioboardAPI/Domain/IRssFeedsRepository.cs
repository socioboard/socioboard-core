using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public interface IRssFeedsRepository
    {
        void AddRssFeed(RssFeeds rss);
        void DeleteRss(RssFeeds rss);
        void UpdateRss(RssFeeds rss);

    }
}