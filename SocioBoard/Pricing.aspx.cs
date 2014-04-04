using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SocioBoard.Helper;

namespace SocialSuitePro.MasterPage
{
    public partial class Pricing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["login"] != null)
                    {
                        standardplan.HRef = "NetworkLogin.aspx?type=" + AccountType.Standard.ToString();
                        deluxeplan.HRef = "NetworkLogin.aspx?type=" + AccountType.Deluxe.ToString();
                        premiumplan.HRef = "NetworkLogin.aspx?type=" + AccountType.Premium.ToString();
                        freePlan.HRef = "NetworkLogin.aspx?type=" + AccountType.Free.ToString();
                    }
                    else if (Session["AjaxLogin"] != null)
                    {
                        standardplan.HRef = "Home.aspx?type=" + AccountType.Standard.ToString(); ;
                        deluxeplan.HRef = "Home.aspx?type=" + AccountType.Deluxe.ToString();
                        premiumplan.HRef = "Home.aspx?type=" + AccountType.Premium.ToString();
                        freePlan.HRef = "Home.aspx?type=" + AccountType.Free.ToString();
                        Session["AjaxLogin"] = null;
                    }
                    else
                    {
                        standardplan.HRef = "Registration.aspx?type=" + AccountType.Standard.ToString();
                        deluxeplan.HRef = "Registration.aspx?type=" + AccountType.Deluxe.ToString();
                        premiumplan.HRef = "Registration.aspx?type=" + AccountType.Premium.ToString();
                        freePlan.HRef = "Registration.aspx?type=" + AccountType.Free.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.StackTrace);
            }
        }
    }
}