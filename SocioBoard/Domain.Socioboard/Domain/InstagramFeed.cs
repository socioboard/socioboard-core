using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
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
        public int CommentCount { get; set; }
        public int IsLike { get; set; }
        public string AdminUser { get; set; }
        public string Feed { get; set; }
        public string ImageUrl { get; set; }
        public string FeedUrl { get; set; }
        public string FromId { get; set; }
    }
}