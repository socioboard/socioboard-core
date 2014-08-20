using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class FacebookInsightPostStats
    {
        public Guid Id {get;set;}
	    public string PageId {get;set;}
	    public string PostId {get;set;}
	    public string PostMessage{get;set;}
	    public int PostLikes{get;set;}
	    public int PostComments{get;set;}
	    public int PostShares{get;set;}
	    public Guid UserId{get;set;}
	    public DateTime EntryDate{get;set;}
        public string PostDate { get; set; }
    
    }
}