using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class FacebookAccount
    {
        public Guid Id { get; set; }
        public string FbUserId { get; set; }
        public string FbUserName { get; set; }
        public string AccessToken { get; set; }
        public int Friends { get; set; }
        public string EmailId { get; set; }
        public string Type { get; set; }
        public string ProfileUrl { get; set; }
        public bool IsActive { get; set; }
        public Guid UserId { get; set; }
       // public string ProfileUrl { get; set; }
    }
}