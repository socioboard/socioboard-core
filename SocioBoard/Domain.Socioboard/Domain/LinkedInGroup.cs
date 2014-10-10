using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Domain.Socioboard.Domain
{
    public class LinkedInGroup
    {

        private XmlDocument xmlResult;

        public LinkedInGroup()
        {
            xmlResult = new XmlDocument();
        }

        public List<Group_Updates> GroupUpdatesList = new List<Group_Updates>();

        public struct Group_Updates
        {
            public string id { get; set; }
            public string LinkedInProfileId { get; set; }
            public string GroupName { get; set; }
            public string GpPostid { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string headline { get; set; }
            public string pictureurl { get; set; }
            public string title { get; set; }
            public string likes_total { get; set; }
            public string comments_total { get; set; }
            public string summary { get; set; }
            public int isFollowing { get; set; }
            public int isLiked { get; set; }

        }
    }
}
