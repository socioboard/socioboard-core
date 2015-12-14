using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class GoogleAnalyticsAccount : ISocialSiteAccount
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string EmailId { get; set; }
        public string GaAccountId { get; set; }
        public string GaAccountName { get; set; }
        public string GaProfileId { get; set; }
        public string GaWebPropertyId { get; set; }
        public string GaProfileName { get; set; }
        public string WebsiteUrl { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public string ProfilePicUrl { get; set; }
        public double Visits { get; set; }
        public double Views { get; set; }
        public double TwitterPosts { get; set; }
        public double WebMentions { get; set; }
        public bool IsActive { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProfileType
        {
            get
            {
                return "googleanalytics";
            }
            set
            {
                value = "googleanalytics";
            }
        }

    }
    
}