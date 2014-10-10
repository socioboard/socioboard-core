using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socioboard.Domain
{
    public class Groups
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public Guid UserId { get; set; }
        public DateTime EntryDate { get; set; }
    }
}