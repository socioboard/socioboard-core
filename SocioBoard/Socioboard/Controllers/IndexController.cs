using Newtonsoft.Json.Linq;
using System;
using System.Web.Mvc;
using Domain.Socioboard.Domain;
using Socioboard.Helper;
using System.Web.Script.Serialization;
using log4net;
using System.IO;
using System.Web;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Security;
using System.Collections.Generic;

namespace Socioboard.Controllers
{
    public class IndexController : BaseController
    {
        //
        // GET: /Default/
        ILog logger = LogManager.GetLogger(typeof(IndexController));
       // [OutputCache(Duration = 604800)]
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                if (!string.IsNullOrEmpty(((User)Session["User"]).EmailId) && !string.IsNullOrEmpty(((User)Session["User"]).Password))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["User"] = null;
                }
            }
            return View();
           
        }

        public ActionResult Logout() 
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AjaxLogin(string username, string password)
        {
            Session.Clear();
            Session.RemoveAll();
            string returnmsg = string.Empty;
            User objUser = new User();
            string uname = Request.QueryString["username"].ToString();
            string pass = Request.QueryString["password"].ToString();
            Api.User.User obj = new Api.User.User();
            string logindata = obj.Login(uname, pass);
            string str = logindata.Replace("\"", string.Empty).Trim();
            if (str != "Not Exist" && !str.Equals("Email Not Exist"))
            {
                 objUser = (User)(new JavaScriptSerializer().Deserialize(logindata, typeof(User)));
                FormsAuthentication.SetAuthCookie(objUser.UserName, false);
                Socioboard.Helper.apiClientProvider ac = new Socioboard.Helper.apiClientProvider(System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"] + "/token");
                try
                {
                    Dictionary<string, string> re = await ac.GetTokenDictionary(username, password);
                    Session["access_token"] = re["access_token"];
                }
                catch (Exception e)
                {
                    objUser = null;

                    // Edited by Antima 

                    HttpCookie myCookie = new HttpCookie("logininfo" + uname.Trim());
                    myCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(myCookie);

                    returnmsg = "Invalid Email or Password";
                    return Content(returnmsg);
                }
            }
            else if (str.Equals("Email Not Exist")) 
            {
                returnmsg = "Sorry, Socioboard doesn't recognize that username.";
                return Content(returnmsg);
            }
            else
            {
                objUser = null;

                // Edited by Antima 

                HttpCookie myCookie = new HttpCookie("logininfo" + uname.Trim());
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);

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
            if (objUser.UserType != "SuperAdmin")
            {
                if (objUser != null)
                {
                    if (objUser.ActivationStatus == "1")
                    {
                        int daysremaining = 0;

                        daysremaining = (objUser.ExpiryDate.Date - DateTime.Now.Date).Days;
                        if (daysremaining > 0)
                        {
                            Session["User"] = objUser;
                            returnmsg = "user";
                            #region Count Used Accounts
                            try
                            {
                                Session["Paid_User"] = "Paid";
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
                        else
                        {
                            Session["User"] = objUser;
                            Session["Paid_User"] = "Unpaid";
                            returnmsg = "unpaid";
                        }
                    }
                    else if (objUser.ActivationStatus == "2")
                    {
                        returnmsg = "User Not Exist!";
                    }
                    else
                    {
                        returnmsg = "notactivated";
                    }
                }
            }
            else
            {
                returnmsg = "SuperAdmin";
                Session["User"] = objUser;
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
            Session["User"] = null;
            return View("_PricingPartial", new PricingModelHelper[] { objPricingModelHelper_Basic, objPricingModelHelper_Standard, objPricingModelHelper_Premium, objPricingModelHelper_Deluxe, objPricingModelHelper_SocioBasic, objPricingModelHelper_SocioStandard, objPricingModelHelper_SocioPremium, objPricingModelHelper_SocioDeluxe });
            
        }
        
        public ActionResult Registration()
        {
            return View();
        }
        
        public ActionResult Signup()
        {
            logger.Error("Register");
            User _user=(User)Session["User"];
            Domain.Socioboard.Domain.User user = new Domain.Socioboard.Domain.User();
            Session["AjaxLogin"] = "register";
            string retmsg = string.Empty;
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
                if (_user != null)
                {
                    if (_user.ActivationStatus == "1") //If Login from Facebook, then ActivationStatus would be 1, refer to FacebookManager Controller
                    {
                        user.ActivationStatus = "1";
                        user.SocialLogin = _user.SocialLogin;
                        user.ProfileUrl = _user.ProfileUrl;
                    }
                    else
                    {
                        user.ActivationStatus = "0";
                    }
                }
                else
                {
                    user.ActivationStatus = "0";
                }
                string firstName = Server.UrlDecode((string)jo["firstname"]);
                string lastName = Server.UrlDecode((string)jo["lastname"]);
                Api.User.User objApiUser = new Api.User.User();
                //string res_Registration = objApiUser.Register(user.EmailId, user.Password, user.AccountType, user.UserName, user.ActivationStatus);
                string res_Registration = string.Empty;
                if (Session["twitterlogin"] != null)
                {
                    if ((string)Session["twitterlogin"] == "twitterlogin")
                    {
                        res_Registration = objApiUser.RegisterbyTwitter(user.EmailId, user.Password, user.AccountType, user.UserName, user.SocialLogin, user.ProfileUrl, user.ActivationStatus);
                    }
                    else
                    {
                        res_Registration = objApiUser.Register(user.EmailId, user.Password, user.AccountType, user.UserName, user.ActivationStatus);
                    }
                }
                else
                {
                    res_Registration = objApiUser.Register(user.EmailId, user.Password, user.AccountType, user.UserName, user.ActivationStatus);
                }
                logger.Error("res_Registration: " + res_Registration);
                if (res_Registration != "Email Already Exists")
                {
                    if (user != null)
                    {
                        Api.User.User obj = new Api.User.User();
                        user = (User)(new JavaScriptSerializer().Deserialize(obj.Login(user.EmailId, user.Password), typeof(User)));
                        Session["User"] = user;
                        if (Session["fblogin"] != null)
                        {
                            string accesstoken = (string)Session["AccesstokenFblogin"];
                            Api.Facebook.Facebook objfacebook = new Api.Facebook.Facebook();
                            Api.Groups.Groups objgroup = new Api.Groups.Groups();
                            //Domain.Socioboard.Domain.Groups group = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(objgroup.GetGroupDetailsByUserId(user.Id.ToString()), typeof(Domain.Socioboard.Domain.Groups)));
                            Groups obpgrp = (Groups)(new JavaScriptSerializer().Deserialize(objgroup.GetGroupDeUserId(user.Id.ToString()), typeof(Groups)));
                            objfacebook.AddFacebookAccountWithloginAsync(accesstoken, user.Id.ToString(), obpgrp.Id.ToString());
                        }
                        if (Session["googlepluslogin"] != null)
                        {
                            string accesstoken = (string)Session["AccesstokenFblogin"];
                            Api.Youtube.Youtube objYoutube = new Api.Youtube.Youtube();
                            Api.Groups.Groups objgroup = new Api.Groups.Groups();
                            //Domain.Socioboard.Domain.Groups group = (Domain.Socioboard.Domain.Groups)(new JavaScriptSerializer().Deserialize(objgroup.GetGroupDetailsByUserId(user.Id.ToString()), typeof(Domain.Socioboard.Domain.Groups)));
                            Groups grp = (Groups)(new JavaScriptSerializer().Deserialize(objgroup.GetGroupDeUserId(user.Id.ToString()), typeof(Groups)));
                            objYoutube.AddYoutubeAccountwithLoginAsync(ConfigurationManager.AppSettings["YtconsumerKey"], ConfigurationManager.AppSettings["YtconsumerSecret"], ConfigurationManager.AppSettings["Ytredirect_uri"], user.Id.ToString(), grp.Id.ToString(), accesstoken);
                        }
                        retmsg = "user";
                    }

                    //Domain.Socioboard.Domain.Invitation _Invitation = (Domain.Socioboard.Domain.Invitation)Session["InvitationInfo"];
                    Api.Invitation.Invitation ApiInvitation = new Api.Invitation.Invitation();
                    if (Session["InvitationCode"] != null)
                    {
                        string invitationcode = Session["InvitationCode"].ToString();
                        ApiInvitation.AddInvitationInfoBycode(invitationcode, user.EmailId, user.Id.ToString());
                    }
                    
                    //if (_Invitation != null)
                    //{
                    //    if (user.EmailId == _Invitation.FriendEmail)
                    //    {
                    //        string ret = ApiInvitation.UpdateInvitatoinStatus(_Invitation.Id.ToString(), user.Id.ToString());
                    //    }
                    //}

                }
                else
                {
                    retmsg = "Email Already Exists";
                }
                //return Content(retmsg);

            }
            catch (Exception ex)
            {
                //logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
            //return View("_RegistrationPartial");
            
            
           
            return Content(retmsg);
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
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];
            if (!String.IsNullOrEmpty(teamid))
            {
                objUser = new Domain.Socioboard.Domain.User();
                Api.Team.Team ApiobjTeam = new Api.Team.Team();
                Team objuserinfo = (Team)(new JavaScriptSerializer().Deserialize(ApiobjTeam.GetTeamById(teamid), typeof(Team)));
                objUser.EmailId = objuserinfo.EmailId;
            }
            return PartialView("_RegistrationPartial", objUser);
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

        public ActionResult RemainingDays()
        {
            int daysremaining = 0;
            string remainingday = string.Empty;
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)Session["User"];

            if (Session["days_remaining"] == null)
            {
                if (objUser.PaymentStatus == "unpaid" && objUser.AccountType != "Free")
                {
                    daysremaining = (objUser.ExpiryDate.Date - DateTime.Now.Date).Days;
                    if (daysremaining < 0)
                    {
                        daysremaining = -1;
                    }
                    Session["days_remaining"] = daysremaining;
                    if (daysremaining <= -1)
                    {
                    }
                    else if (daysremaining == 0)
                    {
                        remainingday = "Your trial " + objUser.AccountType.ToString() + " account will expire Today please upgrade to paid plan.";
                    }
                    else
                    {
                        remainingday = "Your trial " + objUser.AccountType.ToString() + " account will expire in " + daysremaining + " days, please upgrade to paid plan.";

                    }
                }
            }

            return Content(remainingday);
        }

        // Edited by Antima[1/11/2014]

        public ActionResult UserActivationByEmail(string email)
        {
            Api.User.User obj = new Api.User.User();
            Domain.Socioboard.Domain.User objUser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(obj.getUserInfoByEmail(email), typeof(Domain.Socioboard.Domain.User)));
            string Email = email;
            string UserId = objUser.Id.ToString();
            ViewBag.Email = Email;
            ViewBag.UserId = UserId;
            return View("UserActivationByEmail");
        }

        [HttpPost]
        public ActionResult UserActivation()
        {
            return Content("Success");
        }

        public ActionResult SendRegistrationMail(string emailId)
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
                var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_RegistrationMailPartial", objuser);
                string Subject = "Thanks for creating your " + ConfigurationManager.AppSettings["DefaultGroupName"].ToString() + " Account";

                mailsender = ApiobjMailSender.SendChangePasswordMail(emailId, mailBody, Subject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            User _user = (User)Session["User"];
            if (_user != null && _user.ActivationStatus == "1")
            {
                mailsender += ">> Facebook Registration";
            }

            return Content(mailsender);
        }
        [AllowAnonymous]
        public ActionResult ActivateAccount(string Id)
        {
            string ActivationStatus = "1";
            Api.User.User objuser = new Api.User.User();
            int IsAccountActivated = objuser.UpdateUsertoactivate(Id, ActivationStatus);
            if (IsAccountActivated == 1)
            {
                ViewBag.Isaccountactivatedfirsttime = "true";
                return View("Index");
            }
            else
            {
                return null;
            }

        }

        // Edited by Antima[8/11/2014]

        public ActionResult PaypalPage()
        {
            string pay = "";
            try
            {
                Helper.Payment payme = new Payment();

                string amount = "100";
                string plantype = "Professional Installation";
                string UserName = "Socioboard";
                string EmailId = "support@socioboard.com";

                string DownloadSuccessURL = ConfigurationManager.AppSettings["DownloadSuccessURL"];
                string DownloadFailedURL = ConfigurationManager.AppSettings["DownloadFailedURL"];
                string Downloadpaypalemail = ConfigurationManager.AppSettings["Downloadpaypalemail"];
                string userId = "";

                pay = payme.PayWithPayPal(amount, plantype, UserName, "", EmailId, "USD", Downloadpaypalemail, DownloadSuccessURL,
                                        DownloadFailedURL, DownloadSuccessURL, "", "", userId);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            //return View();
             Response.Redirect(pay);
             return Content("");
        }

        public ActionResult PaymentSuccessful()
        {
            return View();
        }

        public ActionResult PaymentFailed()
        {
            return View();
        }

        public ActionResult SendRequestForDemo(string fname, string lname, string email, string Skype, string Subject, string company, string phone, string notes)
        {
            string Body = "Name: " + fname + " " + lname + "</br>" + "Email: " + email + "</br>" + "Skype Id: " + Skype + "</br>" + "Company: " + company + "</br>" + "Phone Number: " + phone + "</br>" + "Message: " + notes + "</br>";
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = ApiobjMailSender.SendRequestForDemo(fname, lname, email, Subject, Body);
            return Content(mailsender);
        }

        // Edited by Antima[24/11/2014]

        public ActionResult SendVideoMail(string EmailId)
        {
            Api.User.User ApiobjUser = new Api.User.User();
            Domain.Socioboard.Domain.User objuser = new Domain.Socioboard.Domain.User();
            try
            {
                objuser = (Domain.Socioboard.Domain.User)(new JavaScriptSerializer().Deserialize(ApiobjUser.getUserInfoByEmail(EmailId), typeof(Domain.Socioboard.Domain.User)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
           
            Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
            string mailsender = "";
            try
            {
                var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_VideoMailPartial", objuser);
                string Subject = "Enjoy Video Mailing through " + ConfigurationManager.AppSettings["DefaultGroupName"].ToString();

                mailsender = ApiobjMailSender.SendChangePasswordMail(EmailId, mailBody, Subject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            return Content(mailsender);
        }

        // Edited by Antima[09/12/2014]

        public ActionResult PaypalAgency(string PlanType)
        {
            string pay = "";
            try
            {
                Helper.Agency agency = new Agency();
                Helper.Payment payme = new Payment();
               
                agency.AgencyPlan(PlanType);
                string amount = agency.amount;
                string plantype = agency.plantype;
                string UserName = agency.UserName;
                string EmailId = agency.EmailId;
                string userId = agency.userId;

                 //pay = payme.PayWithPayPal(amount, plantype, UserName, "", EmailId, "USD", ConfigurationManager.AppSettings["Downloadpaypalemail"], ConfigurationManager.AppSettings["DownloadSuccessURL"],
                 //                     ConfigurationManager.AppSettings["DownloadFailedURL"], ConfigurationManager.AppSettings["DownloadSuccessURL"], ConfigurationManager.AppSettings["EnterPrisecancelurl"], ConfigurationManager.AppSettings["EnterPrisenotifyurl"], userId);

                pay = payme.PayWithPayPal(amount, plantype, UserName, "", EmailId, "USD", ConfigurationManager.AppSettings["Downloadpaypalemail"], ConfigurationManager.AppSettings["DownloadSuccessURL"],
                                    ConfigurationManager.AppSettings["DownloadFailedURL"], ConfigurationManager.AppSettings["DownloadSuccessURL"],"","", userId);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
             return Content(pay);
        }

        public ActionResult Privacy() 
        {
            return View("privacy");
        }
        public ActionResult Disclaimer() 
        {
            return View();
        }

        public ActionResult JoinSocioboard()
        {
            return View("OpenSource");
        }
    }
}
