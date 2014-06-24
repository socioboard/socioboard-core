using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using System.Security.Cryptography;
using System.Text;
using SocioBoard.Model;
using System.IO;
using System.Configuration;
using SocioBoard.Helper;


namespace SocioBoard
{
    public partial class Registration : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignup_Click(object sender, ImageClickEventArgs e)
        {
            User user = new User();
            UserRepository userrepo = new UserRepository();

            try
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    user.AccountType = "free";
                    user.CreateDate = DateTime.Now;
                    user.ExpiryDate = DateTime.Now.AddMonths(1);
                    user.Id = Guid.NewGuid();
                    user.UserName = txtUserName.Text;
                    user.Password = this.MD5Hash(txtPassword.Text);
                    user.EmailId = txtEmail.Text;
                    user.UserStatus = 1;
                    if (!userrepo.IsUserExist(user.EmailId))
                    {
                        UserRepository.Add(user);
                        MailSender.SendEMail(txtUserName.Text, txtPassword.Text, txtEmail.Text);
                        lblerror.Text = "registered successfully !" + "<a href=\"login.aspx\">login</a>";
                    }
                    else
                    {
                        lblerror.Text = "email already exists " + "<a href=\"login.aspx\">login</a>";
                    }
                }

            }
            catch (Exception ex)
            {
                lblerror.Text = "Please Insert Correct Information";
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