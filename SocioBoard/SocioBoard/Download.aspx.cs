using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SocioBoard
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ServerConfigurationCast(object sender, EventArgs e)
        {
            try
            {
                SocioBoard.Helper.Payment payme = new SocioBoard.Helper.Payment();

                string amount = "100";
                string plantype = "Professional Installation";
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