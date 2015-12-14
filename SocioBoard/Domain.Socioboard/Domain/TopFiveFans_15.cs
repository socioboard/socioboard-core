using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class TopFiveFans_15 
    {
            public Guid Id { get; set; }
            public int Count { get; set; }
            public string FromId { get; set; }
            public string ProfileId { get; set; }

            public int Rank { get; set; }
            public string FromName { get; set; }
            public int Retweetcount { get; set; }
            public int Mentioncount { get; set; }
            public string FromImageUrl { get; set; }
         
            public TopFiveFans_15()
            { 
            }

            public TopFiveFans_15(int count, string from_id, string prof_id , string fromname)
            {

                this.Id = Guid.NewGuid();
                this.Count = count;
                this.FromId = from_id;
                this.ProfileId = prof_id;
                this.FromName = fromname;
            }
    }
}
