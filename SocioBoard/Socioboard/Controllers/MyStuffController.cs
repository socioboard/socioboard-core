using Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;
using System.Globalization;


namespace Socioboard.Controllers
{
    public class MyStuffController : BaseController
    {
        //
        // GET: /Ewallet/

        public ActionResult Ewallet()
        {
            if (Session["User"] != null)
            {
                if (Session["Paid_User"] != null && Session["Paid_User"].ToString() == "Unpaid")
                {
                    return RedirectToAction("Billing", "PersonalSetting");
                }
                else
                {
                    return View("Ewallet");
                }
            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult Affiliates()
        {
            if (Session["Paid_User"] != null && Session["Paid_User"].ToString() == "Unpaid")
            {
                return RedirectToAction("Billing", "PersonalSetting");
            }
            else
            {
                User _User = (User)Session["User"];
                Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
                List<Domain.Socioboard.Domain.Invitation> lstInvite = (List<Domain.Socioboard.Domain.Invitation>)(new JavaScriptSerializer().Deserialize(ApiInvitation.GetAllInvitedDataOfUser(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.Invitation>)));
                return View("Affiliates", lstInvite);
            }
        }

        public ActionResult LoadEwalletActivityPartial()
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            User _User = (User)Session["User"];
            Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
            Api.Affiliates.Affiliates ApiAffiliates = new Api.Affiliates.Affiliates();
            List<Domain.Socioboard.Domain.Invitation> lstInvite = (List<Domain.Socioboard.Domain.Invitation>)(new JavaScriptSerializer().Deserialize(ApiInvitation.GetInvitedDataOfAcceotedUser(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.Invitation>)));
            return PartialView("_EwalletActivityPartial", lstInvite);
        }

        public ActionResult LoadEwalletPartial()
        {
            string datetime = string.Empty;
            try
            {
                 datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
                //datetime = Request.Form["localtime"].ToString();
            }
            catch (Exception ex)
            {
                 datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
                //datetime = TempData["localtime"].ToString();
            }
            ViewBag.datetime = datetime;
            Api.User.User ApiobjUser = new Api.User.User();
            User _User = (User)Session["User"];
            _User = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUsersById(_User.Id.ToString(), Session["access_token"].ToString()), typeof(User)));
            Api.PaymentTransaction.PaymentTransaction ApiPaymentTransaction = new Api.PaymentTransaction.PaymentTransaction();
            Api.Affiliates.Affiliates ApiAffiliates = new Api.Affiliates.Affiliates();
            List<Domain.Socioboard.Domain.PaymentTransaction> lsttransactions = (List<Domain.Socioboard.Domain.PaymentTransaction>)(new JavaScriptSerializer().Deserialize(ApiPaymentTransaction.GetPaymentDataByUserId(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.PaymentTransaction>)));
            List<Domain.Socioboard.Domain.Affiliates> lstAffiliates = (List<Domain.Socioboard.Domain.Affiliates>)(new JavaScriptSerializer().Deserialize(ApiAffiliates.GetAffilieteDetailbyUserIdTrans(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.Affiliates>)));
            List<Domain.Socioboard.Domain.EwalletWithdrawRequest> lstEwalletWithdrawRequest = (List<Domain.Socioboard.Domain.EwalletWithdrawRequest>)(new JavaScriptSerializer().Deserialize(ApiAffiliates.GetEwalletWithdraw(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.EwalletWithdrawRequest>)));
            ViewBag.lsttransactions = lsttransactions;
            ViewBag.lstAffiliates = lstAffiliates;
            ViewBag.lstEwalletWithdrawRequest = lstEwalletWithdrawRequest;
            return PartialView("_EwalletPartial", _User);
        }

