using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.MongoDomain
{
    [BsonIgnoreExtraElements]
    public class LinkedInFeed
    {
        public ObjectId Id { get; set; }
        public string Feeds { get; set; }
        public string FeedsDate { get; set; }
        //public DateTime EntryDate { get; set; }
        //public Guid UserId { get; set; }
        public string Type { get; set; }
        public string FeedId { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromPicUrl { get; set; }
        public string ImageUrl { get; set; }
        public string FromUrl { get; set; }
    }
}