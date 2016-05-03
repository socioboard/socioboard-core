using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
   public class PluginInfo
    {
        public Guid id { get; set; }
        public string url { get; set; }
        public string imageurl { get; set; }
        public string description { get; set; }
        public string title { get; set; }
    }
}
