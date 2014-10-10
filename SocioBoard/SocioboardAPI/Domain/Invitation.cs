using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string InvitationBody { get; set; }
        public string Subject { get; set; }
        public string SenderName { get; set; }
        public string FriendName { get; set; }
        public string SenderEmail { get; set; }
        public string FriendEmail { get; set; }
        public DateTime LastEmailSendDate { get; set; }
        public string Status { get; set; }
    }
}