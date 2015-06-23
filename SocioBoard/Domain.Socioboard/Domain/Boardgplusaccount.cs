using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardgplusaccount
    {
        public virtual Guid Id { get; set; }
        public virtual string Pageid { get; set; }
        public virtual string Circledbycount { get; set; }
        public virtual string Plusonecount { get; set; }
        public virtual string Displayname { get; set; }
        public virtual Guid Boardid { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string Aboutme { get; set; }
        public virtual string Pageurl { get; set; }
        public virtual string Profileimageurl { get; set; }
        public virtual string Tagline { get; set; }
        public virtual string Coverphotourl { get; set; }
        public virtual DateTime? Entrydate { get; set; }
    }
}
