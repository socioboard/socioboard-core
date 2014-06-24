using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Domain;
using SocioBoard.Model;
using Facebook;

namespace WooSuiteScheduler
{
    class FacebookScheduler:Scheduler
    {
       

        public override void PostScheduleMessage(dynamic data)
        {
            try
            {
               
                FacebookAccountRepository fbaccrepo = new FacebookAccountRepository();
                FacebookAccount fbaccount = fbaccrepo.getUserDetails(data.ProfileId);
                FacebookClient fbclient = new FacebookClient(fbaccount.AccessToken);
                var args = new Dictionary<string, object>();
                args["message"] = data.ShareMessage;

                var facebookpost = fbclient.Post("/me/feed", args);
                Console.WriteLine("Message post on facebook for Id :" + fbaccount.FbUserId + " and Message: " + data.ShareMessage);
                ScheduledMessageRepository schmesgrepo = new ScheduledMessageRepository();
                schmesgrepo.updateMessage(data.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
 
        }

        public override void PostScheduleMessageWithImage(dynamic data)
        {
            throw new NotImplementedException();
        }
    }
}
