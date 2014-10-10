using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Api.Socioboard.Helper;
using log4net;
using System.IO;
using System.Configuration;
using Api.Socioboard;

namespace Api.Socioboard.Helper
{
    public class MailSender
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MailSender));

        public static void SendEMail(string username, string password, string emailid)
        {

            try
            {
                MailHelper mailhelper = new MailHelper();
                string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/RegistrationMail.html");

                string html = File.ReadAllText(mailpath);

                html = html.Replace("%USERNAME%", username);
                html = html.Replace("%PASSWORD%", password);
                html = html.Replace("%EMAILID%", emailid);
                string fromemail = ConfigurationManager.AppSettings["fromemail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];




                string Body = mailhelper.VerificationMail(html, emailid, "");


                string Subject = "You have Added to Socioboard Account";
                //            MailHelper.SendMailMessage(host, int.Parse(port.ToString()),fromemail,pass,emailid,string.Empty,string.Empty,Subject,Body);


                MailHelper.SendSendGridMail(host, Convert.ToInt32(port), fromemail, "", emailid, "", "", Subject, Body, usernameSend, pass);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        //public static void SendInvitationEmail(string username, string sendername, string email, Guid teamid)
        //{
        //    try
        //    {
        //        Registration reg = new Registration();
        //        string tid = reg.MD5Hash(email);
        //        MailHelper mailhelper = new MailHelper();
        //        string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/SendInvitation.htm");
        //        string html = File.ReadAllText(mailpath);
        //        string fromemail = ConfigurationManager.AppSettings["fromemail"];
        //        string usernameSend = ConfigurationManager.AppSettings["username"];
        //        string host = ConfigurationManager.AppSettings["host"];
        //        string port = ConfigurationManager.AppSettings["port"];
        //        string pass = ConfigurationManager.AppSettings["password"];
        //        string urllogin = "http://ssp.socioboard.com/Default.aspx";
        //        string registrationurl = "http://ssp.socioboard.com/Registration.aspx?tid=" + teamid;
        //        string Body = mailhelper.InvitationMail(html, username, sendername, "", urllogin, registrationurl);
        //        string Subject = "You've been Invited to " + username + " SocialSuitePro Account";
        //        //   MailHelper.SendMailMessage(host, int.Parse(port.ToString()), fromemail, pass, email, string.Empty, string.Empty, Subject, Body);


        //        MailHelper.SendSendGridMail(host, Convert.ToInt32(port), fromemail, "", email, "", "", Subject, Body, usernameSend, pass);

        //    }
        //    catch (Exception ex)
        //    {

        //        logger.Error(ex.Message);
        //    }
        //}

        public static void SendNewsLetterEmail(string username, string sendername, string email)
        {
            try
            {
                MailHelper mailhelper = new MailHelper();
                string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/SendNewsLetter.htm");
                string html = File.ReadAllText(mailpath);
                string fromemail = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["pasword"];
                string Body = mailhelper.NewsLetterMail(html, username, sendername, "");
                string Subject = "News From SocialSuitePro Account";
                MailHelper.SendMailMessage(host, int.Parse(port.ToString()), fromemail, pass, email, string.Empty, string.Empty, Subject, Body);

            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
            }
        }
    }
}