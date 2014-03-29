using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocioBoard
{
    public partial class Enterprise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["i"] == "1")
            {
                PayPalEnterprice_click();
            }
        }

        protected void PayPalEnterprice_click()
        {
            SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();

            string amount = "9999";
            string plantype = "Enterprise";
            string UserName = "Abhay";
            String EmailId = "abhaymondal@globussoft.com";

            String EnterPriseSuccessURL = ConfigurationManager.AppSettings["EnterPriseSuccessURL"];
            String EnterPriseFailedURL = ConfigurationManager.AppSettings["EnterPriseFailedURL"];
            String EnterPrisepaypalemail = ConfigurationManager.AppSettings["EnterPrisepaypalemail"];
            String userId = "";

            string pay = payme.PayWithPayPal(amount, plantype, UserName, "", EmailId, "USD", ConfigurationManager.AppSettings["EnterPrisepaypalemail"], ConfigurationManager.AppSettings["EnterPriseSuccessURL"],
                                    ConfigurationManager.AppSettings["EnterPriseFailedURL"], ConfigurationManager.AppSettings["EnterPriseSuccessURL"], ConfigurationManager.AppSettings["EnterPrisecancelurl"], ConfigurationManager.AppSettings["EnterPrisenotifyurl"], userId);

            Response.Redirect(pay);
        }




    }
}