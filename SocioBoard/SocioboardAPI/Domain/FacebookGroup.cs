using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class FacebookGroup
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ProfileId { get; set; }
        public string GroupId { get; set; }
        public string Icon { get; set; }
        public string Cover { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Privacy { get; set; }
        public DateTime UpdatedTime { get; set; }
        public DateTime EntryDate { get; set; }
    }
}