using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class WordpressAccount : ISocialSiteAccount
    {
        public Guid Id { get; set; }
        public string WpUserId { get; set; }
        public string DisplayName { get; set; }
        public string WpUserName { get; set; }
        public string EmailId { get; set; }
        public string PrimaryBlogId { get; set; }
        public string TokenSiteId { get; set; }
        public string UserAvtar { get; set; }
        public string ProfileUrl { get; set; }
        public int SiteCount { get; set; }
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }

        public string ProfileType
        {
            get
            {
                return "wordpress";
            }
            set
            {
                value = "wordpress";
            }
        }
    }
}
