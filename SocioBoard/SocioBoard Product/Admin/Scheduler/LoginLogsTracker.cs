using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Admin.Scheduler
{
    public class LoginLogsTracker
    {
        private int count;
        private Guid UserId;
        private string UserName;

        public int _count
        {
            get { return count; }
            set { count = value; }
        }
        public Guid _UserId
        {
            get { return UserId; }
            set { UserId = value; }
        }
        public string _UserName
        {
            get { return UserName; }
            set { UserName = value; }
        }


    }
}