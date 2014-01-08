using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public interface ITwitterFeedRepository
    {
        void addTwitterFeed(TwitterFeed twtfeed);
        int deleteTwitterFeed(TwitterFeed twtfeed);
        int updateTwitterFeed(TwitterFeed twtfeed);
        List<TwitterFeed> getAllTwitterFeedOfUsers(Guid UserId, string profileid);
        bool checkTwitterFeedExists(string Id, Guid Userid, string messageId);
        List<TwitterFeed> getAllTwitterFeeds(Guid userid);

    }
}