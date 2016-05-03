using Socioboard.App_Start;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Socioboard.Controllers
{
    public class AdminHomeController : Controller
    {
       
        //
        // GET: /AdminHome/
        [MyExpirePageActionFilter]
        //[Authorize(Users = "Aby Kumar")]
        //[CustomAuthorize("SuperAdmin")]
        public ActionResult Dashboard()
        {
            if (Session["User"] != null)
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)Session["User"];
                if (_User.UserType != "SuperAdmin")
                {
                    return RedirectToAction("Index", "Index");
                }
            }
            else {
                return RedirectToAction("Index", "Index");
            }

            string strUser = string.Empty;
            string strStandard = string.Empty;
            string strDelux = string.Empty;
            string strPremium = string.Empty;
            string strMonth = string.Empty;
            string strAccMonth = string.Empty;
            string strPaidUser = string.Empty;
            string strPaidMonth = string.Empty;
            string strUnPaidUser = string.Empty;
            string strUnPaidMonth = string.Empty;



            #region User Count By Month
            try
            {
                Api.User.User ApiObjUser = new Api.User.User();
                string ArrlstUser = (string)(new JavaScriptSerializer().Deserialize(ApiObjUser.UserCountByMonth(), typeof(string)));
                ArrlstUser = ArrlstUser.Replace("\"","");

                try
                {
                    string[] arr = Regex.Split(ArrlstUser, "_#_");
                    strPaidUser=arr[0];
                    strUnPaidUser=arr[1];
                    strMonth = arr[2];
                }
                catch (Exception ex)
                {

                    strPaidUser = "0,0,0,0,0,0,0,0,0,0,0,0";
                    strUnPaidUser = "0,0,0,0,0,0,0,0,0,0,0,0";
                }

                

                
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            #endregion
                           
            
            ViewBag.Month = strMonth;
            ViewBag.paidUser = strPaidUser;
            //ViewBag.UnPaidMonth = strUnPaidMonth;
            ViewBag.UnPaidUser = strUnPaidUser;



            return View();
        }
        
        
    }
}
