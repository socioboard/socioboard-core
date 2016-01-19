using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class ShareathonViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual Guid Userid { get; set; }
        public virtual FacebookAccount Facebookaccount { get; set; }
        public virtual List<FacebookAccount> Facebookpages { get; set; }
        public virtual Guid Facebookaccountid { get; set; }
        public virtual string[] FacebookPageId { get; set; }
        public virtual string pageid { get; set; }
        public virtual int Timeintervalminutes { get; set; }
        public virtual string Lastpostid { get; set; }
        public virtual DateTime Lastsharetimestamp { get; set; }
        public virtual bool IsHidden { get; set; }
    }
}
