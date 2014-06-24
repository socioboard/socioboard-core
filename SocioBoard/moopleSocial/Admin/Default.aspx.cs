using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace WooSuite.Admin
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
                if (txtUserName.Text.Equals("admin") && txtPassword.Text.Equals("admin"))
                {
                    Response.Redirect("Dashboard.aspx",false);
                }
            }
            catch (Exception Err)
            {
                logger.Error(Err.Message);
                Response.Write(Err.Message.ToString());
            }
        }
    }
}