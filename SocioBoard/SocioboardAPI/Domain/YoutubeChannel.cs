using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class YoutubeChannel
    {
        public virtual Guid Id { get; set; }
        public virtual string Channelid { get; set; }
        public virtual string Likesid { get; set; }
        public virtual string Favoritesid { get; set; }
        public virtual string Uploadsid { get; set; }
        public virtual string Watchhistoryid { get; set; }
        public virtual string Watchlaterid { get; set; }
        public virtual string Googleplususerid { get; set; }
        public virtual int ViewCount { get; set; }
        public virtual int CommentCount { get; set; }
        public virtual int SubscriberCount { get; set; }
        public virtual int HiddenSubscriberCount { get; set; }
        public virtual int VideoCount { get; set; }
    }
}