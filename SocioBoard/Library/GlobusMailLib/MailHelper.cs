using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SendGridMail.Transport;
using log4net;
using System.Net.Mail;
using System.Net;
using System.Web;
using Amazon.SimpleEmail;
using System.Collections;
using Amazon.SimpleEmail.Model;
using Amazon.DynamoDBv2;

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
                //message.from_name = "Socialscoup Support";
                message.html = body;
                message.subject = subject;
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };

                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(sendgridPassword, false);
                var results = mandrillApi.SendMessage(message);
                string status = string.Empty;
                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent)
                    {
                        logger.Error(result.Email + " " + result.RejectReason);
                    }
                    status = Mandrill.EmailResultStatus.Sent.ToString();
                    //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));
                }

                sendMailByMandrill = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                sendMailByMandrill = ex.Message;
            }

            return sendMailByMandrill;
        }



        public string GetStatusFromSendMailByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
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
                string status = string.Empty;
                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent)
                    {
                        logger.Error(result.Email + " " + result.RejectReason);
                    }
                    status = Mandrill.EmailResultStatus.Sent.ToString();
                    //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));
                }

                sendMailByMandrill = status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                sendMailByMandrill = ex.Message;
            }

            return sendMailByMandrill;
        }

        public string SendMailByMandrillForEnterPrise(string name, string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;
            try
            {

                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                //message.from_email = from;
                //message.from_name = from;//"AlexPieter";
                message.from_email = from;
                message.from_name = name;
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
                        logger.Error(result.Email + " " + result.RejectReason);
                    }
                    //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));
                }

                sendMailByMandrill = "success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                sendMailByMandrill = ex.Message;
            }

            return sendMailByMandrill;
        }




        //public string SendMailByAmazonSES(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string AWSAccessKey, string AWSSecretKey)
        //{
        //    string res = "";
        //    //INITIALIZE AWS CLIENT//
        //    //AmazonSimpleEmailServiceConfig amConfig = new AmazonSimpleEmailServiceConfig();
        //    //amConfig.UseSecureStringForAwsSecretKey = false;

        //    //AmazonSimpleEmailServiceConfig amazonConfiguration = new AmazonSimpleEmailServiceConfig();
        //    //AmazonSimpleEmailServiceClient client =new AmazonSimpleEmailServiceClient(AWSAccessKey, AWSSecretKey, amazonConfiguration);

        //    var amazonConfiguration = new AmazonDynamoDBConfig
        //    {
        //        ServiceURL = "https://dynamodb.eu-west-1.amazonaws.com/"
        //    };
        //   // AmazonDynamoDBClient amzClient = new AmazonDynamoDBClient(AWSAccessKey, AWSSecretKey, amazonConfiguration);
        //  //  AmazonSimpleEmailService aa=new AmazonSimpleEmailService ();

        //    //AmazonSimpleEmailServiceClient amzClient = new AmazonSimpleEmailServiceClient(AWSAccessKey, AWSSecretKey, amazonConfiguration);
        //    //ConfigurationManager.AppSettings["AWSAccessKey"].ToString(),
        //    //ConfigurationManager.AppSettings["AWSSecretKey"].ToString(), amConfig);


        //    //ArrayList that holds To Emails. It can hold 1 Email to any
        //    //number of emails in case what to send same message to many users.
        //    ArrayList arrmail = new ArrayList();
        //    arrmail.Add(to);

        //    //Create Your Bcc Addresses as well as Message Body and Subject
        //    Destination dest = new Destination();
        //    //dest.WithBccAddresses((string[])to.ToArray(typeof(string)));
        //    // string body = Body;
        //    // string subject = "Subject : " + txtSubject.Text;
        //    Body bdy = new Body();
        //    bdy.Html = new Amazon.SimpleEmail.Model.Content(body);
        //    Amazon.SimpleEmail.Model.Content title = new Amazon.SimpleEmail.Model.Content(subject);
        //    Message message = new Message(title, bdy);

        //    //Create A Request to send Email to this ArrayList with this body and subject
        //    try
        //    {
        //        SendEmailRequest ser = new SendEmailRequest(from, dest, message);
        //        //SendEmailResponse seResponse = amzClient;
        //        SendEmailResult seResult = seResponse.SendEmailResult;
        //        //SendEmailResult seResult = seResponse.SendEmailResult;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return res;
        //}
    }
}
