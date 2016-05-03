using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Socioboard.Domain;

namespace Domain.Socioboard.Helper
{
    public class UserDetails
    {
        public List<User> lstUser { get; set; }
        public int resultCount { get; set; }
        public int totalCount { get; set; }

    }
}
