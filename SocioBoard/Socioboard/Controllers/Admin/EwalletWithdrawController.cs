using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
    public class EwalletWithdrawController : Controller
    {
        //
        // GET: /EwalletWithdrawDetails/

        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType == "SuperAdmin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        //
        // GET: /EwalletWithdrawDetails/Details

        public ActionResult Details()
        {
            string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            Api.Affiliates.Affiliates ApiAffiliates = new Api.Affiliates.Affiliates();
            List<Domain.Socioboard.Domain.EwalletWithdrawRequest> lstEwalletWithdrawRequest = (List<Domain.Socioboard.Domain.EwalletWithdrawRequest>)(new JavaScriptSerializer().Deserialize(ApiAffiliates.GetAllEwalletWithdraw(), typeof(List<Domain.Socioboard.Domain.EwalletWithdrawRequest>)));
            return PartialView("_WithdrawRequestPartial",lstEwalletWithdrawRequest);
        }

        public ActionResult Updatepaymentstatus() 
        {
            Api.Affiliates.Affiliates ApiAffiliates=new Api.Affiliates.Affiliates();
            string status = Request.Form["Status"].ToString();
            string id = Request.Form["Id"].ToString();
            int ret = ApiAffiliates.UpdatePaymentStatus(id, Int32.Parse(status));
            return Content("success");
        }

        
    }
}
