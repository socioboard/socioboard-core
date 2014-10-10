using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioboardDataScheduler
{
    public class YoutubeSchduler:IScheduler  
    {
        public string PostScheduleMessage(string scheduledmsgguid, string userid, string profileid)
        {
            string str = string.Empty;
            try
            {
                Api.Youtube.Youtube ApiObjYoutube = new Api.Youtube.Youtube();
   
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
