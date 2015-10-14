using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Socioboard.MongoDomain
{
    [BsonIgnoreExtraElements]
    public class InstagramComment
    {

        public ObjectId Id { get; set; }
        public string strId { get; set; }

        //public string EntryDate { get; set; }
        public string InstagramId { get; set; }

        //public Guid UserId { get; set; }
        public string FeedId { get; set; }
        public string CommentId { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
        public string FromName { get; set; }
        public string FromProfilePic { get; set; }

    }
}