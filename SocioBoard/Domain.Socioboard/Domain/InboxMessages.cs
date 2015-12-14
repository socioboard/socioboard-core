using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class InboxMessages
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MessageId { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string RecipientId { get; set; }
        public string RecipientName { get; set; }
        public string Message { get; set; }
        public string FromImageUrl { get; set; }
        public string RecipientImageUrl { get; set; }
        public string ProfileType { get; set; }
        public string MessageType { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EntryTime { get; set; }
        public string FollowerCount { get; set; }
        public string FollowingCount { get; set; }
        public int Status { get; set; }
        public double Positive { get; set; }
        public double Negative { get; set; }
    }
}
