using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Domain;
using SocioBoard.Model;
using Facebook;

namespace SocioBoardScheduler
{
    class FacebookScheduler:Scheduler
    {
       

        public override void PostScheduleMessage(dynamic data)
        {
            try
            {
                FacebookAccountRepository fbaccrepo = new FacebookAccountRepository();
                //IEnumerable<FacebookAccount> lstfbaccount = fbaccrepo.getUserDetails(data.ProfileId);
                FacebookAccount fbaccount = fbaccrepo.getUserDetails(data.ProfileId);
                //FacebookAccount fbaccount = null;
                //foreach (FacebookAccount item in lstfbaccount)
                //{
                //    fbaccount = item;
                //    break;
                //}

                FacebookClient fbclient = new FacebookClient(fbaccount.AccessToken);
                var args = new Dictionary<string, object>();
                args["message"] = data.ShareMessage;

                //var facebookpost = fbclient.Post("/me/feed", args);
                var facebookpost = "";
                if (fbaccount.Type == "page")
                {
                    facebookpost = fbclient.Post("/" + fbaccount.FbUserId + "/feed", args).ToString();
                }
                else
                {
                    facebookpost = fbclient.Post("/me/feed", args).ToString();
                }


                Console.WriteLine("Message post on facebook for Id :" + fbaccount.FbUserId + " and Message: " + data.ShareMessage);
              
                ScheduledMessageRepository schrepo = new ScheduledMessageRepository();
                ScheduledMessage schmsg = new ScheduledMessage();
                schmsg.Id = data.Id;
                schmsg.ProfileId = data.ProfileId;
                schmsg.ProfileType = "";
                schmsg.Status = true;
                schmsg.UserId = data.UserId;
                schmsg.ShareMessage = data.ShareMessage;
                schmsg.ScheduleTime = data.ScheduleTime;
                schmsg.ClientTime = data.ClientTime;
                schmsg.CreateTime = data.CreateTime;
                schmsg.PicUrl = data.PicUrl;

                schrepo.updateMessage(data.Id);
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
