using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDataServices
{
    class SocialSiteDataFeedsFactory
    {
        ISocialSiteData objSocialSiteDataFeeds;
       
        public SocialSiteDataFeedsFactory
            (string networkType)
        {
            switch (networkType)
            {
                case "twitter":
                    //objSocialSiteDataFeeds = new TwitterData();
                    break;
                case "linkedin":
                   // objSocialSiteDataFeeds = new LinkedInData();
                    break;
                case "googleanalytics":
                    //objSocialSiteDataFeeds = new GoogleAnalyticsData();
                    break;
                case "googleplus":
                    //objSocialSiteDataFeeds = new Google();
                    break;
                case "facebook":
                    objSocialSiteDataFeeds = new FacebookData();
                    break;
                case "instagram":
                    //objSocialSiteDataFeeds = new InstagramData();
                    break;
                case "tumblr":
                    //objSocialSiteDataFeeds = new TumblrData();
                    break;
                case "youtube":
                   // objSocialSiteDataFeeds = new YouTubeData();
                    break;
                default:
                    break;
            }
        }

        public ISocialSiteData CreateSocialSiteDataFeedsInstance()
        {
            return objSocialSiteDataFeeds;
        }
    }
}
