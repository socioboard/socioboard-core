using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class GoogleplusLike
    {
       public Guid Id { get;set; }
       public string ProfileId { get; set; }
       public string FeedId { get; set; }
       public string FromId { get; set; }
       public string FromName { get; set; }
       public string FromImageUrl { get; set; }
       public string FromUrl { get; set; }
       public int Status { get; set; }
       public DateTime CreateDate { get; set; }
    }
}
