using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialSiteDataService
{
    public interface SocialSiteDataFeeds
    {
        void GetData(object UserId);

        void GetSearchData(object parameters);
    }
}
