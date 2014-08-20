using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class Package
    {
       public Guid	Id {get;set;}
	   public string PackageName {get;set;}
	   public double Pricing {get;set;}
	   public DateTime EntryDate{get;set;}
       public bool Status { get; set; }
       public int TotalProfiles { get; set; }
    }
}