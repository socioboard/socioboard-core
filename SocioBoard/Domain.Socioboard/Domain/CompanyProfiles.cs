using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class CompanyProfiles
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string FbProfileId { get; set; }
        public string TwitterProfileId { get; set; }
        public string LinkedinProfileId { get; set; }
        public string InstagramProfileId { get; set; }
        public string YoutubeProfileId { get; set; }
        public string GPlusProfileId { get; set; }
        public string TumblrProfileId { get; set; }
        public Guid UserId { get; set; }
    }
}
