using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using SocioBoard.Helper;
using SocioBoard.Domain;
using SocioBoard.Model;

namespace SocialSuitePro.Admin
{
    public partial class Default : System.Web.UI.Page
    {
        ILog logger = LogManager.GetLogger(typeof(Default));

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                SessionFactory.configfilepath = Server.MapPath("~/hibernate.cfg.xml");
                AdminRepository adminRepo = new AdminRepository();
                SocioBoard.Domain.Admin admin = adminRepo.GetUserInfo(txtUserName.Text, txtPassword.Text);
                if (admin != null)
                {
                    Session["AdminProfile"] = admin;
                    Response.Redirect("Dashboard.aspx");
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}