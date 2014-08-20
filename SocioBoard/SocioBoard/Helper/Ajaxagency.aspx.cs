using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocioBoard.Helper
{
    public partial class Ajaxagency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Form != null)
            {
                string fname = Request.Form["fname"].ToString();
                string lname = Request.Form["lname"].ToString();
                string email = Request.Form["email"].ToString();
                string company = Request.Form["company"].ToString();
                string profile = Request.Form["profile"].ToString();
                string phone = Request.Form["phone"].ToString();


                string tomail = ConfigurationManager.AppSettings["tomail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];
                string subject = "Inquiry from Agency Page";
                string Body = "Name: " + fname + " " + lname + "</br>" + "Email: " + email + "</br>" + "Message: " + profile + "</br>" + "company: " + company + "</br>" + "phone: " + phone + "</br>";

                // GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

                MailHelper objMailHelper = new MailHelper();
                //  objMailHelper.SendMailByMandrillForEnterprise(name, host, Convert.ToInt32(port), email, "", "abhaymondal@globussoft.com", "", "", Subject, Body, usernameSend, pass);
                objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", subject, Body, usernameSend, pass);
                Response.Write("success");



            }

        }
    }
}