using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class InstagramAccount
    {
        public Guid Id { get; set; }
        public string InstagramId { get; set; }
        public string AccessToken { get; set; }
        public string InsUserName { get; set; }
        public string ProfileUrl { get; set; }
        public bool IsActive { get; set; }
        public int Followers { get; set; }
        public int FollowedBy { get; set; }
        public int TotalImages { get; set; }
        public Guid UserId { get; set; }
        
    }
}