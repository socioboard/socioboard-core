using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Helper;
using System.Configuration;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;
using SocioBoard;
using System.IO;

namespace SocioBoard
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ForgotPassword));
        string userid = "";
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.QueryString["userid"] != null)
            {
                divtxtmail.Visible = false;
                btnforgetpass.Visible = false;
              //  RequiredFieldValidator1.Visible = false;
                heading.InnerHtml = "Reset Password";

            }
            else
            {
                divtxtpass.Visible = false;
                divtxtconfirmpass.Visible = false;
                btnresetpass.Visible = false;
                heading.InnerHtml = "Forgot Password";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["userid"] != null)
            {
                userid = Request.QueryString["userid"].ToString();
            }

        }

        protected void btnForgotPwd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                bool exist = false;
                UserRepository objUserRepo = new UserRepository();
                Registration regObject = new Registration();

                if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                {
                    string strUrl = string.Empty;
                    User usr = objUserRepo.getUserInfoByEmail(txtEmail.Text);
                    if (usr != null)
                    {
                        string URL = Request.Url.AbsoluteUri;
                        strUrl = URL.Replace("ForgetPassword.aspx", "ChangePassword.aspx" + "?str=" + regObject.MD5Hash(txtEmail.Text) + "&type=forget");
                        strUrl = (strUrl + "?userid=" + usr.Id).ToString();
                        string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/ResetPassword.htm");
                        string MailBody = File.ReadAllText(mailpath);
                        MailBody = MailBody.Replace("%replink%", strUrl);
                        MailBody = MailBody.Replace("%name%", usr.UserName);
                        string username = ConfigurationManager.AppSettings["username"];
                        string host = ConfigurationManager.AppSettings["host"];
                        string port = ConfigurationManager.AppSettings["port"];
                        string pass = ConfigurationManager.AppSettings["password"];
                        string from = ConfigurationManager.AppSettings["fromemail"];

                        //   string Body = mailformat.VerificationMail(MailBody, txtEmail.Text.ToString(), "");
                        string Subject = "Forget Password SocioBoard account";
                        //MailHelper.SendMailMessage(host, int.Parse(port.ToString()), username, pass, txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody);
                        MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody, username, pass);
                        lblerror.Text = "Please check your mail for the instructions.";
                    }
                    else
                    {
                        lblerror.Text = "Your Email is wrong Please try another one";
                    }
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.StackTrace);
            }
        }

        protected void btnResetPwd_Click(object sender, ImageClickEventArgs e)
        {
            UserRepository userrepo = new UserRepository();
            
            try
            {
                Registration regpage = new Registration();

                if (txtpass.Text == txtconfirmpass.Text)
                {
                    string changedpassword = regpage.MD5Hash(txtpass.Text);
                   
                    if (userrepo.ResetPassword(Guid.Parse(userid.ToString()), changedpassword.ToString()) > 0)
                    {
                        User usr = userrepo.getUsersById(Guid.Parse(userid.ToString()));

                        // Code written by Abhay Kr Mondal 8/8/2014
                        ///below block of code is used to send email with user information
                        ///Begin
                        string strUrl = string.Empty;
                        string URL = Request.Url.AbsoluteUri;
                        //strUrl = URL.Replace("ForgetPassword.aspx", "ChangePassword.aspx" + "?str=" + regObject.MD5Hash(txtEmail.Text) + "&type=forget");
                        //strUrl = (strUrl + "?userid=" + usr.Id).ToString();
                        string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/UserInfo.htm");
                        string MailBody = File.ReadAllText(mailpath);
                        MailBody = MailBody.Replace("%FN%", usr.UserName);
                        MailBody = MailBody.Replace("%UN%", usr.EmailId);
                        MailBody = MailBody.Replace("%PWD%", txtpass.Text.ToString());
                        string username = ConfigurationManager.AppSettings["username"];
                        string host = ConfigurationManager.AppSettings["host"];
                        string port = ConfigurationManager.AppSettings["port"];
                        string pass = ConfigurationManager.AppSettings["password"];
                        string from = ConfigurationManager.AppSettings["fromemail"];

                        //   string Body = mailformat.VerificationMail(MailBody, txtEmail.Text.ToString(), "");
                        string Subject = "New Password for Your Socioboard Account";
                        //MailHelper.SendMailMessage(host, int.Parse(port.ToString()), username, pass, txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody);
                        MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", usr.EmailId.ToString(), string.Empty, string.Empty, Subject, MailBody, username, pass);
                       // lblerror.Text = "Please check your mail for the instructions.";

                        // Code written by Abhay Kr Mondal 8/8/2014
                        ///below block of code is used to send email with user information

                        lblerror.Text = "Password Reset Successfully";
                    }
                    else
                    {
                        lblerror.Text = "Problem while resetting password";
                    }
                }
                else
                {
                    lblerror.Text = "Password mismatch!";
                }


            }
            catch (Exception Err)
            {
                logger.Error(Err.StackTrace);
            }
        }




    }
}