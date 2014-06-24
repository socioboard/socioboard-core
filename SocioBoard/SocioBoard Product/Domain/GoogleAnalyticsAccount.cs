using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class GoogleAnalyticsAccount
    {
        public Guid Id {get;set;}
	    public Guid UserId {get;set;}
	    public string EmailId {get;set;}
	    public string GaAccountId {get;set;}
	    public string GaAccountName {get;set;}
        public string GaProfileId { get; set; }
        public string GaProfileName { get; set; }
	    public string AccessToken  {get;set;}
        public string RefreshToken { get; set; }
	    public int    Visits {get;set;}
	    public double AvgVisits {get;set;}
	    public int NewVisits {get;set;}
	    public bool IsActive {get;set;}
        public DateTime EntryDate { get; set; }
    }
}