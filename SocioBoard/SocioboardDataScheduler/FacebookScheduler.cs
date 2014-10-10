using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Domain.Socioboard.Domain;
using Facebook;


namespace SocioboardDataScheduler
{
    class FacebookScheduler : IScheduler
    {


        public string PostScheduleMessage(string scheduledmsgguid,string Userid, string profileid)
        {
            string str = string.Empty;
                try
                {
                    Api.Facebook.Facebook ApiobjFacebook = new Api.Facebook.Facebook();
                    str= ApiobjFacebook.SheduleFacebookMessage(profileid,Userid,scheduledmsgguid);

                }
                catch (FacebookApiLimitException ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
                return str;
            }
        }

        //public void PostScheduleMessageWithImage(string scheduledmsgguid, string userid, string profileid)
        //{
        //    throw new NotImplementedException();
        //}
    
}
