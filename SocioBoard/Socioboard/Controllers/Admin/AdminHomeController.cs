using Socioboard.App_Start;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Dashboard()
        {


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
                ArrayList ArrlstUser = (ArrayList)(new JavaScriptSerializer().Deserialize(ApiObjUser.UserCountByMonth(), typeof(ArrayList)));

                foreach (ArrayList item in ArrlstUser)
                {
                    Array temp = (Array)item.ToArray();
                    //Array temp = new object[20];
                    //temp = (Array)item;

                    //string[] temp = (string[])item.ToArray(typeof(string));

                    strUser = strUser + temp.GetValue(1).ToString() + ",";
                    if (temp.GetValue(0).ToString() == "1")
                        strMonth = strMonth + "'Jan',";
                    else if (temp.GetValue(0).ToString() == "2")
                        strMonth = strMonth + "'Feb',";
                    else if (temp.GetValue(0).ToString() == "3")
                        strMonth = strMonth + "'March',";
                    else if (temp.GetValue(0).ToString() == "4")
                        strMonth = strMonth + "'April',";
                    else if (temp.GetValue(0).ToString() == "5")
                        strMonth = strMonth + "'May',";
                    else if (temp.GetValue(0).ToString() == "6")
                        strMonth = strMonth + "'June',";
                    else if (temp.GetValue(0).ToString() == "7")
                        strMonth = strMonth + "'July',";
                    else if (temp.GetValue(0).ToString() == "8")
                        strMonth = strMonth + "'Aug',";
                    else if (temp.GetValue(0).ToString() == "9")
                        strMonth = strMonth + "'Sep',";
                    else if (temp.GetValue(0).ToString() == "10")
                        strMonth = strMonth + "'Oct',";
                    else if (temp.GetValue(0).ToString() == "11")
                        strMonth = strMonth + "'Nov',";
                    else if (temp.GetValue(0).ToString() == "12")
                        strMonth = strMonth + "'Dec'";
                }

                if (strMonth[strMonth.Length - 1] == ',')
                {
                    strMonth = strMonth.Substring(0, strMonth.Length - 1);
                }


                strUser = strUser.Substring(0, strUser.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            #endregion

            #region User Count By Month & Account Type
            try
            {

                Api.User.User ApiObjUser = new Api.User.User();
                ArrayList ArrlstUser = (ArrayList)(new JavaScriptSerializer().Deserialize(ApiObjUser.UserCountByAccTypeMonth(), typeof(ArrayList)));

                foreach (ArrayList item in ArrlstUser)
                {
                    Array temp = (Array)item.ToArray();
                    if (temp.GetValue(0).ToString() == "1")
                        strAccMonth = strAccMonth + "'Jan',";
                    else if (temp.GetValue(0).ToString() == "2")
                        strAccMonth = strAccMonth + "'Feb',";
                    else if (temp.GetValue(0).ToString() == "3")
                        strAccMonth = strAccMonth + "'March',";
                    else if (temp.GetValue(0).ToString() == "4")
                        strAccMonth = strAccMonth + "'April',";
                    else if (temp.GetValue(0).ToString() == "5")
                        strAccMonth = strAccMonth + "'May',";
                    else if (temp.GetValue(0).ToString() == "6")
                        strAccMonth = strAccMonth + "'June',";
                    else if (temp.GetValue(0).ToString() == "7")
                        strAccMonth = strAccMonth + "'July',";
                    else if (temp.GetValue(0).ToString() == "8")
                        strAccMonth = strAccMonth + "'Aug',";
                    else if (temp.GetValue(0).ToString() == "9")
                        strAccMonth = strAccMonth + "'Sep',";
                    else if (temp.GetValue(0).ToString() == "10")
                        strAccMonth = strAccMonth + "'Oct',";
                    else if (temp.GetValue(0).ToString() == "11")
                        strAccMonth = strAccMonth + "'Nov',";
                    else if (temp.GetValue(0).ToString() == "12")
                        strAccMonth = strAccMonth + "'Dec'";
                    if (temp.GetValue(3) != null)
                    {
                        if (temp.GetValue(3).ToString() == "INDIVIDUAL")
                            strStandard = strStandard + temp.GetValue(1).ToString() + ",";
                        else if (temp.GetValue(3).ToString() == "SMALL BUSINESS")
                            strDelux = strDelux + temp.GetValue(1).ToString() + ",";
                        else if (temp.GetValue(3).ToString() == "CORPORATION")
                            strPremium = strPremium + temp.GetValue(1).ToString() + ",";
                    }
                }
                if (strAccMonth[strAccMonth.Length - 1] == ',')
                {
                    strAccMonth = strAccMonth.Substring(0, strAccMonth.Length - 1);
                }
                strStandard = strStandard.Substring(0, strStandard.Length - 1);
                strDelux = strDelux.Substring(0, strDelux.Length - 1);
                strPremium = strPremium.Substring(0, strPremium.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }
            #endregion

            #region paid User
            try
            {
                Api.User.User ApiObjUser = new Api.User.User();
                ArrayList ArrlstUser = (ArrayList)(new JavaScriptSerializer().Deserialize(ApiObjUser.PaidUserCountByMonth(), typeof(ArrayList)));

                foreach (ArrayList item in ArrlstUser)
                {
                    Array temp = (Array)item.ToArray();
                    strPaidUser = strPaidUser + temp.GetValue(1).ToString() + ",";
                    if (temp.GetValue(0).ToString() == "1")
                        strPaidMonth = strPaidMonth + "'Jan',";
                    else if (temp.GetValue(0).ToString() == "2")
                        strPaidMonth = strPaidMonth + "'Feb',";
                    else if (temp.GetValue(0).ToString() == "3")
                        strPaidMonth = strPaidMonth + "'March',";
                    else if (temp.GetValue(0).ToString() == "4")
                        strPaidMonth = strPaidMonth + "'April',";
                    else if (temp.GetValue(0).ToString() == "5")
                        strPaidMonth = strPaidMonth + "'May',";
                    else if (temp.GetValue(0).ToString() == "6")
                        strPaidMonth = strPaidMonth + "'June',";
                    else if (temp.GetValue(0).ToString() == "7")
                        strPaidMonth = strPaidMonth + "'July',";
                    else if (temp.GetValue(0).ToString() == "8")
                        strPaidMonth = strPaidMonth + "'Aug',";
                    else if (temp.GetValue(0).ToString() == "9")
                        strPaidMonth = strPaidMonth + "'Sep',";
                    else if (temp.GetValue(0).ToString() == "10")
                        strPaidMonth = strPaidMonth + "'Oct',";
                    else if (temp.GetValue(0).ToString() == "11")
                        strPaidMonth = strPaidMonth + "'Nov',";
                    else if (temp.GetValue(0).ToString() == "12")
                        strPaidMonth = strPaidMonth + "'Dec'";
                }

                if (strPaidMonth[strPaidMonth.Length - 1] == ',')
                {
                    strPaidMonth = strPaidMonth.Substring(0, strPaidMonth.Length - 1);
                }


                strPaidUser = strPaidUser.Substring(0, strPaidUser.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }


            #endregion



            #region Unpaid User
            try
            {
                Api.User.User ApiObjUser = new Api.User.User();
                ArrayList ArrlstUser = (ArrayList)(new JavaScriptSerializer().Deserialize(ApiObjUser.UnPaidUserCountByMonth(), typeof(ArrayList)));

                foreach (ArrayList item in ArrlstUser)
                {
                    Array temp = (Array)item.ToArray();
                    strUnPaidUser = strUnPaidUser + temp.GetValue(1).ToString() + ",";
                    if (temp.GetValue(0).ToString() == "1")
                        strUnPaidMonth = strUnPaidMonth + "'Jan',";
                    else if (temp.GetValue(0).ToString() == "2")
                        strUnPaidMonth = strUnPaidMonth + "'Feb',";
                    else if (temp.GetValue(0).ToString() == "3")
                        strUnPaidMonth = strUnPaidMonth + "'March',";
                    else if (temp.GetValue(0).ToString() == "4")
                        strUnPaidMonth = strUnPaidMonth + "'April',";
                    else if (temp.GetValue(0).ToString() == "5")
                        strUnPaidMonth = strUnPaidMonth + "'May',";
                    else if (temp.GetValue(0).ToString() == "6")
                        strUnPaidMonth = strUnPaidMonth + "'June',";
                    else if (temp.GetValue(0).ToString() == "7")
                        strUnPaidMonth = strUnPaidMonth + "'July',";
                    else if (temp.GetValue(0).ToString() == "8")
                        strUnPaidMonth = strUnPaidMonth + "'Aug',";
                    else if (temp.GetValue(0).ToString() == "9")
                        strUnPaidMonth = strUnPaidMonth + "'Sep',";
                    else if (temp.GetValue(0).ToString() == "10")
                        strUnPaidMonth = strUnPaidMonth + "'Oct',";
                    else if (temp.GetValue(0).ToString() == "11")
                        strUnPaidMonth = strUnPaidMonth + "'Nov',";
                    else if (temp.GetValue(0).ToString() == "12")
                        strUnPaidMonth = strUnPaidMonth + "'Dec'";
                }

                if (strUnPaidMonth[strUnPaidMonth.Length - 1] == ',')
                {
                    strUnPaidMonth = strUnPaidMonth.Substring(0, strUnPaidMonth.Length - 1);
                }


                strUnPaidUser = strUnPaidUser.Substring(0, strUnPaidUser.Length - 1);
            }
            catch (Exception Err)
            {
                Console.Write(Err.StackTrace);
            }


            #endregion

            
            ViewBag.PaidMonth = strPaidMonth;
            ViewBag.paidUser = strPaidUser;
            ViewBag.UnPaidMonth = strUnPaidMonth;
            ViewBag.UnPaidUser = strUnPaidUser;



            return View();
        }
        
        
    }
}
