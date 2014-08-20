using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialSiteDataService
{
    class clsSocialSiteDataFeedsFactory
    {
        SocialSiteDataFeeds objSocialSiteDataFeeds;
       
        public clsSocialSiteDataFeedsFactory
            (string networkType)
        {
            switch (networkType)
            {
                case "twitter":
                    objSocialSiteDataFeeds = new TwitterData();
                    break;
                case "linkedin":
                    objSocialSiteDataFeeds = new LinkedInData();
                    break;
                case "googleanalytics":
                    objSocialSiteDataFeeds = new GoogleAnalyticsData();
                    break;
                case "googleplus":
                    //objSocialSiteDataFeeds = new Google();
                    break;
                case "facebook":
                    objSocialSiteDataFeeds = new FacebookData();
                    break;
                case "instagram":
                    objSocialSiteDataFeeds = new InstagramData();
                    break;
                default:
                    break;
            }
        }

        public SocialSiteDataFeeds CreateSocialSiteDataFeedsInstance()
        {
            return objSocialSiteDataFeeds;
        }
    }
}
