using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class InstagramReport
    {
        public Guid Id { get; set; }
        public string Profile_Id { get; set; }
        public string Insta_Name { get; set; }
        public string Full_Name { get; set; }
        public string Media_Count { get; set; }
        public string Follower { get; set; }
        public string Following { get; set; }
        public long follow_15 { get; set; }
        public string perday_follow_15 { get; set; }
        public long follow_30 { get; set; }
        public string perday_follow_30 { get; set; }
        public long follow_60 { get; set; }
        public string perday_follow_60 { get; set; }
        public long follow_90 { get; set; }
        public string perday_follow_90 { get; set; }

        public long following_15 { get; set; }
        public string perday_following_15 { get; set; }

        public long following_30 { get; set; }
        public string perday_following_30 { get; set; }

        public long following_60 { get; set; }
        public string perday_following_60 { get; set; }

        public long following_90 { get; set; }
        public string perday_following_90 { get; set; }


        public long postcomment_15 { get; set; }
        public string perday_postcomment_15 { get; set; }

        public long postcomment_30 { get; set; }
        public string perday_postcomment_30 { get; set; }

        public long postcomment_60 { get; set; }
        public string perday_postcomment_60 { get; set; }

        public long postcomment_90 { get; set; }
        public string perday_postcomment_90 { get; set; }

        public long postlike_15 { get; set; }
        public string perday_postlike_15 { get; set; }
        public long postlike_30 { get; set; }
        public string perday_postlike_30 { get; set; }

        public long postlike_60 { get; set; }
        public string perday_postlike_60 { get; set; }

        public long postlike_90 { get; set; }
        public string perday_postlike_90 { get; set; }

        public long videopost_15 { get; set; }
        public string perday_videopost_15 { get; set; }

        public long videopost_30 { get; set; }
        public string perday_videopost_30 { get; set; }

        public long videopost_60 { get; set; }
        public string perday_videopost_60 { get; set; }

        public long videopost_90 { get; set; }
        public string perday_videopost_90 { get; set; }

        public long imagepost_15 { get; set; }
        public string perday_imagepost_15 { get; set; }

        public long imagepost_30 { get; set; }
        public string perday_imagepost_30 { get; set; }

        public long imagepost_60 { get; set; }
        public string perday_imagepost_60 { get; set; }

        public long imagepost_90 { get; set; }
        public string perday_imagepost_90 { get; set; }

   }

}
