using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
   public class DiscoveryLeads
    {
       public virtual Guid Id { get; set; }
       public virtual Guid UserId { get; set; }
       public virtual string Keyword {get;set;}
    }
}
