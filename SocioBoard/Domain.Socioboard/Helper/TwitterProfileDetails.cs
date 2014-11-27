using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Helper
{
    public class TwitterProfileDetails
    {
        public string screen_name { get; set; }
        public string name { get; set; }
        public string profile_image_url { get; set; }
        public string profile_banner_url { get; set; }
        public string Url { get; set; }
        public string friends_count { get; set; }
        public string followers_count { get; set; }
        public string Status_Text { get; set; }
    }
}
