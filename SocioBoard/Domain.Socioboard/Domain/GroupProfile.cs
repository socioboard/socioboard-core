using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class GroupProfile
    {
        public Guid Id { get; set; }
        public Guid GroupOwnerId{get;set;}
        public string ProfileType { get; set; }
        public string ProfileId { get; set; }
        public Guid GroupId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}