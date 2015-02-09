using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Recaptcha;
using Recaptcha.Web;
using Recaptcha.Web.Mvc;

namespace Socioboard.Controllers
{
    public class VersionsController : BaseController
    {
        //
        // GET: /Versions/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Agency()
        {
            return View();
        }
        public ActionResult Enterprise()
        {
            return View();
        }
        public ActionResult SendMail(string name, string lname, string email, string Subject, string profile)
        {
            //string Body = "Name: " + name + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile +"</br>";
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = ApiobjMailSender.SendMail(name, lname, email, Subject, profile);
            return Content(mailsender);
        }

        [HttpPost]
        public ActionResult SendAgencyMail(string name, string lname, string email, string Company, string Message, string phone)
        {
            //string Body = "Name: " + name + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile +"</br>";
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = ApiobjMailSender.SendAgencyMail(name, lname, email, Company, Message, phone);
            return Content(mailsender);
        }

        [HttpPost]
        public async Task<ActionResult> Index(Socioboard.Helper.Enterprise ent)
        {

            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            string ret;
            if (String.IsNullOrEmpty(recaptchaHelper.Response))
            {
                ModelState.AddModelError("", "Captcha answer cannot be empty.");
                //return View(model);
                return View();
            }

            RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponseTaskAsync();

            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                ModelState.AddModelError("", "Incorrect captcha answer.");
                ViewBag.AlertMsg = "error";
                return RedirectToAction("Enterprise", "Versions", new { hint = "error" });

            }
            else
            {
                Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
                ret = ApiobjMailSender.SendEnterpriseMail(ent.Name, ent.Designation, ent.ContactEmailId, ent.Location, ent.Company, ent.CompanyWebsite, ent.Message, ent.Phone);

                return RedirectToAction("Enterprise", "Versions", new { hint = "success" });
                //return Content(ret);
                //return View();
            }


            // return View(model);
            //return View();
        }

        //[RecaptchaControlMvc.CaptchaValidator]
        //[HttpPost]
        public ActionResult SendEnterpriseMail(string name, string designation, string email, string location, string company, string companywebsite, string messages, string Phone, string captchaErrorMessage, string recaptcha_challenge_field, string recaptcha_response_field)
        {
            bool isCaptchaCodeValid = false;
            string mailsender = string.Empty;
            string CaptchaMessage = "";
            isCaptchaCodeValid = GetCaptchaResponse(out CaptchaMessage, recaptcha_challenge_field, recaptcha_response_field);
            //return RedirectToAction("success");

            if (isCaptchaCodeValid)
            {
                Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
                mailsender = ApiobjMailSender.SendEnterpriseMail(name, designation, email, location, company, companywebsite, messages, Phone);
               
            }
            else
            {
                mailsender = "Invalid Captcha";
            }
            return Content(mailsender);
        }

        private bool GetCaptchaResponse(out string message, string recaptcha_challenge_field, string recaptcha_response_field)
        {
            bool flag = false;
            message = "";

            string[] result;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.google.com/recaptcha/api/verify");
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 0x7530;
            request.Method = "POST";
            request.UserAgent = "reCAPTCHA/ASP.NET";
            request.ContentType = "application/x-www-form-urlencoded";
            string formData = string.Format(
                "privatekey={0}&remoteip={1}&challenge={2}&response={3}",
                new object[]{
            HttpUtility.UrlEncode("6Le3C_ESAAAAADUi4MjHaBqf2qPh1jvmj8IPelB2"),
            HttpUtility.UrlEncode(Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString()),
            HttpUtility.UrlEncode(recaptcha_challenge_field),
            HttpUtility.UrlEncode(recaptcha_response_field)
        });
            byte[] formbytes = Encoding.ASCII.GetBytes(formData);

            using (System.IO.Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formbytes, 0, formbytes.Length);

            }

            try
            {
                using (WebResponse httpResponse = request.GetResponse())
                {
                    using (System.IO.TextReader readStream = new System.IO.StreamReader(httpResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        result = readStream.ReadToEnd().Split(new string[] { "\n", @"\n" }, StringSplitOptions.RemoveEmptyEntries);
                        message = result[1];
                        flag = Convert.ToBoolean(result[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
            return flag;
        }

    }
}


