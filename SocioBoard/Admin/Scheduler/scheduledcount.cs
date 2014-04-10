using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Admin.Scheduler
{
    public class scheduledcount
    {
        private string Id;
        private int count;
        public string _Id
        {
            get { return Id; }
            set { Id = value; }
        }
        public int _count
        {
            get { return count; }
            set { count = value; }
        }
    }
}