using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
    public class LinkedInManager
    {
       public virtual  string GroupId {get;set;}
       public virtual string Code { get; set; }
       public virtual string UserId { get; set; }
        public virtual string comment{get;set;}
        public virtual string title{get;set;}
        public virtual string ImageUrl{get;set;}
        public virtual string ProfileId{get;set;}
        public virtual string Oauth { get; set; }
        public virtual string Updatekey { get; set; }
        public virtual DateTime CurrentTime { get; set; }
        public virtual string ScheduleMessageId { get; set; }
        public virtual DateTime ScheduleTime { get; set; }
           
    }
}
