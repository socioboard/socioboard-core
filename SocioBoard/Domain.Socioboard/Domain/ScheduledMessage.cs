using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class ScheduledMessage
    {
        public Guid Id { get; set; }
        public string ShareMessage { get; set; }
        public DateTime ClientTime { get; set; }
        public DateTime ScheduleTime { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public string ProfileType { get; set; }
        public string ProfileId { get; set; }
        public string PicUrl { get; set; }
        public DateTime CreateTime { get; set; }
      
    }
}