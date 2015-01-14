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
       
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendMail(string name, string lname, string email, string Subject, string profile)
        {
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string subject = Subject;
            string Body = "FirstName: " + name + "</br>" + "LastName:" + lname + "</br>" + "Email: " + email + "</br>" + "Subject:" + Subject + "</br>" + "Message: " + profile + "</br>";

            // GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

            MailHelper objMailHelper = new MailHelper();
            //  objMailHelper.SendMailByMandrillForEnterprise(name, host, Convert.ToInt32(port), email, "", "abhaymondal@globussoft.com", "", "", Subject, Body, usernameSend, pass);
            ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, Body, usernameSend, pass);

            return ret;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendAgencyMail(string name, string lname, string email, string Company,string message,string Phone)
        {
            
            string ret=string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string Body = "Name: " + name + "" + lname + "</br>" + "Email: " + email + "</br>" + "Company: " + Company + "</br>" + "Message: " + message + "</br>" + "Phone: " + Phone + "</br>";
            GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Company, Body, usernameSend, pass);
            return ret;

        }
       
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendEnterpriseMail(string name, string designation, string email,string location, string Company,string companywebsite, string message, string Phone)
        {

            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string Body = "Name: " + name + "</br>" + "Designation:" + designation + "</br>" + "Company: " + Company + "</br>" + "Location: " + location + "</br>" + "Email: " + email + "</br>" + "Company Website: " + companywebsite+"</br>" + "Message: " + message + "</br>" + "Phone: " + Phone + "</br>";
            GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();
            ret = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Company, Body, usernameSend, pass);
            return ret;

        }
       
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendCareerMail(string name, string lname, string email, string Subject, string profile, string filepath, string filename, string filetype)
        {
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string subject = Subject;
            string Body = "Name: " + name + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile + "</br>";

            // GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

            MailHelper objMailHelper = new MailHelper();
            //  objMailHelper.SendMailByMandrillForEnterprise(name, host, Convert.ToInt32(port), email, "", "abhaymondal@globussoft.com", "", "", Subject, Body, usernameSend, pass);
            ret = objMailHelper.SendCareerMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, Body, usernameSend, pass, filepath, filename, filetype);

            return ret;

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendRequestForDemo(string name, string lname, string email, string Subject, string body)
        {
            string ret = string.Empty;
            string tomail = ConfigurationManager.AppSettings["tomail"];
            string usernameSend = ConfigurationManager.AppSettings["Mandrillusername"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            MailHelper objMailHelper = new MailHelper();
            ret = objMailHelper.SendDemoMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, body, name + " " + lname, pass);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendChangePasswordMail(string emailId, string mailBody, string Subject)
        {
            string ret = string.Empty;
            string username = ConfigurationManager.AppSettings["username"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string from = ConfigurationManager.AppSettings["tomail"];
            // string from = ConfigurationManager.AppSettings["Mandrillusername"];

            MailHelper objMailHelper = new MailHelper();
            ret = objMailHelper.SendFeedMailByMandrill(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendFeedMail(string emailId, string feed, string fromname, string mailBody)
        {
            string ret = string.Empty;
            string username = ConfigurationManager.AppSettings["username"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            //   string from = ConfigurationManager.AppSettings["Mandrillusername"];
            string from = ConfigurationManager.AppSettings["tomail"];
            string Subject = "Reported by " + fromname + " through Socio Board";
            MailHelper objMailHelper = new MailHelper();
            ret = objMailHelper.SendFeedMailByMandrill(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            return ret;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendAddNewsLatterMail(string emailId, string mailBody, string Subject)
        {
            string ret = string.Empty;
            string username = ConfigurationManager.AppSettings["username"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string from = ConfigurationManager.AppSettings["tomail"];
            // string from = ConfigurationManager.AppSettings["Mandrillusername"];

            MailHelper objMailHelper = new MailHelper();
            ret = objMailHelper.SendAddNewsLatterMail(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            return ret;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string SendTaskNotificationMail(string emailId, string mailBody, string Subject)
        {
            string ret = string.Empty;
            string username = ConfigurationManager.AppSettings["username"];
            string host = ConfigurationManager.AppSettings["Mandrillhost"];
            string port = ConfigurationManager.AppSettings["Mandrillport"];
            string pass = ConfigurationManager.AppSettings["Mandrillpassword"];
            string from = ConfigurationManager.AppSettings["tomail"];
            // string from = ConfigurationManager.AppSettings["Mandrillusername"];

            MailHelper objMailHelper = new MailHelper();
            ret = objMailHelper.SendFeedMailByMandrill(host, Convert.ToInt32(port), from, pass, emailId, Subject, mailBody);
            return ret;
        }
    }
}
