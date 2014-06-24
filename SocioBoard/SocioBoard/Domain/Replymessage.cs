using System;
using System.Text;
using System.Collections.Generic;


namespace SocioBoard.Domain
{
    public class ReplyMessage {
        public virtual Guid Id { get; set; }
        public virtual string FromUserId { get; set; }
        public virtual string Name { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual Guid MessageId { get; set; }
        public virtual string Message { get; set; }
        public virtual string Type { get; set; }
    }
}
