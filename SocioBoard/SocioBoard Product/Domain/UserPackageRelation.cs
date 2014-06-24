using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class UserPackageRelation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PackageId { get; set; }
        public bool PackageStatus { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}