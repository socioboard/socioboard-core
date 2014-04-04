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

namespace SocialSuitePro
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
                    // c.customer_email = txtEmail.Text.Trim();
                    // exist = custrepo.ExistedCustomerEmail(c);
                    User usr = objUserRepo.getUserInfoByEmail(txtEmail.Text);

                    if (usr != null)
                    {
                        string URL = Request.Url.AbsoluteUri;
                        //strUrl = Server.MapPath("~/ChangePassword.aspx") + "?str=" + txtEmail.Text + "&type=forget";
                        strUrl = URL.Replace("ForgetPassword.aspx", "ChangePassword.aspx" + "?str=" + regObject.MD5Hash(txtEmail.Text) + "&type=forget");
                        strUrl = (strUrl + "?userid=" + usr.Id).ToString();

                        string MailBody = "<body bgcolor=\"#FFFFFF\"><!-- Email Notification from SocioBoard.com-->" +
                    "<table id=\"Table_01\" style=\"margin-top: 50px; margin-left: auto; margin-right: auto;\"" +
                    " align=\"center\" width=\"650px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr>" +
                   "<td height=\"20px\" style=\"background-color: rgb(222, 222, 222); text-align: center; font-size: 15px; font-weight: bold; font-family: Arial; color: rgb(51, 51, 51); float: left; width: 100%; margin-top: 7px; padding-top: 10px; border-bottom: 1px solid rgb(204, 204, 204); padding-bottom: 10px;\">" +
                       "SocioBoard</td></tr><!--Email content--><tr>" +
                   "<td style=\"background-color: #dedede; padding-top: 10px; padding-left: 25px; padding-right: 25px; padding-bottom: 30px; font-family: Tahoma; font-size: 14px; color: #181818; min-height: auto;\"><p>Hi , " + usr.UserName + "</p><p>" +
                       "Please click here to proceed for password <a href=" + strUrl + " style=\"text-decoration:none;\">Reset</a></td></tr><tr>" +
                   "<td style=\"background-color: rgb(222, 222, 222); margin-top: 10px; padding-left: 20px; height: 20px; color: rgb(51, 51, 51); font-size: 15px; font-family: Arial; border-top: 1px solid rgb(204, 204, 204); padding-bottom: 10px; padding-top: 10px;\">Thanks" +
                   "</td></tr></table><!-- End Email Notification From SocioBoard.com --></body>";

                        string username = ConfigurationManager.AppSettings["username"];
                        string host = ConfigurationManager.AppSettings["host"];
                        string port = ConfigurationManager.AppSettings["port"];
                        string pass = ConfigurationManager.AppSettings["password"];
                        string from = ConfigurationManager.AppSettings["fromemail"];

                        //   string Body = mailformat.VerificationMail(MailBody, txtEmail.Text.ToString(), "");
                        string Subject = "Forget Password Socio Board account";
                        //MailHelper.SendMailMessage(host, int.Parse(port.ToString()), username, pass, txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody);
                        MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody, username, pass);



                        lblerror.Text = "Please check your mail for the instructions.";
                    }
                    else
                    {
                        lblerror.Text = "Your Email is wrong Please try another one";
                    }
                }
                else
                {
                    lblerror.Text = "Please enter your Email-Id";
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.StackTrace);
            }
        }

        protected void btnResetPwd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Registration regpage = new Registration();
                string changedpassword = regpage.MD5Hash(txtpass.Text);
                UserRepository userrepo = new UserRepository();
                if (userrepo.ResetPassword(Guid.Parse(userid.ToString()), changedpassword.ToString()) > 0)
                {
                    lblerror.Text = "Password Reset Successfully";
                }
                else
                {
                    lblerror.Text = "Problem Password Reset";
                }

            }
            catch (Exception Err)
            {
                logger.Error(Err.StackTrace);
            }
        }




    }
}