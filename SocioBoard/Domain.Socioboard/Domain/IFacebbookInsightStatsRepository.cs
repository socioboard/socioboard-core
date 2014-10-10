using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Socioboard.Domain
{
    interface IFacebbookInsightStatsRepository
    {
        void addFacebookInsightStats(FacebookInsightStats fbaccount);
        int deleteFacebookInsightStats(string fbuserid, Guid userid);
        void updateFacebookInsightStats(FacebookInsightStats fbaccount);
        ArrayList getAllFacebookInsightStatsOfUser(Guid UserId);
        bool checkFacebookInsightStatsExists(string FbUserId, Guid Userid, string countdate, string agediff);
        ArrayList getFacebookInsightStatsById(string Fbuserid,int days);
    }
}
