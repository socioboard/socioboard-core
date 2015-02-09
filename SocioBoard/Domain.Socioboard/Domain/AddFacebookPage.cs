using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class AddFacebookPage
    {      
        public string ProfilePageId { get; set; }
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LikeCount { get; set; }
    }
   public class AddFacebookGroup
    {
       public string ProfileGroupId { get; set; }
       public string AccessToken { get; set; }
       public string Name { get; set; }
       public string Email { get; set; }  
   }
}
