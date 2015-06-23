using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class GooglePlusAccount : ISocialSiteAccount
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string GpUserId { get; set; }
        public string GpUserName { get; set; }
        public string GpProfileImage { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int IsActive { get; set; }
        public string EmailId { get; set; }
        public int InYourCircles { get; set; }
        public int HaveYouInCircles { get; set; }
        public DateTime EntryDate { get; set; }

        public virtual string ProfileType
        {
            get
            {
                return "gplus";
            }
            set
            {
                value = "gplus";
            }
        }
    }
}