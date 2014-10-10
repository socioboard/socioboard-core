using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;
using System.IO;
using System.Web;
using log4net;

namespace Api.Socioboard.Helper
{
    public class MailHelper
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MailHelper));

        public static void SendMailMessage(string Host, int Port, string from, string Password, string to, string bcc, string cc, string subject, string body)
        {
            try
            {
                // Instantiate a new instance of MailMessage
                MailMessage mMailMessage = new MailMessage();
                // Set the sender address of the mail message
                mMailMessage.From = new MailAddress(from);
                // Set the recepient address of the mail message
                mMailMessage.To.Add(new MailAddress(to));

                // Check if the bcc value is null or an empty string
                if ((bcc != null) && (bcc != string.Empty))
                {
                    // Set the Bcc address of the mail message
                    mMailMessage.Bcc.Add(new MailAddress(bcc));
                }
                // Check if the cc value is null or an empty value
                if ((cc != null) && (cc != string.Empty))
                {
                    // Set the CC address of the mail message
                    mMailMessage.CC.Add(new MailAddress(cc));
                }       // Set the subject of the mail message
                mMailMessage.Subject = subject;
                // Set the body of the mail message
                mMailMessage.Body = body;

                // Set the format of the mail message body as HTML
                mMailMessage.IsBodyHtml = true;
                // Set the priority of the mail message to normal
                mMailMessage.Priority = MailPriority.Normal;

                // Instantiate a new instance of SmtpClient
                SmtpClient mSmtpClient = new SmtpClient(Host, Port);

                //mSmtpClient.Credentials = new NetworkCredential("AKIAJGBVNBAQOJZQIKWQ", "AsxdpGwwJJ4ag14qyu/wjFWsroUi25CSYcSUMRudc6CJ");
                // Send the mail message
                mSmtpClient.Send(mMailMessage);
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
            }
        }


        public static void sendmailMailsetting(string To, string CC, string subject, string body, List<string> AttachmentFile)
        {
            //
            try
            {
                MailMessage mMailMessage = new MailMessage();
                string[] strTo = To.Split(',');
                List<string> lstTo = new List<string>(strTo);
                foreach (string item in lstTo)
                {
                    mMailMessage.To.Add(new MailAddress(item.ToString()));
                }

                string[] strCC = CC.Split(',');
                List<string> lstCC = new List<string>(strCC);
                if (lstCC.Count > 0 && lstCC[0] != "")
                {
                    foreach (string itemCC in lstCC)
                    {
                        mMailMessage.CC.Add(new MailAddress(itemCC.ToString()));
                    }
                }
                // Set the recepient address of the mail message
                // mMailMessage.To.Add(new MailAddress(To));
                //mMailMessage.CC.Add(new MailAddress(CC));
                mMailMessage.Subject = subject;
                if (AttachmentFile.Count > 0)
                {
                    for (int i = 0; i < AttachmentFile.Count; i++)
                    {
                        Attachment attch = new Attachment(AttachmentFile[i]);
                        mMailMessage.Attachments.Add(attch);
                    }

                }

                mMailMessage.Body = body;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.Priority = MailPriority.Normal;

                SmtpClient mSmtpClient = new SmtpClient();
                mSmtpClient.Send(mMailMessage);
            }
            catch (System.Exception ex)
            {

                logger.Error(ex.Message);
            }
        }


        public string InvitationMail(string Body, string Name, string SenderName, string Email, string urllogin, string urlreg)
        {
            return Body.Replace("[Name]", Name).Replace("[SenderName]", SenderName).Replace("[SenderEmail]", Email).Replace("[URLLOGIN]", urllogin).Replace("[URLREG]", urlreg);
        }

        public string NewsLetterMail(string Body, string Name, string SenderName, string Email)
        {
            return Body.Replace("[Name]", Name).Replace("[SenderName]", SenderName).Replace("[SenderEmail]", Email);
        }

        public string VerificationMail(string Body, string VerificationUrl, string ContactUs)
        {
            return Body.Replace("[VerificationUrl]", VerificationUrl).Replace("[ContactUs]", ContactUs);
        }

        public static void SendSendGridMail(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
            }
        }

    }
}