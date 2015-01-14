using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class FeedSentimentalAnalysis
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AssigneUserId { get; set; }
        public string ProfileId { get; set; }
        public string FeedId { get; set; }
        public string Positive { get; set; }
        public string Negative { get; set; }
        public DateTime EntryDate { get; set; }
        public string Network { get; set; }
        public int TicketNo { get; set; }
    }
}
