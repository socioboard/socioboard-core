using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SendGridMail.Transport;
using log4net;
using System.Net.Mail;
using System.Net;
using System.Web;
using Mandrill;
using System.IO;

namespace GlobusMailLib
{
    public class MailHelper
    {

        /// <summary>
        /// LogManager.GetLogger(typeof(MailHelper));
        /// is used for Log4Net to display the logger.Error || logger.Info in log.txt file 
        /// the Log4Net setting is difined in Web.config File.
        /// </summary>
        ILog logger = LogManager.GetLogger(typeof(MailHelper));

        /// <summary>
        /// SendMailBySendGrid
        /// this function is used for sending mail vai SendGrid
        /// the main input  parameter is :
        ///<add key="host" value="smtp.sendgrid.net" />
        /// <add key="port" value="25" />
        /// <add key="username" value="socioboard"/>
        /// <add key="fromemail" value="xyz@xyz.com"/>
        /// <add key="password" value="xyz" />
        ///<add key="tomail" value="xyz@xyz.com" />
        ///
        /// its return : success if mail send else return string.Empty;
        /// 
        /// </summary>
        /// <param name="Host"></param>
        /// <param name="port"></param>
        /// <param name="from"></param>
        /// <param name="passsword"></param>
        /// <param name="to"></param>
        /// <param name="bcc"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="sendgridUserName"></param>
        /// <param name="sendgridPassword"></param>
        /// <returns></returns>

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

        /// <summary>
        /// SendMailBySMTP
        /// this function is used for sending mail vai SMTP</summary>
        /// the main parameter is 
        /// <add key="host" value="smtp.net" />
        /// <add key="port" value="25" />
        /// <add key="username" value="socioboard"/>
        /// <add key="fromemail" value="xyz@xyz.com"/>
        /// <add key="password" value="xyz" />
        ///<add key="tomail" value="xyz@xyz.com" />
        /// its return : success if mail send else return string.Empty;
        /// :<param name="Host"></param>
        /// <param name="port"></param>
        /// <param name="from"></param>
        /// <param name="passsword"></param>
        /// <param name="to"></param>
        /// <param name="bcc"></param>
        /// <param name="cc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="sendgridUserName"></param>
        /// <param name="sendgridPassword"></param>
        /// <returns></returns>

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

        /// <summary>
        /// SendMailByMandrill
        /// this function is used for sending mail vai Mandrill</summary></summary>
        /// the main parameter is
        /// <add key="host" value="smtp.mandrillapp.com" />
        /// <param name="port"></param>
        /// <add key="port" value="25" />
        /// <param name="from"></param>
        /// <add key="username" value="socioboard"/>
        /// <param name="passsword"></param>
        /// <add key="fromemail" value="xyz@xyz.com"/>
        /// <param name="to"></param>
        /// <add key="password" value="xyz" />
        /// <param name="bcc"></param>
        /// add key="tomail" value="xyz@xyz.com" />
        /// its return : success if mail send else return string.Empty;
        /// <param name="cc"></param>
        /// <param name="Host"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="sendgridUserName"></param>
        /// <param name="sendgridPassword"></param>
        /// <returns></returns>

        public string SendMailByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;

            //GetStatusFromSendMailByMandrill(Host, port, from, passsword, to, bcc, cc, subject, body, sendgridUserName, sendgridPassword);

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
                    //status = Mandrill.EmailResultStatus.Sent.ToString();
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


        public string SendMailByMandrill(string Host, int port, string from,string name, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;

            //GetStatusFromSendMailByMandrill(Host, port, from, passsword, to, bcc, cc, subject, body, sendgridUserName, sendgridPassword);

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
                string status = string.Empty;
                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent)
                    {
                        logger.Error(result.Email + " " + result.RejectReason);
                    }
                    //status = Mandrill.EmailResultStatus.Sent.ToString();
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

