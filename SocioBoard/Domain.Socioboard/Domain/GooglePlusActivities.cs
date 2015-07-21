using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class GooglePlusActivities
    {
        public Guid Id  { get; set; }
	    public Guid UserId  { get; set; }
        public string GpUserId { get; set; }
	    public string ActivityId  { get; set; }
	    public string Title { get; set; }
	    public string ActivityUrl { get; set; }
	    public string FromId { get; set; }
	    public string FromUserName { get; set; }
	    public string FromProfileImage { get; set; }
	    public string Content { get; set; }
	    public int RepliesCount { get; set; }
	    public int PlusonersCount { get; set; }
	    public int ResharersCount { get; set; }
        public DateTime PublishedDate { get; set; }
	    public DateTime EntryDate { get; set; }
        public string Attachment { get; set; }
        public string AttachmentType { get; set; }
        public string Link { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleDisplayname { get; set; }

    }
}