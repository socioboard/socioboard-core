using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class TwitterEngagement
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ProfileId{get;set;}
        public string Engagement { get; set; }
        public string RetweetCount { get; set; }
        public string ReplyCount { get; set; }
        public DateTime EntryDate { get; set; }
        public string Influence { get; set; }
    }
}
