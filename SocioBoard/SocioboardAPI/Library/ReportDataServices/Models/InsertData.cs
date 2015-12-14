using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportDataServices.Models
{
    class InsertData
    {
        public Guid GroupId { get; set; }
        public long inbox_15 { get; set; }
        public long inbox_30 { get; set; }
        public long inbox_60 { get; set; }
        public long inbox_90 { get; set; }
        public long sent_15 { get; set; }
        public long sent_30 { get; set; }
        public long sent_60 { get; set; }
        public long sent_90 { get; set; }
        public long twitterfollower_15 { get; set; }
        public long twitterfollower_30 { get; set; }
        public long twitterfollower_60 { get; set; }
        public long twitterfollower_90 { get; set; }
        public long fbfan_15 { get; set; }
        public long fbfan_30 { get; set; }
        public long fbfan_60 { get; set; }
        public long fbfan_90 { get; set; }



    }
}
