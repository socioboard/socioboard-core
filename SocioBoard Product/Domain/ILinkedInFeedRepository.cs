using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    interface ILinkedInFeedRepository
    {
        void addLinkedInFeed(LinkedInFeed fbfeed);
        int deleteLinkedInFeed(LinkedInFeed fbfeed);
        int updateLinkedInFeed(LinkedInFeed fbfeed);
        List<LinkedInFeed> getAllLinkedInFeedsOfUser(Guid UserId, string profileid);
        bool checkLinkedInFeedExists(string feedid, Guid Userid);
        int deleteAllFeedsOfUser(string fbuserid, Guid userid);
    }
}
