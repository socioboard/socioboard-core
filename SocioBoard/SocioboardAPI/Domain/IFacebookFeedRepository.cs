using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Api.Socioboard.Domain
{
    public interface IFacebookFeedRepository
    {
        void addFacebookFeed(FacebookFeed fbfeed);
        int deleteFacebookFeed(FacebookFeed fbfeed);
        int updateFacebookFeed(FacebookFeed fbfeed);
        List<FacebookFeed> getAllFacebookFeedsOfUser(Guid UserId, string profileid);
        bool checkFacebookFeedExists(string feedid, Guid Userid);
        void deleteAllFeedsOfUser(string fbuserid, Guid userid);
        List<FacebookFeed> getAllFacebookUserFeeds(string profileid);
        bool checkFacebookFeedExists(string feedsid);
    }
}