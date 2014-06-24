using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Helper;
using System.Configuration;
using SocioBoard.Model;
using SocioBoard.Domain;
using log4net;

namespace letTalkNew
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(ForgotPassword));
        string userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtpass.Text = "";
            if (Request.QueryString["userid"] != null)
            {
                userid = Request.QueryString["userid"].ToString();
            }
            else
            {
                lblerror.Text = "Problem Password Reset";
                Response.Redirect("login.aspx");
            }
        }
        protected void btnResetPwd_Click(object sender, EventArgs e)
        {
            try
            {
                Registration regpage = new Registration();
                string changedpassword = regpage.MD5Hash(txtpass.Text);
                UserRepository userrepo = new UserRepository();
                if (userrepo.ResetPassword(Guid.Parse(userid.ToString()), changedpassword.ToString()) > 0)
                {
                    lblerror.Text = "Password Reset Successfully";
                }
                else
                {
                    lblerror.Text = "Problem Password Reset";
                }

            }
            catch (Exception Err)
            {
                logger.Error(Err.StackTrace);
            }
        }

        
    }
}