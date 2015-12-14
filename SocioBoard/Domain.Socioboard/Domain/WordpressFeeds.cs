using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class WordpressFeeds
    {
        public Guid Id { get; set; }
        public string PostId { get; set; }
        public string Title { get; set; }
        public string PostUrl { get; set; }
        public string PostContent { get; set; }
        public string CommentCount { get; set; }
        public string LikeCount { get; set; }
        public string ILike { get; set; }
        public string SiteId { get; set; }
        public string WPUserId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime EntryTime { get; set; }

    }
}
