using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class Invitation
    {
        public Guid Id { get; set; }
        public string SenderEmail { get; set; }
        public Guid SenderUserId { get; set; }
        public string FriendEmail { get; set; }
        public Guid FriendUserId { get; set; }
        public string InvitationCode { get; set; }
        public int Status { get; set; }
        public DateTime SendInvitationDate { get; set; }
        public DateTime AcceptInvitationDate { get; set; }
    }
}