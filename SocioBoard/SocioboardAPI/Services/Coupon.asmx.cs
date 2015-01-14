using Api.Socioboard.Helper;
using Api.Socioboard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class Coupon : System.Web.Services.WebService
    {

        CouponRepository ObjCouponRepo = new CouponRepository();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetAllCoupons()
        {
            try
            {
                List<Domain.Socioboard.Domain.Coupon> lstNews = ObjCouponRepo.GetAllCoupon();
                return new JavaScriptSerializer().Serialize(lstNews);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string UpdateCoupons(string ObjCoupons)
        {
            try
            {
                Domain.Socioboard.Domain.Coupon objcoupon = (Domain.Socioboard.Domain.Coupon)(new JavaScriptSerializer().Deserialize(ObjCoupons, typeof(Domain.Socioboard.Domain.Coupon)));
                int res=ObjCouponRepo.SetCouponById(objcoupon);
                if (res==1)
                {
                    return new JavaScriptSerializer().Serialize("Updated Successfully");
                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Coupon Already Exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string GetCouponsById(string ObjCoupons)
        {
            try
            {
                Domain.Socioboard.Domain.Coupon Coupon = (Domain.Socioboard.Domain.Coupon)(new JavaScriptSerializer().Deserialize(ObjCoupons, typeof(Domain.Socioboard.Domain.Coupon)));
                List<Domain.Socioboard.Domain.Coupon> objNews = ObjCouponRepo.GetCouponByCouponId(Coupon);
                return new JavaScriptSerializer().Serialize(objNews);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string AddCoupons(string ObjCoupons)
        {
            try
            {
                Domain.Socioboard.Domain.Coupon objcoupon = (Domain.Socioboard.Domain.Coupon)(new JavaScriptSerializer().Deserialize(ObjCoupons, typeof(Domain.Socioboard.Domain.Coupon)));
                if (ObjCouponRepo.GetCouponByCouponCode(objcoupon).Count < 1 || ObjCouponRepo.GetCouponByCouponCode(objcoupon).Count == 0)
                {
                    ObjCouponRepo.Add(objcoupon);
                    return new JavaScriptSerializer().Serialize("Added Successfully");
                }
                else
                {
                    return new JavaScriptSerializer().Serialize("Coupon Already Exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

    }
}
