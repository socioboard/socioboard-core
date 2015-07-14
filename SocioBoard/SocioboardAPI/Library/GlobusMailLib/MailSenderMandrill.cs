using Mandrill;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GlobusMailLib
{
    public class MailSenderMandrill:IMailSender
    {

        public string SendMail(string from, string passsword, string to, string bcc, string cc, string subject, string body, string MandrillUserName = "", string MandrillPassword = "")
        {
            string sendMailByMandrill = string.Empty;
            try
            {
                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                message.from_email = from;
                message.from_name = from;
                message.html = body;
                message.subject = subject;
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };
                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(MandrillPassword, false);
                var results = mandrillApi.SendMessage(message);

                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent || result.Status == Mandrill.EmailResultStatus.Queued)
                    {
                        sendMailByMandrill = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sendMailByMandrill;
        }

        public string SendMailWithAttachment(string from, string passsword, string to, string bcc, string cc, string subject, string body, string file, string filename, string filetype, string MandrillUserName = "", string MandrillPassword = "")
        {
            string sendMailByMandrill = string.Empty;
            try
            {
                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                message.from_email = from;
                message.from_name = from;
                message.html = body;
                message.subject = subject;
                var fileBytes = File.ReadAllBytes(file);
                message.attachments = new[]
                              {
                                  new attachment
                                  {
                                      content = Convert.ToBase64String(fileBytes),
                                      name = filename,
                                      type = filetype
                                  }
                              };
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };
                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(MandrillPassword, false);
                var results = mandrillApi.SendMessage(message);

                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent || result.Status == Mandrill.EmailResultStatus.Queued)
                    {
                        sendMailByMandrill = "Success";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return sendMailByMandrill;
        }

    }
}
