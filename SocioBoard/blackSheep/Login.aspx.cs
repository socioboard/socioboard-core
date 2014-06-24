using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Model;
using SocioBoard;
using System.Web.Security;
using SocioBoard.Domain;
using GlobusTwitterLib.Authentication;
using System.Configuration;
using SocioBoard.Helper;
using log4net;
using GlobusGooglePlusLib.Authentication;
namespace blackSheep
{
    public partial class Login : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Login));
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

             

                if (!IsPostBack)
                {
                  
                    if (Session["LoggedUser"] != null)
                        Session.Abandon();

                   
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.StackTrace);
            }
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                          UserRepository userrepo = new UserRepository();
                    Registration regObject = new Registration();
                    User user = userrepo.GetUserInfo(txtEmail.Text, regObject.MD5Hash(txtPassword.Text));
                    if (user == null)
                    {
                        Response.Write("user is null");
                    }

                    if (user.PaymentStatus == "unpaid")
                    {
                        if (DateTime.Compare(DateTime.Now, user.ExpiryDate) < 0)
                        {
                            if (user != null)
                            {
                                Session["LoggedUser"] = user;
                                FormsAuthentication.SetAuthCookie(user.UserName, true);
                                Response.Redirect("/Home.aspx", false);
                            }
                            else
                            {
                                //  txterror.Text = "Invalid UserName Or Password";
                            }
                        }
                        else
                        {
                            Response.Redirect("Settings/Billing.aspx");
                        }
                    }
                    else
                    {
                        Session["LoggedUser"] = user;
                        FormsAuthentication.SetAuthCookie(user.UserName, true);
                        Response.Redirect("/Home.aspx", false);
                    
                    }


                }
            }
            catch (Exception ex)
            {

                logger.Error(ex.StackTrace);

                Console.WriteLine(ex.StackTrace);
            }

        }

        public void AuthenticateFacebook(object sender, EventArgs e)
        {

            SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");

            fb_account.HRef = "http://www.facebook.com/dialog/oauth/?scope=publish_stream,read_stream,read_insights,manage_pages,user_checkins,user_photos,read_mailbox,manage_notifications,read_page_mailboxes,email,user_videos,offline_access&client_id=" + ConfigurationManager.AppSettings["ClientId"] + "&redirect_uri=" + ConfigurationManager.AppSettings["RedirectUrl"] + "&response_type=code";
            // fb_cont.HRef = fb_account.HRef;
            Session["login"] = "facebook";
            Response.Redirect(fb_account.HRef);

        }

        public void AuthenticateGooglePlus(object sender, EventArgs e)
        {
            Session["login"] = "googleplus";
            oAuthToken objToken = new oAuthToken();
            Response.Redirect(objToken.GetAutherizationLink("https://www.googleapis.com/auth/userinfo.email+https://www.googleapis.com/auth/userinfo.profile+https://www.googleapis.com/auth/plus.me+https://www.googleapis.com/auth/plus.login"));
        }
    }
}