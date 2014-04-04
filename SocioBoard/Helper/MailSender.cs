using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.IO;
using log4net;
using System.Security.Cryptography;

namespace SocioBoard.Helper
{
    public class MailSender
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MailSender));

        public static void SendEMail(string username, string password, string emailid, string accounttype,string id)
        {

            try
            {
                MailHelper mailhelper = new MailHelper();
                string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/RegistrationMail.html");

                string html = File.ReadAllText(mailpath);

                string mailSenderDomain = ConfigurationManager.AppSettings["MailSenderDomain"];
                string replink=mailSenderDomain + "Home.aspx?stat=activate&id=" + id;

                html = html.Replace("%replink%", replink);
                //html = html.Replace("%replink%", "http://www.socioboard.com/Home.aspx");
                html = html.Replace("%USERNAME%", username);
                html = html.Replace("%pln%", accounttype);
                //html = html.Replace("%PASSWORD%", password);
                html = html.Replace("%EMAILID%", emailid);
                string fromemail = ConfigurationManager.AppSettings["fromemail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];




                string Body = mailhelper.VerificationMail(html, emailid, "");


                string Subject = "Thanks for creating your Socioboard Account";
                //            MailHelper.SendMailMessage(host, int.Parse(port.ToString()),fromemail,pass,emailid,string.Empty,string.Empty,Subject,Body);


                MailHelper.SendSendGridMail(host, Convert.ToInt32(port), fromemail, "", emailid, "", "", Subject, Body, usernameSend, pass);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }



        public static string ReSendEMail(string username, string password, string emailid, string accounttype, string id)
        {
            string ret = string.Empty;
            try
            {
                MailHelper mailhelper = new MailHelper();
                string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/RegistrationMail.html");

                string html = File.ReadAllText(mailpath);

                string mailSenderDomain = ConfigurationManager.AppSettings["MailSenderDomain"];
                string replink = mailSenderDomain + "Home.aspx?stat=activate&id=" + id;

                html = html.Replace("%replink%", replink);

               // html = html.Replace("%replink%", "http://www.socioboard.com/Home.aspx?stat=activate&id=" + id + "");
                //html = html.Replace("%replink%", "http://www.socioboard.com/Home.aspx");
                html = html.Replace("%USERNAME%", username);
                html = html.Replace("%pln%", accounttype);
                //html = html.Replace("%PASSWORD%", password);
                html = html.Replace("%EMAILID%", emailid);
                string fromemail = ConfigurationManager.AppSettings["fromemail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];




                string Body = mailhelper.VerificationMail(html, emailid, "");


                string Subject = "Thanks for creating your Socioboard Account";
                //            MailHelper.SendMailMessage(host, int.Parse(port.ToString()),fromemail,pass,emailid,string.Empty,string.Empty,Subject,Body);


                //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), fromemail, "", emailid, "", "", Subject, Body, usernameSend, pass);
                MailHelper objMailHelper = new MailHelper();
                ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), fromemail, "", emailid, "", "", Subject, Body, usernameSend, pass);

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
            return ret;
        }





        public static void SendInvitationEmail(string username, string sendername, string email,Guid teamid)
        {
            try
            {
                Registration reg = new Registration();
                string tid = reg.MD5Hash(email);
                MailHelper mailhelper = new MailHelper();
                string mailpath = HttpContext.Current.Server.MapPath("~/Layouts/Mails/SendInvitation.htm");
                string html = File.ReadAllText(mailpath);
                string fromemail = ConfigurationManager.AppSettings["fromemail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];
                string urllogin = "http://socioboard.com/Default.aspx";
                string registrationurl = "http://socioboard.com/Registration.aspx?tid="+teamid;
                string Body = mailhelper.InvitationMail(html, username, sendername, "", urllogin, registrationurl);
                string Subject = "You've been Invited to " + username + " Socioboard Account";
                //   MailHelper.SendMailMessage(host, int.Parse(port.ToString()), fromemail, pass, email, string.Empty, string.Empty, Subject, Body);


                MailHelper.SendSendGridMail(host, Convert.ToInt32(port), fromemail, "", email, "", "", Subject, Body, usernameSend, pass);

            }
            catch (Exception ex)
            {

                logger.Error(ex.Message);
            }
        }

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