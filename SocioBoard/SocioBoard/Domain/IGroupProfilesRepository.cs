using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public interface IGroupProfilesRepository
    {
        void AddGroupProfile(GroupProfile group);
        int DeleteGroupProfile(Guid userid,string profileid,Guid groupId);
        void UpdateGroupProfile(GroupProfile group);
        List<GroupProfile> getAllGroupProfiles(Guid Userid, Guid groupid);
        bool checkGroupProfileExists(Guid userid, Guid groupid, string profileid);
     //   List<GroupProfile> getGroupDetails(Guid userid, Guid groupid);

    }
}