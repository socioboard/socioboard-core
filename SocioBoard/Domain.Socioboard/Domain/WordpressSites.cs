using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class WordpressSites
    {
        public Guid Id { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string Description { get; set; }
        public string SiteURL { get; set; }
        public string Post_Count { get; set; }
        public string Subscribers_Count { get; set; }
        public Guid UserId { get; set; }
        public string WPUserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
