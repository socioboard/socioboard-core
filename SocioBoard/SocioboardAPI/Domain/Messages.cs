using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public struct Messages
    {
        public string ProfileId { get; set; }
        public string Network { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromProfileUrl { get; set; }
        public DateTime MessageDate { get; set; }
        public string Message { get; set; }
        public string FbComment { get; set; }
        public string FbLike { get; set; }
        public string MessageId { get; set; }
        public string Type { get; set; }
        public int ReadStatus { get; set; }
    }


}