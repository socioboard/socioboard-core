using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Domain;
using SocioBoard.Helper;
namespace WooSuite
{
    public partial class Plans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["login"] != null)
                {
                    standardplan.HRef = "SocialNetworkLogin.aspx?type=" + AccountType.Standard.ToString();
                    deluxeplan.HRef = "SocialNetworkLogin.aspx?type=" + AccountType.Deluxe.ToString();
                    premiumplan.HRef = "SocialNetworkLogin.aspx?type=" + AccountType.Premium.ToString();
                }
                else if (Session["AjaxLogin"] != null)
                {
                    standardplan.HRef = "Home.aspx?type=" + AccountType.Standard.ToString(); ;
                    deluxeplan.HRef = "Home.aspx?type="+AccountType.Deluxe.ToString();
                    premiumplan.HRef = "Home.aspx?type="+AccountType.Premium.ToString();
                    Session["AjaxLogin"] = null;
                }
                else if (Session["FirstRegistration"] != null)
                {


                }
                else
                {
                    standardplan.HRef = "Registration.aspx?type=" + AccountType.Standard.ToString();
                    deluxeplan.HRef = "Registration.aspx?type=" + AccountType.Deluxe.ToString();
                    premiumplan.HRef = "Registration.aspx?type=" + AccountType.Premium.ToString();
                }
            }
        }



    }
}