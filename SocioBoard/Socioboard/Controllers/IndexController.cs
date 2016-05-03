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
using System.Globalization;
using System.Threading;
using Socioboard.App_Start;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net.Http;

namespace Socioboard.Controllers
{
    public class IndexController : BaseController
    {

        public bool IsUserLogedIn()
        {
            bool IsUserLogedIn = false;
            if (((Session["User"] != null && Session["access_token"] != null)) || Session["fblogin"] != null || Session["addfbaccount"] != null || Session["googlepluslogin"] != null)
            {
                IsUserLogedIn = true;
            }
            return IsUserLogedIn;
        }

        //
        // GET: /Default/
        ILog logger = LogManager.GetLogger(typeof(IndexController));
        [OutputCache(Duration = 604800)]
        // [MyExpirePageActionFilter]
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

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["hint"]))
                {
                    return View("Error");
                }
            }
            catch { }
            return View();

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            if (Request.Cookies["referal_url"] != null)
            {
                Response.Cookies["referal_url"].Expires = DateTime.Now.AddDays(-1);
            }
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
            HttpResponseMessage response = await WebApiReq.GetReq("api/ApiUser/Login?EmailId=" + uname + "&PasswordHash=" + SBUtils.MD5Hash(pass), "", "");
            if (response.IsSuccessStatusCode)
            {
                objUser = await response.Content.ReadAsAsync<Domain.Socioboard.Domain.User>();
                if (objUser != null)
                {
                    Response.Cookies.Add(FormsAuthentication.GetAuthCookie(objUser.UserName, true));
                    // FormsAuthentication.SetAuthCookie(objUser.UserName, false);
                    Socioboard.Helper.apiClientProvider ac = new Socioboard.Helper.apiClientProvider(System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"] + "/token");
                    try
                    {
                        Dictionary<string, string> re = await ac.GetTokenDictionary(username, SBUtils.MD5Hash(pass));
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

                    if (objUser != null)
                    {
                        if (objUser.ActivationStatus == "1")
                        {
                            int daysremaining = 0;

                            daysremaining = (objUser.ExpiryDate.Date - DateTime.Now.Date).Days;
                            if (daysremaining > 0)
                            {
                                Session["User"] = objUser;
                                Session["group"] = await SBHelper.LoadGroups(objUser.Id);
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
                    if (objUser.UserType == "SuperAdmin")
                    {
                        returnmsg = "SuperAdmin";
                        Session["User"] = objUser;
                    }
                }
            }
            else
            {
                objUser = null;
                HttpCookie myCookie = new HttpCookie("logininfo" + uname.Trim());
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);
                returnmsg = "Invalid Email or Password";
                return Content(returnmsg);
            }
            return Content(returnmsg);
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> AjaxPluginLogin()
        {
            Session.Clear();
            Session.RemoveAll();

            string returnmsg = string.Empty;

            string uname = Request.Form["email"].ToString();
            string pass = Request.Form["password"].ToString();
            User objUser = new User();
            Api.User.User ApiUser = new Api.User.User();
            string logindata = ApiUser.Login(uname, pass);
            string str = logindata.Replace("\"", string.Empty).Trim();
            if (str != "Not Exist" && !str.Equals("Email Not Exist"))
            {
                objUser = (User)(new JavaScriptSerializer().Deserialize(logindata, typeof(User)));
                FormsAuthentication.SetAuthCookie(objUser.UserName, false);
                Socioboard.Helper.apiClientProvider ac = new Socioboard.Helper.apiClientProvider(System.Configuration.ConfigurationManager.AppSettings["ApiDomainName"] + "/token");
                try
                {
                    Dictionary<string, string> re = await ac.GetTokenDictionary(uname, SBUtils.MD5Hash(pass));
                    Session["access_token"] = re["access_token"];
                }
                catch (Exception e)
                {
                    returnmsg = "Invalid Email or Password";
                    return Content(returnmsg);
                }
            }
            else if (str.Equals("Email Not Exist"))
            {
                returnmsg = "Sorry, " + ConfigurationManager.AppSettings["domain"] + " doesn't recognize that username.";
                return Content(returnmsg);
            }
            else
            {
                returnmsg = "Invalid Email or Password";
                return Content(returnmsg);
            }

            if (objUser != null)
            {
                if (objUser.ActivationStatus == "1")
                {
                    int daysremaining = 0;
                    daysremaining = (objUser.ExpiryDate.Date - DateTime.Now.Date).Days;
                    Api.SocialProfile.SocialProfile apiobjSocialProfile = new Api.SocialProfile.SocialProfile();
                    #region Count Used Accounts
                    try
                    {
                        Session["ProfileCount"] = Convert.ToInt16(apiobjSocialProfile.GetAllSocialProfilesOfUserCount(objUser.Id.ToString()).ToString());
                        Session["TotalAccount"] = Convert.ToInt16(SBUtils.GetUserPackageProfileCount(objUser.AccountType));
                    }
                    catch (Exception ex)
                    {
                        Session["ProfileCount"] = 0;
                        Session["TotalAccount"] = 0;
                    }
                    #endregion
                    if (daysremaining > 0)
                    {
                        Session["User"] = objUser;
                        returnmsg = "user";
                        Session["Paid_User"] = "Paid";
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
            User _user = (User)Session["User"];
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

                user.AccountType = Server.UrlDecode((string)jo["package"]);
                user.CreateDate = DateTime.Now;
                user.ExpiryDate = DateTime.Now.AddDays(30);
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
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
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
                        //remainingday = "Your trial " + objUser.AccountType.ToString() + " account will expire Today please upgrade to paid plan.";
                        remainingday = objUser.AccountType.ToString() + "##" + daysremaining.ToString();
                    }
                    else
                    {
                        //remainingday = "Your trial " + objUser.AccountType.ToString() + " account will expire in " + daysremaining + " days, please upgrade to paid plan.";
                        remainingday = objUser.AccountType.ToString() + "##" + daysremaining.ToString();
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
                                    ConfigurationManager.AppSettings["DownloadFailedURL"], ConfigurationManager.AppSettings["DownloadSuccessURL"], "", "", userId);


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
        [HttpGet]
        public ActionResult SBApp(string profileType, string url, string content, string imageUrl, string name, string userImage, string screenName, string tweet, string tweetId, string type)
        {

            Domain.Socioboard.Helper.PluginData _PluginData = new Domain.Socioboard.Helper.PluginData();
            _PluginData.profileType = profileType;
            _PluginData.content = content;
            _PluginData.imageUrl = imageUrl;
            _PluginData.name = name;
            _PluginData.screenName = screenName;
            _PluginData.tweet = tweet;
            _PluginData.tweetId = tweetId;
            _PluginData.url = url;
            _PluginData.userImage = userImage;
            _PluginData.type = type;

            if (Session["User"] != null)
            {
                if (!string.IsNullOrEmpty(url) && profileType != "pinterest")
                {
                    Domain.Socioboard.Helper.ThumbnailDetails plugindata = PluginHelper.CreateThumbnail(url);
                    _PluginData._ThumbnailDetails = plugindata;
                }

                ViewBag.plugin = _PluginData;
                //Dictionary<string, object> dict_TeamMember = new Dictionary<string, object>();
                List<Domain.Socioboard.Helper.PluginProfile> lstsb = new List<Domain.Socioboard.Helper.PluginProfile>();
                lstsb = SBUtils.GetProfilesForPlugin();
                return View("RMain", lstsb);
            }
            return View("Rlogin");
        }

        public ActionResult PluginSignUp()
        {
            string name = Request.Form["name"].ToString();
            string email = Request.Form["email"].ToString();
            string password = Request.Form["password"].ToString();
            Api.User.User objApiUser = new Api.User.User();
            string res = objApiUser.Register(email, password, "free", name, "0");
            if (res == "Email Already Exists")
            {
                return Content("email exist");
            }
            else
            {
                Domain.Socioboard.Domain.User _User = (Domain.Socioboard.Domain.User)new JavaScriptSerializer().Deserialize(res, typeof(Domain.Socioboard.Domain.User));

                Api.MailSender.MailSender ApiobjMailSender = new Api.MailSender.MailSender();
                string mailsender = "";
                try
                {
                    var mailBody = Helper.SBUtils.RenderViewToString(this.ControllerContext, "_RegistrationMailPartial", _User);
                    string Subject = "Thanks for creating your " + ConfigurationManager.AppSettings["DefaultGroupName"].ToString() + " Account";

                    mailsender = ApiobjMailSender.SendChangePasswordMail(email, mailBody, Subject);
                }
                catch { }
            }
            return Content("user");
        }
        public ActionResult SendFbCreads(string userId, string password)
        {
            Api.User.User ApiUser = new Api.User.User();
            string _user = ApiUser.SaveFacebookId(userId, password);
            return Content(_user);
        }



        public string SendLocalEmail(string toAddress)
        {
            string result = "Message Sent Successfully..!!";
            string senderID = "suresh@socioboard.com";// use sender’s email id here..
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "127.0.0.1", // smtp server address here…
                    Port = 25,
                    //EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //Credentials = new System.Net.NetworkCredential(senderID, senderPassword),
                    Timeout = 30000,
                };
                MailMessage message = new MailMessage(senderID, toAddress, "Test Mail", "Test Body");
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                result = "Error sending email.!!!";
                logger.Error(ex.StackTrace);
                logger.Error(ex.Message);
            }
            return result;
        }

        public ActionResult IsUserSession()
        {
            if (Session["User"] != null)
            {
                return Content("user");
            }
            else
            {
                return Content("");
            }
        }

    }


}
