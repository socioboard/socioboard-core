using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class LinkedinCompanyPage : ISocialSiteAccount
    {

        public Guid Id { get; set; }
        public string LinkedinPageId { get; set; }
        public string LinkedinPageName { get; set; }
        public string EmailDomains { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthSecret { get; set; }
        public string OAuthVerifier { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public string FoundedYear { get; set; }
        public string EndYear { get; set; }
        public string Locations { get; set; }
        public string Specialties { get; set; }
        public string WebsiteUrl { get; set; }
        public string Status { get; set; }
        public string EmployeeCountRange { get; set; }
        public string Industries { get; set; }
        public string CompanyType { get; set; }
        public string LogoUrl { get; set; }
        public string SquareLogoUrl { get; set; }
        public string BlogRssUrl { get; set; }
        public string UniversalName { get; set; }
        public int NumFollowers { get; set; }
        public string ProfileType
        {
            get { return "linkedincompanypage"; }
            set { value = "linkedincompanypage"; }
        }
    }
}
