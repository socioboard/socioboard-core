using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Socioboard.Domain
{
   public class Fbpublicpagereports
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Pageid { get; set; }
        public virtual float Likescount { get; set; }
        public virtual float Postscount { get; set; }
        public virtual float Commentscount { get; set; }
        public virtual float Sharescount { get; set; }
    }

   public class FbpublicpagereportsView 
   {
       public virtual string name { get; set; }
       public virtual float[] data { get; set; }
   }
}
