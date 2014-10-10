using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Api.Socioboard.Domain
{
    public interface IGoogleAnalyticsStatsRepository
    {
        void addGoogleAnalyticsStats(GoogleAnalyticsStats gastats);
        int deleteGoogleAnalyticsStats(string gaAccountId, Guid userid);
        void updateGoogleAnalyticsStats(GoogleAnalyticsStats gastats);
        ArrayList getAllGoogleAnalyticsStatsOfUser(Guid UserId, int days);
        bool checkGoogleAnalyticsStatsExists(string gaAccountId, Guid Userid);
        ArrayList getGoogleAnalyticsStatsById(string gaAccountId, Guid userId, int days);
    }
}
