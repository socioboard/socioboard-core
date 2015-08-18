using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class TwitterFollowerNames
    {
        public Guid Id { get; set; }
        public string TwitterProfileId { get; set; }
        public string FollowerId { get; set; }
        public string Followerscrname { get; set; }
        public string Name { get; set; }
    
    }
}
