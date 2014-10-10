using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class TwitterAccount:ISocialSiteAccount
    {
        public Guid Id { get; set; }
        public string TwitterUserId { get; set; }
        public string TwitterScreenName { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthSecret { get; set; }
        public Guid UserId { get; set; }
        public int FollowersCount { get; set; }
        public bool IsActive { get; set; }
        public int FollowingCount { get; set; }
        public string ProfileUrl { get; set; }
        public string ProfileImageUrl { get; set; }
        public string TwitterName { get; set; }

        public string ProfileType
        {
            get
            {
                return "twitter";
            }
            set
            {
                value = "twitter";
            }
        }

        public ISocialSiteAccount BindJArray(Newtonsoft.Json.Linq.JObject jObject)
        {
            TwitterAccount objTwitterAccount = new TwitterAccount();
            //Api.FacebookAccount.FacebookAccount ApiobjFacebookAccount = new Api.FacebookAccount.FacebookAccount();
            //JObject FacebookAccountDetails = JObject.Parse(ApiobjFacebookAccount.getFacebookAccountDetailsById(objUser.Id.ToString(), objTeamMemberProfile.ProfileId.ToString()));
            objTwitterAccount.Id = Guid.Parse(jObject["Id"].ToString());
            objTwitterAccount.TwitterUserId = jObject["TwitterUserId"].ToString();
            objTwitterAccount.TwitterScreenName = jObject["TwitterScreenName"].ToString();
            objTwitterAccount.OAuthToken = jObject["OAuthToken"].ToString();
            objTwitterAccount.OAuthSecret = Convert.ToString(jObject["OAuthSecret"]);
            objTwitterAccount.UserId = Guid.Parse(jObject["UserId"].ToString());
            objTwitterAccount.IsActive = Convert.ToBoolean(jObject["IsActive"].ToString());
            objTwitterAccount.FollowersCount = Convert.ToInt16(Convert.ToString(jObject["FollowersCount"]));
            objTwitterAccount.FollowingCount = Convert.ToInt16(Convert.ToString(jObject["FollowingCount"]));
            objTwitterAccount.ProfileUrl = jObject["ProfileUrl"].ToString();
            objTwitterAccount.ProfileImageUrl = jObject["ProfileImageUrl"].ToString();
            objTwitterAccount.TwitterName = jObject["TwitterName"].ToString();
            return objTwitterAccount;
        }
    }
}