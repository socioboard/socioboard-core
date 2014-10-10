using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public interface IRssReader
    {

        void AddRssReader(RssReader rss);
        int DeleteRssReader(RssReader rss);
        void UpdateRssReader(RssReader rss);
        List<RssReader> getAllRss(Guid UserId);

            

    }
}