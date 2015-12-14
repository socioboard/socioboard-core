using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{
    
    public class Userfacebook {
        public virtual Guid Id { get; set; }
        public virtual Guid Userid { get; set; }
        public virtual Guid Facebookaccountid { get; set; }
    }
}
