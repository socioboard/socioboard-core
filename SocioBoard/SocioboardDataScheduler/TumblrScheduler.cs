using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioboardDataScheduler
{
    public class TumblrScheduler:IScheduler
    {
        public string PostScheduleMessage(string scheduledmsgguid, string userid, string profileid)
        {
            string str = string.Empty;
            try
            {
                Api.Tumblr.Tumblr ApiObjTumblr = new Api.Tumblr.Tumblr();
                str=ApiObjTumblr.SheduleTumblrMessage(profileid, userid, scheduledmsgguid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return str;
        }

        //public override void PostScheduleMessageWithImage(dynamic data)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
