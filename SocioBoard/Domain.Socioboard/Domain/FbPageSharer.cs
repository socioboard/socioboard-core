using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class FbPageSharer
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PostId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
    }
}
