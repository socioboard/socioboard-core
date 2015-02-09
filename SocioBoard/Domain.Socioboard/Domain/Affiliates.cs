using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class Affiliates
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid FriendUserId { get; set; }
        public DateTime AffiliateDate { get; set; }
        public string Amount { get; set; }
    }
}
