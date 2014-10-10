using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Socioboard.Domain
{
    interface IFacebookInsightPostStats
    {
        void addFacebookInsightPostStats(FacebookInsightPostStats fbaccount);
        int deleteFacebookInsightPostStats(string fbuserid, Guid userid);
        void updateFacebookInsightPostStats(FacebookInsightPostStats fbaccount);
        ArrayList getAllFacebookInsightPostStatsOfUser(Guid UserId);
        bool checkFacebookInsightPostStatsExists(string FbUserId, string postId, Guid Userid, string PostDate);
        ArrayList getFacebookInsightPostStatsById(string Fbuserid, Guid userId, int days);
    }
}
