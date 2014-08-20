using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocioBoard
{
    public partial class ActivationLink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ActivationError"] != null)
                {

                    message.InnerHtml = Session["ActivationError"].ToString();
                    Session["ActivationError"] = null;
                }
                else
                {
                    message.InnerHtml = "Please check your mail to Activate Your Account!";
                    Session["ActivationError"] = null;
                }

            }

        }
    }
}