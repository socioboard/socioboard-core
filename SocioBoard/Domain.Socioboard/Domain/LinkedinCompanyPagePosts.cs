using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class LinkedinCompanyPagePosts
    {
        public Guid Id { get; set; }
        public string Posts { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string PostId { get; set; }
        public string PageId { get; set; }
        public string PostImageUrl { get; set; }
        public string UpdateKey { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int IsLiked { get; set; }
    }
}
