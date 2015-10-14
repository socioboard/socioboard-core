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
   public class FacebookMessage
    {
         [BsonId]
        public ObjectId Id { get; set; }
        public string Message { get; set; }
        public string MessageDate { get; set; }
        public string EntryDate { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromProfileUrl { get; set; }
        public string FbComment { get; set; }
        public string FbLike { get; set; }
        public string MessageId { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public string IsArchived { get; set; }
    }
}
