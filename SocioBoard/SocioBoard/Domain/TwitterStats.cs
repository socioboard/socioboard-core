using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class TwitterStats
    {
        public Guid Id { get; set; }
        public string TwitterId { get; set; }
        public Guid UserId { get; set; }
        public int FollowingCount { get; set; }
        public int FollowerCount { get; set; }
        public int DMRecievedCount { get; set; }
        public int DMSentCount { get; set; }
        public int Engagement { get; set; }
        public int Influence { get; set; }
        public int Age1820 { get; set; }
        public int Age2124 { get; set; }
        public int Age2534 { get;set; }
        public int Age3544 {get;set;}
        public int Age4554 { get; set; }
        public int Age5564 { get; set; }
        public int Age65 { get; set; }
        public DateTime EntryDate { get; set; }
    }
}