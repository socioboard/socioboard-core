using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Socioboard.Helper
{
    public class Agency
    {
        public string UserName = "Socioboard";
        public string EmailId = "support@socioboard.com";
        public string userId = "";
        public string amount = "";
        public string plantype = "";

        public void AgencyPlan(string PlanType)
        {
            if (PlanType == "AgencyLite")
            {
                amount = "499";
                plantype = "Agency Lite";
            }
            if (PlanType == "AgencyPro")
            {
                amount = "999";
                plantype = "Agency Pro";
            }
            if (PlanType == "AgencyPremium")
            {
                amount = "2999";
                plantype = "Agency Premium";
            }
        }
    }
}