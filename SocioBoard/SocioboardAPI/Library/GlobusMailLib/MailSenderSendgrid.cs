using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GlobusMailLib
{
    public class MailSenderSendgrid : IMailSender
    {
        public string SendMail(string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName = "", string sendgridPassword = "")
        {
            string sendMailBySendGrid = string.Empty;
            try
            {
                //var smtp = new SmtpClient();
                //smtp.Port = 25;
                //smtp.Host = "smtp.sendgrid.net";
                SendGrid myMessage = SendGridMail.SendGrid.GetInstance();
                myMessage.AddTo(to);
                myMessage.From = new MailAddress(from);
                myMessage.Subject = subject;

                //Add the HTML and Text bodies
                myMessage.Html = body;

                //myMessage.InitializeFilters();
                var credentials = new NetworkCredential(sendgridUserName, sendgridPassword);

                var transportWeb = SMTP.GetInstance(credentials);

                // Send the email.
                transportWeb.Deliver(myMessage);

                sendMailBySendGrid = "Success";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sendMailBySendGrid;
        }

        public string SendMailWithAttachment(string from, string passsword, string to, string bcc, string cc, string subject, string body, string file, string filename, string filetype, string sendgridUserName = "", string sendgridPassword = "")
        {
            string sendMailBySendGrid = string.Empty;
            try
            {
                //var smtp = new SmtpClient();
                //smtp.Port = 25;
                //smtp.Host = "smtp.sendgrid.net";
                using (Stream attachmentFileStream = new FileStream(file, FileMode.Open))
                {
                    SendGrid myMessage = SendGridMail.SendGrid.GetInstance();
                    myMessage.AddTo(to);
                    myMessage.From = new MailAddress(from);
                    myMessage.Subject = subject;

                    //Add the HTML and Text bodies
                    myMessage.Html = body;

                    myMessage.AddAttachment(attachmentFileStream, filename + "." + filetype);

                    //myMessage.InitializeFilters();
                    var credentials = new NetworkCredential(sendgridUserName, sendgridPassword);

                    var transportWeb = SMTP.GetInstance(credentials);

                    // Send the email.
                    transportWeb.Deliver(myMessage);
                }
                sendMailBySendGrid = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sendMailBySendGrid;
        }

    }
}
