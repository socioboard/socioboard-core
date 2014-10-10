using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class DiscoverySearch
    {
        public Guid Id { get; set; }
        public string FromName { get; set; }
        public string FromId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EntryDate { get; set; }
        public string Network { get; set; }
        public string ProfileImageUrl { get; set; }
        public string MessageId { get; set; }
        public string SearchKeyword { get; set; }
        public Guid UserId { get; set; }
    }
}