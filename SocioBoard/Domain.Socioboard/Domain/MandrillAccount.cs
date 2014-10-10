using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class MandrillAccount
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public DateTime EntryDate { get; set; }
      
    }
}