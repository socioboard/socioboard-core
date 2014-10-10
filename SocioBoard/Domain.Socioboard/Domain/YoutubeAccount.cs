using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class YoutubeAccount : ISocialSiteAccount
    {
        public virtual Guid Id { get; set; }
        public virtual string Ytuserid { get; set; }
        public virtual string Ytusername { get; set; }
        public virtual string Ytprofileimage { get; set; }
        public virtual string Accesstoken { get; set; }
        public virtual string Refreshtoken { get; set; }
        public virtual int? Isactive { get; set; }
        public virtual string Emailid { get; set; }
        public virtual DateTime? Entrydate { get; set; }
        public virtual Guid UserId { get; set; }

        public virtual string ProfileType
        {
            get
            {
                return "youtube";
            }
            set
            {
                value = "youtube";
            }
        }
    }
}