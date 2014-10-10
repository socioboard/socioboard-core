using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class ReplyMessage
    {
        public Guid Id { get; set; }
        public string FromUserId { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid MessageId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
    }
}
