using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class LoginLogs
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime LoginTime { get; set; }

    }
}