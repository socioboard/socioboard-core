using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
   public class FbPageComment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PostId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string CommentId { get; set; }
        public string Comment { get; set; }
        public string PictureUrl { get; set; }
        public int Likes { get; set; }
        public int UserLikes { get; set; }
        public DateTime Commentdate { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
