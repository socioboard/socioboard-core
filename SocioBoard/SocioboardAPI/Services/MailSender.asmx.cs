using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using GlobusMailLib;

namespace Api.Socioboard.Services
{
    /// <summary>
    /// Summary description for Group
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class MailSender : System.Web.Services.WebService
    {
        public string usernameSend=string.Empty;
        public string pass=string.Empty;


        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendMail(string name, string lname, string email, string Subject, string profile)
        {

            MailSenderFactory objMailSenderFactory=null;
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
        
            objMailSenderFactory = MailHelper();
            string subject = Subject;
            string Body = "FirstName: " + name + "</br>" + "LastName:" + lname + "</br>" + "Email: " + email + "</br>" + "Subject:" + Subject + "</br>" + "Message: " + profile + "</br>";

            // GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

            //MailHelper objMailHelper = new MailHelper();
            //  objMailHelper.SendMailByMandrillForEnterprise(name, host, Convert.ToInt32(port), email, "", "abhaymondal@globussoft.com", "", "", Subject, Body, usernameSend, pass);
            //ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, Body, usernameSend, pass);

            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", tomail, "", "", subject, Body,usernameSend,pass);

            return ret; 

        }

        public MailSenderFactory MailHelper()
        {
            MailSenderFactory objMailSenderFactory = null;
            string mailtype = ConfigurationManager.AppSettings["MailType"];

            if (mailtype == "Gmail")
            {
                usernameSend = ConfigurationManager.AppSettings["GoogleUserName"];
                pass = ConfigurationManager.AppSettings["GooglePassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Gmail);
            }
            else if (mailtype == "Mandrill")
            {
                usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
                pass = ConfigurationManager.AppSettings["Mandrillpassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Mandrill);
            }
            else if (mailtype == "Sendgrid")
            {
                usernameSend = ConfigurationManager.AppSettings["GendgridUserName"];
                pass = ConfigurationManager.AppSettings["GendgridPassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Sendgrid);
            }
            else if (mailtype == "Zoho")
            {
                usernameSend = ConfigurationManager.AppSettings["ZohoUserName"];
                pass = ConfigurationManager.AppSettings["ZohoPassword"];
                objMailSenderFactory = new MailSenderFactory(MailSendingType.Zoho);
            }
            return objMailSenderFactory;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendAgencyMail(string name, string lname, string email, string Company, string message, string Phone)
        {
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            MailSenderFactory objMailSenderFactory = MailHelper();
            string subject="Socioboard Agency";
            string Body = "Name: " + name + "" + lname + "</br>" + "Email: " + email + "</br>" + "Company: " + Company + "</br>" + "Message: " + message + "</br>" + "Phone: " + Phone + "</br>";
            //GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            //ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Company, Body, usernameSend, pass);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", tomail, "", "", subject, Body, usernameSend, pass);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendEnterpriseMail(string name, string designation, string email, string location, string Company, string companywebsite, string message, string Phone)
        {
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            MailSenderFactory objMailSenderFactory = MailHelper();
            string subject = "Socioboard Enterprise";
            string Body = "Name: " + name + "</br>" + "Designation:" + designation + "</br>" + "Company: " + Company + "</br>" + "Location: " + location + "</br>" + "Email: " + email + "</br>" + "Company Website: " + companywebsite + "</br>" + "Message: " + message + "</br>" + "Phone: " + Phone + "</br>";
            //GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            //ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Company, Body, usernameSend, pass);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", tomail, "", "", subject, Body, usernameSend, pass);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendCareerMail(string name, string lname, string email, string Subject, string profile, string filepath, string filename, string filetype)
        {
            string ret = string.Empty;
            MailSenderFactory objMailSenderFactory = MailHelper();
            string tomail = ConfigurationManager.AppSettings["tomail"];
          
            string subject = Subject;
            string Body = "Name: " + name + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile + "</br>";

            // GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            //MailHelper objMailHelper = new MailHelper();
            //ret = objMailHelper.SendCareerMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, Body, usernameSend, pass, filepath, filename, filetype);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMailWithAttachment(tomail, "", tomail, "", "", Subject, Body, filepath, filename, filepath, usernameSend, pass);
            string replysubject = string.Empty;
            string replybody = string.Empty;
            if (ret=="Success")
            {
                replysubject = "Resume Received";
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
                replysubject = "Resume Not Received";
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
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", email, "", "", replysubject, replybody, usernameSend, pass);
            return ret;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendRequestForDemo(string name, string lname, string email, string Subject, string body)
        {
            string ret = string.Empty;
            MailSenderFactory objMailSenderFactory = MailHelper();
            string tomail = ConfigurationManager.AppSettings["tomail"];
          
            //MailHelper objMailHelper = new MailHelper();
            //ret = objMailHelper.SendDemoMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, body, name + " " + lname, pass);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", tomail, "", "", Subject, body, usernameSend, pass);
            string replysubject = string.Empty;
            string replybody = string.Empty;
            if (ret=="Success")
            {
                replysubject = "Request Received";
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
                replysubject = "Request Not Received";
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
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(tomail, "", email, "", "", replysubject, replybody, usernameSend, pass);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendChangePasswordMail(string emailId, string mailBody, string Subject)
        {
            string ret = string.Empty;
            MailSenderFactory objMailSenderFactory = MailHelper();
            string from = ConfigurationManager.AppSettings["tomail"];
            // MailHelper objMailHelper = new MailHelper();
            //ret = objMailHelper.SendFeedMailByMandrill(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            //ret = objMailHelper.SendMailBySendGrid("", Int32.Parse(port), "suport@socioboard.com","", emailId, "", "", Subject, mailBody, "", "");
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(from, "", emailId, "", "", Subject, mailBody, usernameSend, pass);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendFeedMail(string emailId, string feed, string fromname, string mailBody)
        {
            string ret = string.Empty;
            MailSenderFactory objMailSenderFactory = MailHelper();
            string from = ConfigurationManager.AppSettings["tomail"];
            string Subject = "Reported by " + fromname + " through " + ConfigurationManager.AppSettings["DefaultGroupName"];
            //MailHelper objMailHelper = new MailHelper();
            //ret = objMailHelper.SendFeedMailByMandrill(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(from, "", emailId, "", "", Subject, mailBody,usernameSend,pass);
            return ret;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendAddNewsLatterMail(string emailId, string mailBody, string Subject)
        {
            string ret = string.Empty;
            MailSenderFactory objMailSenderFactory = MailHelper();
            string from = ConfigurationManager.AppSettings["tomail"];
            //MailHelper objMailHelper = new MailHelper();
            //ret = objMailHelper.SendAddNewsLatterMail(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(from, "", emailId, "", "", Subject, mailBody, usernameSend, pass);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendTaskNotificationMail(string emailId, string mailBody, string Subject)
        {
            string ret = string.Empty;
            MailSenderFactory objMailSenderFactory = MailHelper();
            string from = ConfigurationManager.AppSettings["tomail"];
            //MailHelper objMailHelper = new MailHelper();
            //ret = objMailHelper.SendFeedMailByMandrill(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            ret = objMailSenderFactory.GetMailSenderInstance().SendMail(from, "", emailId, "", "", Subject, mailBody, usernameSend, pass);
            return ret;
        }
           
    }
}