        public ActionResult RechrgeEwalletByPaypal(string amount)
        {
            if (Session["User"] != null)
            {
                string pay = string.Empty;
                Api.User.User ApiobjUser = new Api.User.User();
                User objUser = (User)Session["User"];
                objUser = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUsersById(objUser.Id.ToString(), Session["access_token"].ToString()), typeof(User)));
                Helper.Payment payme = new Payment();
                Session["RechrgeAmount"] = amount;
                string UpgradeAccountSuccessURL = ConfigurationManager.AppSettings["RechargeEwalletSuccessURL"];
                string UpgradeAccountFailedURL = ConfigurationManager.AppSettings["RechargeEwalletFailedURL"];
                string UpgradeAccountpaypalemail = ConfigurationManager.AppSettings["RechargeEwalletpaypalemail"];
                string userId = objUser.Id.ToString();
                string UserName = objUser.UserName;
                string EmailId = objUser.EmailId;
                pay = payme.PayWithPayPal(amount, "Recharge Ewallet", UserName, "", EmailId, "USD", UpgradeAccountpaypalemail, UpgradeAccountSuccessURL,
                                            UpgradeAccountFailedURL, UpgradeAccountSuccessURL, "", "", userId);
                //Response.Redirect(pay);
                return Content(pay);
            }
            else {
                return RedirectToAction("Index", "Index");
            }
        }

        public ActionResult RechrgeEwalletSuccessful()
        {
            Api.PaymentTransaction.PaymentTransaction ApiPaymentTransaction = new Api.PaymentTransaction.PaymentTransaction();
            Api.User.User ApiobjUser = new Api.User.User();
            User objUser = (User)Session["User"];
            objUser = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUsersById(objUser.Id.ToString(), Session["access_token"].ToString()), typeof(User)));
            try
            {
                if (Session["RechrgeAmount"] != null)
                {
                    string RechargeAmount = Session["RechrgeAmount"].ToString();
                    objUser.Ewallet = (float.Parse(objUser.Ewallet) + float.Parse(RechargeAmount)).ToString();
                    string IsUpdated = ApiobjUser.UpdateEwalletAmount(objUser.Id.ToString(), objUser.Ewallet, Session["access_token"].ToString());
                    Session["User"] = objUser;
                    ApiPaymentTransaction.SavePayPalTransaction(objUser.Id.ToString(), RechargeAmount);
                }
            }
            catch (Exception ex)
            {

            }
            Session["RechrgeAmount"] = null;
            return RedirectToAction("Ewallet", "MyStuff");
        }

        public ActionResult InviteFriendByEmail(string EmailId)
        {
            string ret = string.Empty;
            User _User = (User)Session["User"];
            Api.User.User ApiUser = new Api.User.User();
            Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
            if (ApiUser.CheckEmailId(EmailId) == "NotExist")
            {
                //if (!ApiInvitation.IsFriendAlreadydInvited(_User.Id.ToString(), EmailId))
                //{
                ret = ApiInvitation.SendInvitationMail(_User.EmailId, _User.UserName, EmailId);
                //}
                //else
                //{
                //    ret = EmailId + " is already Invited";
                //}
            }
            else
            {
                ret = EmailId + " is already registered";
            }
            return Content(ret);
        }

        public ActionResult AffiliateSystem(string code)
        {
            //    Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
            //    Domain.Socioboard.Domain.Invitation _Invitation=(Domain.Socioboard.Domain.Invitation)(new JavaScriptSerializer().Deserialize(ApiInvitation.GetInvitationInfoBycode(code),typeof(Domain.Socioboard.Domain.Invitation)));
            //    Session["InvitationInfo"] = _Invitation;
            //    Domain.Socioboard.Domain.User _User = new User();
            //    _User.EmailId = _Invitation.FriendEmail;
            //    _User.ActivationStatus = "1";
            //    Session["User"] = _User;
            Session["InvitationCode"] = code;
            return RedirectToAction("Registration", "Index");
        }

        public ActionResult PaymentFailed()
        {
            return RedirectToAction("Ewallet", "MyStuff");
        }

        public ActionResult OpenSummayModel(string FriendId)
        {
            string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
            //string datetime = Request.Form["localtime"].ToString();
            ViewBag.datetime = datetime;
            User _User = (User)Session["User"];
            Api.User.User Apiuser = new Api.User.User();
            User NewUser = (User)new JavaScriptSerializer().Deserialize(Apiuser.getUsersById(FriendId, Session["access_token"].ToString()), typeof(User));
            List<Affiliates> lstAffiliate = SBUtils.GetAffiliatesData(_User.Id, Guid.Parse(FriendId));
            ViewBag.FriendsEmail = NewUser.EmailId;
            return PartialView("_SummaryPartial", lstAffiliate);
        }

        public ActionResult SMSServices()
        {
            User _User = (User)Session["User"];
            Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
            List<Domain.Socioboard.Domain.Invitation> lstInvite = (List<Domain.Socioboard.Domain.Invitation>)(new JavaScriptSerializer().Deserialize(ApiInvitation.GetAllInvitedDataOfUser(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.Invitation>)));
            return View("Affiliates", lstInvite);
        }

        public ActionResult GetPublicToken()
        {
            User _User = (User)Session["User"];
            string PublicUrl = ConfigurationManager.AppSettings["DomainName"].ToString() + "/MyStuff/AffiliateSystem?code=" + _User.UserCode.ToString();
            return Content(PublicUrl);
        }

        public ActionResult RequestToWithdraw(string localtime)
        {
            if (Session["User"] != null)
            {
                string datetime = Helper.Extensions.ToClientTime(DateTime.UtcNow);
                TempData["localtime"] = datetime;
                Api.User.User ApiobjUser = new Api.User.User();
                User _User;
                _User = (User)Session["User"];
                _User = (User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUsersById(_User.Id.ToString(), Session["access_token"].ToString()), typeof(User)));
                string amount = Request.Form["Amount"];
                string paypalemail = Request.Form["PayPalEmail"];
                string ibancode = Request.Form["IbnaCode"];
                string swiftcode = Request.Form["SwiftCode"];
                string other = Request.Form["Other"];
                string paymentmethod = Request.Form["Method"];
                Api.Affiliates.Affiliates ApiobjAffiliates = new Api.Affiliates.Affiliates();
                if (Double.Parse(amount, CultureInfo.InvariantCulture.NumberFormat) <= Double.Parse(_User.Ewallet, CultureInfo.InvariantCulture.NumberFormat))
                {
                    _User = (User)(new JavaScriptSerializer().Deserialize(ApiobjAffiliates.AddRequestToWithdraw(amount, paymentmethod, paypalemail, ibancode, swiftcode, other, _User.Id.ToString()), typeof(User)));
                    Session["User"] = _User;
                    return RedirectToAction("LoadEwalletPartial", "MyStuff");
                }
                else
                {
                    Session["User"] = _User;
                    return Content("Amount_Exceeded");
                }

            }
            else
            {
                return RedirectToAction("Index", "Index");
            }
        }
    }
}
