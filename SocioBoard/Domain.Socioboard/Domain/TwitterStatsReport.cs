using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
  public class TwitterStatsReport  
   {
        public Guid Id { get; set; }
        public string TwitterId { get; set; }
        public string TwtUserName { get; set; }
        public string TwtProfImgUrl { get; set; }
        public Guid UserId { get; set; }
        public int FollowingCount { get; set; }
        public int FollowerCount { get; set; }
        public int DMRecievedCount { get; set; }
        public int DMSentCount { get; set; }
        public string Engagement { get; set; }
        public string Influence { get; set; }      
        public string EntryDate { get; set; }
        public int TwtMention { get; set; }
        public int TwtRetweet { get; set; }
        public int days { get; set; }
        public int Age1820 { get; set; }
        public int Age2124 { get; set; }
        public int Age2534 { get; set; }
        public int Age3544 { get; set; }
        public int Age4554 { get; set; }
        public int Age5564 { get; set; }
        public int Age65 { get; set; }
 
    }
}
