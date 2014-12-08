using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Socioboard.Helper;

namespace Socioboard.Controllers
{
    public class ForgotPasswordController : Controller
    {
        //
        // GET: /ForgotPassword/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return PartialView("_ForgetPasswordPartial");
        }

        public ActionResult SendFogotPassMail(string emailId)
        {
            Api.User.User ApiobjUser = new Api.User.User();
            Domain.Socioboard.Domain.User objuser = new Domain.Socioboard.Domain.User();
            try
            {
                objuser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(emailId), typeof(Domain.Socioboard.Domain.User)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = "";
            try
            {
                //Generate a random unique string
                string strRandomUnique = SBUtils.GenerateRandomUniqueString();

                //Update random unique string in User
                string res_UpdateChangePasswordKey = ApiobjUser.UpdateForgetPasswordKey(objuser.Id.ToString(), strRandomUnique);

                if (res_UpdateChangePasswordKey=="1")
                {
                    ViewBag.ForgetPasswordKey = strRandomUnique;
                }

                var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_ForgotPasswordMailBodyPartial", objuser);
                string Subject = "Forget password Socioboard Account";

                mailsender = ApiobjMailSender.SendChangePasswordMail(emailId, mailBody, Subject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(mailsender);
        }

        public ActionResult ResetPassword(string ForgetPasswordKey)
        {
            return PartialView("_ResetPasswordPartial", ForgetPasswordKey);
        }

        public ActionResult SendResetPasswordMail(string emailId, string Password)
        {
            Api.User.User ApiobjUser = new Api.User.User();
            
            Domain.Socioboard.Domain.User objuser = new Domain.Socioboard.Domain.User();

            string mailsender = "";

            try
            {
                objuser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(emailId), typeof(Domain.Socioboard.Domain.User)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            string changedpassword = Helper.SBUtils.MD5Hash(Password);

            string IsPasswordReset = (string)new JavaScriptSerializer().Deserialize(ApiobjUser.ResetPassword(objuser.Id, changedpassword), typeof(string));
            if (IsPasswordReset == "1")
            {

                objuser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(emailId), typeof(Domain.Socioboard.Domain.User)));
                try
                {
                    Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();

                    try
                    {
                        objuser.Password = Password;
                        var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_ResetPasswordMailBodyPartial", objuser);
                        string Subject = "New password for your Socioboard Account";

                        mailsender = ApiobjMailSender.SendChangePasswordMail(emailId, mailBody, Subject);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            return Content(mailsender);
        }

    }
}
