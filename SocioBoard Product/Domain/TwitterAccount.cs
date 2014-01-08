using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class TwitterAccount
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
    }
}