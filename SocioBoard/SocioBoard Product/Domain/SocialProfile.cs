using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class SocialProfile
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ProfileId { get; set; }
        public string ProfileType { get; set; }
        public DateTime ProfileDate { get; set; }
        public int ProfileStatus { get; set; }
        


    }
}