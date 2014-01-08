using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Exception { get; set; }
        public string ModuleName { get; set; }
        public string ProfileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

    }
}