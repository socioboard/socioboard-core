using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocialScoup.Settings
{
    public partial class BillingCancel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Paypall Cancel", "<script type=\"text/javascript\">alert('Your transaction has been cancel !');</script>", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Script", "MyJavascriptFunction();", true);

                Response.Redirect("../Home.aspx?paymentTransaction=Cancel");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}