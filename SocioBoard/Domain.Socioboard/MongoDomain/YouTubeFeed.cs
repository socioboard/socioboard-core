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
    public class YouTubeFeed
    {
        public ObjectId Id { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public string ChannelName { get; set; }
        public string PublishTime { get; set; }
        public string ChannelId { get; set; }
        public string YoutubeId { get; set; }
        public string Description { get; set; }
        public string VideoId { get; set; }
        public string viewCount { get; set; }
        public string likeCount { get; set; }
        public string dislikeCount { get; set; }
        public string favoriteCount { get; set; }
        public string commentCount { get; set; }
        public string duration { get; set; }

    }
}
