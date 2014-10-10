using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public interface ISocialSiteFeed : ISerializable
    {
        //string ProfileType { get; set; }

        Guid Id { get; set; }
        string FeedDescription { get; set; }
        DateTime FeedDate { get; set; }
        DateTime EntryDate { get; set; }
        string ProfileId { get; set; }
        string FromId { get; set; }
        string FromName { get; set; }
        string FromProfileUrl { get; set; }
        Guid UserId { get; set; }
        string Type { get; set; }
        string FbComment { get; set; }
        string FbLike { get; set; }
        string FeedId { get; set; }
        int ReadStatus { get; set; }
        string ProfileType { get; set; }


        string TwitterMsg { get; set; }
        DateTime MessageDate { get; set; }
        string ScreenName { get; set; }
        string MessageId { get; set; }
        string InReplyToStatusUserId { get; set; }
        string SourceUrl { get; set; }
        string FromScreenName { get; set; }
    }
}
