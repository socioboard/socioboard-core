using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class TeamMemberProfile
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public int Status { get; set; }
        public string ProfileType { get; set; }
        public DateTime StatusUpdateDate { get; set; }
        public string ProfileId { get; set; }
        public List<TeamMemberProfile> lstTeamMemberProfile { get; set; }
        public string ProfilePicUrl { get; set; }
        public string ProfileName { get; set; }
    }

  
	
	
}