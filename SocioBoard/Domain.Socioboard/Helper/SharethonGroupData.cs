using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Socioboard.Domain;

namespace Domain.Socioboard.Helper
{
    public class SharethonGroupData
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
        public int postCount { get; set; }

        public SharethonGroupData()
        { }

        public SharethonGroupData(string groupId, string groupName, int postCount)
        {
            this.groupId = groupId;
            this.groupName = groupName;
            this.postCount = postCount;
        }

    }

    public class SharethonPageData
    {
        public string profileId { get; set; }
        public int postCount { get; set; }
        public FacebookAccount _FacebookAccount { get; set; }
        public SharethonPageData()
        { }

        public SharethonPageData(string profileId, int postCount)
        {
            this.profileId = profileId;
            this.postCount = postCount;
        
        }
    
    }
}