        /// <summary>
        /// SendMailByMandrill
        /// this function is used for sending mail vai Mandrill</summary></summary>
        /// the main parameter is
        /// <add key="host" value="smtp.mandrillapp.com" />
        /// <param name="port"></param>
        /// <add key="port" value="25" />
        /// <param name="from"></param>
        /// <add key="username" value="socioboard"/>
        /// <param name="passsword"></param>
        /// <add key="fromemail" value="xyz@xyz.com"/>
        /// <param name="to"></param>
        /// <add key="password" value="xyz" />
        /// <param name="bcc"></param>
        /// add key="tomail" value="xyz@xyz.com" />
        /// its return : EmailResultStatus i.e Sent, Rejected etc.
        /// <param name="cc"></param>
        /// <param name="Host"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="sendgridUserName"></param>
        /// <param name="sendgridPassword"></param>
        /// <returns></returns>

        public string GetStatusFromSendMailByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;
            try
            {

                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                //message.from_email = from;
                //message.from_name = from;//"AlexPieter";
                message.from_email = "support@socioboard.com";
                message.from_name = "Socioboard Support";
                message.html = body;
                message.subject = subject;
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };


                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(sendgridPassword, false);


                //List<RejectInfo> ri=mandrillApi.ListRejects();

                var results = mandrillApi.SendMessage(message);
                string status = string.Empty;
                foreach (var result in results)
                {
                    try
                    {
                        if (result.Status != Mandrill.EmailResultStatus.Sent)
                        {
                            logger.Error(result.Email + " " + result.RejectReason);

                        }

                        if (Mandrill.EmailResultStatus.Sent == result.Status)
                        {
                            status = Mandrill.EmailResultStatus.Sent.ToString();
                        }
                        else if (Mandrill.EmailResultStatus.Invalid == result.Status)
                        {
                            status = Mandrill.EmailResultStatus.Invalid.ToString();
                        }
                        else if (Mandrill.EmailResultStatus.Queued == result.Status)
                        {
                            status = Mandrill.EmailResultStatus.Queued.ToString();
                        }
                        else if (Mandrill.EmailResultStatus.Rejected == result.Status)
                        {
                            status = Mandrill.EmailResultStatus.Rejected.ToString();
                        }
                        else if (Mandrill.EmailResultStatus.Scheduled == result.Status)
                        {
                            status = Mandrill.EmailResultStatus.Scheduled.ToString();
                        }
                        else
                        {
                            status = result.RejectReason;
                        }
                        //status = Mandrill.EmailResultStatus;
                        //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        logger.Error(ex.Message);
                        sendMailByMandrill = ex.Message;
                    }
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


        /// <summary>
        /// SendMailByMandrill
        /// this function is used for Get Rejected Mail of Mandrill</summary></summary>
        /// the main parameter is
        /// <add key="host" value="smtp.mandrillapp.com" />
        /// <param name="port"></param>
        /// <add key="port" value="25" />
        /// <param name="from"></param>
        /// <add key="username" value="socioboard"/>
        /// <param name="passsword"></param>
        /// <add key="fromemail" value="xyz@xyz.com"/>
        /// <param name="to"></param>
        /// <add key="password" value="xyz" />
        /// <param name="bcc"></param>
        /// add key="tomail" value="xyz@xyz.com" />
        /// its return : List<RejectInfo> ie hardbouce , softbounce, rejected etc.
        /// /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="sendgridUserName"></param>
        /// <param name="sendgridPassword"></param>
        /// <returns></returns>

        public List<RejectInfo> GetListRejectInfoByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;
            List<RejectInfo> ri = new List<RejectInfo>();
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


                ri = mandrillApi.ListRejects();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex.Message);
                sendMailByMandrill = ex.Message;
            }

