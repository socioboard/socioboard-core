using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocialSuitePro.Helper
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string name = "";
                string lname = "";
                string email = ""; 
                string subject = "";
                string body = "";

                name = Request.Form["name"];
                lname = Request.Form["lname"];
                email = Request.Form["email"];
                subject = Request.Form["Subject"];
                body = Request.Form["profile"];

                SendMail(name, lname, email, subject, body);
                Response.Write("success");

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error:"+ex.StackTrace);
            }
            

        }
        public void SendMail(string name, string lname, string email, string subject, string body)
        {
            try
            {
                 string MailBody = "<body bgcolor=\"#FFFFFF\"><!-- Email Notification from socioboard.com-->" +
                    "<table id=\"Table_01\" style=\"margin-top: 50px; margin-left: auto; margin-right: auto;\"" +
                    " align=\"center\" width=\"650px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr>" +
                   "<td height=\"20px\" style=\"background-color: rgb(222, 222, 222); text-align: center; font-size: 15px; font-weight: bold; font-family: Arial; color: rgb(51, 51, 51); float: left; width: 100%; margin-top: 7px; padding-top: 10px; border-bottom: 1px solid rgb(204, 204, 204); padding-bottom: 10px;\">" +
                       "SocioBoard</td></tr><!--Email content--><tr>" +
                   "<td style=\"background-color: #dedede; padding-top: 10px; padding-left: 25px; padding-right: 25px; padding-bottom: 30px; font-family: Tahoma; font-size: 14px; color: #181818; min-height: auto;\"><p>This is , " + name + "</p><p>" +
                       "" + body + "</td></tr><tr>" +
                   "<td style=\"background-color: rgb(222, 222, 222); margin-top: 10px; padding-left: 20px; height: 20px; color: rgb(51, 51, 51); font-size: 15px; font-family: Arial; border-top: 1px solid rgb(204, 204, 204); padding-bottom: 10px; padding-top: 10px;\">Thanks" +
                   "</td></tr></table><!-- End Email Notification From SocioBoard.com --></body>";

                string username = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];
                string from = email;//ConfigurationManager.AppSettings["fromemail"];
                string tomail = ConfigurationManager.AppSettings["tomail"];
                
                MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", tomail, string.Empty, string.Empty, subject, MailBody, username, pass);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error:" + ex.StackTrace);
            }
        }
    }
}