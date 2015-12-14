using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
    public class GoogleAnalyticsReport
    {
       public Guid Id { get; set; }
       public string GaProfileId { get; set; }
       public string Visits { get; set; }
       public string Views { get; set; }
       public string TwitterMention { get; set; }
       public string Article_Blogs { get; set; }
    }
}
