using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class vwTwitterStats
    {
        //private Guid id;
        //private Guid userid;
        //public Guid Id 
        //{ 
        //    get {
        //        Guid gid=new Guid(id.ToString());
        //        return gid;
        //    }
        //    set {id=value;} 
        //}
        public virtual DateTime EntryDate { get; set; }
        //public Guid UserId
        //{
        //    get
        //    {
        //        Guid guserid = new Guid(userid.ToString());
        //        return guserid;
        //    }
        //    set { userid = value; } 
        //}
        public virtual Guid Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual string TwitterUserId { get; set; }
        public virtual string Type { get; set; }
        public virtual int Feeds { get; set; }
        public virtual int Tweets { get; set; }
        public virtual int Followers { get; set; }
        public virtual int Following { get; set; }
    }
}