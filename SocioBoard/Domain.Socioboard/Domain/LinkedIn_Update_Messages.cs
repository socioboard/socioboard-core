using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class LinkedIn_Update_Messages
    {
        public string ProfileId { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfileName { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedDate { get; set; }
        public string Likes { get; set; }
        public string Comments { get; set; }
        public string FeedId { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
