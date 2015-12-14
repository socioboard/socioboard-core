using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    [BsonIgnoreExtraElements]
    public class MongoFacebookFeed
    {
        public ObjectId Id { get; set; }
        public string FeedDescription { get; set; }
        public string FeedDate { get; set; }
        public string EntryDate { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromProfileUrl { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }
        public string FbComment { get; set; }
        public string FbLike { get; set; }
        public string FeedId { get; set; }
        public int ReadStatus { get; set; }
        public string Picture { get; set; }
        public double Positive { get; set; }
        public double Negative { get; set; }
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
                value = "";
            }
        }

        public string MessageDate
        {
            get
            {
                return new DateTime().ToString();
            }
            set
            {
                value = new DateTime().ToString();
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