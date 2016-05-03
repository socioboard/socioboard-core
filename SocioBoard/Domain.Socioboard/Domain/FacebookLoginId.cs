using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
    public class FacebookLoginId
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
