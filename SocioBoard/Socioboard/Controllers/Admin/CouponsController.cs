using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers.Admin
{
   //[Authorize(Users = "Aby Kumar")]
    public class CouponsController : Controller
    {
        //
        // GET: /Coupons/

        public ActionResult ManageCoupons()
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
        public ActionResult LoadCoupons()
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

            Api.Coupon.Coupon apiobjCoupons = new Api.Coupon.Coupon();
            List<Domain.Socioboard.Domain.Coupon> lstCoupons = (List<Domain.Socioboard.Domain.Coupon>)(new JavaScriptSerializer().Deserialize(apiobjCoupons.GetAllCoupons(), typeof(List<Domain.Socioboard.Domain.Coupon>)));

            return View("_ManageCouponsPartial", lstCoupons);
        }
        public ActionResult EditCoupons(string Id)
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

            Domain.Socioboard.Domain.Coupon objCoupon = new Domain.Socioboard.Domain.Coupon();
            objCoupon.Id = Guid.Parse(Id);
            string Objcoupon = (new JavaScriptSerializer().Serialize(objCoupon));
            Api.Coupon.Coupon apiobjCoupons = new Api.Coupon.Coupon();
            List<Domain.Socioboard.Domain.Coupon> lstCoupons = (List<Domain.Socioboard.Domain.Coupon>)(new JavaScriptSerializer().Deserialize(apiobjCoupons.GetCouponsById(Objcoupon), typeof(List<Domain.Socioboard.Domain.Coupon>)));
            Domain.Socioboard.Domain.Coupon objCoupons = lstCoupons[0];
            Session["CouponsToUpdate"] = objCoupons;
            return View(objCoupons);
        }

        public ActionResult UpdateCoupons(string couponcode, string EntryDate, string ExpiryDate, string Status)
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

            Domain.Socioboard.Domain.Coupon objCoupons = (Domain.Socioboard.Domain.Coupon)Session["CouponsToUpdate"];
            objCoupons.CouponCode = couponcode;
            objCoupons.EntryCouponDate = Convert.ToDateTime(EntryDate);
            objCoupons.ExpCouponDate = Convert.ToDateTime(ExpiryDate);
            objCoupons.Status = Status;
            string ObjCoupn = (new JavaScriptSerializer().Serialize(objCoupons));
            Api.Coupon.Coupon apiobjNews = new Api.Coupon.Coupon();
            string CouponUpdateMessage = (string)(new JavaScriptSerializer().Deserialize(apiobjNews.UpdateCoupons(ObjCoupn), typeof(string)));
            return Content(CouponUpdateMessage);
        }
        public ActionResult CreateCoupons()
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

            Api.Coupon.Coupon apiobjCoupons = new Api.Coupon.Coupon();
            List<Domain.Socioboard.Domain.Coupon> lstCoupons = (List<Domain.Socioboard.Domain.Coupon>)(new JavaScriptSerializer().Deserialize(apiobjCoupons.GetAllCoupons(), typeof(List<Domain.Socioboard.Domain.Coupon>)));
            int CouponNo = lstCoupons.Count + 1;
            return View(CouponNo);
        }
        public ActionResult AddCoupons(string Couponcode, string EntryDate, string ExpiryDate, string Status, string Percentage)
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

            Domain.Socioboard.Domain.Coupon objCoupons = new  Domain.Socioboard.Domain.Coupon();
            objCoupons.Id = Guid.NewGuid();
            objCoupons.CouponCode = Couponcode;
            objCoupons.EntryCouponDate = Convert.ToDateTime(EntryDate);
            objCoupons.ExpCouponDate = Convert.ToDateTime(ExpiryDate);
            objCoupons.Status = Status;
            objCoupons.Discount = Convert.ToInt16(Percentage);
            string ObjCoupn = (new JavaScriptSerializer().Serialize(objCoupons));
            Api.Coupon.Coupon apiobjCoupon = new Api.Coupon.Coupon();
            string CouponUpdateMessage = (string)(new JavaScriptSerializer().Deserialize(apiobjCoupon.AddCoupons(ObjCoupn), typeof(string)));
            return Content(CouponUpdateMessage);
        }

    }
}
