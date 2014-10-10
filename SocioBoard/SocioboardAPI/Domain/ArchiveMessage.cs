using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class ArchiveMessage
    {
            public Guid Id { get; set; }
            public string ImgUrl { get; set; }
            public Guid UserId { get; set; }
            public string ProfileId { get; set; }
            public string SocialGroup { get; set; }
            public string UserName { get; set; }
            public string MessageId { get; set; }
            public string Message { get; set; }
            public DateTime CreatedDateTime { get; set; }
    }
}