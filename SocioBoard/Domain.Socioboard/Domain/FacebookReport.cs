using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class FacebookReport
    {

        public Guid Id { get; set; }
        public long likes_15 { get; set; }
        public long likes_30 { get; set; }
        public long likes_60 { get; set; }
        public long likes_90 { get; set; }
        public string perday_likes_90 { get; set; }


        public long unlikes_15 { get; set; }
        public long unlikes_30 { get; set; }
        public long unlikes_60 { get; set; }
        public long unlikes_90 { get; set; }
        public string perday_unlikes_90 { get; set; }


        public long impression_15 { get; set; }
        public long impression_30 { get; set; }
        public long impression_60 { get; set; }
        public long impression_90 { get; set; }
        public string perday_impression_90 { get; set; }



        public long impressionbyloc_15 { get; set; }
        public long impressionbyloc_30 { get; set; }
        public long impressionbyloc_60 { get; set; }
        public long impressionbyloc_90 { get; set; }
        public string perday_impressionbyloc_90 { get; set; }


        public long impressionbyday_15 { get; set; }
        public long impressionbyday_30 { get; set; }
        public long impressionbyday_60 { get; set; }
        public long impressionbyday_90 { get; set; }
        public string perday_impressionbyday_90 { get; set; }


        public long impressionbyage_15 { get; set; }
        public long impressionbyage_30 { get; set; }
        public long impressionbyage_60 { get; set; }
        public long impressionbyage_90 { get; set; }
        public string perday_impressionbyage_90 { get; set; }


        public long impressionbygender_15 { get; set; }
        public long impressionbygender_30 { get; set; }
        public long impressionbygender_60 { get; set; }
        public long impressionbygender_90 { get; set; }
        public string perday_impressionbygender_90 { get; set; }


        public long sharing_15 { get; set; }
        public long sharing_30 { get; set; }
        public long sharing_60 { get; set; }
        public long sharing_90 { get; set; }
        public string perday_sharing_90 { get; set; }

        public string fan { get; set; }

        public string pagepost { get; set; }
        public string mention { get; set; }
        public string checkin { get; set; }
        public string question { get; set; }
        public string coupon { get; set; }
        public string userpost { get; set; }
        public string other { get; set; }
        public string Event { get; set; }
        public string organic { get; set; }
        public string viral { get; set; }
        public string paid { get; set; }

    
    }
}
