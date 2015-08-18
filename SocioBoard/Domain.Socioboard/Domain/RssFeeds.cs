using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class RssFeeds
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public bool Status { get; set; }
        public string ProfileScreenName { get; set; }
        public string FeedUrl { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishingDate { get; set; }
        public string Duration { get; set; }
        public string ProfileId { get; set; }
        public string Profiletype { get; set; }
        public string Link { get; set; }
        public string Message { get; set; }
    }
}