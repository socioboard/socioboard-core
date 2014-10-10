using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Socioboard.Domain
{
    public class TwitterMessage
    {
        public Guid Id { get; set; }
        public string TwitterMsg { get; set; }
        public DateTime MessageDate { get; set; }
        public DateTime EntryDate { get; set; }
        public string ProfileId { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string FromProfileUrl { get; set; }
        public string ScreenName { get; set; }
        public string MessageId { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
        public string InReplyToStatusUserId { get; set; }
        public string SourceUrl { get; set; }
        public string FromScreenName { get; set; }
        public int ReadStatus { get; set; }

        public string ProfileType
        {
            get
            {
                return "twitter";
            }
            set
            {
                value = "twitter";
            }
        }

        //public string FeedDescription
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public DateTime FeedDate
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string FbComment
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string FbLike
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public string FeedId
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

      

        //public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        //{
        //    throw new NotImplementedException();
        //}
    }
}