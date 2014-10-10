using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class FacebookAccount:ISocialSiteAccount
    {
        public Guid Id { get; set; }
        public string FbUserId { get; set; }
        public string FbUserName { get; set; }
        public string AccessToken { get; set; }
        public int Friends { get; set; }
        public string EmailId { get; set; }
        public string Type { get; set; }
        public string ProfileUrl { get; set; }
        public int IsActive { get; set; }
        public Guid UserId { get; set; }


        public string ProfileType
        {
            get
            {
                return "facebook";
            }
            set
            {
                value = "facebook";
            }
        }


        //public ISocialSiteAccount BindJArray(JObject FacebookAccountDetails)
        //{
        //    FacebookAccount objFacebookAccount = new FacebookAccount();
        //    //Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
        //    //JObject FacebookAccountDetails = JObject.Parse(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()));
        //    objFacebookAccount.Id = Guid.Parse(FacebookAccountDetails["Id"].ToString());
        //    objFacebookAccount.FbUserId = FacebookAccountDetails["FbUserId"].ToString();
        //    objFacebookAccount.FbUserName = FacebookAccountDetails["FbUserName"].ToString();
        //    objFacebookAccount.AccessToken = FacebookAccountDetails["AccessToken"].ToString();
        //    objFacebookAccount.Friends = Convert.ToInt16(Convert.ToString(FacebookAccountDetails["Friends"]));
        //    objFacebookAccount.EmailId = FacebookAccountDetails["EmailId"].ToString();
        //    objFacebookAccount.Type = FacebookAccountDetails["Type"].ToString();
        //    objFacebookAccount.IsActive = Convert.ToInt16(Convert.ToString(FacebookAccountDetails["IsActive"]));
        //    objFacebookAccount.UserId = Guid.Parse(FacebookAccountDetails["UserId"].ToString());
        //    objFacebookAccount.ProfileUrl = FacebookAccountDetails["ProfileUrl"].ToString();

        //    return objFacebookAccount;
        //}
    }
}