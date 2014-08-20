using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocioBoard.Domain
{
    public class GoogleAnalyticsStats
    {
       public Guid Id { get; set; }
	   public string GaAccountId { get; set; }
	   public string GaProfileId { get; set; }
	   public string gaDate { get; set; }
	   public string gaMonth { get; set; }
	   public string gaYear { get; set; }
	   public string gaCountry { get; set; }
	   public string gaRegion { get; set; }
	   public string gaVisits { get; set; }
	   public DateTime EntryDate { get; set; }
       public Guid UserId { get; set; }
    }
}