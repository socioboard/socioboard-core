using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class InstagramPostLikes
    {
        public Guid Id { get; set; }
        public string Profile_Id { get; set; }
        public string Feed_Id { get; set; }
        public string Liked_By_Id { get; set; }
        public string Liked_By_Name { get; set; }
        public string Feed_Type { get; set; }
        public DateTime Created_Date { get; set; }
        public int Status { get; set; }

    }
}
