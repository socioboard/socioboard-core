using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class FacebookStats
    {
        public Guid Id { get; set; }
        public string FbUserId { get; set; }
        public Guid UserId { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
        public int ReachCount { get; set; }
        public int PeopleTalkingCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int ShareCount { get; set; }
        public int FanCount { get; set; }
        public DateTime EntryDate{ get; set; }
       
    }
}