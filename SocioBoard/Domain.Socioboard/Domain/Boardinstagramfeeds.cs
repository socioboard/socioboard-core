using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardinstagramfeeds
    {
        public virtual Guid Id { get; set; }
        public virtual string Link { get; set; }
        public virtual string Imageurl { get; set; }
        public virtual string Tags { get; set; }
        public virtual DateTime? Createdtime { get; set; }
        public virtual Guid Instagramaccountid { get; set; }
        public virtual string Feedid { get; set; }
        public virtual bool Isvisible { get; set; }
        public virtual string FromName { get; set; }
        public virtual string FromId { get; set; }
        public virtual string FromPicUrl { get; set; }
    }
}
