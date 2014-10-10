using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class Blog_Comments
    {
        public Guid Id { get; set; }
        public Guid CommentPostId { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentAuthorEmail { get; set; }
        public string CommentAuthorUrl { get; set; }
        public string CommentAuthorIp { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public string CommentApproved { get; set; }
        
    }
}