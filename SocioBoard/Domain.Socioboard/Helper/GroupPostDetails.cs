using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Socioboard.Domain;

namespace Domain.Socioboard.Helper
{
    public class GroupPostDetails
    {
        private Domain.SharethonGroupPost sharethonGroupPost;
        private int p;

        public SharethonGroupPost _SharethonGroupPost { get; set; }
        public int postCount { get; set; }

        public GroupPostDetails() { }
        public GroupPostDetails(SharethonGroupPost _SharethonGroupPost, int postCount)
        {
            this._SharethonGroupPost = _SharethonGroupPost;
            this.postCount = postCount;
        }

        
    }
}
