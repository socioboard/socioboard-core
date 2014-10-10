using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class LinkedInMessage
    {
        public Guid Id { get; set; }

        public string CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromUrl { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
    }

}