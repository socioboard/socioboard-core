using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardgplusfeeds
    {
        public virtual Guid Id { get; set; }
        public virtual string Feedlink { get; set; }
        public virtual Guid Gplusboardaccprofileid { get; set; }
        public virtual DateTime? Publishedtime { get; set; }
        public virtual string Imageurl { get; set; }
        public virtual string Title { get; set; }
        public virtual string  Feedid { get; set; }
        public virtual string FromName { get; set; }
        public virtual string FromId { get; set; }
        public virtual string FromPicUrl { get; set; }
    }
}
