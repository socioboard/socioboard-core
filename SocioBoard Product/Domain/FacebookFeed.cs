using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class FacebookFeed
    {
        public Guid Id { get; set; }
        public string FeedDescription {get;set;}
        public DateTime FeedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromProfileUrl { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string FbComment{get;set;}
        public string FbLike { get; set; }
        public string FeedId { get; set; }
        public int ReadStatus { get; set; }
    }
}