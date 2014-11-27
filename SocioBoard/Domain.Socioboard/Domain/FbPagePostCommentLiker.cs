using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
   public class FbPagePostCommentLiker
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CommentId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
    }
}
