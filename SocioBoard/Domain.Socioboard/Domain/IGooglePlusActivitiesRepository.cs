using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Domain.Socioboard.Domain
{
   public interface IGooglePlusActivitiesRepository
    {

       void addgoogleplusActivity(GooglePlusActivities gamessage);
       int deletegoogleplusActivity(GooglePlusActivities gamessage);
       int updategoogleplusActivity(GooglePlusActivities gamessage);
       List<GooglePlusActivities> getAllgoogleplusActivityOfUser(Guid UserId, string Profileid);
       bool checkgoogleplusActivityExists(string Id, Guid Userid);
        void deleteAllActivitysOfUser(string gpuserid, Guid userid);
    }
}
