using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class FacebookReport_90
    {

        public Guid Id { get; set; }
        public string FacebookId { get; set; }
        
        public string TotalLikes { get; set; }
        public string TalkingAbout { get; set; }

        public int Likes { get; set; }
        public string PerDayLikes { get; set; }

        public int Unlikes { get; set; }
        public string PerDayUnlikes { get; set; }

        public int Impression { get; set; }
        public string PerDayImpression { get; set; }

        public int UniqueUser { get; set; }

        public int StoryShare { get; set; }
        public string PerDayStoryShare { get; set; }

        public int ImpressionFans { get; set; }
        public int ImpressionPagePost { get; set; }
        public int ImpressionuserPost { get; set; }
        public int ImpressionCoupn { get; set; }
        public int ImpressionOther { get; set; }
        public int ImpressionMention { get; set; }
        public int ImpressionCheckin { get; set; }
        public int ImpressionQuestion { get; set; }
        public int ImpressionEvent { get; set; }
         
        public int Organic { get; set; }
        public int Viral { get; set; }
        public int Paid { get; set; }
        
        public int M_13_17 { get; set; }
        public int M_18_24 { get; set; }
        public int M_25_34 { get; set; }
        public int M_35_44 { get; set; }
        public int M_45_54 { get; set; }
        public int M_55_64 { get; set; }
        public int M_65 { get; set; }
       
        public int F_13_17 { get; set; }
        public int F_18_24 { get; set; }
        public int F_25_34 { get; set; }
        public int F_35_44 { get; set; }
        public int F_45_54 { get; set; }
        public int F_55_64 { get; set; }
        public int F_65 { get; set; }
         
        public int Sharing_M_13_17 { get; set; }
        public int Sharing_M_18_24 { get; set; }
        public int Sharing_M_25_34 { get; set; }
        public int Sharing_M_35_44 { get; set; }
        public int Sharing_M_45_54 { get; set; }
        public int Sharing_M_55_64 { get; set; }
        public int Sharing_M_65 { get; set; }
        
        public int Sharing_F_13_17 { get; set; }
        public int Sharing_F_18_24 { get; set; }
        public int Sharing_F_25_34 { get; set; }
        public int Sharing_F_35_44 { get; set; }
        public int Sharing_F_45_54 { get; set; }
        public int Sharing_F_55_64 { get; set; }
        public int Sharing_F_65 { get; set; }
        public int Story_Fans { get; set; }
        public int Story_PagePost { get; set; }
        public int Story_UserPost { get; set; }
        public int Story_Coupon { get; set; }
        public int Story_Other { get; set; }
        public int Story_Mention { get; set; }
        public int Story_Checkin { get; set; }
        public int Story_Question { get; set; }
        public int Story_Event { get; set; }
    }
}
