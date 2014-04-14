using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using SocioBoard.Helper;
using System.Configuration;

namespace SocioBoard
{
    public partial class Company : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["response"] != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(" + Session["response"].ToString() + ");", true);
                Label1.Attributes.CssStyle.Add("margin-left", "7%");
                Label1.Text = "Mail  send successfully";
                Session["response"] = null;
                Session["career"] = null;
                Session["careerinfo"] = null;
            }

            if (Session["career"] != null)
            {
                check();
                Session["career"] = null;
                if (Session["careerinfo"] != null)
                {
                    fillinfo();
                }
            }

        }

        private void fillinfo()
        {
            try
            {
                string[] str = Regex.Split(Session["careerinfo"].ToString(), "<:>");
                fname.Text = str[0];
                lname.Text = str[1];
                email.Text = str[2];
                message.InnerText = str[3];
                Session["careerinfo"] = null;
            }
            catch (Exception ex)
            {
                // Label1.Text = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }



        private void check()
        {
            try
            {
                if (Session["career"] == "field")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill all the fields!');", true);
                }
                if (Session["career"] == "email")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Email id is not correct!');", true);
                } if (Session["career"] == "nofile")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload your cv!');", true);
                }
                if (Session["career"] == "invaliedfile")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Invalid file format!');", true);
                }
                if (Session["career"] == "bigsize")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Maximum file size is 300 kb!');", true);
                }
            }
            catch (Exception ex)
            {
                // Label1.Text = ex.Message;
                Console.WriteLine(ex.Message);
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            try
            {
                string fullpath = string.Empty;

                if (fname.Text.Trim() == "" || lname.Text.Trim() == "" || email.Text.Trim() == "" || message.InnerText.Trim() == "")
                {
                    Session["careerinfo"] = fname.Text.Trim() + "<:>" + lname.Text.Trim() + "<:>" + email.Text.Trim() + "<:>" + message.InnerText.Trim();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill all the fields !');", true);
                    Session["career"] = "field";
                    Response.Redirect("Company.aspx#verticalTab4|company4");

                }
                if (!emailvalidation(email.Text.Trim()))
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill all the fields !');", true);
                    Session["career"] = "email";
                    Response.Redirect("Company.aspx#verticalTab4|company4");
                }

                Session["careerinfo"] = fname.Text.Trim() + "<:>" + lname.Text.Trim() + "<:>" + email.Text.Trim() + "<:>" + message.InnerText.Trim();

                if (cvfile.HasFile)
                {

                    string filename = Guid.NewGuid() + cvfile.FileName;
                    filename = filename.Replace(" ", "");
                    string path = Server.MapPath("~/Contents/cv/" + filename);
                    string extension = Path.GetExtension(filename).ToLower().Replace(".", "");
                    if (extension != "doc" && extension != "docx")
                    {
                        Session["career"] = "invaliedfile";
                        Response.Redirect("Company.aspx#verticalTab4|company4");
                    }
                    int size = ((cvfile.PostedFile.ContentLength) / 1024);
                    if (size > 3)
                    {
                        Session["career"] = "bigsize";
                        Response.Redirect("Company.aspx#verticalTab4|company4");
                    }
                    cvfile.SaveAs(path);
                    fullpath = ConfigurationManager.AppSettings["MailSenderDomain"] + "Contents/cv/" + filename;
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please fill all the fields !');", true);
                    Session["career"] = "nofile";
                    Response.Redirect("Company.aspx#verticalTab4|company4");
                }
                //send mail
                string MailBody = "<body bgcolor=\"#FFFFFF\"><!-- Email Notification from socialscoup.socioboard.com-->" +
                   "<table id=\"Table_01\" style=\"margin-top: 50px; margin-left: auto; margin-right: auto;\"" +
                   " align=\"center\" width=\"650px\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" ><tr>" +
                  "<td height=\"20px\" style=\"background-color: rgb(222, 222, 222); text-align: center; font-size: 15px; font-weight: bold; font-family: Arial; color: rgb(51, 51, 51); float: left; width: 100%; margin-top: 7px; padding-top: 10px; border-bottom: 1px solid rgb(204, 204, 204); padding-bottom: 10px;\">" +
                      "Socioboard</td></tr><!--Email content--><tr>" +
                  "<td style=\"background-color: #dedede; padding-top: 10px; padding-left: 25px; padding-right: 25px; padding-bottom: 30px; font-family: Tahoma; font-size: 14px; color: #181818; min-height: auto;\"><p>Name , " + fname.Text + " " + lname.Text + "</p><p>" +
                  "email id is " + email.Text + "<a href=\"" + fullpath + "\" style=\"text-decoration:none;\"> Click here </a> to Downlaod</p><p>" +
                      "Message : " + message.InnerText + "</td></tr><tr>" +
                  "<td style=\"background-color: rgb(222, 222, 222); margin-top: 10px; padding-left: 20px; height: 20px; color: rgb(51, 51, 51); font-size: 15px; font-family: Arial; border-top: 1px solid rgb(204, 204, 204); padding-bottom: 10px; padding-top: 10px;\">Thanks" +
                  "</td></tr></table><!-- End Email Notification From socialscoup.socioboard.com --></body>";

                string username = ConfigurationManager.AppSettings["username"];
                string host = ConfigurationManager.AppSettings["host"];
                string port = ConfigurationManager.AppSettings["port"];
                string pass = ConfigurationManager.AppSettings["password"];
                string tomail = ConfigurationManager.AppSettings["tomail"];

                //   string Body = mailformat.VerificationMail(MailBody, txtEmail.Text.ToString(), "");
                string Subject = "Socioboard career";
                //MailHelper.SendMailMessage(host, int.Parse(port.ToString()), username, pass, txtEmail.Text.ToString(), string.Empty, string.Empty, Subject, MailBody);
                //MailHelper.SendSendGridMail(host, Convert.ToInt32(port), from, "", from, string.Empty, string.Empty, Subject, MailBody, username, pass);

                MailHelper objMailHelper = new MailHelper();
                Session["response"] = objMailHelper.SendMailByMandrill(host, Convert.ToInt32(port), email.Text, "", tomail, string.Empty, string.Empty, Subject, MailBody, username, pass);
                //send mail
                Session["careerinfo"] = null;
                Session["career"] = null;
                Response.Redirect("Company.aspx#verticalTab4|company4");
                // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Thanks for your interest ');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(" + res + ");", true);
            }
            catch (Exception ex)
            {
                // Label1.Text = ex.Message;
                Console.WriteLine(ex.Message);

            }
        }


        public bool emailvalidation(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

        }
    }
}