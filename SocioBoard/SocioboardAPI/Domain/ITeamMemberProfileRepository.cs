using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public interface ITeamMemberProfileRepository
    {
        void addNewTeamMember(TeamMemberProfile team);
        int deleteTeamMember(TeamMemberProfile team);
        void updateTeamMember(TeamMemberProfile team);
        List<TeamMemberProfile> getAllTeamMemberProfilesOfTeam(Guid TeamId);
        TeamMemberProfile getTeamMemberProfile(Guid Teamid, string ProfileId);

    }
}