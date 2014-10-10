using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public interface ISocialSiteMessage
    {
        Guid Id { get; set; }
        string Message { get; set; }
        DateTime MessageDate { get; set; }
        DateTime EntryDate { get; set; }
        string ProfileId { get; set; }
        string FromId { get; set; }
        string FromName { get; set; }
        string FromProfileUrl { get; set; }
        string FbComment { get; set; }
        string FbLike { get; set; }
        string MessageId { get; set; }
        string Type { get; set; }
        Guid UserId { get; set; }
        string Picture { get; set; }


        string RecipientId { get; set; }
        string RecipientScreenName { get; set; }
        string RecipientProfileUrl { get; set; }
        DateTime CreatedDate { get; set; }
        string SenderId { get; set; }
        string SenderScreenName { get; set; }
        string SenderProfileUrl { get; set; }
       
    }
}
