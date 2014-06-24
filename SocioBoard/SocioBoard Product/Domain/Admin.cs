using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class Admin
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string TimeZone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
