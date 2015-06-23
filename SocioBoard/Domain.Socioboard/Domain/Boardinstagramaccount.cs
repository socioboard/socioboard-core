using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardinstagramaccount
    {
        public virtual Guid Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Followedbycount { get; set; }
        public virtual string Followscount { get; set; }
        public virtual Guid Boardid { get; set; }
        public virtual string Profilepicurl { get; set; }
        public virtual string Profileid { get; set; }
        public virtual string Url { get; set; }
        public virtual string Bio { get; set; }
        public virtual string Media { get; set; }
        public virtual DateTime? Entrydate { get; set; }
    }
}
