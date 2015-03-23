using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDataScheduler
{
    class LinkedinGroupScheduler:IScheduler
    {
        public string PostScheduleMessage(string scheduledmsgguid, string Userid, string profileid)
        {
            string str = string.Empty;
            try
            {
                Api.Linkedin.Linkedin ApiObjLinkedin = new Api.Linkedin.Linkedin();
                str = ApiObjLinkedin.ScheduleLinkedinGroupMessage(scheduledmsgguid,Userid,profileid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return str;
        }
    }
}
