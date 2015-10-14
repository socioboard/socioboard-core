using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public  class TwitterRecentDetails
    {

       public Guid Id { get; set; }
       public string TwitterId { get; set; }
       public string AccountCreationDate { get; set; }
       public string LastActivityDate { get; set; }
       public string LastFeed { get; set; }
       public string FeedId { get; set; }
       public string FeedRetweetCount { get; set; }
       public string FeedFavoriteCount { get; set; }
       public string MediaType { get; set; }
       public string MediaUrl { get; set; }


    }
}
