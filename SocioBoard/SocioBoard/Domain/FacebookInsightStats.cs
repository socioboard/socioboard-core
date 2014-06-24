using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class FacebookInsightStats
    {
        public Guid Id { get; set; }
        public string FbUserId { get; set; }
        public Guid UserId { get; set; }
        public string AgeDiff { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public int PeopleCount { get; set; }
        public int StoriesCount { get; set; }
        public string CountDate { get; set; }
        public int PageImpressionCount { get; set; }
        public DateTime EntryDate { get; set; }
    }
}