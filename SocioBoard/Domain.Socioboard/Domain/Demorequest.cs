using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
    public class Demorequest
    {
        public virtual Guid Id { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Designation { get; set; }
        public virtual string Email { get; set; }
        public virtual string Company { get; set; }
        public virtual string Location { get; set; }
        public virtual string Skype { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Message { get; set; }
        public virtual string CompanyWebsite { get; set; }
        public virtual DemoPlanType DemoPlanType { get; set; }
    }

    public enum DemoPlanType 
    {
        Demo = 0,
        Enterprise =1,
        Agency =2


    }
}
