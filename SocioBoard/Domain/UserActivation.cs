using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class UserActivation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ActivationStatus { get; set; }
    }
}