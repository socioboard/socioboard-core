using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardtwitterfeeds
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime? Createdat { get; set; }
        public virtual string Feedid { get; set; }
        public virtual string Feedurl { get; set; }
        public virtual string Imageurl { get; set; }
        public virtual string Text { get; set; }
        public virtual string Hashtags { get; set; }
        public virtual Guid Twitterprofileid { get; set; }
        public virtual bool Isvisible { get; set; }
        public virtual string FromName { get; set; }
        public virtual string FromId { get; set; }
        public virtual string FromPicUrl { get; set; }
        public virtual int Retweetcount { get; set; }
        public virtual int Favoritedcount { get; set; }
    }
}
