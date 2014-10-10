using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class LinkedInFeed
    {
        public Guid Id { get; set; }
        public string Feeds { get; set; }
        public DateTime FeedsDate { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string FeedId { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromPicUrl { get; set; }

    }
}