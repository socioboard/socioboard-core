using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class ShareathonGroupViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual Guid Userid { get; set; }
        public virtual FacebookAccount Facebookaccount { get; set; }
        public virtual Guid Facebookaccountid { get; set; }
        public virtual string FacebookPageUrl { get; set; }
        public virtual string[] FacebookGroupId { get; set; }
        public virtual int Timeintervalminutes { get; set; }
        public virtual string Lastpostid { get; set; }
        public virtual DateTime Lastsharetimestamp { get; set; }
        public virtual bool IsHidden { get; set; }
        public virtual string AccessToken { get; set; }
    }
}
