using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.MongoDomain
{
    [BsonIgnoreExtraElements]
    public class TumblrFeed
    {
        //public Guid Id { get; set; }
        //public Guid UserId { get; set; }
        public ObjectId Id { get; set; }
        public string ProfileId { get; set; }
        public string blogname { get; set; }
        public string blogId { get; set; }
        public string blogposturl { get; set; }
        public string description { get; set; }
        public string slug { get; set; }
        public string type { get; set; }
        public string date { get; set; }
        public string reblogkey { get; set; }
        public int liked { get; set; }
        public int followed { get; set; }
        public int canreply { get; set; }
        public string sourceurl { get; set; }
        public string sourcetitle { get; set; }
        //public string timestamp { get; set; }
        public string imageurl { get; set; }
        public string videourl {get;set;}
        public int notes { get; set; }
        public string postId { get; set; }
        public string content { get; set; }

    }
}