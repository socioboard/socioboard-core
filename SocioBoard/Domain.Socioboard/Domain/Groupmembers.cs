using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
  public class Groupmembers
    {
        public virtual Guid Id { get; set; }
        public virtual string Userid { get; set; }
        public virtual GroupUserStatus Status { get; set; }
        public virtual string Emailid { get; set; }
        public virtual string Groupid { get; set; }
        public virtual bool IsAdmin { get; set; }
        public virtual string Membercode { get; set; } 
    }
  public enum GroupUserStatus 
  {
      Pending = 0, Accepted =1, Rejected =2
  }
}
