using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Model;
using System.Web.Security;
using SocioBoard.Helper;
using System.Security.Cryptography;
using System.Text;

namespace SocioBoard
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // btnLogin_Click(null,null);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserRepository userrepo = new UserRepository();
                Registration regObject = new Registration();
                User user = userrepo.GetUserInfo(txtLoginId.Text, regObject.MD5Hash(txtPassword.Text));
               
                if (user.PaymentStatus == "unpaid")
                    {
                        if (DateTime.Compare(DateTime.Now, user.ExpiryDate) < 0)
                        {
                            if (user != null)
                            {
                                Session["LoggedUser"] = user;
                                FormsAuthentication.SetAuthCookie(user.UserName, true);
                                Response.Redirect("/Home.aspx", false);
                            }
                            else
                            {
                               
                            }
                        }
                        else
                        {
                            Response.Redirect("Setting/Billing.aspx");
                        }
                    }
                    else
                    {
                        Session["LoggedUser"] = user;
                        FormsAuthentication.SetAuthCookie(user.UserName, true);
                        Response.Redirect("/Home.aspx", false);
                    }


                
            }
            catch (Exception ex)
            {

                // logger.Error("Inside buttonclick of login page "+ ex.Message);

                Console.WriteLine(ex.StackTrace);
            }

        }

        protected void btnSignup_Click(object sender, ImageClickEventArgs e)
        {
            User user = new User();
            UserRepository userrepo = new UserRepository();

            try
            {
                if (txtPasswordSignUp.Text == txtConfirmPassword.Text)
                {
                    user.PaymentStatus = "unpaid";
                    user.AccountType = Request.QueryString["type"];
                    if (string.IsNullOrEmpty(user.AccountType))
                    {
                        user.AccountType = "deluxe";
                    }

                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Id = Guid.NewGuid();
                    user.UserName = txtFirstName.Value + " " + txtLastName.Value;
                    user.Password = this.MD5Hash(txtPasswordSignUp.Text);
                    user.EmailId = txtEmail.Value;
                    user.UserStatus = 1;
                    if (!userrepo.IsUserExist(user.EmailId))
                    {
                        UserRepository.Add(user);
                        MailSender.SendEMail(user.UserName, txtPasswordSignUp.Text, txtEmail.Value);
                        lblError.InnerHtml = "Registered Successfully !";
                        Session["LoggedUser"] = user;
                        Response.Redirect("Home.aspx");
                    //    lblerror.Text = "Registered Successfully !" + "<a href=\"login.aspx\">Login</a>";
                    }
                    else
                    {
                        lblregerror.Visible = true;
                        lblregerror.Text = "Email Already Exists " + "<a href=\"Login.aspx\">login</a>";
                    }
                }

            }
            catch (Exception ex)
            {
               // lblerror.Text = "Please Insert Correct Information";


                Console.WriteLine(ex.StackTrace);
            }
        }
        public string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }


    }
}