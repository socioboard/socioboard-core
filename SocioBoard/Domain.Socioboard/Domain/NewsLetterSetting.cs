using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
    public class NewsLetterSetting
    {
        public Guid Id { get; set; }
        public string userId { get; set; }
        public int groupReport_Daily { get; set; }
        public int groupReport_7 { get; set; }
        public int groupReport_15 { get; set; }
        public int groupReport_30 { get; set; }
        public int groupReport_60 { get; set; }
        public int groupReport_90 { get; set; }
        public int others { get; set; }
    }
}
