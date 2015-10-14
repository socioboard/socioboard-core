using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class InstagramSelfFeed
    {
        public Guid Id { get; set; }
        public string ProfileId { get; set; }
        public string FeedId { get; set; }
        public string Accesstoken { get; set; }
        public string User_name { get; set; }
        public string Post_url { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public DateTime Created_Time { get; set; } 
    }
}
