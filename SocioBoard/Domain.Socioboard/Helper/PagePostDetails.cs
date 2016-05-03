using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Socioboard.Domain;

namespace Domain.Socioboard.Helper
{
   public class PagePostDetails
    {
       private Domain.SharethonPost sharethonGroupPost;
        private int p;

        public SharethonPost _SharethonPost { get; set; }
        public int postCount { get; set; }

        public PagePostDetails() { }
        public PagePostDetails(SharethonPost _SharethonPost, int postCount)
        {
            this._SharethonPost = _SharethonPost;
            this.postCount = postCount;
        }
    }
}
