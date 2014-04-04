using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Domain;
using SocioBoard.Model;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;

namespace SocioBoardScheduler
{
   public class LinkedInScheduler:Scheduler
    {
        public override void PostScheduleMessage(dynamic data)
        {
            try
            {
                LinkedInAccountRepository linkedinrepo = new LinkedInAccountRepository();

                LinkedInAccount linkedinaccount = linkedinrepo.getLinkedinAccountDetailsById(data.ProfileId);

                Console.WriteLine("=========================================================================");

             //   IEnumerable<LinkedInAccount> lstlinkedinaccount = linkedinrepo.getLinkedinAccountDetailsById(data.ProfileId);

               //foreach (LinkedInAccount item in lstlinkedinaccount)
               //{
               //    linkedinaccount = item;
               //    break;

               //}        

                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                Linkedin_oauth.ConsumerKey = System.Configuration.ConfigurationSettings.AppSettings["LiApiKey"].ToString();
                Linkedin_oauth.ConsumerSecret = System.Configuration.ConfigurationSettings.AppSettings["LiSecretKey"].ToString();
                Linkedin_oauth.FirstName = linkedinaccount.LinkedinUserName;
                Linkedin_oauth.Token = linkedinaccount.OAuthToken;
                Linkedin_oauth.TokenSecret = linkedinaccount.OAuthSecret;
                Linkedin_oauth.Verifier = linkedinaccount.OAuthVerifier;
                LinkedInUser linkeduser = new LinkedInUser();
                var response = linkeduser.SetStatusUpdate(Linkedin_oauth, data.ShareMessage);
                Console.WriteLine("Message post on linkedin for Id :" + linkedinaccount.LinkedinUserId + " and Message: " + data.ShareMessage);
                Console.WriteLine("=============================================================");
                ScheduledMessageRepository schrepo = new ScheduledMessageRepository();
                ScheduledMessage schmsg = new ScheduledMessage();
                schmsg.Id = data.Id;
                schmsg.ProfileId = data.ProfileId;
                schmsg.ProfileType = "linkedin";
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
