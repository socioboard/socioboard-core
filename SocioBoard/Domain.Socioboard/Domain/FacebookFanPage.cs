using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
   public class FacebookFanPage
    {
        public Guid Id { get; set; }
        public string ProfilePageId { get; set; }
        public string FacebookAccountId { get; set; }
        public string FanpageCount { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid UserId { get; set; }

    }
}
