using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public interface IGroupRepository
    {
         void AddGroup(Groups group);
         int DeleteGroup(Groups group);
         void UpdateGroup(Groups group);
         List<Groups> getAllGroups(Guid Userid);
         bool checkGroupExists(Guid userid,string groupname);

         Groups getGroupDetails(Guid userid, string groupname);
    }
}