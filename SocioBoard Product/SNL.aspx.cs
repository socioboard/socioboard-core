using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;

using SocioBoard.Model;
using log4net;
using SocioBoard.Helper;
using SocioBoard;

namespace SocialSuitePro
{
    public partial class SocialNetworkLogin : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(SocialNetworkLogin));

        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["LoggedUser"];
            if (!IsPostBack)
            {
                if (user == null)
                    Response.Redirect("Default.aspx");

                txtEmail.Text = user.EmailId;
                string[] name = user.UserName.Split(' ');
                txtFirstName.Text = name[0];
                txtLastName.Text = name[1];
            }
        }

        protected void btnRegister_Click(object sender, ImageClickEventArgs e)
        {
            Session["login"] = null;
            Registration regpage = new Registration();
            User user = (User)Session["LoggedUser"];

            if (user != null)
            {
                user.EmailId = txtEmail.Text;
                user.UserName = txtFirstName.Text + " " + txtLastName.Text;
              
                UserRepository userrepo = new UserRepository();
                if (userrepo.IsUserExist(user.EmailId))
                {

                    try
                    {
                        user.AccountType = Request.QueryString["type"];
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.StackTrace);
                        Console.WriteLine(ex.StackTrace);
                    }
                    if (string.IsNullOrEmpty(user.Password))
                    {
                        user.Password = regpage.MD5Hash(txtPassword.Text);
                        userrepo.UpdatePassword(user.EmailId, user.Password, user.Id, user.UserName,user.AccountType);
                        MailSender.SendEMail(txtFirstName.Text + " " + txtLastName.Text, txtPassword.Text, txtEmail.Text);
                    }
                }
                Session["LoggedUser"] = user;

                Response.Redirect("Home.aspx");
            }
        }
    }
}