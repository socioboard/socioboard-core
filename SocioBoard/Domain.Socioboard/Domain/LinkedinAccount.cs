using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class LinkedInAccount : ISocialSiteAccount
    {
        public Guid Id { get; set; }
        public string LinkedinUserId { get; set; }
        public string LinkedinUserName { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthVerifier { get; set; }
        public string OAuthSecret { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfileImageUrl { get;set; }
        public bool IsActive { get; set; }
        public string EmailId { get; set; }
        public Guid UserId { get; set; }
        public int Connections { get; set; }
        public string ProfileType
        {
            get { return "linkedin"; }
            set { value = "linkedin"; }
        }
        
       // public string ProfileImageUrl { get; set; }


     



      
    }
}