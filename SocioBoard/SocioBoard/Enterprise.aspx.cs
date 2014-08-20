using System;
using System.Web.UI;
using System.Configuration;
using Recaptcha.Web;
using System.Text.RegularExpressions;
using SocioBoard.Helper;

namespace SocioBoard
{
    public partial class Enterprise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //    if (Request.QueryString["i"] == "1")
            //    {
            //        PayPalEnterprice_click();
            //    }


        }

        protected void PayPalEnterprice_click()
        {
            SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();

            string amount = "9999";
            string plantype = "Enterprise";
            string UserName = "Abhay";
            String EmailId = "abhaymondal@globussoft.com";

            String EnterPriseSuccessURL = ConfigurationManager.AppSettings["EnterPriseSuccessURL"];
            String EnterPriseFailedURL = ConfigurationManager.AppSettings["EnterPriseFailedURL"];
            String EnterPrisepaypalemail = ConfigurationManager.AppSettings["EnterPrisepaypalemail"];
            String userId = "";

            string pay = payme.PayWithPayPal(amount, plantype, UserName, "", EmailId, "USD", ConfigurationManager.AppSettings["EnterPrisepaypalemail"], ConfigurationManager.AppSettings["EnterPriseSuccessURL"],
                                    ConfigurationManager.AppSettings["EnterPriseFailedURL"], ConfigurationManager.AppSettings["EnterPriseSuccessURL"], ConfigurationManager.AppSettings["EnterPrisecancelurl"], ConfigurationManager.AppSettings["EnterPrisenotifyurl"], userId);

            Response.Redirect(pay);
        }

        protected void btncaptcha_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Recaptcha.Response))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Captcha cannot be empty.');", true);
                // Label1.Text = "Captcha casnnot be empty.";
            }
            else
            {
                RecaptchaVerificationResult verify = Recaptcha.Verify();
                if (verify == RecaptchaVerificationResult.Success)
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Captcha is correct');", true);

                    if (IsValidName(name.Value))
                    {
                        if (IsValidDesignation(designation.Value))
                        {
                            if (IsValidCompany(company.Value))
                            {
                                if (IsValidEmail(email.Value))
                                {
                                    if (IsItNumber(phone.Value))
                                    {
                                        if (name.Value == "" || designation.Value == "" || company.Value == "" || location.Value == "" || website.Value == "" || email.Value == "" || phone.Value == "" || message.Value == "")
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill all fields!');", true);
                                            return ;
                                        }
                                        else
                                        {
                                            sendmail(name.Value, designation.Value, company.Value, location.Value, website.Value, email.Value, phone.Value, message.Value);
                                            name.Value = null;
                                            designation.Value = null;
                                            company.Value = null;
                                            location.Value = null;
                                            website.Value = null;
                                            email.Value = null;
                                            phone.Value = null;
                                            message.Value = null;
                                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Mail send successfully');", true);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Phone No');", true);
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid email address');", true);
                                }

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Company Name');", true);
                            }

                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Designation');", true);
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid Name');", true);
                    }


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Captcha information not matched');", true);
                    //  Label1.Text = "Captcha information not matched";
                }
            }
        }

        public string sendmail(string name, string designation, string company, string location, string website, string email, string phone, string message)
        {
            string ret = string.Empty;

            string tomail = ConfigurationManager.AppSettings["tomail"];
            string usernameSend = ConfigurationManager.AppSettings["username"];
            string host = ConfigurationManager.AppSettings["host"];
            string port = ConfigurationManager.AppSettings["port"];
            string pass = ConfigurationManager.AppSettings["password"];
            string Subject = "Enterprise Edition Enquiry";
            string Body = "Name: " + name + "</br>" + "Company: " + company + "</br>" + "Location: " + location + "</br>" + "Website: " + website + "</br>" + "Email: " + email + "</br>" + "Phone: " + phone + "</br>" + "Message: " + message + "</br>";
            MailHelper objMailHelper = new MailHelper();
            objMailHelper.SendMailByMandrillForEnterprise(name, host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, Body, usernameSend, pass);
           // objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email, "", tomail, "", "", Subject, Body, usernameSend, pass);




            return ret;
        }

        bool IsValidEmail(string email)
        {
            bool ret = false;
            ret = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return ret;
        }
        public static bool IsItNumber(string inputvalue)
        {
            Regex isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(inputvalue);
        }

        public static bool IsValidName(string name)
        {
            bool ret = false;
            ret = Regex.IsMatch(name, @"[a-z_]+$");
            return ret;
        }


        bool IsValidDesignation(string designation)
        {
            bool ret = false;
            ret = Regex.IsMatch(designation, @"[a-z_]+$");
            return ret;
        }

        bool IsValidCompany(string Company)
        {
            bool ret = false;
            ret = Regex.IsMatch(Company, @"[a-z_]+$");
            return ret;
        }

    }
}