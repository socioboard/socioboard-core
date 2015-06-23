using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardfbfeeds
    {
        public virtual Guid Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Link { get; set; }
        public virtual string Message { get; set; }
        public virtual DateTime? Createddate { get; set; }
        public virtual string Description { get; set; }
        public virtual string Feedid { get; set; }
        public virtual bool Isvisible { get; set; }
        public virtual string Videolink { get; set; }
        public virtual string Story { get; set; }
        public virtual Guid Fbpageprofileid { get; set; }
        public virtual string Coverphoto { get; set; }
        public virtual string Image { get; set; }
        public virtual string Likes { get; set; }
        public virtual string FromName { get; set; }
        public virtual string FromId { get; set; }
    }
}
