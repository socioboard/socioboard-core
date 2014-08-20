using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocioBoard
{
    public partial class Agency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString != null)
            {
                if (Request.QueryString["id"] == "wlabel")
                {
                    PayPalEnterprice_click();
                }
            }
        }
        protected void PayPalEnterprice_click()
        {
            try
            {
                SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();

                string amount = "2999";
                string plantype = "White Label";
                string UserName = "Socioboard";
                String EmailId = "support@socioboard.com";

                String EnterPriseSuccessURL = ConfigurationManager.AppSettings["EnterPriseSuccessURL"];
                String EnterPriseFailedURL = ConfigurationManager.AppSettings["EnterPriseFailedURL"];
                String EnterPrisepaypalemail = ConfigurationManager.AppSettings["EnterPrisepaypalemail"];
                String userId = "";

                string pay = payme.PayWithPayPal(amount, plantype, UserName, "", EmailId, "USD", ConfigurationManager.AppSettings["EnterPrisepaypalemail"], ConfigurationManager.AppSettings["EnterPriseSuccessURL"],
                                        ConfigurationManager.AppSettings["EnterPriseFailedURL"], ConfigurationManager.AppSettings["EnterPriseSuccessURL"], ConfigurationManager.AppSettings["EnterPrisecancelurl"], ConfigurationManager.AppSettings["EnterPrisenotifyurl"], userId);

                Response.Redirect(pay);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}