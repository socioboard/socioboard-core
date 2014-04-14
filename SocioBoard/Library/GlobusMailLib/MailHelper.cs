using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SendGridMail.Transport;
using log4net;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace GlobusMailLib
{
    public class MailHelper
    {
        ILog logger = LogManager.GetLogger(typeof(MailHelper));

        public string SendMailBySendGrid(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailBySendGrid = string.Empty;
            try
            {

                var myMessage = SendGridMail.SendGrid.GetInstance();
                myMessage.From = new System.Net.Mail.MailAddress(from);
                myMessage.AddTo(to);
                myMessage.Subject = subject;

                //Add the HTML and Text bodies
                myMessage.Html = body;
                //myMessage.Text = "Hello World plain text!";
                var username = sendgridUserName;
                var pswd = sendgridPassword;

                var credentials = new System.Net.NetworkCredential(username, pswd);
                
                var transportWeb = SMTP.GetInstance(credentials);

                // Send the email.
                transportWeb.Deliver(myMessage);
                
                sendMailBySendGrid = "Success";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
            return sendMailBySendGrid;
        }

        public string SendMailBySMTP(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailBySMTP = string.Empty;
            try
            {
                MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                Msg.From = new MailAddress(from);
                // Recipient e-mail address.
                Msg.To.Add(to);
                //Msg.Bcc.Add(bcc);
                //Msg.CC.Add(cc);
                Msg.Subject = subject;
                Msg.IsBodyHtml = true;
                Msg.Body = body;
                // your remote SMTP server IP.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host;//"smtp.gmail.com";
                smtp.Port = port;//587;
                smtp.Credentials = new System.Net.NetworkCredential(from, sendgridPassword);
                smtp.EnableSsl = true;

                smtp.Send(Msg);
                Msg = null;
                //ScriptManager.RegisterStartupScript("UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
            return sendMailBySMTP;
        }

        public string SendMailByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;
            try
            {

                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                //message.from_email = from;
                //message.from_name = from;//"AlexPieter";
                message.from_email = from;
                message.from_name = "Socioboard Support";
                message.html = body;
                message.subject = subject;
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };

                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(sendgridPassword, false);
                var results = mandrillApi.SendMessage(message);

                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent)
                    {
                        logger.Error(result.Email+" "+result.RejectReason);
                    }
                      //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));
                }

                sendMailByMandrill = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                sendMailByMandrill=ex.Message;
            }

            return sendMailByMandrill;
        }
    }
}
