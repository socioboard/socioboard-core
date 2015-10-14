using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class GoogleplusComments
    {
       public Guid Id { get; set; }
       public string GplusUserId { get; set; }
       public string FeedId { get; set; }
       public string CommentId { get; set; }
       public string Comment { get; set; }
       public string FromId { get; set; }
       public string FromName { get; set; }
       public string FromImageUrl { get; set; }
       public string FromUrl { get; set; }
       public DateTime CreatedDate { get; set; } 
    }
}
