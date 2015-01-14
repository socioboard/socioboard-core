using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
    //[Authorize(Users = "Aby Kumar")]
    public class ServicesController : Controller
    {
        //
        // GET: /Services/

        public ActionResult Scheduling()
        {
            return View();
        }

        public ActionResult LoadSheduling()
        {
            Api.ScheduledMessage.ScheduledMessage apiobjNews = new Api.ScheduledMessage.ScheduledMessage();
            List<Domain.Socioboard.Helper.ScheduledTracker> lstScheduleDetails = (List<Domain.Socioboard.Helper.ScheduledTracker>)(new JavaScriptSerializer().Deserialize(apiobjNews.GetAllScheduledDetails(), typeof(List<Domain.Socioboard.Helper.ScheduledTracker>)));
            return PartialView("_SchedulingPartial", lstScheduleDetails);
        }

        public ActionResult SchedulingDetails(string Id)
        {
            Session["UserScheduleId"] = Id.ToString();
            return View();
        }

        public ActionResult LoadShedulingDetails()
        {
            string Id = Session["UserScheduleId"].ToString();
            Api.ScheduledMessage.ScheduledMessage apiobjSchdl = new Api.ScheduledMessage.ScheduledMessage();
            List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduleDetails = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(apiobjSchdl.GetAllScheduledMessageByUserId(Id), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_SchedulingDetailsPartial", lstScheduleDetails);
        }

    }
}
