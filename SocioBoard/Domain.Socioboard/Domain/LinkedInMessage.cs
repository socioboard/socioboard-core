using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class LinkedInMessage
    {
        public Guid Id { get; set; }
        public string ProfileId { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfileName { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string Likes { get; set; }
        public string Comments { get; set; }
        public string FeedId { get; set; }
        public string ProfileImageUrl { get; set; }
    }

}