using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
  public class FacebookGroupData
    {
        public string accesstoken { get; set; }
        public string PostId { get; set; }
        public string PostUserId { get; set; }
        public string ProfileId { get; set; }
        public string GroupId { get; set; }
        public string ProfilePic { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Picture { get; set; }
        public string Link { get; set; }
        public string Userlikes { get; set; }
        public string Likecount { get; set; }     
        public DateTime CreatedTime { get; set; }
       

    }
}
