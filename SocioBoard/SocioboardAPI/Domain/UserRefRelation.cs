using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class UserRefRelation
    {
        public Guid Id { get; set; }
        public Guid ReferenceUserId { get; set; }
        public Guid RefereeUserId { get; set; }
        public string ReferenceUserEmail { get; set; }
        public string RefereeUserEmail { get; set; }
        public DateTime EntryDate { get; set; }
        public string Status { get; set; }
        
    }
}