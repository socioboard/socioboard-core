using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioboardDataScheduler
{
   public class InstagramScheduler:IScheduler
    {
       public string PostScheduleMessage(string scheduledmsgguid, string userid, string profileid)
        {
            string str = string.Empty; 
           try
            {
                Api.Instagram.Instagram ApiObjInstagram = new Api.Instagram.Instagram();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

           return str;
        }

        //public void PostScheduleMessageWithImage(dynamic data)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
