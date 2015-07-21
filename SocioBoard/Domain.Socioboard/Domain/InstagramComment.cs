using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class InstagramComment
    {

        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string InstagramId { get; set; }
        public Guid UserId { get; set; }
        public string FeedId { get; set; }
        public string CommentId { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
        public string FromName { get; set; }
        public string FromProfilePic { get; set; }

    }
}