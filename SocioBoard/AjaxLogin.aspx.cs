using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Model;
using SocioBoard.Domain;
using Newtonsoft.Json.Linq;
using SocioBoard.Helper;
using GlobusGooglePlusLib.Authentication;
using System.Configuration;
namespace SocialSuitePro
{
    public partial class AjaxLogin : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(AjaxLogin));

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                logger.Error(ex.StackTrace);
            }
        }
        public void ProcessRequest()
        {
            if (Request.QueryString["op"] == "login")
            {
                try
                {
                    string email = Request.QueryString["username"];
                    string password = Request.QueryString["password"];
                    SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                    UserRepository userrepo = new UserRepository();
                    LoginLogs objLoginLogs = new LoginLogs();
                    LoginLogsRepository objLoginLogsRepository = new LoginLogsRepository();
                    User user = userrepo.GetUserInfo(email, password);
                    if (user == null)
                    {
                        Response.Write("Invalid Email or Password");
                    }
                    else
                    {
                        if (user.UserStatus == 1)
                        {
                            Session["LoggedUser"] = user;
                            // List<User> lstUser = new List<User>();
                            if (Session["LoggedUser"] != null)
                            {
                                SocioBoard.Domain.User.lstUser.Add((User)Session["LoggedUser"]);
                                Application["OnlineUsers"] = SocioBoard.Domain.User.lstUser;
                                objLoginLogs.Id = new Guid();
                                objLoginLogs.UserId = user.Id;
                                objLoginLogs.UserName = user.UserName;
                                objLoginLogs.LoginTime = DateTime.Now.AddHours(11.50);
                                objLoginLogsRepository.Add(objLoginLogs);
                            }
                            Response.Write("user");
                        }

                        else
                        {
                            Response.Write("You are Blocked by Admin Please contact Admin!");
                        }


                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    logger.Error(ex.StackTrace);
                }
            }
            else if (Request.QueryString["op"] == "register")
            {
                User user = new User();
                UserActivation objUserActivation = new UserActivation();
                UserRepository userrepo = new UserRepository();
                SocioBoard.Helper.SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                Session["AjaxLogin"] = "register";
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(Request.InputStream);
                    string line = "";
                    line = sr.ReadToEnd();
                    JObject jo = JObject.Parse(line);
                    user.PaymentStatus = "unpaid";
                    if (!string.IsNullOrEmpty(Request.QueryString["type"]))
                    {
                        user.AccountType = Request.QueryString["type"];
                    }
                    else
                    {
                        user.AccountType = "deluxe";
                    }
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Id = Guid.NewGuid();
                    user.UserName = Server.UrlDecode((string)jo["firstname"]) + " " + Server.UrlDecode((string)jo["lastname"]);
                    user.EmailId = Server.UrlDecode((string)jo["email"]);
                    user.Password = Server.UrlDecode((string)jo["password"]);
                    user.UserStatus = 1;
                    if (!userrepo.IsUserExist(user.EmailId))
                    {
                        UserRepository.Add(user);
                        Session["LoggedUser"] = user;
                        Response.Write("user");

                        objUserActivation.Id = Guid.NewGuid();
                        objUserActivation.UserId = user.Id;
                        objUserActivation.ActivationStatus = "0";
                        UserActivationRepository.Add(objUserActivation);

                        //add value in userpackage
                        UserPackageRelation objUserPackageRelation = new UserPackageRelation();
                        UserPackageRelationRepository objUserPackageRelationRepository = new UserPackageRelationRepository();
                        PackageRepository objPackageRepository = new PackageRepository();

                        Package objPackage = objPackageRepository.getPackageDetails(user.AccountType);
                        objUserPackageRelation.Id = new Guid();
                        objUserPackageRelation.PackageId = objPackage.Id;
                        objUserPackageRelation.UserId = user.Id;
                        objUserPackageRelation.PackageStatus = true;

                        objUserPackageRelationRepository.AddUserPackageRelation(objUserPackageRelation);


                        SocioBoard.Helper.MailSender.SendEMail(user.UserName, user.Password, user.EmailId, user.AccountType.ToString(), user.Id.ToString());




                        //MailSender.SendEMail(user.UserName, user.Password, user.EmailId);
                        // lblerror.Text = "Registered Successfully !" + "<a href=\"login.aspx\">Login</a>";
                    }
                    else
                    {
                        Response.Write("Email Already Exists !");
                    }
                }


                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);


                    Console.WriteLine(ex.StackTrace);
                }


            }
            else if (Request.QueryString["op"] == "facebooklogin")
            {
                SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");

                string redi = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
                Session["login"] = "facebook";
                Response.Write(redi);



            }
            else if (Request.QueryString["op"] == "googlepluslogin")
            {
                Session["login"] = "googleplus";
                oAuthToken objToken = new oAuthToken();
                Response.Write(objToken.GetAutherizationLink("https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/plus.login"));
            }
            else if (Request.QueryString["op"] == "removeuser")
            {
                try
                {
                    if (Session["LoggedUser"] != null)
                    {
                        SocioBoard.Domain.User.lstUser.Remove((User)Session["LoggedUser"]);
                    }
                }
                catch (Exception Err)
                {
                    logger.Error(Err.StackTrace);
                    Response.Write(Err.StackTrace);
                }
            }


        }
    }
}