using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Domain;
using SocioBoard.Model;
using GlobusLinkedinLib.Authentication;
using GlobusLinkedinLib.App.Core;

namespace blackSheepScheduler
{
    class LinkedInScheduler:Scheduler
    {
        public override void PostScheduleMessage(dynamic data)
        {
            try
            {
                LinkedInAccountRepository linkedinrepo = new LinkedInAccountRepository();
                LinkedInAccount linkedinaccount = linkedinrepo.getUserInformation(data.UserId, data.ProfileId);
                oAuthLinkedIn Linkedin_oauth = new oAuthLinkedIn();
                Linkedin_oauth.ConsumerKey = System.Configuration.ConfigurationSettings.AppSettings["LiApiKey"];
                Linkedin_oauth.ConsumerSecret = System.Configuration.ConfigurationSettings.AppSettings["LiSecretKey"];
                Linkedin_oauth.FirstName = linkedinaccount.LinkedinUserName;
                Linkedin_oauth.Token = linkedinaccount.OAuthToken;
                Linkedin_oauth.TokenSecret = linkedinaccount.OAuthSecret;
                Linkedin_oauth.Verifier = linkedinaccount.OAuthVerifier;
                LinkedInUser linkeduser = new LinkedInUser();
                var response = linkeduser.SetStatusUpdate(Linkedin_oauth, data.ShareMessage);
                Console.WriteLine("Message post on linkedin for Id :" + linkedinaccount.LinkedinUserId + " and Message: " + data.ShareMessage);
                ScheduledMessageRepository schrepo = new ScheduledMessageRepository();
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
