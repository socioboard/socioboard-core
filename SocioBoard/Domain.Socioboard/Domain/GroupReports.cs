using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
    public class GroupReports
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public long inbox_15 { get; set; }
        public string perday_inbox_15 { get; set; }
        public long inbox_30 { get; set; }
        public string perday_inbox_30 { get; set; }
     
        public long inbox_60 { get; set; }
        public string perday_inbox_60 { get; set; }
     
        public long inbox_90 { get; set; }
        public string perday_inbox_90 { get; set; }
     
        public long sent_15 { get; set; }
        public string perday_sent_15 { get; set; }
        public long sent_30 { get; set; }
        public string perday_sent_30 { get; set; }
     
        public long sent_60 { get; set; }
        public string perday_sent_60 { get; set; }
     
        public long sent_90 { get; set; }
        public string perday_sent_90 { get; set; }
     
        public long twitterfollower_15 { get; set; }
        public string perday_twitterfollower_15 { get; set; }
      
        public long twitterfollower_30 { get; set; }
        public string perday_twitterfollower_30 { get; set; }
      
        public long twitterfollower_60 { get; set; }
        public string perday_twitterfollower_60 { get; set; }
      
        public long twitterfollower_90 { get; set; }
        public string perday_twitterfollower_90 { get; set; }
      
        public long fbfan_15 { get; set; }
        public string perday_fbfan_15 { get; set; }
  
        public long fbfan_30 { get; set; }
        public string perday_fbfan_30 { get; set; }
  
        public long fbfan_60 { get; set; }
        public string perday_fbfan_60 { get; set; }
  
        public long fbfan_90 { get; set; }
        public string perday_fbfan_90 { get; set; }
  
        public long interaction_15 { get; set; }
        public string perday_interaction_15 { get; set; }
        
        public long interaction_30 { get; set; }
        public string perday_interaction_30 { get; set; }
       
        public long interaction_60 { get; set; }
        public string perday_interaction_60 { get; set; }
       
        public long interaction_90 { get; set; }
        public string perday_interaction_90 { get; set; }

        public long twtmentions_15 { get; set; }
        public string perday_twtmentions_15 { get; set; }
       
        public long twtmentions_30 { get; set; }
        public string perday_twtmentions_30 { get; set; }
       
        public long twtmentions_60 { get; set; }
        public string perday_twtmentions_60 { get; set; }
       
        public long twtmentions_90 { get; set; }
        public string perday_twtmentions_90 { get; set; }
       
        public long twtretweets_15 { get; set; }
        public string perday_twtretweets_15 { get; set; }
        
        public long twtretweets_30 { get; set; }
        public string perday_twtretweets_30 { get; set; }
       
        public long twtretweets_60 { get; set; }
        public string perday_twtretweets_60 { get; set; }
       
        public long twtretweets_90 { get; set; }
        public string perday_twtretweets_90 { get; set; }
        public string sexratio { get; set; }


        public long uniqueusers_15 { get; set; }
        public string perday_uniqueusers_15 { get; set; }
        public long uniqueusers_30 { get; set; }
        public string perday_uniqueusers_30 { get; set; }
        public long uniqueusers_60 { get; set; }
        public string perday_uniqueusers_60 { get; set; }
        public long uniqueusers_90 { get; set; }
        public string perday_uniqueusers_90 { get; set; }
        public long twitter_account_count { get; set; }


    }
}
