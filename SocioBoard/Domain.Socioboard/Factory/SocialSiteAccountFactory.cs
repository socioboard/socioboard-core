using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Factory
{
    public class SocialSiteAccountFactory
    {
        public ISocialSiteAccount objISocialSiteAccount = null;

        public SocialSiteAccountFactory(string ProfileType)
        {
            switch (ProfileType)
            {
                case "facebook":
                    objISocialSiteAccount = new FacebookAccount();
                    break;
                case "twitter":
                    objISocialSiteAccount = new TwitterAccount();
                    break;
                case "linkedin":
                    objISocialSiteAccount = new LinkedInAccount();
                    break;
                case "tumblr":
                    objISocialSiteAccount = new TumblrAccount();
                    break;
                case "linkedincompanypage":
                    objISocialSiteAccount = new LinkedinCompanyPage();
                    break;
                default:
                    break;
            }
        }

        public ISocialSiteAccount GetSocialSiteAccount()
        {
            if (objISocialSiteAccount!=null)
            {
                return objISocialSiteAccount;
            }
            return null;
        }

    }
}
