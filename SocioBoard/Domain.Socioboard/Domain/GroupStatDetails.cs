using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
   public class GroupStatDetails
    {
        public  int IncommingMessage  { get; set; }
        public int SentMessage { get; set; }
        public int TwitterFollower { get; set; }
        public int FacebookFan { get; set; }
        public int TwitterStats { get; set; }
        public int Mention { get; set; }
        public int Retweet { get; set; }
        public int PageImpession { get; set; }
        public int  days { get; set; }
        public string RetweetGraph { get; set; }
        public string MentionGraph { get; set; }
        public string UserTweetGraph { get; set; }
        public int PlainText { get; set; }
        public int PhotoLink { get; set; }

    }
}
