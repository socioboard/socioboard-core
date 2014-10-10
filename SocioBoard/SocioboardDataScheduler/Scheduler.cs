using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDataScheduler
{
 public  interface IScheduler
    {
        string PostScheduleMessage(string scheduledmsgguid,string userid,string profileid);
        //void PostScheduleMessageWithImage(string scheduledmsgguid, string userid, string profileid);
    }
}
