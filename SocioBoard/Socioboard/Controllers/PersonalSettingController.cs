using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;

namespace Socioboard.Controllers
{
    [Authorize]
    public class PersonalSettingController : Controller
    {
        //
        // GET: /PersonalSetting/

        public ActionResult Index()
        {
            if (Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                return View();
            }
            //return View();
        }
        public ActionResult LoadPersonalSetting()
        {
              User objUser = (User)Session["User"];
              return PartialView("_PersonalSettingPartial", objUser);
        }
        //vikash [20/11/2014]
        public ActionResult Billing()
        {
            return View();
        }
        public ActionResult LoadBillingPartial()
        {
            User objUser = (User)Session["User"];
            return PartialView("_BillingPartial", objUser);
        }
        public ActionResult EditUserInfo(string id,string fname,string lname,string email,string dt)
        {
              User objUser = (User)Session["User"];
              Api.User.User ApiobjUser = new Api.User.User();
              string ret = ApiobjUser.UpdateUser(id, fname, lname, dt);
              if (ret == "1")
              {
                  objUser.UserName = fname + " " + lname;
                  objUser.TimeZone = dt;
                  Session["User"] = objUser;
              }
              return Content(ret);
        }

        public ActionResult ChangePassword(string id, string oldpass, string newpass, string confirmpass)
        {
            User objUser = (User)Session["User"];
            Api.User.User ApiobjUser = new Api.User.User();
            //UserRepository userrepo = new UserRepository();
            //Domain.Socioboard.Domain.User user = ApiobjUser.getUserInfoByEmail(;
            string user = ApiobjUser.getUserInfoByEmail(id);
            string ret = string.Empty;
            if (newpass.Equals(confirmpass))
            {
                ret = ApiobjUser.ChangePassword(id, oldpass, newpass);
            }
            else
            {
                ret = "New Password and Confirm Password mismatch";
            }
            //string ret = ApiobjUser.UpdateUser(id, fname, lname, dt);
            //if (ret == "1")
            //{
            //    objUser.UserName = fname + " " + lname;
            //    objUser.TimeZone = dt;
            //    Session["User"] = objUser;
            //}
            return Content(ret);
        }
        // Edited by Antima

        public ActionResult PaypalUpgradePlan(string UpgradeType)
        {
            string pay = "";
            string amount = "";
            try
            {
                User objUser = (User)Session["User"];
                Helper.Payment payme = new Payment();

                if (UpgradeType == "MonthlyUpgrade")
                {
                    amount = "99.99";
                }
                else
                {
                    amount = "1199.88";
                }

                Session["PaymentAmount"] = amount;
                Session["UpgradeType"] = UpgradeType;

                string AccountType = objUser.AccountType;
                string UserName = objUser.UserName;
                string EmailId = objUser.EmailId;

                string UpgradePlanSuccessURL = ConfigurationManager.AppSettings["UpgradePlanSuccessURL"];
                string UpgradePlanFailedURL = ConfigurationManager.AppSettings["UpgradePlanFailedURL"];
                string UpgradePlanpaypalemail = ConfigurationManager.AppSettings["UpgradePlanpaypalemail"];
                string userId = objUser.Id.ToString();

                pay = payme.PayWithPayPal(amount, AccountType, UserName, "", EmailId, "USD", UpgradePlanpaypalemail, UpgradePlanSuccessURL,
                                        UpgradePlanFailedURL, UpgradePlanSuccessURL, "", "", userId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(pay);
        }

        public ActionResult PaymentSuccessful()
        {
            User objUser = (User)Session["User"];
            objUser.PaymentStatus = "Paid";
            string paidamount = (string)Session["PaymentAmount"];
            string UpgradeType = (string)Session["UpgradeType"];

            Api.User.User ApiobjUser = new Api.User.User();
    
            if (DateTime.Compare(objUser.ExpiryDate, DateTime.Now) > 0)
            {
                if (UpgradeType == "MonthlyUpgrade")
                {
                    objUser.ExpiryDate = objUser.ExpiryDate.AddDays(30);
                }
                else if (UpgradeType == "YearlyUpgrade")
                {
                    objUser.ExpiryDate = objUser.ExpiryDate.AddDays(365);
                } 
            }
            else
            {
                if (UpgradeType == "MonthlyUpgrade")
                {
                    objUser.ExpiryDate = DateTime.Now.AddDays(30);
                }
                else if (UpgradeType == "YearlyUpgrade")
                {
                    objUser.ExpiryDate = DateTime.Now.AddDays(365);
                } 
            }

            //Change Payment status to 1
            ApiobjUser.changePaymentStatus(objUser.Id.ToString(), "paid");

            //Update Paymenttransaction table
            Api.PaymentTransaction.PaymentTransaction objApiPaymentTransaction = new Api.PaymentTransaction.PaymentTransaction();

            string res_PaymentTransaction = objApiPaymentTransaction.SavePayPalTransaction(objUser.Id.ToString(), paidamount);

            if (res_PaymentTransaction=="Success")
            {
                RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult PaymentFailed()
        {
            return View();
        }
        // vikash [20/11/2014]
        public ActionResult UpgradeAccountByPayPal(string plan, string price)
        {
            User objUser = (User)Session["User"];
            Helper.Payment payme = new Payment();
            string amount = price.Replace("$", "").Trim();
            Session["PaymentAmount"] = amount;
            Session["AccountType"] = plan;
            if (amount == "Free")
            {
                amount = "0";
            }
            string UserName = objUser.UserName;
            string EmailId = objUser.EmailId;

            string UpgradeAccountSuccessURL = ConfigurationManager.AppSettings["UpgradeAccountSuccessURL"];
            string UpgradeAccountFailedURL = ConfigurationManager.AppSettings["UpgradeAccountFailedURL"];
            string UpgradeAccountpaypalemail = ConfigurationManager.AppSettings["UpgradeAccountpaypalemail"];
            string userId = objUser.Id.ToString();

            string pay = payme.PayWithPayPal(amount, plan, UserName, "", EmailId, "USD", UpgradeAccountpaypalemail, UpgradeAccountSuccessURL,
                                     UpgradeAccountFailedURL, UpgradeAccountSuccessURL, "", "", userId);

            return Content(pay);
        }

        public ActionResult UpgradeAccountSuccessful()
        {
            User objUser = (User)Session["User"];
            string paidamount = (string)Session["PaymentAmount"];
            string accountType = (string)Session["AccountType"];
            objUser.PaymentStatus = "Paid";
            objUser.AccountType = accountType;
            Api.User.User ApiobjUser = new Api.User.User();
            Api.PaymentTransaction.PaymentTransaction objApiPaymentTransaction = new Api.PaymentTransaction.PaymentTransaction();
            if (DateTime.Compare(objUser.ExpiryDate, DateTime.Now) > 0)
            {
                objUser.ExpiryDate = objUser.ExpiryDate.AddDays(30);
            }
            else
            {
                objUser.ExpiryDate = DateTime.Now.AddDays(30);
            }
            int i = ApiobjUser.UpdateUserAccountInfoByUserId(objUser.Id.ToString(), objUser.AccountType, objUser.ExpiryDate, objUser.PaymentStatus);
            string res_PaymentTransaction = objApiPaymentTransaction.SavePayPalTransaction(objUser.Id.ToString(), paidamount);
            Session["Paid_User"] = "Paid";
            return RedirectToAction("Index", "Home");
        }


    }
}
