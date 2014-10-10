using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class TwitterAccountFollowers
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TwitterAccountId { get; set; }
        public string ProfileId { get; set; }       
        public int FollowersCount { get; set; }      
        public int FollowingsCount { get; set; }
        public DateTime EntryDate { get; set; }


    }
}
