using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Socioboard.Domain
{
   public class TwitterDirectMessages
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public string RecipientId { get; set; }
        public string RecipientScreenName { get; set; }
        public string RecipientProfileUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string SenderId { get; set; }
        public string SenderScreenName { get; set; }
        public string SenderProfileUrl { get; set; }
        public string Type { get; set; }
        public string MessageId { get; set; }

    }
}