            return ri;
        }


        /// <summary>
        /// SendMailByMandrill
        /// this function is used for sending mail vai Mandrill for Enterprise Module</summary></summary>
        /// the main parameter is
        /// <add key="host" value="smtp.mandrillapp.com" />
        /// <param name="port"></param>
        /// <add key="port" value="25" />
        /// <param name="from"></param>
        /// <add key="username" value="socioboard"/>
        /// <param name="passsword"></param>
        /// <add key="fromemail" value="xyz@xyz.com"/>
        /// <param name="to"></param>
        /// <add key="password" value="xyz" />
        /// <param name="bcc"></param>
        /// add key="tomail" value="xyz@xyz.com" />
        /// its return : success if mail send else return string.Empty;
        /// <param name="cc"></param>
        /// <param name="Host"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="sendgridUserName"></param>
        /// <param name="sendgridPassword"></param>
        /// <returns></returns>


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
        //vikash
        public string SendCareerMailByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword, string file, string filename, string filetype)
        {
            string sendMailByMandrill = string.Empty;
            try
            {
                var fileBytes = File.ReadAllBytes(file);
                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                message.from_email = from;
                message.from_name = sendgridUserName;
                message.html = body;
                message.subject = subject;
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
                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(sendgridPassword, false);
                var results = mandrillApi.SendMessage(message);
                string status = string.Empty;
                string replysubject = string.Empty;
                string replybody = string.Empty;
                foreach (var result in results)
                {
                    if (result.Status == Mandrill.EmailResultStatus.Sent || result.Status == Mandrill.EmailResultStatus.Queued)
                    {
                        replysubject = "Resume Rcieved";
                        replybody = "Hello there,</br>We have received your resume. </br>Our HR team will connect to you in time.</br>Warm regards,<p>Best regards,<br/><br />" +
                        "Support Team<br/>Socioboard Technologies Pvt. Ltd.<br /><br /><a href=\"http://www.socioboard.com\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/logo-txt2_zpsc7861ad5.png\" alt=\"\"></a></p>" +
                        "<p style=\"font-family:Calibri(Body); font-size:12px;.\"><b>Mumbai Office:</b> Unit 206, Shri Krishna Building,Lokhandwala, Andheri West,Mumbai 400053India </br>" +
                        "<b>Phone:</b> +91-90090-23807, <b>Skype:</b> socioboard.support </br>Socioboard Enterprise and SaaS Versions: <a href=\"http://www.socioboard.com\">http://www.socioboard.com<br /></a> Socioboard Community Version: <a href=\"http://www.socioboard.org\">http://www.socioboard.org</a><br>" +
                        "</p><table><tr><td><a href=\"https://www.facebook.com/SocioBoard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/facebook-48_zps62d89d59.png\" alt=”Facebook” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://plus.google.com/s/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/googleplus-30_zps62d89d59.png\" alt=\"G+\"width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"http://www.linkedin.com/company/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/linkedin-48_zpsceb0f4e2.png\" alt=”LinkedIn” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://twitter.com/Socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/twitter-48_zps57c64c90.png\" alt=”Twitter” width=”35? height=”35? border=0></a></td>" +
                        "</tr></table>";
                    }
                    else
                    {
                        logger.Error(result.Email + " " + result.RejectReason);
                        replysubject = "Resume Not Recieved";
                        replybody = "Hi there,</br>Oops!!</br>Apparently there was some error and we couldn’t receive your resume.</br>Please try again." +
                        "<p>Best regards,<br />Support Team<br/>Socioboard Technologies Pvt. Ltd.<br /><a href=\"http://www.socioboard.com\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/logo-txt2_zpsc7861ad5.png\" alt=\"\"></a>" +
                        "<br/></p><p style=\"font-family:Calibri(Body); font-size:12px;\"><b>Bangalore Office:</b> L V Complex, 2nd Floor, No.58, 7th Block, 80 Feet Road, Koramangala, Bangalore-560095</br>" +
                        "Karnataka, India</br><b><br />Phone:</b> +91-90090-23807, <b>Skype:</b> socioboard.support </br><br />Socioboard Enterprise and SaaS Versions: <a href=\"http://www.socioboard.com\">http://www.socioboard.com<br />" +
                        "</a>  Socioboard Community Version: <a href=\"http://www.socioboard.org\">http://www.socioboard.org</a><br></p><table><tr>" +
                        "<td><a href=\"https://www.facebook.com/SocioBoard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/facebook-48_zps62d89d59.png\" alt=”Facebook” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://plus.google.com/s/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/googleplus-30_zps62d89d59.png\" alt=\"G+\"width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"http://www.linkedin.com/company/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/linkedin-48_zpsceb0f4e2.png\" alt=”LinkedIn” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://twitter.com/Socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/twitter-48_zps57c64c90.png\" alt=”Twitter” width=”35? height=”35? border=0></a></td>" +
                        "</tr></table>";
                    }
                    string rtn = ReplyForCareerMail(to, replybody, replysubject, from, sendgridPassword);
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

        public string SendDemoMailByMandrill(string Host, int port, string from, string passsword, string to, string bcc, string cc, string subject, string body, string sendgridUserName, string sendgridPassword)
        {
            string sendMailByMandrill = string.Empty;
            try
            {

                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                message.from_email = from;
                message.from_name = sendgridUserName;
                message.html = body;
                message.subject = subject;
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };
                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(sendgridPassword, false);
                var results = mandrillApi.SendMessage(message);
                string status = string.Empty;
                string replysubject = string.Empty;
                string replybody = string.Empty;
                foreach (var result in results)
                {
                    if (result.Status == Mandrill.EmailResultStatus.Sent || result.Status == Mandrill.EmailResultStatus.Queued)
                    {
                        replysubject = "Request Rcieved";
                        replybody = "Hello there,</br>We have received your request for a demo. </br>Our Support team will connect to you in time.</br>Warm regards,<p>Best regards,<br/><br />" +
                        "Support Team<br/>Socioboard Technologies Pvt. Ltd.<br /><br /><a href=\"http://www.socioboard.com\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/logo-txt2_zpsc7861ad5.png\" alt=\"\"></a></p>" +
                        "<p style=\"font-family:Calibri(Body); font-size:12px;.\"><b>Mumbai Office:</b> Unit 206, Shri Krishna Building,Lokhandwala, Andheri West,Mumbai 400053India </br>" +
                        "<b>Phone:</b> +91-90090-23807, <b>Skype:</b> socioboard.support </br>Socioboard Enterprise and SaaS Versions: <a href=\"http://www.socioboard.com\">http://www.socioboard.com<br /></a> Socioboard Community Version: <a href=\"http://www.socioboard.org\">http://www.socioboard.org</a><br>" +
                        "</p><table><tr><td><a href=\"https://www.facebook.com/SocioBoard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/facebook-48_zps62d89d59.png\" alt=”Facebook” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://plus.google.com/s/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/googleplus-30_zps62d89d59.png\" alt=\"G+\"width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"http://www.linkedin.com/company/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/linkedin-48_zpsceb0f4e2.png\" alt=”LinkedIn” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://twitter.com/Socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/twitter-48_zps57c64c90.png\" alt=”Twitter” width=”35? height=”35? border=0></a></td>" +
                        "</tr></table>";
                    }
                    else
                    {
                        logger.Error(result.Email + " " + result.RejectReason);
                        replysubject = "Request Not Recieved";
                        replybody = "Hi there,</br>Oops!!</br>Apparently there was some error and we couldn’t receive your resume.</br>Please try again." +
                        "<p>Best regards,<br />Support Team<br/>Socioboard Technologies Pvt. Ltd.<br /><a href=\"http://www.socioboard.com\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/logo-txt2_zpsc7861ad5.png\" alt=\"\"></a>" +
                        "<br/></p><p style=\"font-family:Calibri(Body); font-size:12px;\"><b>Bangalore Office:</b> L V Complex, 2nd Floor, No.58, 7th Block, 80 Feet Road, Koramangala, Bangalore-560095</br>" +
                        "Karnataka, India</br><b><br />Phone:</b> +91-90090-23807, <b>Skype:</b> socioboard.support </br><br />Socioboard Enterprise and SaaS Versions: <a href=\"http://www.socioboard.com\">http://www.socioboard.com<br />" +
                        "</a>  Socioboard Community Version: <a href=\"http://www.socioboard.org\">http://www.socioboard.org</a><br></p><table><tr>" +
                        "<td><a href=\"https://www.facebook.com/SocioBoard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/facebook-48_zps62d89d59.png\" alt=”Facebook” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://plus.google.com/s/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/googleplus-30_zps62d89d59.png\" alt=\"G+\"width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"http://www.linkedin.com/company/socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/linkedin-48_zpsceb0f4e2.png\" alt=”LinkedIn” width=”35? height=”35? border=0></a></td>" +
                        "<td><a href=\"https://twitter.com/Socioboard\" target=\"_blank\"><img src=\"http://i739.photobucket.com/albums/xx33/Alan_Wilson3526/twitter-48_zps57c64c90.png\" alt=”Twitter” width=”35? height=”35? border=0></a></td>" +
                        "</tr></table>";
                    }
                    string rtn = ReplyForCareerMail(to, replybody, replysubject, from, sendgridPassword);
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

        public string ReplyForCareerMail(string from, string body, string subject, string to, string password)
        {
            string sendMailByMandrill = string.Empty;
            try
            {
                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                message.from_email = from;
                message.from_name = "Socioboard Support";
                message.html = body;
                message.subject = subject;
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(to)
                };
                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(password, false);
                var results = mandrillApi.SendMessage(message);
                string status = string.Empty;
                foreach (var result in results)
                {
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

        public string SendFeedMailByMandrill(string Host, int port, string from, string passsword, string to, string subject, string body)
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

                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(passsword, false);
                var results = mandrillApi.SendMessage(message);

                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent)
                    {
                        // logger.Error(result.Email + " " + result.RejectReason);
                    }
                    //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));
                }

                sendMailByMandrill = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //logger.Error(ex.Message);
            }

            return sendMailByMandrill;
        }

        public string SendAddNewsLatterMail(string Host, int port, string from, string passsword, string to, string subject, string body)
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

                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(passsword, false);
                var results = mandrillApi.SendMessage(message);

                foreach (var result in results)
                {
                    if (result.Status != Mandrill.EmailResultStatus.Sent)
                    {
                        // logger.Error(result.Email + " " + result.RejectReason);
                    }
                    //  LogManager.Current.LogError(result.Email, "", "", "", null, string.Format("Email failed to send: {0}", result.RejectReason));
                }

                sendMailByMandrill = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //logger.Error(ex.Message);
            }

            return sendMailByMandrill;
        }

        public string SendInvitationMailByMandrill(string SenderEmail, string SenderName, string FriendsEmail, string password, string body)
        {
            string sendMailByMandrill = string.Empty;
            try
            {
                Mandrill.EmailMessage message = new Mandrill.EmailMessage();
                message.from_email = SenderEmail;
                message.from_name = SenderName;
                message.html = body;
                message.subject = "Your friend "+ SenderName + "has invited you to join socioboard";
                message.to = new List<Mandrill.EmailAddress>()
                {
                  new Mandrill.EmailAddress(FriendsEmail)
                };
                Mandrill.MandrillApi mandrillApi = new Mandrill.MandrillApi(password, false);
                var results = mandrillApi.SendMessage(message);
                foreach (var result in results)
                {
                    if (result.Status == Mandrill.EmailResultStatus.Sent || result.Status == Mandrill.EmailResultStatus.Queued)
                    {
                        sendMailByMandrill = "success";
                    }
                    else
                    {
                        sendMailByMandrill = "failed";
                    }
                }
            }
            catch (Exception ex)
            {
                sendMailByMandrill = "failed";
            }
            return sendMailByMandrill;
        }

    }
}
