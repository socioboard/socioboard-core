using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioBoard.Domain
{
    interface IAdsRepository
    {
        void AddAds(Ads ads);
        int DeleteAds(Guid newsid);
        void UpdateAds(Ads ads);
        List<Ads> getAllAds();
        bool checkAdsExists(string newsname);
        Ads getAdsDetails(string newsname);

    }
}
