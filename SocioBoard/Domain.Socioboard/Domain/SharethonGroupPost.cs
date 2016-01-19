using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
    public class SharethonGroupPost
    {
        public virtual Guid Id { get; set; }
        public virtual string Facebookgroupid { get; set; }
        public virtual string Facebookaccountid { get; set; }
        public virtual DateTime PostedTime { get; set; }
        public virtual string PostId { get; set; }

    }
}
