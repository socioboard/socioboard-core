using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class News
    {
       public Guid Id {get;set;}
	   public string NewsDetail {get;set;}
	   public DateTime EntryDate{get;set;}
	   public DateTime ExpiryDate{get;set;}
       public bool Status { get; set; }
       
       }
}