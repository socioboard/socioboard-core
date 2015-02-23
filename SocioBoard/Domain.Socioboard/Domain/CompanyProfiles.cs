using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class CompanyProfiles
    {
        public virtual Guid Id { get; set; }
        public virtual string Companyname { get; set; }
        public virtual string Instagramprofileid { get; set; }
        public virtual string Fbprofileid { get; set; }
        public virtual string Twitterprofileid { get; set; }
        public virtual string Linkedinprofileid { get; set; }
        public virtual string Youtubeprofileid { get; set; }
        public virtual string Gplusprofileid { get; set; }
        public virtual string Tumblrprofileid { get; set; }
        public virtual string Userid { get; set; }
    }
}
