using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class Blog_Posts
    {
        public Guid Id { get; set; }
        public Guid PostAuthor { get; set; }
        public DateTime PostDate { get; set; }
        public string PostContent { get; set; }
        public string PostTitle { get; set; }
        public string PostStatus { get; set; }
        public string CommentStatus { get; set; }
        public string PostName { get; set; }
        public DateTime PostModifiedDate { get; set; }
        public int CommentCount { get; set; }
    }
}