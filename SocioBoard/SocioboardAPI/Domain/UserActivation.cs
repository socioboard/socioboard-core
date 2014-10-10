using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class UserActivation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ActivationStatus { get; set; }
    }
}