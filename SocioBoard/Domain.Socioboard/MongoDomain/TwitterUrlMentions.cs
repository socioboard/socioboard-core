using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Domain.Socioboard.MongoDomain
{
     [BsonIgnoreExtraElements]
    public class TwitterUrlMentions
    {
        public ObjectId Id { get; set; }
        public string Feed { get; set; }
        public string FeedId { get; set; }
        public long Feeddate { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromImageUrl { get; set; }
        public string HostName { get; set; }

    }
}
