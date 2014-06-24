using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SocialScoup.Settings
{
    public partial class Billingfailed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Paypall Failed", "alert('Your transaction has been failed !');", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "Script", "MyJavascriptFunction();", true);

                Response.Redirect("../Home.aspx?paymentTransaction=Failed");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}