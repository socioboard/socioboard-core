using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;


namespace SocioBoard.Helper
{
    public class MailSender
    {

        public static void SendEMail(string username,string password,string emailid)
        {
            MailHelper mailhelper = new MailHelper();
            string mailpath =HttpContext.Current.Server.MapPath("~/Mails/Layouts/RegistrationMail.html");
            string html = File.ReadAllText(mailpath);
            html = html.Replace("%USERNAME%", username);
            html = html.Replace("%PASSWORD%", password);
            html = html.Replace("%EMAILID%", emailid);

            string host = ConfigurationManager.AppSettings["host"];
            string port = ConfigurationManager.AppSettings["port"];
            string pass = ConfigurationManager.AppSettings["pasword"];
            string Body = mailhelper.VerificationMail(html, emailid, "");
            string Subject = "You've been added to " + username + " SocioBoard Account";
            MailHelper.SendMailMessage(host, int.Parse(port.ToString()), emailid, pass, emailid, string.Empty, string.Empty, Subject, Body);

        }

       
    }
}