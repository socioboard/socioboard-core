using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class TumblrAccount:ISocialSiteAccount
    {

        public Guid Id { get; set; }
        public string tblrUserName { get; set; }
        public Guid UserId { get; set; }
        public string tblrAccessToken { get; set; }
        public string tblrAccessTokenSecret { get; set; }
        public string tblrProfilePicUrl { get; set; }
        public int IsActive { get; set; }


        public string ProfileType
        {
            get
            {
                return "tumblr";
            }
            set
            {
                value = "tumblr";
            }
        }

    }
}