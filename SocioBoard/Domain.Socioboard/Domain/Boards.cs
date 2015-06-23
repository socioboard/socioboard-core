using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class Boards
    {
        public virtual Guid BoardId { get; set; }
        public virtual string BoardName { get; set; }
        public virtual string Instagramprofileid { get; set; }
        public virtual string Fbprofileid { get; set; }
        public virtual string Twitterprofileid { get; set; }
        public virtual string Linkedinprofileid { get; set; }
        public virtual string Youtubeprofileid { get; set; }
        public virtual string Gplusprofileid { get; set; }
        public virtual string Tumblrprofileid { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual bool IsHidden { get; set; }
    }
}
