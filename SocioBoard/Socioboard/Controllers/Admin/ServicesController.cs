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
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            return View();
        }

        public ActionResult LoadSheduling()
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            Api.ScheduledMessage.ScheduledMessage apiobjNews = new Api.ScheduledMessage.ScheduledMessage();
            List<Domain.Socioboard.Helper.ScheduledTracker> lstScheduleDetails = (List<Domain.Socioboard.Helper.ScheduledTracker>)(serializer.Deserialize(apiobjNews.GetAllScheduledDetails(), typeof(List<Domain.Socioboard.Helper.ScheduledTracker>)));
            return PartialView("_SchedulingPartial", lstScheduleDetails);
        }

        public ActionResult SchedulingDetails(string Id)
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            Session["UserScheduleId"] = Id.ToString();
            return View();
        }

        public ActionResult LoadShedulingDetails()
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
            string Id = Session["UserScheduleId"].ToString();
            Api.ScheduledMessage.ScheduledMessage apiobjSchdl = new Api.ScheduledMessage.ScheduledMessage();
            List<Domain.Socioboard.Domain.ScheduledMessage> lstScheduleDetails = (List<Domain.Socioboard.Domain.ScheduledMessage>)(new JavaScriptSerializer().Deserialize(apiobjSchdl.GetAllScheduledMessageByUserId(Id), typeof(List<Domain.Socioboard.Domain.ScheduledMessage>)));
            return PartialView("_SchedulingDetailsPartial", lstScheduleDetails);
        }

    }
}
