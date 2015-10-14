using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class FacebookPagePost
    {
       public Guid Id { get; set; }
       public string PageId { get; set; }
       public string PageName { get; set; }
       public string PostId { get; set; }
       public string Message { get; set; }
       public string Picture { get; set; }
       public string Link { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public string Type { get; set; }
       public string Likes { get; set; }
       public string Comments { get; set; }
       public string Shares { get; set; }
       public string Reach { get; set; }
       public string Talking { get; set; }
       public string EngagedUsers { get; set; }
       public DateTime CreatedTime { get; set; }
    }
}
