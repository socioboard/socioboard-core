using Domain.Socioboard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Helper
{
   public class ScheduledTracker
    {
        public int Count { get; set; }
        public int SentCount { get; set; }
        public int RemainingCount { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string PicUrl { get; set; }

        public ScheduledTracker(int count, int SentCount, int RemainingCount, string Name, Guid UserId, string PicUrl)
        {
            this.Count = count;
            this.SentCount = SentCount;
            this.RemainingCount = RemainingCount;
            this.Name = Name;
            this.UserId = UserId;
            this.PicUrl = PicUrl;
        }
        public ScheduledTracker()
        { }
    }

   public class ScheduledMessageDetails
   {

       public UInt64 Status { get; set; }
       public Guid UserId { get; set; }
       public string UserName { get; set; }
       public string ProfileUrl { get; set; }
       
   }
}
