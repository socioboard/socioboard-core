using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDataScheduler
{
    class clsSocialSiteScheduler
    {
        IScheduler objDataScheduler;
       
        public clsSocialSiteScheduler
            (string networkType)
        {
            switch (networkType)
            {
                case "twitter":
                    objDataScheduler = new TwitterScheduler();
                    break;
                case "linkedin":
                    objDataScheduler = new LinkedInScheduler();
                    break;
                case "googleanalytics":
                    //objSocialSiteDataFeeds = new GoogleAnalyticsData();
                    break;
                case "googleplus":
                    //objSocialSiteDataFeeds = new Google();
                    break;
                case "facebook":
                    objDataScheduler = new FacebookScheduler();
                    break;
                case "instagram":
                    //objDataScheduler = new InstagramScheduler();
                    break;
                case "tumblr":
                    objDataScheduler = new TumblrScheduler();
                    break;
                case "youtube":
                    //objDataScheduler = new YoutubeSchduler();
                    break;
                default:
                    break;
            }
        }

        public IScheduler CreateSocialSiteSchedulerInstance()
        {
            return objDataScheduler;
        }
    }
}
