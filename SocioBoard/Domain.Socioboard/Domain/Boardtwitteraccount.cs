using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardtwitteraccount
    {
        public virtual Guid Id { get; set; }
        public virtual string Screenname { get; set; }
        public virtual string Statuscount { get; set; }
        public virtual string Friendscount { get; set; }
        public virtual string Followerscount { get; set; }
        public virtual string Favouritescount { get; set; }
        public virtual Guid Boardid { get; set; }
        public virtual string Twitterprofileid { get; set; }
        public virtual string Profileimageurl { get; set; }
        public virtual string Url { get; set; }
        public virtual string Tweet { get; set; }
        public virtual string Photosvideos { get; set; }
        public virtual string Followingscount { get; set; }
        public virtual DateTime? Entrydate { get; set; }
    }
}
