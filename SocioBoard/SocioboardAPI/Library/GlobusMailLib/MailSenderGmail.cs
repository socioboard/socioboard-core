using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GlobusMailLib
{
    public class MailSenderGmail : IMailSender
    {
        

        public string SendMail(string from, string passsword, string to, string bcc, string cc, string subject, string body, string UserName = "", string Password = "")
        {
            string response = "";
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(to);
                    mail.From = new MailAddress(from);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(UserName, Password);
                    smtp.Send(mail);
                    response = "Success";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            return response;
        }

        public string SendMailWithAttachment(string from, string passsword, string to, string bcc, string cc, string subject, string body, string file, string filename, string filetype, string UserName = "", string Password = "")
        {
            string response = "";
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    using (Stream attachmentFileStream = new FileStream(file, FileMode.Open))
                    {
                    Attachment attachment;
                    MailMessage mail = new MailMessage();
                    mail.To.Add(to);
                    mail.From = new MailAddress(from);
                    mail.Subject = subject;
                    mail.Body = body;
                   
                        attachment = new Attachment(attachmentFileStream, filename);
                        mail.Attachments.Add(attachment); 
                  
                    mail.IsBodyHtml = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential(UserName, Password);
                    smtp.Send(mail);
                    }
                    response = "Success";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }

            return response;
        }
    }
}
