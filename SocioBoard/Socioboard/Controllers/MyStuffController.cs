using Socioboard.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using System.Web.Script.Serialization;


namespace Socioboard.Controllers
{
    public class MyStuffController : BaseController
    {
        //
        // GET: /Ewallet/

        public ActionResult Ewallet()
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
            User _User = (User)Session["User"];
            Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation(); 
            Api.Affiliates.Affiliates ApiAffiliates = new Api.Affiliates.Affiliates();
            List<Domain.Socioboard.Domain.Invitation> lstInvite = (List<Domain.Socioboard.Domain.Invitation>)(new JavaScriptSerializer().Deserialize(ApiInvitation.GetInvitedDataOfAcceotedUser(_User.Id.ToString()), typeof(List<Domain.Socioboard.Domain.Invitation>)));
            return PartialView("_EwalletActivityPartial", lstInvite);
        }

        //public ActionResult LoadEwalletPartial()
        //{
        //    User _User = (User)Session["User"];
        //    Api.PaymentTransaction.PaymentTransaction ApiPaymentTransaction = new Api.PaymentTransaction.PaymentTransaction();
        //    Api.Affiliates.Affiliates ApiAffiliates = new Api.Affiliates.Affiliates(); 
        //    List<Domain.Socioboard.Domain.PaymentTransaction> lsttransactions=(List<Domain.Socioboard.Domain.PaymentTransaction>)(new JavaScriptSerializer().Deserialize(ApiPaymentTransaction.GetPaymentDataByUserId(_User.Id.ToString()),typeof(List<Domain.Socioboard.Domain.PaymentTransaction>)));
        //    List<Domain.Socioboard.Domain.Affiliates> lstAffiliates=(List<Domain.Socioboard.Domain.Affiliates>)(new JavaScriptSerializer().Deserialize(ApiAffiliates.GetAffilieteDetailbyUserIdTrans(_User.Id.ToString()),typeof(List<Domain.Socioboard.Domain.Affiliates>)));

        //    ViewBag.lsttransactions = lsttransactions;
        //    ViewBag.lstAffiliates = lstAffiliates;

        //    return PartialView("_EwalletPartial",_User);
        //}

        public ActionResult RechrgeEwalletByPaypal(string amount)
        {
            string pay = string.Empty;
            Api.User.User ApiobjUser = new Api.User.User();
            User objUser = (User)Session["User"];
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

        public ActionResult RechrgeEwalletSuccessful()
        {
            Api.User.User ApiobjUser = new Api.User.User();
            User objUser = (User)Session["User"];
            string RechargeAmount = Session["RechrgeAmount"].ToString();
            objUser.Ewallet = (float.Parse(objUser.Ewallet) + float.Parse(RechargeAmount)).ToString();
            string IsUpdated = ApiobjUser.UpdateEwalletAmount(objUser.Id.ToString(),objUser.Ewallet);
            Session["User"] = objUser;
            return RedirectToAction("Ewallet", "MyStuff");
        }
        public ActionResult InviteFriendByEmail(string EmailId)
        {
            string ret = string.Empty;
            User _User=(User)Session["User"];
            Api.User.User ApiUser = new Api.User.User();
            Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
            //if (ApiUser.CheckEmailId(EmailId) == "NotExist")
            //{
                //if (!ApiInvitation.IsFriendAlreadydInvited(_User.Id.ToString(), EmailId))
                //{
                    ret = ApiInvitation.SendInvitationMail(_User.EmailId, _User.UserName, EmailId);
                //}
                //else
                //{
                //    ret = EmailId + " is already Invited";
                //}
            //}
            //else {
            //    ret = EmailId + " is already registered";
            //}
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
            User _User = (User)Session["User"];
            Api.User.User Apiuser = new Api.User.User();
            User NewUser =(User)new JavaScriptSerializer().Deserialize(Apiuser.getUsersById(FriendId),typeof(User));
            List<Affiliates> lstAffiliate = SBUtils.GetAffiliatesData(_User.Id, Guid.Parse(FriendId));
            ViewBag.FriendsEmail = NewUser.EmailId;
            return PartialView("_SummaryPartial",lstAffiliate);
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


    }
}
