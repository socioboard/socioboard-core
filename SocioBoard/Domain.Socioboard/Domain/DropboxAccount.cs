using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class DropboxAccount
    {
        public virtual Guid Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string DropboxUserId { get; set; }
        public virtual string DropboxUserName { get; set; }
        public virtual string DropboxEmail { get; set; }
        public virtual string OauthToken { get; set; }
        public virtual string OauthTokenSecret { get; set; }
        public virtual string AccessToken { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}