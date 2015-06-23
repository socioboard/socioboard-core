using System;
using System.Text;
using System.Collections.Generic;


namespace Domain.Socioboard.Domain
{

    public class Boardfbpage
    {
        public virtual Guid Id { get; set; }
        public virtual string FbPageId { get; set; }
        public virtual string TalkingAboutCount { get; set; }
        public virtual string Likes { get; set; }
        public virtual string Checkins { get; set; }
        public virtual string fbPageName { get; set; }
        public virtual string ProfileImageUrl { get; set; }
        public virtual Guid BoardId { get; set; }
        public virtual string PageUrl { get; set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Website { get; set; }
        public virtual string Founded { get; set; }
        public virtual string Mission { get; set; }
        public virtual string Description { get; set; }
        public virtual string About { get; set; }
        public virtual string Companyname { get; set; }
        public virtual DateTime? Entrydate { get; set; }
    }
}
