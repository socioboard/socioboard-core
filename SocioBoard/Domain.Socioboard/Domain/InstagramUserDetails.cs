using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class InstagramUserDetails
    {
        public Guid Id { get; set; }
        public string Profile_Id { get; set; }
        public string Insta_Name { get; set; }
        public string Full_Name { get; set; }
        public string Media_Count { get; set; }
        public DateTime Created_Time { get; set; }
        public string Follower { get; set; }
        public string Following { get; set; }

    }
}
