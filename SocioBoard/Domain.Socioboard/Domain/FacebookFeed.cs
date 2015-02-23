using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class FacebookFeed
    {
        public Guid Id { get; set; }
        public string FeedDescription {get;set;}
        public DateTime FeedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromProfileUrl { get; set; }
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string FbComment{get;set;}
        public string FbLike { get; set; }
        public string FeedId { get; set; }
        public int ReadStatus { get; set; }
        public string Picture { get; set; }

        public string ProfileType
        {
            get
            {
                return "facebook";
            }
            set
            {
                value = "facebook";
            }
        }

        


        public string TwitterMsg
        {
            get
            {
                return "";
            }
            set
            {
                value="";
            }
        }

        public DateTime MessageDate
        {
            get
            {
                return new DateTime();
            }
            set
            {
                value = new DateTime();
            }
        }

        public string ScreenName
        {
            get
            {
                return "";
            }
            set
            {
                value = "";
            }
        }

        public string MessageId
        {
            get
            {
                return "";
            }
            set
            {
                value = "";
            }
        }

        public string InReplyToStatusUserId
        {
            get
            {
                return "";
            }
            set
            {
                value = "";
            }
        }

        public string SourceUrl
        {
            get
            {
                return "";
            }
            set
            {
                value = "";
            }
        }

        public string FromScreenName
        {
            get
            {
                return "";
            }
            set
            {
                value = "";
            }
        }
    }
}