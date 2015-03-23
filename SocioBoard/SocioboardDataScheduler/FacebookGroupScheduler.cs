using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocioboardDataScheduler
{
    class FacebookGroupScheduler:IScheduler
    {

        public string PostScheduleMessage(string scheduledmsgguid, string Userid, string profileid)
        {
            string str = string.Empty;
            try
            {
                Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                str = ApiobjFacebook.SheduleFacebookGroupMessage(profileid, Userid, scheduledmsgguid);
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            return str;
        }


    }
}
