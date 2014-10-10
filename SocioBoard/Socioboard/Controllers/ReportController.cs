using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Socioboard.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult loadmenu()
        {
            return PartialView("_ReportMenuPartial", Helper.SBUtils.GetReportsMenuAccordingToGroup());
        }
     
        public ActionResult TwitterReportPartial(FormCollection frmcollection)
        {
            string twtProfileId = frmcollection["twtProfileId"].ToString();

            int days = Convert.ToInt32(frmcollection["days"]);
            return PartialView("_TwitterReportPartial",Helper.SBUtils.GetTwitterReportData(twtProfileId,days));        
        }

        public ActionResult Teamreportpartial(FormCollection frmcollection)
        {
            int days = Convert.ToInt32(frmcollection["days"]);
            return PartialView("_TeamReportPartial", Helper.SBUtils.GetTeamReportData(days));  
            //return PartialView("_TeamReportPartial");
        }

        public ActionResult GroupStatPartial(FormCollection frmcollection)
        {
            int days = Convert.ToInt32(frmcollection["days"]);
            return PartialView("_GroupStatPartial", Helper.SBUtils.GetGroupStatsData(days));              
        }
    }
}
