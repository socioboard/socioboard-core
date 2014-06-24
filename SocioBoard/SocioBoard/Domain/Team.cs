using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class Team
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int InviteStatus { get; set; }
        public DateTime InviteDate { get; set; }
        public DateTime StatusUpdateDate { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string AccessLevel { get; set; }
    }
}