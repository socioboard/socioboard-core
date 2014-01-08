using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SocioBoard.Domain
{
    interface IFacebookStatsRepository
    {
        void addFacebookStats(FacebookStats fbaccount);
        int deleteFacebookStats(string fbuserid, Guid userid);
        void updateFacebookStats(FacebookStats fbaccount);
        ArrayList getAllFacebookStatsOfUser(Guid UserId, int days);
        bool checkFacebookStatsExists(string FbUserId, Guid Userid);
        FacebookStats getFacebookStatsById(string Fbuserid, Guid userId);
    }
}
