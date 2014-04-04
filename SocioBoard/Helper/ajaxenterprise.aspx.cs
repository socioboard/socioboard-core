using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocioBoard.Helper
{
    public partial class ajaxenterprise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString != null)
            {
                string name = Request.QueryString["name"];
                string designation = Request.QueryString["designation"];
                string company = Request.QueryString["company"];
                string location = Request.QueryString["location"];
                string website = Request.QueryString["website"];
                string email = Request.QueryString["email"];
                string phone = Request.QueryString["phone"];
                string message = Request.QueryString["message"];
                string ccaptcha = Request.QueryString["captcha"];
                string scaptcha = Session["captcha"].ToString();


                string tomail = ConfigurationManager.AppSettings["tomail"];
                string usernameSend = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];
                string Subject = "Enterprise Edition Enquiry";
                string Body = "Name: " + name + "</br>" + "Company: " + company + "</br>" + "Location: " + location + "</br>" + "Website: " + website + "</br>" + "Email: " + email + "</br>" + "Phone: " + phone + "</br>" + "Message: " + message + "</br>";
                if (ccaptcha == scaptcha)
                {
                   // GlobusMailLib.MailHelper objMailHelper = new GlobusMailLib.MailHelper();

                   MailHelper objMailHelper = new MailHelper();
                   objMailHelper.SendMailByMandrillForEnterprise(name, host, Convert.ToInt32(port), email, "", "ajaypandey@globussoft.com", "", "", Subject, Body, usernameSend, pass);
                   // objMailHelper.SendMailByMandrillForEnterPrise(name, host, Convert.ToInt32(port), email, "", "abhaymondal@globussoft.com", "", "", Subject, Body, usernameSend, pass);
                    Response.Write("success");
                }
                else
                {
                    Response.Write("Wrong captcha");
                }
            }
        }
    }
}