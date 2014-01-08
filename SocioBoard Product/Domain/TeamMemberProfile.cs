using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class TeamMemberProfile
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public int Status { get; set; }
        public string ProfileType { get; set; }
        public DateTime StatusUpdateDate { get; set; }
        public string ProfileId { get; set; } 
    }
}