using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
   public class dailymotionpost
    {
       public Guid Id { get; set; }
       public string VideoId {get;set;}
       public string Title {get;set;}
       public string Url {get;set;}
       public string Description {get;set;}
       public string VideoUrl { get; set; }
       public double  Created_Time { get; set; }
       public double Entry_Time { get; set; }
    }
}
