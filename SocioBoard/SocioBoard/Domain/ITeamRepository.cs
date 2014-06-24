using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public interface ITeamRepository
    {
        void addNewTeam(Team team);
        int deleteTeam(Team team);
        void updateTeam(Team team);
        List<Team> getAllTeamsOfUser(Guid Userid);
        Team getTeam(string Emailid, Guid UserId);
        Team getMemberById(Guid memberId, Guid UserId);
    }
}