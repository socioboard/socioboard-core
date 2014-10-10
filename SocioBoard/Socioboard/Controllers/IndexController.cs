using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;
using Facebook;
using System.Web.Script.Serialization;
using log4net;
using System.IO;

namespace Socioboard.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Default/
        ILog logger = LogManager.GetLogger(typeof(IndexController));

        public ActionResult Index()
        {
            logger.Error("Abhay");
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        public ActionResult AjaxLogin(string username, string password)
        {
            string returnmsg = string.Empty;
            User objUser = new User();
            string uname = Request.QueryString["username"].ToString();
            string pass = Request.QueryString["password"].ToString();

            Api.User.User obj = new Api.User.User();
            string logindata = obj.Login(uname, pass);
            string str = logindata.Replace("\"", string.Empty).Trim();
            if (str != "Not Exist")
            {
                objUser = (User)(new JavaScriptSerializer().Deserialize(logindata, typeof(User)));
            }
            else
            {
                objUser = null;
                returnmsg = "Invalid Email or Password";
                return Content(returnmsg);
            }

            #region Unused code
            //JObject profile = JObject.Parse(obj.Login(uname, pass));
            //objUser.Id = Guid.Parse(profile["Id"].ToString());
            //objUser.UserName = profile["UserName"].ToString();
            //objUser.AccountType = profile["AccountType"].ToString();
            //objUser.ProfileUrl = profile["ProfileUrl"].ToString();
            //objUser.EmailId = profile["EmailId"].ToString();
            //objUser.CreateDate = Convert.ToDateTime(profile["CreateDate"].ToString());
            //objUser.ExpiryDate = Convert.ToDateTime(profile["ExpiryDate"].ToString());
            //objUser.UserStatus = Convert.ToInt16(profile["UserStatus"].ToString());
            //objUser.Password = profile["Password"].ToString();
            //objUser.TimeZone = profile["TimeZone"].ToString();
            //objUser.PaymentStatus = profile["PaymentStatus"].ToString();
            //objUser.ActivationStatus = profile["ActivationStatus"].ToString();
            //objUser.CouponCode = profile["CouponCode"].ToString();
            //objUser.ReferenceStatus = profile["ReferenceStatus"].ToString();
            //objUser.RefereeStatus = profile["RefereeStatus"].ToString();
            //objUser.UserType = profile["UserType"].ToString(); 
            #endregion

            if (objUser != null)
            {
                Session["User"] = objUser;
                returnmsg = "user";
                #region Count Used Accounts
                try
                {
                    Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                    Session["ProfileCount"] = Convert.ToInt16(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                    Session["TotalAccount"] = Convert.ToInt16(SBUtils.GetUserPackageProfileCount(objUser.AccountType));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                #endregion
            }
            return Content(returnmsg);
        }

        public ActionResult Download()
        {

            return View();
        }

        public ActionResult Contact()
        {

            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Pricing()
        {
            PricingModelHelper objPricingModelHelper_Basic = new PricingModelHelper("Basic", "FREE", "Every plan is a unique package. This one fits for individuals.", "Comprehensive Dashboard", null);
            PricingModelHelper objPricingModelHelper_Standard = new PricingModelHelper("Standard", "$4.99 Per User/Month", "Comprises of great tools suitable for small teams.", "Smart Inbox", null);
            PricingModelHelper objPricingModelHelper_Premium = new PricingModelHelper("Premium", "$9.99 Per User/Month", "Package you need to efficiently manage an expanding social media network.", "All Standard Plan Features", null);
            PricingModelHelper objPricingModelHelper_Deluxe = new PricingModelHelper("Deluxe", "$19.99 Per User/Month", "Includes sophisticated tools for complex objectives", "All Basic, Standard & Deluxe Features", null);
            PricingModelHelper objPricingModelHelper_SocioBasic = new PricingModelHelper("Socio Basic", "$29.99 Per User/Month", "Includes sophisticated tools for complex objectives", "All Basic, Standard & Deluxe Features", null);
            PricingModelHelper objPricingModelHelper_SocioStandard = new PricingModelHelper("Socio Standard", "$49.99 Per User/Month", "Includes sophisticated tools for complex objectives", "All Basic, Standard & Deluxe Features", null);
            PricingModelHelper objPricingModelHelper_SocioPremium = new PricingModelHelper("Socio Premium", " $79.99 Per User/Month", "Includes sophisticated tools for complex objectives", "All Basic, Standard & Deluxe Features", null);
            PricingModelHelper objPricingModelHelper_SocioDeluxe = new PricingModelHelper("Socio Deluxe", " $99.99 Per User/Month", "Includes sophisticated tools for complex objectives", "All Basic, Standard & Deluxe Features", null);

            if (Session["login"] != null)
            {
                ViewBag.standardplanHRef = "NetworkLogin.aspx?type=" + AccountType.Standard.ToString();
                ViewBag.deluxeplanHRef = "NetworkLogin.aspx?type=" + AccountType.Deluxe.ToString();
                ViewBag.premiumplanHRef = "NetworkLogin.aspx?type=" + AccountType.Premium.ToString();
                ViewBag.freePlanHRef = "NetworkLogin.aspx?type=" + AccountType.Free.ToString();
            }
            else if (Session["AjaxLogin"] != null)
            {
                ViewBag.standardplanHRef = "Home.aspx?type=" + AccountType.Standard.ToString(); ;
                ViewBag.deluxeplanHRef = "Home.aspx?type=" + AccountType.Deluxe.ToString();
                ViewBag.premiumplanHRef = "Home.aspx?type=" + AccountType.Premium.ToString();
                ViewBag.freePlanHRef = "Home.aspx?type=" + AccountType.Free.ToString();
                Session["AjaxLogin"] = null;
            }
            else
            {
                ViewBag.standardplanHRef = "/Default/Registration?type=" + AccountType.Standard.ToString();
                ViewBag.deluxeplanHRef = "/Default/Registration?type=" + AccountType.Deluxe.ToString();
                ViewBag.premiumplanHRef = "/Default/Registration?type=" + AccountType.Premium.ToString();
                ViewBag.freePlanHRef = "/Default/Registration?type=" + AccountType.Free.ToString();
            }
            return View("_PricingPartial", new PricingModelHelper[] { objPricingModelHelper_Basic, objPricingModelHelper_Standard, objPricingModelHelper_Premium, objPricingModelHelper_Deluxe, objPricingModelHelper_SocioBasic, objPricingModelHelper_SocioStandard, objPricingModelHelper_SocioPremium, objPricingModelHelper_SocioDeluxe });
        }
        public ActionResult Registration()
        {

            return View();
        }
        public ActionResult Signup()
        {
            Domain.Socioboard.Domain.User user = new Domain.Socioboard.Domain.User();
            Session["AjaxLogin"] = "register";
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                string line = "";
                line = sr.ReadToEnd();
                JObject jo = JObject.Parse(line);
                user.PaymentStatus = "unpaid";
                //if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                //{
                //    user.AccountType = Request.QueryString["type"];

                //    ViewBag.DropDownList1 = Request.QueryString["type"];
                //}
                //else
                //{
                //    user.AccountType = "Free";
                //}
                user.AccountType = Server.UrlDecode((string)jo["package"]);
                user.CreateDate = DateTime.Now;
                user.ExpiryDate = DateTime.Now.AddMonths(1);
                user.Id = Guid.NewGuid();
                user.UserName = Server.UrlDecode((string)jo["firstname"]) + " " + Server.UrlDecode((string)jo["lastname"]);
                user.EmailId = Server.UrlDecode((string)jo["email"]);
                user.Password = Server.UrlDecode((string)jo["password"]);
                user.UserStatus = 1;
                string firstName = Server.UrlDecode((string)jo["firstname"]);
                string lastName = Server.UrlDecode((string)jo["lastname"]);
                Api.User.User objApiUser = new Api.User.User();
                string res_Registration = objApiUser.Register(user.EmailId, user.Password, user.AccountType, user.UserName);
                if (user != null)
                {
                    Session["User"] = user;
                }
                return Content("user");

            }
            catch (Exception ex)
            {
                //logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            return View("_RegistrationPartial");
        }

        public ActionResult Company()
        {

            return View();
        }

        public ActionResult Features()
        {
            return View();
        }
        public ActionResult LoadRegistration(string teamid)
        {
            string ss = Request.QueryString["teamid"];
            User objUser = (User)Session["User"];
            if (teamid!=null)
          {
              objUser = new Domain.Socioboard.Domain.User();
              Api.Team.Team ApiobjTeam = new Api.Team.Team();
              Team objuserinfo = (Team)(new JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamById(teamid), typeof(Team)));
              objUser.EmailId = objuserinfo.EmailId;
          }
            return View("_RegistrationPartial", objUser);
        }


        public ActionResult SendMail(string name, string lname, string email, string Subject, string profile)
        {
            string Body = "Name: " + name + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile + "</br>";
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = ApiobjMailSender.SendMail(name, lname, email, Subject, profile);
            return Content(mailsender);
        }
        //vikash
        public ActionResult SendCareerMail(string fname, string lname, string email, string Subject, string profile)
        {
            var fi = Request.Files["file"];
            string file = string.Empty;
            var path = Server.MapPath("~/Themes/" + System.Configuration.ConfigurationManager.AppSettings["domain"] + "Contents/img/upload");
            file = path + "/" + fi.FileName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            fi.SaveAs(file);
            path = path + "/" + fi.FileName;
            string Body = "Name: " + fname + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile + "</br>";
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = ApiobjMailSender.SendCareerMail(fname, lname, email, Subject, profile, file, fi.FileName, fi.ContentType);
            return Content(mailsender);
        }
        
    }
}
