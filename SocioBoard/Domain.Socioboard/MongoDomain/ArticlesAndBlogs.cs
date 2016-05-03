using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.MongoDomain
{
    [BsonIgnoreExtraElements]
   public class ArticlesAndBlogs
    {
        [BsonId]
        public Object Id { get; set; }
        public string Type { get; set; }
        public string VideoId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }
        public string HostName { get; set; }
        public long Created_Time { get; set; }
    }
}
