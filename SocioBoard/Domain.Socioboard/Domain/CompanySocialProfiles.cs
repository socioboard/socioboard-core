using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Domain.Socioboard.Domain
{
    public class CompanySocialProfiles
    {
        
       
        [Key]
        public System.Guid Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Url { get; set; }
        public string IsVerified { get; set; }
        public string UserId { get; set; }
        public System.DateTime RegistrationDate { get; set; }

        public Nullable<int> facebookpageinfoID { get; set; }
        public Nullable<int> googleplusinfoID { get; set; }
        public Nullable<int> instagrampageID { get; set; }
        public Nullable<int> linkedinpageID { get; set; }
        public Nullable<int> twitterpageID { get; set; }
        public Nullable<int> youtubepageID { get; set; }

       
    }
}