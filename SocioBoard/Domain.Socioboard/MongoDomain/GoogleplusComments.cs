using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.MongoDomain
{
    [BsonIgnoreExtraElements]
    public class GoogleplusComments
    {
        public ObjectId Id { get; set; }
        public string GplusUserId { get; set; }
        public string FeedId { get; set; }
        public string CommentId { get; set; }
        public string Comment { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromImageUrl { get; set; }
        public string FromUrl { get; set; }
        public string CreatedDate { get; set; } 
    }
}
