using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;


namespace SocialSuitePro.Settings
{
    public partial class BusinessSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["LoggedUser"];
                memberName.Text = user.UserName;
            }
        }

       
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}