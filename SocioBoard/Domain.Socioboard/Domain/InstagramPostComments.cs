using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Socioboard.Domain
{
   public class InstagramPostComments
    {
        public Guid Id { get; set; }
        public string Profile_Id { get; set; }
        public string Feed_Id { get; set; }
        public string Commented_By_Id { get; set; }
        public string Commented_By_Name { get; set; }
        public string Comment { get; set; }
        public DateTime Created_Time { get; set; }
        public string Comment_Id { get; set; }
        public string Feed_Type { get; set; }

 
   }
}
