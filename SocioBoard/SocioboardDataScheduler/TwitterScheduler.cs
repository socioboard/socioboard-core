using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Socioboard.Domain;
using System.Configuration;


namespace SocioboardDataScheduler
{
  public  class TwitterScheduler:IScheduler
    {


      public string PostScheduleMessage(string scheduledmsgguid, string userid, string profileid)
      {
          string str = string.Empty;
          try
          {
              Api.Twitter.Twitter ApiObjTwitter = new Api.Twitter.Twitter();
              str=ApiObjTwitter.SheduleTwitterMessage(profileid, userid, scheduledmsgguid);

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
