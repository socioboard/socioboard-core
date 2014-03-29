using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocioBoard.Helper;
using GlobusLinkedinLib.Authentication;
using SocioBoard.Model;
using System.Collections;
using SocioBoard.Domain;

namespace SocialSiteDataService
{
    class LinkedInData
    {
        public void GetLinkedIndata(object UserId)
        {
            Guid userId = (Guid)UserId;

            LinkedInHelper objliHelper = new LinkedInHelper();
            LinkedInAccountRepository objLiRepo = new LinkedInAccountRepository();
            oAuthLinkedIn _oauth = new oAuthLinkedIn();
            ArrayList arrLiAccount = objLiRepo.getAllLinkedinAccountsOfUser(userId);
            foreach (LinkedInAccount itemLi in arrLiAccount)
            {
                _oauth.Token = itemLi.OAuthToken;
                _oauth.TokenSecret = itemLi.OAuthSecret;
                _oauth.Verifier = itemLi.OAuthVerifier;
                objliHelper.GetLinkedInFeeds(_oauth, itemLi.LinkedinUserId, userId);
            }

        }
    }
}
