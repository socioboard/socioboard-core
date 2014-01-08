using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class InstagramFeed
    {
        public Guid Id { get; set; }
        public string FeedImageUrl { get; set; }
        public string FeedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string InstagramId { get; set; }
        public Guid UserId { get; set; }
        public int LikeCount { get; set; }
        public string FeedId { get; set; }
    }
}