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
    public class Rss
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string strId { get; set; }
        public string RssFeedUrl { get; set; }
        public string ProfileId { get; set; }
        public string ProfileType { get; set; }
        public string UserId { get; set; }
        public string CreatedOn { get; set; }
        public string ProfileName { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
