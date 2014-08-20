using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    interface IInstagramFeed
    {
        void addInstagramFeed(InstagramFeed insfeed);
        int deleteInstagramFeed(InstagramFeed insfeed);
        int updateInstagramFeed(InstagramFeed insfeed);
        List<InstagramFeed> getAllInstagramFeedsOfUser(Guid UserId, string profileid);
        bool checkInstagramFeedExists(string feedid, Guid Userid);
        void deleteAllFeedsOfUser(string fbuserid, Guid userid);
    }
}
