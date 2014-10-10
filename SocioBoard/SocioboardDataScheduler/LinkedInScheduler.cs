using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Socioboard.Domain;

namespace SocioboardDataScheduler
{
   public class LinkedInScheduler:IScheduler
    {
       public string PostScheduleMessage(string scheduledmsgguid, string userid, string profileid)
        {
            string str = string.Empty;  
           try
            {
                Api.Linkedin.Linkedin ApiObjLinkedin = new Api.Linkedin.Linkedin();
               str= ApiObjLinkedin.SheduleLinkedInMessage(profileid, userid, scheduledmsgguid);
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
