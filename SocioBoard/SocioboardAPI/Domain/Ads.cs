using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Socioboard.Domain
{
    public class Ads
    {
        public Guid Id {get;set;}
        public string Advertisment { get; set; }
	    public string ImageUrl  {get;set;}
	    public string  Script{get;set;}
	    public DateTime EntryDate{get;set;}
	    public DateTime ExpiryDate{get;set;}
        public bool Status { get; set; }
    }
}